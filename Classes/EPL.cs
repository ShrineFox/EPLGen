using GFDLibrary;
using GFDLibrary.Effects;
using GFDLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static EPLGen.MainForm;

namespace EPLGen
{
    public class EPL
    {
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

        public static void Build(ModelSettings model, string outputDir, int angleSeed = 0)
        {
            EPL epl = new EPL();
            epl.childNodeName = NameData(model.Name);
            epl.eplNodeName = NameData(model.Name);
            epl.eplLeafName = NameData(model.Name);
            epl.particleEffect.embeddedFileName = NameData(model.Name);
            epl.rootNodeRotation = model.Rotation;
            epl.rootNodeTranslation = model.Translation;
            epl.rootNodeScale = model.Scale;
            epl.particleEffect.particleSpeed = model.Particle.ParticleSpeed;
            epl.particleEffect.randomSpawnDelay = model.Particle.RandomSpawnDelay;
            epl.particleEffect.respawnTimer = model.Particle.RespawnTimer;
            epl.particleEffect.spawnChoker = model.Particle.SpawnChoker;
            epl.particleEffect.spawnerAngles = model.Particle.SpawnerAngles;
            epl.particleEffect.explosionEffect.Field170 = model.Particle.Field170;
            epl.particleEffect.explosionEffect.Field188 = model.Particle.Field188;
            epl.particleEffect.explosionEffect.Field178 = model.Particle.Field178;
            epl.particleEffect.explosionEffect.Field180 = model.Particle.Field180;
            epl.particleEffect.explosionEffect.Field190 = model.Particle.Field190;

            // Randomize angle that particles will appear at
            epl.particleEffect.angleSeed = Convert.ToUInt32(angleSeed);

            // Use base GMD and update with new texture, texture name, and transforms
            ModelPack gmd = Resource.Load<ModelPack>("./model.gmd");
            gmd.Textures.First().Value.Name = model.Name + ".dds";
            if (File.Exists(model.TexturePath))
                gmd.Textures.First().Value.Data = File.ReadAllBytes(model.TexturePath);
            gmd.Materials.First().Value.Name = model.Name;
            gmd.Materials.First().Value.DiffuseMap.Name = model.Name + ".dds";
            gmd.Model.Nodes.Single(x => x.Name.Equals("SMWSpriteMesh1x4")).Translation = model.Particle.Translation;
            gmd.Model.Nodes.Single(x => x.Name.Equals("SMWSpriteMesh1x4")).Rotation = model.Particle.Rotation;
            gmd.Model.Nodes.Single(x => x.Name.Equals("SMWSpriteMesh1x4")).Scale = model.Particle.Scale;
            gmd.Model.Nodes.Single(x => x.Name.Equals("SMWSpriteMesh1x4")).Attachments.First(x => 
                x.GetValue().ResourceType.Equals(ResourceType.Mesh)).GetValue<Mesh>().MaterialName = model.Name;

            // Get GMD as byte array
            using (MemoryStream memStream = new MemoryStream())
            {
                gmd.Save(memStream, false);
                epl.particleEffect.embeddedFile = memStream.ToArray();
            }
            epl.particleEffect.dataLength = Convert.ToUInt32(epl.particleEffect.embeddedFile.Length);

            using (EndianBinaryWriter writer = new EndianBinaryWriter(
                new FileStream(Path.Combine(outputDir, $"{model.Name}.epl"), FileMode.OpenOrCreate), Endianness.BigEndian))
            {
                // Start EPL
                writer.Write(epl.header);
                writer.Write(epl.childNodeName);
                WriteVector3(writer, epl.rootNodeTranslation);
                WriteQuaternion(writer, epl.rootNodeRotation);
                WriteVector3(writer, epl.rootNodeScale);
                writer.Write(epl.beforeLeaf);
                writer.Write(epl.dummyChild);
                writer.Write(epl.eplNodeName);
                WriteVector3(writer, epl.eplNodeTranslation);
                WriteQuaternion(writer, epl.eplNodeRotation);
                WriteVector3(writer, epl.eplNodeScale);
                // Start EPL Leaf
                writer.Write(epl.subNodeHeader);
                writer.Write(epl.eplLeafName);
                // Start Particle Data
                writer.Write(epl.particleEffect.particleHeader);
                writer.Write(epl.particleEffect.randomSpawnDelay);
                writer.Write(epl.particleEffect.particleLife);
                writer.Write(epl.particleEffect.angleSeed);
                writer.Write(epl.particleEffect.respawnTimer);
                WriteVector2(writer, epl.particleEffect.spawnChoker);
                writer.Write(epl.particleEffect.colorOverLifeOffset);
                writer.Write(epl.particleEffect.FieldC4);
                WriteVector2(writer, epl.particleEffect.opacityOverLife);
                writer.Write(epl.particleEffect.colorOverLife_Bezier);
                writer.Write(epl.particleEffect.sizeOverLife);
                WriteVector2(writer, epl.particleEffect.spawnerAngles);
                WriteVector2(writer, epl.particleEffect.cycleRateFromBirth);
                writer.Write(epl.particleEffect.cycleRateGlobal);
                writer.Write(epl.particleEffect.unknownFields);
                writer.Write(epl.particleEffect.particleScale);
                writer.Write(epl.particleEffect.particleSpeed);
                // Start Explosion Effect
                WriteVector2(writer, epl.particleEffect.explosionEffect.Field170);
                WriteVector2(writer, epl.particleEffect.explosionEffect.Field188);
                WriteVector2(writer, epl.particleEffect.explosionEffect.Field178);
                WriteVector2(writer, epl.particleEffect.explosionEffect.Field180);
                WriteVector2(writer, epl.particleEffect.explosionEffect.Field190);
                writer.Write(epl.particleEffect.explosionEffect.Field198);
                // End Explosion Effect
                writer.Write(epl.particleEffect.embeddedFileName);
                writer.Write(epl.particleEffect.embeddedFileFields);
                writer.Write(epl.particleEffect.dataLength);
                writer.Write(epl.particleEffect.embeddedFile);
                // End EPL Leaf
                writer.Write(epl.subNodeFooter);
                writer.Write(epl.animationData);
                // End EPL
            }
        }

        private static void WriteVector2(EndianBinaryWriter writer, Vector2 vec2)
        {
            writer.Write(vec2.X);
            writer.Write(vec2.Y);
        }

        private static void WriteVector3(EndianBinaryWriter writer, Vector3 vec3)
        {
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.X)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.Y)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.Z)));
        }

        private static void WriteQuaternion(EndianBinaryWriter writer, Quaternion quaternion)
        {
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.X))); 
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.Y)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.Z)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.W)));
        }

        // Node Attachment Type: EPL (7), EplFlags: 5
        public byte[] header { get; } = new byte[] { 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x05 };

        public byte[] childNodeName = new byte[] { };

        // Translation, rotation, scale
        public Vector3 rootNodeTranslation { get; set; } = new Vector3() { X = 0f, Y = 0f, Z = 0f };
        public Quaternion rootNodeRotation { get; set; } = new Quaternion(0f, 0f, 0f, 1f); // -0.7071068, 0, 0, 0.7071068 for floor
        public Vector3 rootNodeScale { get; set; } = new Vector3() { X = 1f, Y = 1f, Z = 1f };

        // AttachmentCount: 0, HasProperties: 0, FieldE0: 1, ChildCount: 2
        public byte[] beforeLeaf { get; } = new byte[] { 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02 };

        // First child of EPL node, nothing to edit
        public byte[] dummyChild { get; } = new byte[] { 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
            0x00, 0x08, 0x00, 0x00, 0x00, 0x05, 0x00, 0x05, 0x45, 0x4D, 0x50, 0x54, 0x59, 0x91, 0x51, 0x87,
            0x19, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00 };

        // 2nd Child of EPL node
        public byte[] eplNodeName { get; set; } = new byte[] { };
        public Vector3 eplNodeTranslation { get; set; } = new Vector3() { X = 0f, Y = 0f, Z = 0f };
        public Quaternion eplNodeRotation { get; set; } = new Quaternion(0f, 0f, 0f, 1f);
        public Vector3 eplNodeScale { get; set; } = new Vector3() { X = 1f, Y = 1f, Z = 1f };

        // Start of EPL Leaf 
        // Attachment count: 1, Attachment Type: EPL Leaf (8), EfPL Flags: 5
        public byte[] subNodeHeader { get; } = new byte[] { 
            0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x05 };

        public byte[] eplLeafName { get; set; } = new byte[] { };

        // Particle data
        public ParticleEffect particleEffect { get; set; } = new ParticleEffect();

        // Has Properties: 0, FieldE0: 1, ChildCount: 0
        public byte[] subNodeFooter { get; } = new byte[] {
            0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        // Binary representation of particle animation controllers & frame timings
        public byte[] animationData { get; } = new byte[]
        {
            0x00, 0x00, 0x00, 0x00, 0x41, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x3F, 0xC0, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x08, 0x56, 0xC2, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03,
            0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x08, 0x56, 0xC4, 0x40, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03,
            0x00, 0x00, 0x00, 0x00, 0x3F, 0xBB, 0xBB, 0xBC, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x3F, 0xBB, 0xBB, 0xBC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02,
            0x00, 0x00, 0x00, 0x00, 0x3F, 0xBB, 0xBB, 0xBC, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00
        };
    }

    public class ParticleEffect
    {
        // Type: 10, Field04: 2, Type: 2, Field00: 0, Field04: 30, Field08, Field0C, Field10
        public byte[] particleHeader { get; } = new byte[] {
            0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x1E, 0x08, 0xB9, 0x91, 0x40, 0x00, 0x00, 0x00, 0x80, 0x10, 0xF6, 0xFA, 0xE0 };

        // Particle Emitter (Effect Generator)
        public uint randomSpawnDelay { get; set; } = 0;
        public float particleLife { get; set; } = 0.5f;
        public uint angleSeed { get; set; } = 1;
        public float respawnTimer { get; set; } = 0f;
        public Vector2 spawnChoker { get; set; } = new Vector2(0f, 2f);
        public float colorOverLifeOffset { get; set; } = 0.8980392f;
        public uint FieldC4 { get; set; } = 2;
        public Vector2 opacityOverLife { get; set; } = new Vector2(1f, 1f);
        public byte[] colorOverLife_Bezier { get; } = new byte[] {
            0x00, 0x02, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0x3D, 0xF1, 0x20, 0x22, 0x3E, 0xEA, 0x14, 0x51, 0x3E, 0xDB, 0x34, 0x70, 0x3F, 0x72,
            0x4C, 0xBD, 0x23, 0x92, 0x3F, 0xA8, 0x57, 0x28, 0x6B, 0x76, 0x7D, 0x42, 0x8C, 0xFE, 0x9B, 0x2B,
            0xA7, 0xD4, 0xB3, 0x3C, 0xBD, 0x81, 0xC6, 0xCC, 0xCF, 0x28, 0xD6, 0xAC, 0xDD, 0x6F, 0xE3, 0x82,
            0xE8, 0xE2, 0xED, 0xA1, 0xF1, 0xC6, 0xF5, 0x60, 0xF8, 0x74, 0xFB, 0x08, 0xFD, 0x24, 0xFE, 0xC9 };

        // Field C0, SizeOverLife, Field12C: 1, Field130: 1
        public byte[] sizeOverLife { get; } = new byte[]
        {
            0x3F, 0x80, 0x00, 0x00, 0x00, 0x03, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3E, 0xAD, 0x33, 0x8A, 0x3D, 0xC4, 0x03, 0x6C, 0x3F, 0x62,
            0x6D, 0xE0, 0x3F, 0x0A, 0xFB, 0xC4, 0x0A, 0x07, 0x14, 0x44, 0x1E, 0xA8, 0x29, 0x44, 0x34, 0x13,
            0x3F, 0x12, 0x4A, 0x3C, 0x55, 0x8C, 0x60, 0xFF, 0x6C, 0x90, 0x78, 0x2A, 0x83, 0xDA, 0x8F, 0x8B,
            0x9B, 0x1A, 0xA6, 0x97, 0xB1, 0xDF, 0xBC, 0xF4, 0xC7, 0xB8, 0xD2, 0x20, 0xDC, 0x20, 0xE5, 0xBC,
            0xEE, 0xEA, 0xF7, 0xB0, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00
        };
        public Vector2 spawnerAngles { get; set; } = new Vector2(-360f, 360f); // 0,0 for floor
        public Vector2 cycleRateFromBirth { get; set; } = new Vector2(0f, 0f);
        public float cycleRateGlobal { get; set; } = 0.0001f;
        // Field138, 13C, 150: 0 
        public byte[] unknownFields { get; } = new byte[12];
        public float particleScale { get; set; } = 1f;
        public float particleSpeed { get; set; } = 0.5f;

        public ExplosionEffect explosionEffect { get; set; } = new ExplosionEffect();

        // Particle Embedded File (GMD)
        public byte[] embeddedFileName { get; set; } = new byte[] { };

        // Field18, Field00: 2
        public byte[] embeddedFileFields { get; } = new byte[] { 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02 };
        public uint dataLength { get; set; } = 0;
        public byte[] embeddedFile { get; set; } = new byte[0];
    }

    public class ExplosionEffect
    {
        // Explosion Effect Params
        public Vector2 Field170 { get; set; } = new Vector2(102.6f, 84f);
        public Vector2 Field188 { get; set; } = new Vector2(0f, 0f);
        public Vector2 Field178 { get; set; } = new Vector2(-1f, 2f);
        public Vector2 Field180 { get; set; } = new Vector2(-1f, 2f); // 0,0 for floor
        public Vector2 Field190 { get; set; } = new Vector2(0f, 0f);
        public float Field198 { get; set; } = 0f;
    }

    public class GMD
    {
        // Start of a GMD container
        public static byte[] gmdHeader { get; } = new byte[]
        {
            0x47, 0x46, 0x53, 0x30, 0x01, 0x10, 0x50, 0x90, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x10, 0x50, 0x90, 0x00, 0x01, 0x00, 0xFC, 0x00, 0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x01, 0x10, 0x50, 0x90, 0x00, 0x01, 0x00, 0xFB, 0x00, 0x00, 0x00, 0x14,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x10, 0x50, 0x90, 0x00, 0x01, 0x00, 0x03,
            0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x40, 0xA0, 0x00, 0x00,
            0x41, 0x20, 0x00, 0x00, 0x34, 0x6A, 0xAC, 0x84, 0xC0, 0xA0, 0x00, 0x00, 0xB5, 0x00, 0x00, 0x00,
            0xB4, 0x6A, 0xAC, 0x84, 0x00, 0x00, 0x00, 0x00, 0x40, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x40, 0xE2, 0x46, 0x31, 0x00, 0x08, 0x52, 0x6F, 0x6F, 0x74, 0x4E, 0x6F, 0x64, 0x65, 0x48, 0xE1,
            0xB0, 0xE5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00
        };

        // attachment count

        // End of a GMD container
        public static byte[] gmdFooter { get; } = new byte[]
        {
            0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x10, 0x50, 0x90, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
    }
}
