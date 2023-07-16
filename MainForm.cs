using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DarkUI.Controls;
using DarkUI.Forms;
using GFDLibrary.Effects;
using Newtonsoft.Json;
using ShrineFox.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static EPLGen.MainForm;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            SetMenuStripIcons();
            AddInputFields();
        }

        private void SetMenuStripIcons()
        {
            List<Tuple<string, string>> menuStripIcons = new List<Tuple<string, string>>() {
                new Tuple<string, string>("fileToolStripMenuItem", "disk"),
                new Tuple<string, string>("loadToolStripMenuItem", "folder_page"),
                new Tuple<string, string>("saveToolStripMenuItem", "disk_multiple"),
                new Tuple<string, string>("exportEPLToolStripMenuItem", "package_go"),
                new Tuple<string, string>("addToolStripMenuItem", "add"),
                new Tuple<string, string>("addSpriteToolStripMenuItem", "add"),
                new Tuple<string, string>("removeToolStripMenuItem", "delete"),
                new Tuple<string, string>("removeSelectedToolStripMenuItem", "delete"),
                new Tuple<string, string>("renameToolStripMenuItem", "textfield_rename"),
                new Tuple<string, string>("renameSelectedToolStripMenuItem", "textfield_rename"),
                new Tuple<string, string>("setImageToolStripMenuItem", "picture_add"),
                new Tuple<string, string>("chooseImageFileToolStripMenuItem", "picture_add"),
            };

            // Context Menu Strips
            foreach (DarkContextMenu menuStrip in new DarkContextMenu[] { darkContextMenu_Sprites, darkContextMenu_Texture })
                ApplyIconsFromList(menuStrip.Items, menuStripIcons);

            // Menu Strip Items
            foreach (DarkMenuStrip menuStrip in this.FlattenChildren<DarkMenuStrip>())
                ApplyIconsFromList(menuStrip.Items, menuStripIcons);
        }

        private void ApplyIconsFromList(ToolStripItemCollection items, List<Tuple<string, string>> menuStripIcons)
        {
            foreach (ToolStripMenuItem tsmi in items)
            {
                // Apply context menu icon
                if (menuStripIcons.Any(x => x.Item1 == tsmi.Name))
                    ApplyIconFromFile(tsmi, menuStripIcons);
                // Apply drop down menu icon
                foreach (ToolStripMenuItem tsmi2 in tsmi.DropDownItems)
                    if (menuStripIcons.Any(x => x.Item1 == tsmi2.Name))
                        ApplyIconFromFile(tsmi2, menuStripIcons);
            }
        }

        private void ApplyIconFromFile(ToolStripMenuItem tsmi, List<Tuple<string, string>> menuStripIcons)
        {
            tsmi.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        $"Icons\\{menuStripIcons.Single(x => x.Item1 == tsmi.Name).Item2}.png"));
        }

        private void AddInputFields()
        {
            AddAxisControls(tlp_ModelSettings, "Pos Offset:", "ModelTranslation", 3, 1);
            AddAxisControls(tlp_ModelSettings, "Rotate:", "ModelRot", 4, 2);
            AddAxisControls(tlp_ModelSettings, "Scale:", "ModelScale", 3, 3);

            AddAxisControls(tlp_ParticleSettings, "Pos Offset:", "ParticleTranslation", 3, 0);
            AddAxisControls(tlp_ParticleSettings, "Rotate:", "ParticleRot", 4, 1);
            AddAxisControls(tlp_ParticleSettings, "Scale:", "ParticleScale", 3, 2);
            AddNumControls(tlp_ParticleSettings, "Speed:", "ParticleSpeed", 3);
            AddNumControls(tlp_ParticleSettings, "Spawn Delay:", "RandomSpawnDelay", 4, false);
            AddNumControls(tlp_ParticleSettings, "Life Length:", "ParticleLife", 5);
            AddNumControls(tlp_ParticleSettings, "Repawn Time:", "RespawnTimer", 6);
            AddAxisControls(tlp_ParticleSettings, "Spawn Choke:", "SpawnChoker", 2, 7);
            AddAxisControls(tlp_ParticleSettings, "Spawn Angles:", "SpawnerAngles", 2, 8);
            AddAxisControls(tlp_ParticleSettings, "Field170:", "Field170", 2, 9);
            AddAxisControls(tlp_ParticleSettings, "Field188:", "Field188", 2, 10);
            AddAxisControls(tlp_ParticleSettings, "Field178:", "Field178", 2, 11);
            AddAxisControls(tlp_ParticleSettings, "Field180:", "Field180", 2, 12);
            AddAxisControls(tlp_ParticleSettings, "Field190:", "Field190", 2, 13);
        }

        private void AddNumControls(TableLayoutPanel parentTlp, string labelText, string fieldPrefix, int row, bool decimalPlaces = true)
        {
            var numUpDwn = new DarkNumericUpDown() { Minimum = -999999, Maximum = 999999, Name = $"num_{fieldPrefix}_x",
                Anchor = AnchorStyles.Left, AutoSize = true  };
            if (!decimalPlaces)
                numUpDwn.DecimalPlaces = 7;
            parentTlp.Controls.Add(new DarkLabel() { Text = labelText, Anchor = AnchorStyles.Right, AutoSize = true }, 0, row);
            parentTlp.Controls.Add(numUpDwn, 1, row);
        }

        private void AddAxisControls(TableLayoutPanel parentTlp, string labelText, string fieldPrefix, int numOfOptions, int row)
        {
            TableLayoutPanel tlp = new TableLayoutPanel() { Dock = DockStyle.Fill };
            for (int i = 0; i < numOfOptions; i++)
            {
                string axis = "x";
                switch (i)
                {
                    case 1: axis = "y"; break;
                    case 2: axis = "z"; break;
                    case 3: axis = "w"; break;
                }

                decimal maxMin = 360;
                float columnWidth = 25f;
                if (numOfOptions < 4)
                {
                    maxMin = 99999;
                    columnWidth = 33f;
                }

                var numUpDwn = new DarkNumericUpDown() { Minimum = maxMin * -1, Maximum = maxMin, Dock = DockStyle.Top, Name = $"num_{fieldPrefix}_{axis}", DecimalPlaces = 7 };
                numUpDwn.ValueChanged += FieldValueChanged;
                tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Percent, Height = 50f });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.Controls.Add(new DarkLabel() { Text = axis.ToUpper(), Dock = DockStyle.Bottom }, i, 0);
                tlp.Controls.Add(numUpDwn, i, 1);
            }
            parentTlp.Controls.Add(new DarkLabel() { Text = labelText, Anchor = AnchorStyles.Right, AutoSize = true  }, 0, row);
            parentTlp.Controls.Add(tlp, 1, row);
        }

        private void FieldValueChanged(object sender, EventArgs e)
        {
            DarkNumericUpDown numUpDwn = (DarkNumericUpDown)sender;
            UpdateOptionValue(numUpDwn.Name.Split('_')[1], numUpDwn.Name.Split('_')[2], (float)numUpDwn.Value);
        }

        private void UpdateOptionValue(string name, string axis, float value)
        {
            foreach (var item in listBox_Sprites.SelectedItems)
            {
                var modelSetting = modelSettings.First(x => x.Name.Equals(item.ToString()));
                switch (name)
                {
                    case "ModelRot":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Rotation.X = value;
                                break;
                            case "y":
                                modelSetting.Rotation.Y = value;
                                break;
                            case "z":
                                modelSetting.Rotation.Z = value;
                                break;
                            case "w":
                                modelSetting.Rotation.W = value;
                                break;
                        }
                        break;
                    case "ParticleRot":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Rotation.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Rotation.Y = value;
                                break;
                            case "z":
                                modelSetting.Particle.Rotation.Z = value;
                                break;
                            case "w":
                                modelSetting.Particle.Rotation.W = value;
                                break;
                        }
                        break;
                    case "ModelScale":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Scale.X = value;
                                break;
                            case "y":
                                modelSetting.Scale.Y = value;
                                break;
                            case "z":
                                modelSetting.Scale.Z = value;
                                break;
                        }
                        break;
                    case "ParticleScale":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Scale.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Scale.Y = value;
                                break;
                            case "z":
                                modelSetting.Particle.Scale.Z = value;
                                break;
                        }
                        break;
                    case "ModelTranslation":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Translation.X = value;
                                break;
                            case "y":
                                modelSetting.Translation.Y = value;
                                break;
                            case "z":
                                modelSetting.Translation.Z = value;
                                break;
                        }
                        break;
                    case "ParticleTranslation":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Translation.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Translation.Y = value;
                                break;
                            case "z":
                                modelSetting.Particle.Translation.Z = value;
                                break;
                        }
                        break;
                    case "ParticleSpeed":
                        modelSetting.Particle.ParticleSpeed = value;
                        break;
                    case "RandomSpawnDelay":
                        modelSetting.Particle.RandomSpawnDelay = Convert.ToUInt32(value);
                        break;
                    case "ParticleLife":
                        modelSetting.Particle.ParticleLife = value;
                        break;
                    case "RespawnTimer":
                        modelSetting.Particle.RespawnTimer = value;
                        break;
                    case "SpawnChoker":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.SpawnChoker.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.SpawnChoker.Y = value;
                                break;
                        }
                        break;
                    case "SpawnerAngles":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.SpawnerAngles.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.SpawnerAngles.Y = value;
                                break;
                        }
                        break;
                    case "Field170":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Field170.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Field170.Y = value;
                                break;
                        }
                        break;
                    case "Field188":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Field188.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Field188.Y = value;
                                break;
                        }
                        break;
                    case "Field178":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Field178.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Field178.Y = value;
                                break;
                        }
                        break;
                    case "Field180":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Field180.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Field180.Y = value;
                                break;
                        }
                        break;
                    case "Field190":
                        switch (axis)
                        {
                            case "x":
                                modelSetting.Particle.Field190.X = value;
                                break;
                            case "y":
                                modelSetting.Particle.Field190.Y = value;
                                break;
                        }
                        break;
                }
                
            }
        }

        List<ModelSettings> modelSettings = new List<ModelSettings>();

        public class ParticleSettings
        {
            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
            public float ParticleSpeed = 0.5f;
            public uint RandomSpawnDelay = 0;
            public float ParticleLife = 0.5f;
            public float RespawnTimer = 0f;
            public Vector2 SpawnChoker = new Vector2(0f, 2f);
            public Vector2 SpawnerAngles = new Vector2(-360f, 360f);
            public Vector2 Field170 = new Vector2(102.6f, 84f);
            public Vector2 Field188 = new Vector2(0f, 0f);
            public Vector2 Field178 = new Vector2(-1f, 2f);
            public Vector2 Field180 = new Vector2(-1f, 2f); // 0,0 for floor
            public Vector2 Field190 = new Vector2(0f, 0f);
        }

        public class ModelSettings
        {
            public string Name = "Untitled";
            public string TexturePath = "";
            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
            public ModelType EplType = ModelType.Cone;
            public ParticleSettings Particle = new ParticleSettings();
        }

        public enum ModelType
        {
            Cone,
            Floor
        }

        private void AddSprite_Click(object sender, EventArgs e)
        {
            AddModel();

            UpdateSpriteList();
        }

        private void AddModel(string name = "", string texPath = "")
        {
            ModelSettings model = new ModelSettings();
            if (string.IsNullOrEmpty(name))
                name = model.Name;
            else
                model.Name = name;

            if (!string.IsNullOrEmpty(texPath))
                model.TexturePath = texPath;

            int i = 1;
            while (modelSettings.Any(x => x.Name.Equals(model.Name)))
            {
                i++;
                model.Name = name + i;
            }
            modelSettings.Add(model);
        }

        private void UpdateSpriteList()
        {
            // Disable listbox to prevent event from firing
            listBox_Sprites.Enabled = false;

            // Try to retain previous selection
            int selected = listBox_Sprites.SelectedIndex;

            // Re-add items to listbox
            listBox_Sprites.Items.Clear();
            foreach (var item in modelSettings)
            {
                listBox_Sprites.Items.Add(item.Name);
            }

            // Re-enable listbox and re-select previous value if within bounds
            if (selected != -1 && listBox_Sprites.Items.Count - 1 >= selected)
                listBox_Sprites.SelectedIndex = selected;

            // Re-enable listbox
            listBox_Sprites.Enabled = true;
        }

        private void SpriteList_IndexChanged(object sender, EventArgs e)
        {
            LoadOptionValues();
        }

        private void LoadOptionValues()
        {
            comboBox_Mode.Enabled = false;

            if (listBox_Sprites.SelectedIndex != -1 && listBox_Sprites.Enabled 
                && modelSettings.Count - 1 >= listBox_Sprites.SelectedIndex)
            {
                var selectedModel = modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString()));

                // Try to use values from selected model object
                foreach (var ctrl in new string[] { "ModelRot", "ParticleRot" })
                    foreach (var axis in new string[] { "x", "y", "z", "w" })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedModel, numUpDwn, ctrl, axis);
                    }
                foreach (var ctrl in new string[] { "ModelScale", "ModelTranslation", "ParticleScale", "ParticleTranslation" })
                    foreach (var axis in new string[] { "x", "y", "z", })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedModel, numUpDwn, ctrl, axis);
                    }

                foreach (var ctrl in new string[] { "SpawnChoker", "SpawnerAngles", "Field170", "Field188", "Field178", "Field180", "Field190" })
                    foreach (var axis in new string[] { "x", "y" })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedModel, numUpDwn, ctrl, axis);
                    }

                foreach (var ctrl in new string[] { "ParticleSpeed", "RandomSpawnDelay", "ParticleLife", "RespawnTimer" })
                {
                    DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_x");
                    LoadOptionValue(selectedModel, numUpDwn, ctrl, "x");
                }
                comboBox_Mode.SelectedIndex = comboBox_Mode.Items.IndexOf(selectedModel.EplType.ToString());

                LoadTexturePreview(selectedModel.TexturePath);
            }
            else
                LoadTexturePreview();

            comboBox_Mode.Enabled = true;
        }

        private void LoadOptionValue(ModelSettings selectedModel, DarkNumericUpDown numUpDwn, string ctrl, string axis)
        {
            switch (ctrl)
            {
                case "ModelRot":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Rotation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Rotation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Rotation.Z);
                            break;
                        case "w":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Rotation.W);
                            break;
                    }
                    break;
                case "ParticleRot":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Rotation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Rotation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Rotation.Z);
                            break;
                        case "w":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Rotation.W);
                            break;
                    }
                    break;
                case "ModelScale":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Scale.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Scale.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Scale.Z);
                            break;
                    }
                    break;
                case "ParticleScale":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Scale.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Scale.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Scale.Z);
                            break;
                    }
                    break;
                case "ModelTranslation":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Translation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Translation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Translation.Z);
                            break;
                    }
                    break;
                case "ParticleTranslation":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Translation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Translation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Translation.Z);
                            break;
                    }
                    break;
                case "ParticleSpeed":
                    numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.ParticleSpeed);
                    break;
                case "RandomSpawnDelay":
                    numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.RandomSpawnDelay);
                    break;
                case "ParticleLife":
                    numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.ParticleLife);
                    break;
                case "RespawnTimer":
                    numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.RespawnTimer);
                    break;
                case "SpawnChoker":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.SpawnChoker.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.SpawnChoker.Y);
                            break;
                    }
                    break;
                case "SpawnerAngles":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.SpawnerAngles.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.SpawnerAngles.Y);
                            break;
                    }
                    break;
                case "Field170":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field170.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field170.Y);
                            break;
                    }
                    break;
                case "Field188":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field188.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field188.Y);
                            break;
                    }
                    break;
                case "Field178":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field178.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field178.Y);
                            break;
                    }
                    break;
                case "Field180":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field180.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field180.Y);
                            break;
                    }
                    break;
                case "Field190":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field190.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(selectedModel.Particle.Field190.Y);
                            break;
                    }
                    break;
            }
        }

        private void LoadTexturePreview(string texPath = "")
        {
            if (!String.IsNullOrEmpty(texPath) && File.Exists(texPath))
            {
                pictureBox_Tex.Image = GFDLibrary.Textures.TextureDecoder.Decode(File.ReadAllBytes(texPath),
                    GFDLibrary.Textures.TextureFormat.DDS);
            }
            else
            {
                pictureBox_Tex.Image = null;
            }
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox_Sprites.SelectedItems)
                if (modelSettings.Any(x => x.Name.Equals(item.ToString())))
                    modelSettings.Remove(modelSettings.First(x => x.Name.Equals(item.ToString())));

            UpdateSpriteList();
        }

        private void Mode_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1 || !comboBox_Mode.Enabled)
                return;

            foreach (var item in listBox_Sprites.SelectedItems)
                if (modelSettings.Any(x => x.Name.Equals(item.ToString())))
                    ChangeMode(modelSettings.First(x => x.Name.Equals(item.ToString())), 
                        (ModelType)Enum.Parse(typeof(ModelType), comboBox_Mode.SelectedItem.ToString()));
        }

        private void ChangeMode(ModelSettings model, ModelType eplType)
        {
            model.EplType = eplType;

            // Change values based on selected type
            switch (eplType)
            {
                case ModelType.Floor:
                    model.Rotation = new Quaternion(-0.7071068f, 0f, 0f, 0.7071068f);
                    model.Particle.SpawnerAngles = new Vector2(0f, 0f);
                    model.Particle.Field180 = new Vector2(0f, 0f);
                    model.Particle.Translation = new Vector3(0, 0.5f, 0);
                    model.Particle.Rotation = new Quaternion(0f, 1f, 0f, 0f);
                    break;
                default:
                    model.Rotation = new Quaternion(0, 0f, 0f, 1f);
                    model.Particle.SpawnerAngles = new Vector2(-360f, 360f);
                    model.Particle.Field180 = new Vector2(-1f, 2f);
                    model.Particle.Translation = new Vector3(0, 0f, 0);
                    model.Particle.Rotation = new Quaternion(0f, 0f, 0f, 1f);
                    break;
            }

            LoadOptionValues();
        }

        private void ExportEPL_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("./Output"))
                Directory.Delete("./Output", true);
            Directory.CreateDirectory("./Output");

            foreach (var model in modelSettings)
                EPL.Build(model);

            List<byte> combinedEpl = new List<byte>();
            var eplFiles = Directory.GetFiles("./Output", "*.epl", SearchOption.AllDirectories);
            foreach (var eplFile in eplFiles)
                combinedEpl = combinedEpl.Concat(File.ReadAllBytes(eplFile)).ToList();

            // Output combined EPL file with attachment count at the beginning (for hex editing gmd attachments)
            byte[] eplCount = BitConverter.GetBytes(EndiannessSwapUtility.Swap(Convert.ToUInt32(eplFiles.Count())));
            byte[] combinedEpls = eplCount.Concat(combinedEpl).ToArray();
            File.WriteAllBytes("./COMBINED.EPL", combinedEpls);
            // Output combined EPL wrapped in a GMD (for attaching to objects)
            byte[] combinedGMD = GMD.gmdHeader.Concat(combinedEpls).Concat(GMD.gmdFooter).ToArray();
            File.WriteAllBytes("./COMBINED.GMD", combinedGMD);

            MessageBox.Show($"Done exporting EPL:\n{Path.GetFullPath("./COMBINED.EPL")}", "EPL Export Successful");
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex != -1 && modelSettings.Any(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())))
            {
                RenameForm renameForm = new RenameForm(listBox_Sprites.SelectedItem.ToString());
                var result = renameForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string newName = renameForm.RenameText;
                    if (string.IsNullOrEmpty(newName))
                    {
                        MessageBox.Show("The name you provided is either null or empty.",
                            "Error: Invalid name");
                        return;
                    }

                    if (modelSettings.Any(x => x.Name.Equals(renameForm.RenameText)))
                    {
                        MessageBox.Show("There is already an item with the same name! Please choose a different one.",
                            "Error: Duplicate entry name");
                        return;
                    }
                    else
                    {
                        modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).Name = renameForm.RenameText;
                    }
                }
                else
                    return;

                UpdateSpriteList();
            }
        }

        private void ChooseImageFile_Click(object sender, EventArgs e)
        {
            BrowseForTexture();
        }

        private void BrowseForTexture()
        {
            if (listBox_Sprites.SelectedIndex != -1)
            {
                // Ask user to select .dds file(s)
                List<string> texPaths = WinFormsEvents.FilePath_Click("Choose sprite texture", true, new string[1] { "DDS Image (.dds)" });
                for (int i = 0; i < texPaths.Count; i++)
                {
                    // Update texture path for model object matching selected listbox item, otherwise create new one named after file
                    if (i == 0)
                        modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).TexturePath = texPaths[i];
                    else
                    {
                        AddModel(Path.GetFileNameWithoutExtension(texPaths[i]), texPaths[i]);
                        UpdateSpriteList();
                    }

                    // Show texture preview if it's the last one loaded
                    if (i == texPaths.Count - 1)
                        LoadTexturePreview(texPaths[i]);
                }
            }
        }

        private void SpritesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveSelected_Click(sender, e);
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
                Rename_Click(sender, e);
        }

        private void SavePreset_Click(object sender, EventArgs e)
        {
            var selection = WinFormsEvents.FilePath_Click("Save preset file...", true, new string[] { "json (.json)" }, true);
            if (selection.Count == 0)
                return;

            string outPath = selection.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath += ".json";

            File.WriteAllText(selection.First(), JsonConvert.SerializeObject(modelSettings, Newtonsoft.Json.Formatting.Indented));
            MessageBox.Show($"Saved preset file to:\n{selection.First()}", "Preset Saved Successfully");
        }

        private void LoadPreset_Click(object sender, EventArgs e)
        {
            var selection = WinFormsEvents.FilePath_Click("Load preset file...", true, new string[] { "json (.json)" });
            if (selection.Count == 0)
                return;

            modelSettings = JsonConvert.DeserializeObject<List<ModelSettings>>(File.ReadAllText(selection.First()));

            UpdateSpriteList();
        }
    }

    public static class ControlExtensions
    {
        public static IEnumerable<Control> FlattenChildren<T>(this Control control)
        {
            return control.FlattenChildren().OfType<T>().Cast<Control>();
        }

        public static IEnumerable<Control> FlattenChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();
            return children.SelectMany(c => FlattenChildren(c)).Concat(children);
        }
    }
}
