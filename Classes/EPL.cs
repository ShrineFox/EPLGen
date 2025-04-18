﻿using GFDLibrary;
using GFDLibrary.Models;
using System.Numerics;
using System.Text;
using static EPLGen.MainForm;
using ShrineFox.IO;

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

        public static byte[] Build(UserSettings settings, int angleSeed = 0)
        {
            EPL epl = new EPL();
            epl.Name = NameData(settings.ModelName);
            epl.Translation = settings.Translation;
            epl.Rotation = settings.Rotation;
            epl.Scale = settings.Scale;

            // Add child nodes
            epl.ChildCount = settings.Particles.Count;
            foreach (var particle in settings.Particles.Where(x => x.TexturePath.ToLower().EndsWith(".dds")))
            {
                // Apply settings from mainform
                var eplNode = new EplChildNode()
                {
                    Name = NameData(particle.Name),
                    LeafName = NameData(particle.Name),
                    Translation = particle.Translation,
                    Rotation = particle.Rotation,
                    Scale = particle.Scale,
                    ParticleData = new ParticleData()
                    {
                        AngleSeed = Convert.ToUInt32(angleSeed),
                        EmbeddedFileName = NameData(particle.Name),
                        ParticleSpeed = particle.ParticleSpeed,
                        RandomSpawnDelay = particle.RandomSpawnDelay,
                        DespawnTimer = particle.DespawnTimer,
                        SpawnChoker = particle.SpawnChoker,
                        SpawnerAngles = particle.SpawnerAngles,
                        ExplosionEffect = new ExplosionEffectData()
                        {
                            Field170 = particle.Field170,
                            Field188 = particle.Field188,
                            Field178 = particle.Field178,
                            Field180 = particle.Field180,
                            Field190 = particle.Field190
                        }
                    }
                };

                // Use base GMD and update with new texture, texture name, and transforms
                ModelPack gmd = Resource.Load<ModelPack>(settings.GMD);
                string textureName = Path.GetFileNameWithoutExtension(particle.TexturePath);
                gmd.Textures.First().Value.Name = textureName + ".dds";
                if (File.Exists(particle.TexturePath))
                    gmd.Textures.First().Value.Data = File.ReadAllBytes(particle.TexturePath);
                gmd.Materials.First().Value.Name = textureName;
                gmd.Materials.First().Value.DiffuseMap.Name = textureName + ".dds";
                gmd.Model.Nodes.Single(x => x.Name.Equals("Bone")).Translation = particle.Translation;
                gmd.Model.Nodes.Single(x => x.Name.Equals("Bone")).Rotation = particle.Rotation;
                gmd.Model.Nodes.Single(x => x.Name.Equals("Bone")).Scale = particle.Scale;
                gmd.Model.Nodes.Single(x => x.Name.Equals("Bone")).Attachments.First(x =>
                    x.GetValue().ResourceType.Equals(ResourceType.Mesh)).GetValue<Mesh>().MaterialName = textureName;

                // Get GMD as byte array
                using (MemoryStream memStream = new MemoryStream())
                {
                    gmd.Save(memStream, false);
                    eplNode.ParticleData.EmbeddedFile = memStream.ToArray();
                }
                eplNode.ParticleData.DataLength = Convert.ToUInt32(eplNode.ParticleData.EmbeddedFile.Length);

                epl.ChildNodes.Add(eplNode);
            }

            epl.Animation.SubControllerCount = epl.ChildCount;

            for (int i = 0; i < epl.Animation.SubControllerCount; i++)
            {
                epl.Animation.SubControllers.Add(new SubAnimController() { TargetID = i * 220  });
            }
            epl.Animation.Field10 = epl.ChildCount + 1;
            epl.Animation.Controllers.Add(new AnimController() { Field0C = 0, ControllerIndex = -1 });
            for (int i = 0; i < epl.ChildCount; i++)
            {
                epl.Animation.Controllers.Add(new AnimController() { Field0C = epl.ChildCount - i, ControllerIndex = i });
            }

            using (MemoryStream memStream = new MemoryStream())
            {
                using (EndianBinaryWriter memWriter = new EndianBinaryWriter(memStream, Endianness.BigEndian))
                {
                    // Start EPL
                    memWriter.Write(epl.Header);
                    memWriter.Write(epl.Flags);
                    memWriter.Write(epl.Name);
                    WriteVector3(memWriter, epl.Translation);
                    WriteQuaternion(memWriter, epl.Rotation);
                    WriteVector3(memWriter, epl.Scale);
                    memWriter.Write(epl.BeforeLeaf);

                    // Child Nodes
                    memWriter.Write(epl.ChildCount);

                    foreach (EplChildNode node in epl.ChildNodes)
                    {
                        memWriter.Write(node.Name);
                        WriteVector3(memWriter, node.Translation);
                        WriteQuaternion(memWriter, node.Rotation);
                        WriteVector3(memWriter, node.Scale);
                        // Particle Data
                        memWriter.Write(node.LeafHeader);
                        memWriter.Write(node.LeafName);
                        memWriter.Write(node.ParticleData.ParticleHeader);
                        memWriter.Write(node.ParticleData.RandomSpawnDelay);
                        memWriter.Write(node.ParticleData.ParticleLife);
                        memWriter.Write(node.ParticleData.AngleSeed);
                        memWriter.Write(node.ParticleData.DespawnTimer);
                        WriteVector2(memWriter, node.ParticleData.SpawnChoker);
                        memWriter.Write(node.ParticleData.ColorOverLifeOffset);
                        memWriter.Write(node.ParticleData.FieldC4);
                        WriteVector2(memWriter, node.ParticleData.OpacityOverLife);
                        memWriter.Write(node.ParticleData.ColorOverLife_Bezier);
                        memWriter.Write(node.ParticleData.SizeOverLife);
                        WriteVector2(memWriter, node.ParticleData.SpawnerAngles);
                        WriteVector2(memWriter, node.ParticleData.CycleRateFromBirth);
                        memWriter.Write(node.ParticleData.CycleRateGlobal);
                        memWriter.Write(node.ParticleData.UnknownFields);
                        memWriter.Write(node.ParticleData.ParticleScale);
                        memWriter.Write(node.ParticleData.ParticleSpeed);
                        // Explosion Data
                        WriteVector2(memWriter, node.ParticleData.ExplosionEffect.Field170);
                        WriteVector2(memWriter, node.ParticleData.ExplosionEffect.Field188);
                        WriteVector2(memWriter, node.ParticleData.ExplosionEffect.Field178);
                        WriteVector2(memWriter, node.ParticleData.ExplosionEffect.Field180);
                        WriteVector2(memWriter, node.ParticleData.ExplosionEffect.Field190);
                        memWriter.Write(node.ParticleData.ExplosionEffect.Field198);
                        // Embedded Data
                        memWriter.Write(node.ParticleData.EmbeddedFileName);
                        memWriter.Write(node.ParticleData.EmbeddedFileFields);
                        memWriter.Write(node.ParticleData.DataLength);
                        memWriter.Write(node.ParticleData.EmbeddedFile);
                        memWriter.Write(node.SubNodeFooter);
                    }

                    // Animation Data
                    memWriter.Write(epl.Animation.Field00);
                    memWriter.Write(epl.Animation.Field04);
                    memWriter.Write(epl.Animation.Flags);
                    memWriter.Write(epl.Animation.Duration);
                    memWriter.Write(epl.Animation.SubControllerCount);
                    foreach (var subCtrl in epl.Animation.SubControllers)
                    {
                        memWriter.Write(subCtrl.TargetKind);
                        memWriter.Write(subCtrl.TargetID);
                        memWriter.Write(subCtrl.TargetName);
                        memWriter.Write(subCtrl.Layers);
                    }

                    memWriter.Write(epl.Animation.Field10);
                    foreach (var ctrl in epl.Animation.Controllers)
                    {
                        memWriter.Write(ctrl.Field00);
                        memWriter.Write(ctrl.Field04);
                        memWriter.Write(ctrl.ControllerIndex);
                        memWriter.Write(ctrl.Field0C);
                    }

                    memWriter.Write(epl.Field40);
                }
                return memStream.ToArray();
            }
        }

        public static void WriteVector2(EndianBinaryWriter writer, Vector2 vec2)
        {
            writer.Write(vec2.X);
            writer.Write(vec2.Y);
        }

        public static void WriteVector3(EndianBinaryWriter writer, Vector3 vec3)
        {
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.X)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.Y)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(vec3.Z)));
        }

        public static void WriteQuaternion(EndianBinaryWriter writer, Quaternion quaternion)
        {
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.X)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.Y)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.Z)));
            writer.Write(BitConverter.GetBytes(EndiannessSwapUtility.Swap(quaternion.W)));
        }

        // GFS0 header + EEplFlags (5)
        public byte[] Header { get; } = new byte[] { 0x47, 0x46, 0x53, 0x30,
            0x01, 0x10, 0x50, 0x70, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00 };

        public byte[] Flags { get; } = new byte[] { 0x00, 0x00, 0x00, 0x05 }; 

        public byte[] Name = new byte[] { };

        // Translation, rotation, scale
        public Vector3 Translation { get; set; } = new Vector3() { X = 0f, Y = 0f, Z = 0f };
        public Quaternion Rotation { get; set; } = new Quaternion(0f, 0f, 0f, 1f);
        public Vector3 Scale { get; set; } = new Vector3() { X = 1f, Y = 1f, Z = 1f };

        // AttachmentCount: 0, HasProperties: 0, FieldE0: 1, ChildCount: 2
        public byte[] BeforeLeaf { get; } = new byte[] { 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00 };

        public int ChildCount { get; set; } = 0;

        public List<EplChildNode> ChildNodes { get; set; } = new List<EplChildNode>();

        // Binary representation of particle animation controllers & frame timings
        public AnimationData Animation { get; set; } = new AnimationData();
        public byte[] Field40 { get; set; } = new byte[] { 0x00, 0x00 };

    }

    public class EplChildNode
    {
        public byte[] Name { get; set; } = new byte[] { };
        public Vector3 Translation { get; set; } = new Vector3() { X = 0f, Y = 0f, Z = 0f };
        public Quaternion Rotation { get; set; } = new Quaternion(0f, 0f, 0f, 1f);
        public Vector3 Scale { get; set; } = new Vector3() { X = 1f, Y = 1f, Z = 1f };

        // Start of EPL Leaf 
        // Attachment count: 1, Attachment Type: EPL Leaf (8), EfPL Flags: 5
        public byte[] LeafHeader { get; } = new byte[] {
            0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x05 }; // 0D instead of 05 = fix y to ground

        public byte[] LeafName { get; set; } = new byte[] { };

        // Explosion effect (animated sprites appearing and disappearing on floor)
        public ParticleData ParticleData { get; set; } = new ParticleData();

        // Has Properties: 0, FieldE0: 1, ChildCount: 0
        public byte[] SubNodeFooter { get; } = new byte[] {
            0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
    }

    public class ParticleData
    {
        // Type: 10, Field04: 2, Type: 2, Field00: 0, Field04: 2, Field08, Field0C, Field10
        public byte[] ParticleHeader { get; } = new byte[] {
            0x00, 0x00, 0x00, 0x0A, 
            0x00, 0x00, 0x00, 0x02, 
            0x00, 0x00, 0x00, 0x02, 
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x02, 
            0x08, 0xB9, 0x91, 0x40, // 1.116843e-33
            0x00, 0x00, 0x00, 0x80, // 1.793662e-43
            0x10, 0xF6, 0xFA, 0xE0 // 9.741643e-29
        };

        // Particle Emitter (Effect Generator)
        public uint RandomSpawnDelay { get; set; } = 6;
        public float ParticleLife { get; set; } = 3f;
        public uint AngleSeed { get; set; } = 0;
        public float DespawnTimer { get; set; } = 0f;
        public Vector2 SpawnChoker { get; set; } = new Vector2(0f, 4.18f);
        public float ColorOverLifeOffset { get; set; } = 1f;
        public uint FieldC4 { get; set; } = 2;
        public Vector2 OpacityOverLife { get; set; } = new Vector2(0f, 1f);
        public byte[] ColorOverLife_Bezier { get; } = new byte[] {
            0x00, 0x02, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x0A, 0xA7, 0x15, 0x5B, 0x20, 0x00, 0x2A, 0xAA, 0x35, 0x50, 0x3F, 0xFA, 0x4A, 0xB0,
            0x55, 0x60, 0x60, 0x01, 0x6A, 0xAA, 0x75, 0x5E, 0x7F, 0xF3, 0x8A, 0xA0, 0x95, 0x54, 0x9F, 0xFD,
            0xAA, 0x9E, 0xB5, 0x4E, 0xC0, 0x04, 0xCA, 0xAE, 0xD5, 0x54, 0xDF, 0xFE, 0xEA, 0xA3, 0xF5, 0x57,
        };

        // Field C0, SizeOverLife, Field12C: 1, Field130: 1
        public byte[] SizeOverLife { get; } = new byte[]
        {
            0x00, 0x80, 0x00, 0x00,
            0x00, 0x03, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80,
            0x00, 0x00, 0x0A, 0xA7, 0x15, 0x5B, 0x20, 0x00, 0x2A, 0xAA, 0x35, 0x50, 0x3F, 0xFA, 0x4A, 0xB0,
            0x55, 0x60, 0x60, 0x01, 0x6A, 0xAA, 0x75, 0x5E, 0x7F, 0xF3, 0x8A, 0xA0, 0x95, 0x54, 0x9F, 0xFD,
            0xAA, 0x9E, 0xB5, 0x4E, 0xC0, 0x04, 0xCA, 0xAE, 0xD5, 0x54, 0xDF, 0xFE, 0xEA, 0xA3, 0xF5, 0x57,
            0x3F, 0x80, 0x00, 0x00, 
            0x3F, 0x80, 0x00, 0x00
        };
        public Vector2 SpawnerAngles { get; set; } = new Vector2(90f, 90f); // 90,90 for flat against floor, -360, 360 for dome
        public Vector2 CycleRateFromBirth { get; set; } = new Vector2(0f, 0f);
        public float CycleRateGlobal { get; set; } = 0f;

        // Field138, 13C, 150: 0 
        public byte[] UnknownFields { get; } = new byte[12];
        public float ParticleScale { get; set; } = 1f;
        public float ParticleSpeed { get; set; } = 1f;

        public ExplosionEffectData ExplosionEffect { get; set; } = new ExplosionEffectData();

        // Particle Embedded File (GMD)
        public byte[] EmbeddedFileName { get; set; } = new byte[] { };

        // Field18, Field00: 2
        public byte[] EmbeddedFileFields { get; } = new byte[] { 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x05 };
        public uint DataLength { get; set; } = 0;
        public byte[] EmbeddedFile { get; set; } = new byte[0];
    }

    public class ExplosionEffectData
    {
        // Explosion Effect Params
        public Vector2 Field170 { get; set; } = new Vector2(34.2f, 91.2f);
        public Vector2 Field188 { get; set; } = new Vector2(0f, 0f);
        public Vector2 Field178 { get; set; } = new Vector2(-1f, 2f);
        public Vector2 Field180 { get; set; } = new Vector2(0f, 0f); // 0,0 for floor, -1f, 2f for dome
        public Vector2 Field190 { get; set; } = new Vector2(0f, 0f);
        public float Field198 { get; set; } = 0f;
    }


    public class AnimationData
    {
        public int Field00 { get; set; } = 0;
        public float Field04 { get; set; } = 30f; // total frames?
        public byte[] Flags { get; } = new byte[] {
            0x00, 0x00, 0x00, 0x01 }; // 1

        public float Duration { get; set; } = 1f;
        public int SubControllerCount { get; set; } = 0;  // controller count - 1
        public List<SubAnimController> SubControllers { get; set; } = new List<SubAnimController>();
        public int Field10 { get; set; } = 0; // controller count
        public List<AnimController> Controllers { get; set; } = new List<AnimController>();
    }

    public class SubAnimController
    {
        public byte[] TargetKind { get; } = new byte[] { 0x00, 0x01 }; // 1
        public int TargetID { get; set; } = 0;

        public byte[] TargetName { get; set; } = new byte[] { 0x00, 0x00 };

        public byte[] Layers { get; set; } =
        {
            0x00, 0x00, 0x00, 0x03, // layer count = 3
            0x00, 0x00, 0x00, 0x11, // KeyType = PRSByte (17)
            0x00, 0x00, 0x00, 0x00, // KeyCount = 0
            0x00, 0x00, 0x00, 0x12, // KeyType = MeshColor (18)
            0x00, 0x00, 0x00, 0x00, // KeyCount = 0
            0x00, 0x00, 0x00, 0x13,  // KeyType = SingleByte (19)
            0x00, 0x00, 0x00, 0x00 // KeyCount = 0
        };
    }

    public class AnimController
    {
        public float Field00 { get; set; } = 0f;
        public float Field04 { get; set; } = 0.9666666f;
        public int ControllerIndex { get; set; } = 0; // -1 for first one, counting up for each subsequent one
        public int Field0C { get; set; } = 0; // 0 for the first one, then childnode count - 1 and counting down for each subsequent one

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
            0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 
            0x00, 0x00, 0x00, 0x01, // attachment count
            0x00, 0x00, 0x00, 0x07 // attachment type (epl)
        };

        // attachment count

        // 0x00, 0x00, 0x00, 0x07

        // epl minus first 16 bytes

        // End of a GMD container
        public static byte[] gmdFooter { get; } = new byte[]
        {
            0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x10, 0x50, 0x90, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public static byte[] gmdFooterWithAnimPack { get; } = new byte[]
        {
            0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x10, 0x51, 0x00, 0x00, 0x01, 0x00, 0xFD,
            0x00, 0x00, 0x00, 0x28, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x10, 0x51, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
    }
}
