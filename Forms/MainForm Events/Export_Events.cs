using DarkUI.Forms;
using EPLGen.Classes;
using GFDLibrary.Models;
using GFDLibrary;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        /*
         * DDS Particles Export
         */

        // Exports each DDS in file list as part of a particle effect EPL
        private void ExportDDS_WrappedInEPL_Click(object sender, EventArgs e)
        {
            var outPath = WinFormsDialogs.SelectFile("Choose EPL Destination", false, new string[] { "EPL (.EPL)" }, true);
            if (string.IsNullOrEmpty(outPath.First()))
                return;

            if (!outPath.First().ToLower().EndsWith(".epl"))
                outPath[0] += ".EPL";

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(outPath.First(), FileMode.Create), Endianness.BigEndian))
            {
                writer.Write(WrapDDS_InParticleEffectEPL(outPath.First()));
            }

            MessageBox.Show($"Done exporting EPL:\n{outPath.First()}", "EPL Export Successful");
        }

        // Export each DDS in file list as an EPT file
        private void ExportDDS_AsEPTs(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose EPT Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            if (!userSettings.Particles.Any(x => x.TexturePath.ToLower().EndsWith(".dds")))
                return;

            foreach (var particle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".dds")))
            {
                EPT ept = new EPT();
                ept.imageData = File.ReadAllBytes(particle.TexturePath);
                ept.imageName = Path.GetFileName(particle.TexturePath);
                ept.Build(outDir);
            }

            MessageBox.Show("Done exporting EPTs.", "EPTs Saved Successfully");
        }

        // Output DDS as GMD using selected base GMD (first Material and Texture in GMD will be replaced)
        private void ExportDDS_WrappedIGMD_Click(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose EPT Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            if (!userSettings.Particles.Any(x => x.TexturePath.ToLower().EndsWith(".dds")))
                return;

            foreach (var particle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".dds")))
            {
                ModelPack baseGMD = Resource.Load<ModelPack>(userSettings.GMD);
                string outPath = Path.Combine(outDir, Path.GetFileNameWithoutExtension(particle.TexturePath) + ".GMD");

                string textureName = Path.GetFileNameWithoutExtension(particle.TexturePath);
                baseGMD.Textures.First().Value.Name = textureName + ".dds";
                baseGMD.Textures.First().Value.Data = File.ReadAllBytes(particle.TexturePath);
                baseGMD.Materials.First().Value.Name = textureName;
                baseGMD.Materials.First().Value.DiffuseMap.Name = textureName + ".dds";
                baseGMD.Model.Nodes.Single(x => x.Name.Equals("Bone")).Attachments.First(x =>
                    x.GetValue().ResourceType.Equals(ResourceType.Mesh)).GetValue<Mesh>().MaterialName = textureName;
                baseGMD.Save(outPath);

            }

            MessageBox.Show("Done exporting EPTs.", "EPTs Saved Successfully");
        }

        // Output DDS particle EPL wrapped in a dummy GMD (for attaching to objects)
        private void ExportDDS_WrappedInEPL_WrappedInGMD_Click(object sender, EventArgs e)
        {
            var outPath = WinFormsDialogs.SelectFile("Choose GMD Destination", false, new string[] { "GMD (.GMD)" }, true);
            if (string.IsNullOrEmpty(outPath.First()))
                return;

            string gmdOutPath = outPath.First();
            if (!gmdOutPath.ToLower().EndsWith(".gmd"))
                gmdOutPath += ".GMD";

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(outPath[0], FileMode.Create), Endianness.BigEndian))
            {
                writer.Write(WrapEPL_InDummyGMD(WrapDDS_InParticleEffectEPL(outPath.First())));
            }

            MessageBox.Show($"Done exporting GMD:\n{outPath.First()}", "GMD Export Successful");
        }

        private byte[] WrapDDS_InParticleEffectEPL(string modelName)
        {
            userSettings.ModelName = Path.GetFileNameWithoutExtension(modelName);

            if (userSettings.Particles.Any(x => x.TexturePath.ToLower().EndsWith(".dds")))
                return EPL.Build(userSettings);

            return null;
        }

        /*
         * Screenspace EPL Export
         */

        // Exports each GMD in particle list as a MODEL3D EPL.
        // Useful for attaching animated GMDs to nodes.
        private void ExportGMDs_WrappedInEPLs_Click(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose EPL Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            foreach (var gmdParticle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".gmd")))
            {
                var gmd = gmdParticle.TexturePath;
                string eplOutDir = Path.Combine(outDir, Path.GetFileName(gmd).Replace(".gmd", ".epl").Replace(".GMD", ".EPL"));

                using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(eplOutDir, FileMode.Create), Endianness.BigEndian))
                {
                    writer.Write(WrapGMD_InEPL(File.ReadAllBytes(gmd), gmd));
                }
            }

            MessageBox.Show($"Done exporting GMDs.", "GMD Export Successful");
        }

        private void ExportGMDs_WrappedInEPLs_WrappedInDummyGMD_Click(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose GMD Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            foreach (var gmdParticle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".gmd")))
            {
                var gmd = gmdParticle.TexturePath;
                string gmdOutDir = Path.Combine(outDir, Path.GetFileName(gmd));

                using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(gmdOutDir, FileMode.Create), Endianness.BigEndian))
                {
                    var eplBytes = WrapGMD_InEPL(File.ReadAllBytes(gmd), gmd);
                    writer.Write(WrapEPL_InDummyGMD(eplBytes));
                }
            }

            MessageBox.Show($"Done exporting GMDs.", "GMD Export Successful");
        }

        // Exports each GMD in particle list as a screenspace EPL.
        // Useful for displaying GMDs fixed to the screen via field effect banks using flowscript.
        private void ExportGMDs_WrappedInScreenspaceEPLs_Click(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose EPL Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            foreach (var gmdParticle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".gmd")))
            {
                var gmd = gmdParticle.TexturePath;
                string eplOutDir = Path.Combine(outDir, Path.GetFileName(gmd).Replace(".gmd", ".epl").Replace(".GMD", ".EPL"));

                using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(eplOutDir, FileMode.Create), Endianness.BigEndian))
                {
                    writer.Write(WrapGMD_InScreenspaceEPL(File.ReadAllBytes(gmd), gmd, gmdParticle.DistanceFromScreen));
                }
            }

            MessageBox.Show($"Done exporting GMDs.", "GMD Export Successful");
        }


        // Exports each GMD in particle list as a GMD wrapped in a screenspace EPL wrapped in another GMD.
        // Useful for attaching screenspace effects to GMD nodes.
        private void ExportGMDs_WrappedInScreenspaceEPLs_WrappedInDummyGMD(object sender, EventArgs e)
        {
            var outDir = WinFormsDialogs.SelectFolder("Choose GMD Destination Folder");
            if (string.IsNullOrEmpty(outDir))
                return;

            Directory.CreateDirectory(outDir);

            foreach (var gmdParticle in userSettings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".gmd")))
            {
                var gmd = gmdParticle.TexturePath;

                using (EndianBinaryWriter writer = new EndianBinaryWriter(
                    new FileStream(Path.Combine(outDir, Path.GetFileName(gmd)), FileMode.Create), Endianness.BigEndian))
                {
                    var eplBytes = WrapGMD_InScreenspaceEPL(File.ReadAllBytes(gmd), gmd, gmdParticle.DistanceFromScreen);
                    writer.Write(WrapEPL_InDummyGMD(eplBytes));
                }
            }

            MessageBox.Show($"Done exporting GMDs.", "GMD Export Successful");
        }


        // Exports a GAP where each animation is a GMD in the file list wrapped in a screenspace EPL.
        // Useful for attaching screenspace effects to GMD nodes and toggling displayed GMD via flowscript.
        private void ExportGMDs_WrappedInScreenspaceEPLs_WrappedInGAP_Click(object sender, EventArgs e)
        {
            var outPath = WinFormsDialogs.SelectFile("Choose GAP Destination", false, new string[] { "GFD Animation Pack (.GAP)" }, true);
            if (outPath.Count <= 0 || string.IsNullOrEmpty(outPath[0]))
                return;

            if (!outPath[0].ToLower().EndsWith(".gap"))
                outPath[0] += ".GAP";

            GAP.Build(userSettings, outPath[0]);

            MessageBox.Show($"Done exporting GAP.", "GAP Export Successful");
        }

        private void ExportGMDs_WrappedInScreenspaceEPLs_WrappedInGAP_WrappedInDummyGMD_Click(object sender, EventArgs e)
        {
            var outPath = WinFormsDialogs.SelectFile("Choose GMD Destination", false, new string[] { "GFS Model Pack (.GMD)" }, true);
            if (outPath.Count <= 0 || string.IsNullOrEmpty(outPath[0]))
                return;

            if (!outPath[0].ToLower().EndsWith(".gmd"))
                outPath[0] += ".GMD";

            string gapPath = outPath[0] + ".GAP";
            GAP.Build(userSettings, gapPath);
            using (FileSys.WaitForFile(gapPath)) { }

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                new FileStream(outPath[0], FileMode.Create), Endianness.BigEndian))
            {
                writer.Write(WrapGAP_InDummyGMD(File.ReadAllBytes(gapPath)));
            }

            MessageBox.Show($"Done exporting GMD.", "GMD Export Successful");
        }
    }
}
