using ShrineFox.IO;
using System.Text;

namespace EPLGen.Classes
{
    public class EPT
    {
        private byte[] header { get; } = new byte[] {
            0x47, 0x46, 0x53, 0x30, 0x01, 0x10, 0x50, 0x70, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03 }; // end in 00 for no loop, 01 for loop (non-intermittent)
        public int tile_x { get; } = 32;
        public int tile_y { get; } = 32;
        public int fps { get; } = 6;
        public string imageName { get; set; } = "sprite.dds";
        private byte[] beforeImageData { get; } = new byte[]{
            0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01 };
        public byte[] imageData { get; set; } = new byte[] { };

        public void Build(string outputDir)
        {
            Directory.CreateDirectory(outputDir);

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                new FileStream(Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(imageName)}.ept"), FileMode.OpenOrCreate), Endianness.BigEndian))
            {
                // Start EPT
                writer.Write(header);
                writer.Write(tile_x);
                writer.Write(tile_y);
                writer.Write(fps);
                // Image
                writer.Write(NameData(imageName));
                writer.Write(beforeImageData);
                writer.Write(Convert.ToUInt32(imageData.Length));
                writer.Write(imageData);
            }
        }

        public static byte[] NameData(string name)
        {
            List<byte> data = new List<byte>();

            ushort length = Convert.ToUInt16(name.Length);
            int nameHash = StringHasher.GenerateStringHash(name);

            data = data.Concat(BitConverter.GetBytes(EndiannessSwapUtility.Swap(length))).ToList();
            data = data.Concat(Encoding.ASCII.GetBytes(name)).ToList();
            data = data.Concat(BitConverter.GetBytes(EndiannessSwapUtility.Swap(nameHash))).ToList();

            return data.ToArray();
        }
    }
}
