﻿using EPLGen.Classes;
using GFDLibrary.Common;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EPLGen
{
    public class P_RING
    {
        public byte[] eplHeader = new byte[] { 0x47, 0x46, 0x53, 0x30, 0x01, 0x10, 0x50, 0x70, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00 };
        public byte[] beforeChildCount = new byte[] { 0x00, 0x00, 0x00, 0x15, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x41, 0x00, 0x00, 0x00, 0x41, 0x00, 0x00, 0x00, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00 };
        public int childCount;
        public List<P_RING_ChildNode> childNodes;
        public List<P_RING_TEPLAnimation> EPLanimationData;
        public byte[] footer = new byte[] { 0x00, 0x00 };
        public void Build(string outputPath, string inputEptDir)
        {
            var eptFiles = Directory.GetFiles(inputEptDir, "*.ept", SearchOption.TopDirectoryOnly);
            childCount = eptFiles.Length;

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                new FileStream(outputPath, FileMode.OpenOrCreate), Endianness.BigEndian))
            {
                // Start EPL
                writer.Write(eplHeader);
                writer.Write(beforeChildCount);
                writer.Write(childCount);
                // Write child nodes
                List<TEPLAnimation> TEPLAnimation = new List<TEPLAnimation>();
                List<TEPLAnimationController> TEPLAnimationController = new List<TEPLAnimationController>();
                for (int i = 0; i < eptFiles.Length; i++)
                {
                    var eptData = File.ReadAllBytes(eptFiles[i]);
                    childNodes.Add(new P_RING_ChildNode()
                    {
                        nameData = P_RING_ChildNode.GetNameData(Path.GetFileName(eptFiles[i])),
                        eptData = eptData,
                        eptLength = eptData.Length
                    });

                }
                foreach (var node in childNodes)
                {
                    writer.Write(node.beforeEPT);
                    writer.Write(node.nameData);
                    writer.Write(node.eptLength);
                    writer.Write(node.eptData);
                    writer.Write(node.afterEPT);
                }
                // Write animation data
                P_RING_TEPLAnimation teplAnimation = new P_RING_TEPLAnimation();
                teplAnimation.controllerCount = childCount;
                teplAnimation.idk = childCount + 1;

            }
        }

        public class P_RING_ChildNode
        {
            public byte[] beforeEPT = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
            0x00, 0x08, 0x00, 0x00, 0x00, 0x05, 0x00, 0x07, 0x50, 0x2D, 0x52, 0x49, 0x4E, 0x47, 0x30, 0x1A,
            0x6A, 0x03, 0x69, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x05, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x40, 0x00, 0x00, 0x00, 0x01, 0x2D, 0xAE, 0x8E, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x30, 0xA3, 0xD7, 0x3F, 0x51, 0xD1, 0xD2, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x7E, 0xB5, 0x04, 0x00, 0x02, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x3E, 0xAA, 0xAA,
            0xAB, 0x3E, 0xAA, 0xAA, 0xAB, 0x3F, 0x2A, 0xAA, 0xAB, 0x3F, 0x2A, 0xAA, 0xAB, 0x0A, 0xA7, 0x15,
            0x57, 0x1F, 0xFF, 0x2A, 0xA7, 0x35, 0x57, 0x3F, 0xFF, 0x4A, 0xA7, 0x55, 0x57, 0x5F, 0xF7, 0x6A,
            0xA7, 0x75, 0x57, 0x7F, 0xF7, 0x8A, 0xA7, 0x95, 0x57, 0x9F, 0xFF, 0xAA, 0xA7, 0xB5, 0x57, 0xC0,
            0x07, 0xCA, 0xA7, 0xD5, 0x57, 0xE0, 0x07, 0xEA, 0xA7, 0xF5, 0x57, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x03, 0x40, 0xD6, 0x66, 0x67, 0x40, 0xC9, 0x99, 0x9A, 0x40, 0xE4, 0x44, 0x45, 0x40, 0xB1, 0x11,
            0x11, 0x40, 0xF2, 0x22, 0x22, 0x40, 0x98, 0x88, 0x88, 0x41, 0x00, 0x00, 0x00, 0x40, 0x80, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x3E, 0xB9, 0xDF, 0xE5, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x45, 0x47,
            0xD1, 0x22, 0xAE, 0x32, 0x47, 0x3E, 0xB1, 0x49, 0x8F, 0x53, 0x6D, 0x5C, 0xA2, 0x65, 0x62, 0x6D,
            0xBD, 0x75, 0xC5, 0x7D, 0x9A, 0x85, 0x4D, 0x8C, 0xCB, 0x94, 0x47, 0x9B, 0xBC, 0xA3, 0x29, 0xAA,
            0x9B, 0xB2, 0x2F, 0xB9, 0xED, 0xC1, 0xDE, 0xCA, 0x25, 0xD2, 0xFB, 0xDC, 0xB5, 0xE8, 0x61, 0x3F,
            0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3A, 0x83, 0x12, 0x6F, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3D, 0xCC, 0xCC, 0xC5, 0x3F, 0x80, 0x00, 0x00, 0x43,
            0xF8, 0xCC, 0xCD, 0x44, 0xBB, 0xCC, 0xCD, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xBF,
            0x80, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3C, 0x23, 0xD7, 0x0A, 0x00, 0x00, 0x00, 0x00 };
            public byte[] nameData;
            public int eptLength;
            public byte[] eptData;
            public byte[] afterEPT = { 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            public static byte[] GetNameData(string name)
            {
                List<byte> data = new List<byte>();

                ushort length = Convert.ToUInt16(name.Length);
                int nameHash = StringHasher.GenerateStringHash(name);

                data = data.Concat(BitConverter.GetBytes(EndiannessSwapUtility.Swap(length))).ToList();
                data = data.Concat(Encoding.ASCII.GetBytes(name)).ToList();
                data = data.Concat(BitConverter.GetBytes(EndiannessSwapUtility.Swap(nameHash))).ToList();
                byte[] afterEPTName = new byte[] { 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02 };
                data = data.Concat(afterEPTName).ToList();

                return data.ToArray();
            }
        }

        public class P_RING_TEPLAnimation
        {
            public byte[] beforeControllerCount = new byte[] {0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x41, 0xF0, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x01, 0x3F, 0x80, 0x00, 0x00};
            public int controllerCount; // same as childCount
            public List<TEPLAnimation> tEplAnimData;
            public int idk; // childCount + 1
            public List<TEPLAnimationController> AnimControllers;
        }

        public class TEPLAnimation
        {
            int Field00 = 0;
            float Field04 = 30;
            public TAnimation tAnimData;

        }

        public class TAnimation
        {
            public byte[] beforeControllerCount = new byte[] { 0x00, 0x00, 0x00, 0x01, 0x3F, 0x80, 0x00, 0x00 };
            int controllerCount; // childCount
            List<TAnimController> controllers;
        }

        public class TAnimController
        {
            public byte[] targetKind = new byte[] { 0x00, 0x01 };
            int targetId = 901273936; // random number?
            public byte[] afterTargetId = new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00 };
        }

        public class TEPLAnimationController
        {
            float Field00 = 0f; // always 0?
            float Field04 = 1f;
            int controllerIndex = -1; // -1 first entry, next entry childCount, decrement 1 each subsequent entry
            int Field10; // 0 first entry, next entry 1, count up to childCount - 1
        }
    }
}
