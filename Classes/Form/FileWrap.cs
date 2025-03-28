using DarkUI.Forms;
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
        /// <summary>
        /// Returns a GMD with the provided GAP data wrapped inside. Useful for displaying multiple EPLs controlled by a GAP.
        /// </summary>
        /// <param name="gapBytes"></param>
        /// <returns></returns>
        private byte[] WrapGAP_InDummyGMD(byte[] gapBytes)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    var gmdBytes = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\GAP\\firsthalf.gmd"));
                    var gmdBytes2 = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\GAP\\secondhalf.gmd"));

                    memWriter.Write(gmdBytes);
                    memWriter.Write(gapBytes);
                    memWriter.Write(gmdBytes2);
                }
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Returns a GMD with the provided EPL data wrapped inside. Useful for attaching to GMD nodes.
        /// </summary>
        /// <param name="eplBytes"></param>
        /// <returns></returns>
        public static byte[] WrapEPL_InDummyGMD(byte[] eplBytes)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    memWriter.Write(GMD.gmdHeader);
                    memWriter.Write(eplBytes.Skip(16).ToArray()); // strip 16 byte EPL header
                    memWriter.Write(GMD.gmdFooter);
                }
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Returns a GMD with the provided GMD data wrapped inside an EPL. Useful for attaching animated GMDs to a node.
        /// </summary>
        /// <param name="gmdPath"></param>
        /// <param name="distanceFromScren"></param>
        /// <returns></returns>
        private static byte[] WrapGMD_InEPL(byte[] gmdBytes, string modelName = "Untitled")
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    var eplBytes = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\firsthalf.epl"));
                    var eplBytes2 = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\secondhalf.epl"));

                    memWriter.Write(eplBytes);

                    memWriter.Write(EPL.NameData($"{Path.GetFileName(modelName)}"));
                    memWriter.Write(2);
                    memWriter.Write(2);

                    memWriter.Write(Convert.ToUInt32(gmdBytes.Length));
                    memWriter.Write(gmdBytes);

                    memWriter.Write(eplBytes2);
                }
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Returns a GMD with the provided GMD data wrapped inside an EPL. Useful for displaying GMDs fixed to the screen when attached to a node.
        /// </summary>
        /// <param name="gmdPath"></param>
        /// <param name="distanceFromScren"></param>
        /// <returns></returns>
        private static byte[] WrapGMD_InScreenspaceEPL(byte[] gmdBytes, string modelName = "Untitled", float distanceFromScren = 10f)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    var eplBytes = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\Screenspace\\firsthalf.epl"));
                    var eplBytes2 = File.ReadAllBytes(Path.Combine(Exe.Directory(), "Dependencies\\EPL\\Screenspace\\secondhalf.epl"));

                    memWriter.Write(eplBytes);

                    memWriter.Write(distanceFromScren);
                    memWriter.Write(new byte[] { 0x01 }); // Attachment Count
                    memWriter.Write(EPL.NameData($"{Path.GetFileName(modelName)}"));
                    memWriter.Write(2);
                    memWriter.Write(5);

                    memWriter.Write(Convert.ToUInt32(gmdBytes.Length));
                    memWriter.Write(gmdBytes);

                    memWriter.Write(eplBytes2);
                }
                return memStream.ToArray();
            }
        }
    }
}
