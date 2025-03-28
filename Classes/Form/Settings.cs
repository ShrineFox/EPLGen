using DarkUI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {

        UserSettings userSettings = new UserSettings();

        public class UserSettings()
        {
            public string ModelName { get; set; } = "Untitled";
            public string GMD { get; set; } = "";

            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
            public ModelType EplType = ModelType.Cone;
            public List<Particle> Particles = new List<Particle>();
        }

        private void LoadJson(string file)
        {
            userSettings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(file));

            UpdateSpriteList();
        }

        private void SaveJson(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(userSettings, Formatting.Indented));
        }


        public class Particle
        {
            public string TexturePath = "";
            public string Name { get; set; } = "Untitled";
            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
            public float ParticleSpeed = 1f;
            public uint RandomSpawnDelay = 6;
            public float ParticleLife = 1f;
            public float DespawnTimer = 0f;
            public Vector2 SpawnChoker = new Vector2(0f, 2f);
            public Vector2 SpawnerAngles = new Vector2(-360f, 360f);
            public Vector2 Field170 = new Vector2(102.6f, 84f);
            public Vector2 Field188 = new Vector2(0f, 0f);
            public Vector2 Field178 = new Vector2(-1f, 2f);
            public Vector2 Field180 = new Vector2(-1f, 2f); // 0,0 for floor
            public Vector2 Field190 = new Vector2(0f, 0f);
            public float DistanceFromScreen = 10f;

            public override string ToString()
            {
                return Name;
            }
        }

        public enum ModelType
        {
            Cone,
            Floor
        }
    }
}
