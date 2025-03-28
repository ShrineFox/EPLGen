using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using ShrineFox.IO;
using static EPLGen.MainForm;

namespace EPLGen
{
    public class GAP
    {
        public byte[] AnimPackHeader = {
            0x47, 0x46, 0x53, 0x30, 0x01, 0x10, 0x51, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x10, 0x51, 0x00, 0x00, 0x01, 0x00, 0xFD
        };
        public uint AnimPackSize { get; set; } = 0;
        public uint Reserved = 0;
        public uint AnimPackFlags = 8;
        public uint AnimCount { get; set; } = 0;
        public List<GapAnim> Animations { get; set; } = new List<GapAnim>();

        public uint BlendAnimCount = 0;

        public static void Build(UserSettings settings, string outputPath)
        {
            GAP gap = new GAP();
            gap.AnimCount = (uint)settings.Particles.Count;

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                new FileStream(outputPath, FileMode.Create), Endianness.BigEndian))
            {
                writer.Write(gap.AnimPackHeader);
                writer.Write(gap.AnimPackSize);
                writer.Write(gap.Reserved);
                writer.Write(gap.AnimPackFlags);
                writer.Write(gap.AnimCount);
                foreach (var particle in settings.Particles)
                {
                    var anim = new GapAnim();

                    writer.Write(anim.AnimFlags);
                    writer.Write(anim.Duration);
                    writer.Write(anim.ControllerCount);
                    foreach (var controller in anim.Controllers)
                    {
                        writer.Write(controller.TargetKind);
                        writer.Write(controller.TargetID);
                        writer.Write(EPL.NameData(particle.Name));
                        writer.Write(controller.LayerCount);
                        foreach (var layer in controller.Layers)
                        {
                            writer.Write(layer.KeyType);
                            writer.Write(layer.KeyCount);
                            foreach (var timing in layer.KeyTimings)
                            {
                                writer.Write(timing);
                            }

                            foreach (var key in layer.NodePRSKeys)
                            {
                                EPL.WriteVector3(writer, particle.Translation);
                                EPL.WriteQuaternion(writer, particle.Rotation);
                                EPL.WriteVector3(writer, particle.Scale);
                            }
                        }

                        writer.Write(controller.EmbeddedEPLCount);

                        var gmd = particle.TexturePath;

                        controller.EmbeddedEPLData.Add(WrapGMD_InScreenspaceEPL(gmd).Skip(16).ToArray());
                        writer.Write(controller.EmbeddedEPLData[0]);
                        
                        writer.Write(EPL.NameData(particle.Name)); // epl hash string

                        EPL.WriteVector3(writer, controller.BoundingBoxMin);
                        EPL.WriteVector3(writer, controller.BoundingBoxMax);
                    }
                }
                writer.Write(0); // BlendAnimCount
            }
        }

        private static byte[] WrapGMD_InScreenspaceEPL(string gmd, float distanceFromScren = 10f)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    var eplBytes = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\Screenspace\\firsthalf.epl"));
                    memWriter.Write(eplBytes);

                    memWriter.Write(distanceFromScren);
                    memWriter.Write(new byte[] { 0x01 }); // Attachment Count
                    memWriter.Write(EPL.NameData($"{Path.GetFileName(gmd)}"));
                    memWriter.Write(2);
                    memWriter.Write(5);

                    byte[] gmdBytes = File.ReadAllBytes(gmd);
                    memWriter.Write(Convert.ToUInt32(gmdBytes.Length));
                    memWriter.Write(gmdBytes);

                    var eplBytes2 = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\Screenspace\\secondhalf.epl"));
                    memWriter.Write(eplBytes2);
                }
                return memStream.ToArray();
            }
        } 
    }

    public class GapAnim
    {
        public byte[] AnimFlags = {
            0x50, 0x00, 0x00, 0x01
        };
        public float Duration { get; set; } = 2;
        public uint ControllerCount { get; set; } = 1;
        public List<GapAnimController> Controllers { get; set; } = new List<GapAnimController>() { new GapAnimController() };
    }

    public class GapAnimController
    {
        public short TargetKind = 1; // Node
        public int TargetID { get; set; } = 0;
        public byte[] TargetName { get; set; } = new byte[] { };
        public uint LayerCount { get; set; } = 1;
        public List<GapAnimLayer> Layers { get; set; } = new List<GapAnimLayer>() { new GapAnimLayer() };
        public uint EmbeddedEPLCount { get; set; } = 1;

        public List<byte[]> EmbeddedEPLData { get; set; } = new List<byte[]>() { }; // EPL starting with 00 00 00 05
        public Vector3 BoundingBoxMin { get; set; } = new Vector3(-3747.618896f, 13840.197266f, 3538.263672f);
        public Vector3 BoundingBoxMax { get; set; } = new Vector3(-5435.104492f, 13410.140625f, -4183.845215f);

    }

    public class GapAnimLayer
    {
        public uint KeyType = 2; // EKeyType_NodePRS (2)
        public uint KeyCount { get; set; } = 1;
        public List<Single> KeyTimings { get; set; } = new List<Single>() { 0f };
        public List<Tuple<Vector3, Quaternion, Vector3>> NodePRSKeys {get;set;} = new List<Tuple<Vector3, Quaternion, Vector3>>() { new Tuple<Vector3, Quaternion, Vector3>(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), new Vector3(1f, 1f, 1f)) { }  }; // Pos, Rot, Scale
    }
}
