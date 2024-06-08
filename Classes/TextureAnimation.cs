using GFDLibrary.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPLGen
{
    public class TextureAnimation
    {
        public byte[] animHeader = new byte[] { 0x01, 0x10, 0x51, 0x00, 0x00, 0x01, 0x00, 0xFD };
        public int length = 0; // doesn't need to be accurate?
        public byte[] beforeAnimData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02,
            0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00 };
        public List<byte> targetMaterialName = EPL.NameData("Material").ToList();
        public byte[] beforeKeyData = new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x15 }; // layercount 1, nointerp
        public int keyCount = 8;
        List<float> keyTimings = new List<float>(); // size: 4 * keycount
        List<TSingle5Key> keys = new List<TSingle5Key>(); // size: 20 * keycount
        public byte[] animFooter = new byte[] { 0x00, 0x00, 0x00, 0x00, // blend anim count, EOF
            0x01, 0x10, 0x50, 0x90, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        public void Build(string outPath, int KeyCount)
        {
            keyCount = KeyCount;

            List<byte> animBytes = new List<byte>();
            animBytes.AddRange(animHeader);
            animBytes.AddRange(BitConverter.GetBytes(length));
            animBytes.AddRange(beforeAnimData);
            animBytes.AddRange(targetMaterialName);
            animBytes.AddRange(beforeKeyData);
            animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(keyCount)));
            for (int i = 0; i < keyCount; i++)
            {
                float timing = 1f / keyCount;
                keyTimings.Add( (i + 1) * timing );
                keys.Add( new TSingle5Key() { x_offset = i * timing });
            }
            foreach (var keyTiming in keyTimings)
            {
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(keyTiming)));
            }
            foreach (var key in keys)
            {
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(key.x_offset)));
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(key.y_offset)));
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(key.x_scale)));
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(key.y_scale)));
                animBytes.AddRange(BitConverter.GetBytes(EndiannessSwapUtility.Swap(key.uv_rotation)));
            }
            animBytes.AddRange(animFooter);

            File.WriteAllBytes(outPath, animBytes.ToArray());
        }
    }

    public class TSingle5Key
    {
        public float x_offset = 0; // 1 divided by key count if textures lined up horizontally
        public float y_offset = 0;
        public float x_scale = 1;
        public float y_scale = 1;
        public float uv_rotation = 0;
    }
}
