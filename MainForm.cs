﻿using System.Data;
using System.Numerics;
using DarkUI.Controls;
using DarkUI.Forms;
using EPLGen.Classes;
using Newtonsoft.Json;
using ShrineFox.IO;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            MenuStripHelper.SetMenuStripIcons(MenuStripHelper.GetMenuStripIconPairs("./Dependencies/Icons.txt"), this);
            AddInputFields();

#if DEBUG
            LoadJson("./test2.json");
            ExportEPL("./Test2.EPL");
#endif
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
            AddNumControls(tlp_ParticleSettings, "Depawn Time:", "DespawnTimer", 6);
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
            var numUpDwn = new DarkNumericUpDown()
            {
                Minimum = -999999,
                Maximum = 999999,
                Name = $"num_{fieldPrefix}_x",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
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
            parentTlp.Controls.Add(new DarkLabel() { Text = labelText, Anchor = AnchorStyles.Right, AutoSize = true }, 0, row);
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
                var particle = userSettings.Particles.First(x => x.Name.Equals(item.ToString()));
                switch (name)
                {
                    case "ModelRot":
                        switch (axis)
                        {
                            case "x":
                                userSettings.Rotation.X = value;
                                break;
                            case "y":
                                userSettings.Rotation.Y = value;
                                break;
                            case "z":
                                userSettings.Rotation.Z = value;
                                break;
                            case "w":
                                userSettings.Rotation.W = value;
                                break;
                        }
                        break;
                    case "ParticleRot":
                        switch (axis)
                        {
                            case "x":
                                particle.Rotation.X = value;
                                break;
                            case "y":
                                particle.Rotation.Y = value;
                                break;
                            case "z":
                                particle.Rotation.Z = value;
                                break;
                            case "w":
                                particle.Rotation.W = value;
                                break;
                        }
                        break;
                    case "ModelScale":
                        switch (axis)
                        {
                            case "x":
                                userSettings.Scale.X = value;
                                break;
                            case "y":
                                userSettings.Scale.Y = value;
                                break;
                            case "z":
                                userSettings.Scale.Z = value;
                                break;
                        }
                        break;
                    case "ParticleScale":
                        switch (axis)
                        {
                            case "x":
                                particle.Scale.X = value;
                                break;
                            case "y":
                                particle.Scale.Y = value;
                                break;
                            case "z":
                                particle.Scale.Z = value;
                                break;
                        }
                        break;
                    case "ModelTranslation":
                        switch (axis)
                        {
                            case "x":
                                userSettings.Translation.X = value;
                                break;
                            case "y":
                                userSettings.Translation.Y = value;
                                break;
                            case "z":
                                userSettings.Translation.Z = value;
                                break;
                        }
                        break;
                    case "ParticleTranslation":
                        switch (axis)
                        {
                            case "x":
                                particle.Translation.X = value;
                                break;
                            case "y":
                                particle.Translation.Y = value;
                                break;
                            case "z":
                                particle.Translation.Z = value;
                                break;
                        }
                        break;
                    case "ParticleSpeed":
                        particle.ParticleSpeed = value;
                        break;
                    case "RandomSpawnDelay":
                        particle.RandomSpawnDelay = Convert.ToUInt32(value);
                        break;
                    case "ParticleLife":
                        particle.ParticleLife = value;
                        break;
                    case "DespawnTimer":
                        particle.DespawnTimer = value;
                        break;
                    case "SpawnChoker":
                        switch (axis)
                        {
                            case "x":
                                particle.SpawnChoker.X = value;
                                break;
                            case "y":
                                particle.SpawnChoker.Y = value;
                                break;
                        }
                        break;
                    case "SpawnerAngles":
                        switch (axis)
                        {
                            case "x":
                                particle.SpawnerAngles.X = value;
                                break;
                            case "y":
                                particle.SpawnerAngles.Y = value;
                                break;
                        }
                        break;
                    case "Field170":
                        switch (axis)
                        {
                            case "x":
                                particle.Field170.X = value;
                                break;
                            case "y":
                                particle.Field170.Y = value;
                                break;
                        }
                        break;
                    case "Field188":
                        switch (axis)
                        {
                            case "x":
                                particle.Field188.X = value;
                                break;
                            case "y":
                                particle.Field188.Y = value;
                                break;
                        }
                        break;
                    case "Field178":
                        switch (axis)
                        {
                            case "x":
                                particle.Field178.X = value;
                                break;
                            case "y":
                                particle.Field178.Y = value;
                                break;
                        }
                        break;
                    case "Field180":
                        switch (axis)
                        {
                            case "x":
                                particle.Field180.X = value;
                                break;
                            case "y":
                                particle.Field180.Y = value;
                                break;
                        }
                        break;
                    case "Field190":
                        switch (axis)
                        {
                            case "x":
                                particle.Field190.X = value;
                                break;
                            case "y":
                                particle.Field190.Y = value;
                                break;
                        }
                        break;
                }

            }
        }

        UserSettings userSettings = new UserSettings();

        public class UserSettings()
        {
            public string ModelName { get; set; } = "Untitled";
            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
            public ModelType EplType = ModelType.Cone;
            public List<Particle> Particles = new List<Particle>();
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
        }

        public enum ModelType
        {
            Cone,
            Floor
        }

        private void AddSprite_Click(object sender, EventArgs e)
        {
            var files = WinFormsDialogs.SelectFile("Choose DDS Textures", true, new string[] { "DDS Texture (.DDS)" });

            if (files.Count <= 0)
                return;

            foreach(var file in files)
                AddParticle(Path.GetFileNameWithoutExtension(file), file);

            UpdateSpriteList();
        }

        private void AddParticle(string name = "", string texPath = "")
        {
            Particle particle = new Particle();
            if (string.IsNullOrEmpty(name))
                name = particle.Name;
            else
                particle.Name = name;

            if (!string.IsNullOrEmpty(texPath))
                particle.TexturePath = texPath;

            int i = 1;
            while (userSettings.Particles.Any(x => x.Name.Equals(particle.Name)))
            {
                i++;
                particle.Name = name + i;
            }
            userSettings.Particles.Add(particle);
        }

        private void UpdateSpriteList()
        {
            // Disable listbox to prevent event from firing
            listBox_Sprites.Enabled = false;

            // Try to retain previous selection
            int selected = listBox_Sprites.SelectedIndex;

            // Re-add items to listbox
            listBox_Sprites.Items.Clear();
            foreach (var item in userSettings.Particles)
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
                && userSettings.Particles.Count - 1 >= listBox_Sprites.SelectedIndex)
            {
                var selectedParticle = userSettings.Particles.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString()));

                // Try to use values from selected model object
                foreach (var ctrl in new string[] { "ModelRot", "ParticleRot" })
                    foreach (var axis in new string[] { "x", "y", "z", "w" })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedParticle, numUpDwn, ctrl, axis);
                    }
                foreach (var ctrl in new string[] { "ModelScale", "ModelTranslation", "ParticleScale", "ParticleTranslation" })
                    foreach (var axis in new string[] { "x", "y", "z", })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedParticle, numUpDwn, ctrl, axis);
                    }

                foreach (var ctrl in new string[] { "SpawnChoker", "SpawnerAngles", "Field170", "Field188", "Field178", "Field180", "Field190" })
                    foreach (var axis in new string[] { "x", "y" })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedParticle, numUpDwn, ctrl, axis);
                    }

                foreach (var ctrl in new string[] { "ParticleSpeed", "RandomSpawnDelay", "ParticleLife", "DespawnTimer" })
                {
                    DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_x");
                    LoadOptionValue(selectedParticle, numUpDwn, ctrl, "x");
                }
                comboBox_Mode.SelectedIndex = comboBox_Mode.Items.IndexOf(userSettings.EplType.ToString());

                LoadTexturePreview(selectedParticle.TexturePath);
            }
            else
                LoadTexturePreview();

            comboBox_Mode.Enabled = true;
        }

        private void LoadOptionValue(Particle particle, DarkNumericUpDown numUpDwn, string ctrl, string axis)
        {
            switch (ctrl)
            {
                case "ModelRot":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Rotation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Rotation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Rotation.Z);
                            break;
                        case "w":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Rotation.W);
                            break;
                    }
                    break;
                case "ParticleRot":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Rotation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Rotation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(particle.Rotation.Z);
                            break;
                        case "w":
                            numUpDwn.Value = Convert.ToDecimal(particle.Rotation.W);
                            break;
                    }
                    break;
                case "ModelScale":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Scale.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Scale.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Scale.Z);
                            break;
                    }
                    break;
                case "ParticleScale":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Scale.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Scale.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(particle.Scale.Z);
                            break;
                    }
                    break;
                case "ModelTranslation":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Translation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Translation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(userSettings.Translation.Z);
                            break;
                    }
                    break;
                case "ParticleTranslation":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Translation.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Translation.Y);
                            break;
                        case "z":
                            numUpDwn.Value = Convert.ToDecimal(particle.Translation.Z);
                            break;
                    }
                    break;
                case "ParticleSpeed":
                    numUpDwn.Value = Convert.ToDecimal(particle.ParticleSpeed);
                    break;
                case "RandomSpawnDelay":
                    numUpDwn.Value = Convert.ToDecimal(particle.RandomSpawnDelay);
                    break;
                case "ParticleLife":
                    numUpDwn.Value = Convert.ToDecimal(particle.ParticleLife);
                    break;
                case "DespawnTimer":
                    numUpDwn.Value = Convert.ToDecimal(particle.DespawnTimer);
                    break;
                case "SpawnChoker":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.SpawnChoker.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.SpawnChoker.Y);
                            break;
                    }
                    break;
                case "SpawnerAngles":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.SpawnerAngles.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.SpawnerAngles.Y);
                            break;
                    }
                    break;
                case "Field170":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field170.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field170.Y);
                            break;
                    }
                    break;
                case "Field188":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field188.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field188.Y);
                            break;
                    }
                    break;
                case "Field178":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field178.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field178.Y);
                            break;
                    }
                    break;
                case "Field180":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field180.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field180.Y);
                            break;
                    }
                    break;
                case "Field190":
                    switch (axis)
                    {
                        case "x":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field190.X);
                            break;
                        case "y":
                            numUpDwn.Value = Convert.ToDecimal(particle.Field190.Y);
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
                if (userSettings.Particles.Any(x => x.Name.Equals(item.ToString())))
                    userSettings.Particles.Remove(userSettings.Particles.First(x => x.Name.Equals(item.ToString())));

            UpdateSpriteList();
        }

        private void Mode_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1 || !comboBox_Mode.Enabled)
                return;

            ChangeMode(userSettings, (ModelType)Enum.Parse(typeof(ModelType), comboBox_Mode.SelectedItem.ToString()));
        }

        private void ChangeMode(UserSettings settings, ModelType eplType)
        {
            settings.EplType = eplType;

            // Change values based on selected type
            switch (eplType)
            {
                case ModelType.Floor:
                    settings.Rotation = new Quaternion(0f, 0f, 0f, 1f);
                    foreach (var particle in settings.Particles)
                    {
                        particle.SpawnerAngles = new Vector2(90f, 90f);
                        particle.Field180 = new Vector2(0f, 0f);
                    }
                    break;
                default:
                    settings.Rotation = new Quaternion(0, 0f, 0f, 1f);
                    foreach (var particle in settings.Particles)
                    {
                        particle.SpawnerAngles = new Vector2(-360f, 360f);
                        particle.Field180 = new Vector2(-1f, 2f);                    
                    }
                    break;
            }

            LoadOptionValues();
        }

        private void ExportEPL_Click(object sender, EventArgs e)
        {
            var outPath = WinFormsDialogs.SelectFile("Choose EPL Destination", false, new string[] { "EPL (.EPL)" }, true);
            if (string.IsNullOrEmpty(outPath.First()))
                return;

            if (!outPath.First().ToLower().EndsWith(".epl"))
                outPath[0] += ".EPL";

            ExportEPL(outPath.First());

            MessageBox.Show($"Done exporting EPL:\n{outPath.First()}", "EPL Export Successful");
        }

        private void ExportEPL(string outPath)
        {
            userSettings.ModelName = Path.GetFileNameWithoutExtension(outPath);

            EPL.Build(userSettings, outPath);
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex != -1 && userSettings.Particles.Any(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())))
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

                    if (userSettings.Particles.Any(x => x.Name.Equals(renameForm.RenameText)))
                    {
                        MessageBox.Show("There is already an item with the same name! Please choose a different one.",
                            "Error: Duplicate entry name");
                        return;
                    }
                    else
                    {
                        userSettings.Particles.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).Name = renameForm.RenameText;
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
                List<string> texPaths = WinFormsDialogs.SelectFile("Choose sprite texture", true, new string[1] { "DDS Image (.dds)" });
                for (int i = 0; i < texPaths.Count; i++)
                {
                    // Update texture path for model object matching selected listbox item, otherwise create new one named after file
                    if (i == 0)
                        userSettings.Particles.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).TexturePath = texPaths[i];
                    else
                    {
                        AddParticle(Path.GetFileNameWithoutExtension(texPaths[i]), texPaths[i]);
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
            var selection = WinFormsDialogs.SelectFile("Save preset file...", true, new string[] { "json (.json)" }, true);
            if (selection.Count == 0)
                return;

            string outPath = selection.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath = $"{outPath}.json";

            File.WriteAllText(outPath, JsonConvert.SerializeObject(userSettings, Newtonsoft.Json.Formatting.Indented));
            MessageBox.Show($"Saved preset file to:\n{outPath}", "Preset Saved Successfully");
        }

        private void LoadPreset_Click(object sender, EventArgs e)
        {
            var selection = WinFormsDialogs.SelectFile("Load preset file...", true, new string[] { "json (.json)" });
            if (selection.Count == 0 || !File.Exists(selection.First()))
                return;

            LoadJson(selection.First());
        }

        private void LoadJson(string file)
        {
            userSettings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(file));

            UpdateSpriteList();
        }

        private void CreateEPTs_Click(object sender, EventArgs e)
        {
            var inputDir = WinFormsDialogs.SelectFolder("Choose DDS Files Directory");
            if (!Directory.Exists(inputDir))
                return;

            foreach (var ddsPath in Directory.GetFiles(inputDir, "*.dds", SearchOption.AllDirectories))
            {
                EPT ept = new EPT();
                ept.imageData = File.ReadAllBytes(ddsPath);
                ept.imageName = Path.GetFileName(ddsPath);
                ept.Build(inputDir);
            }

            MessageBox.Show($"Done converting DDS to EPT in:\n{inputDir}", "EPTs Saved Successfully");
        }

        private void CreateUVAnim_Click(object sender, EventArgs e)
        {
            string outPath = "./animdata.bin";
            TextureAnimation texAnim = new TextureAnimation();
            texAnim.Build(outPath, Convert.ToInt32(txt_SpriteCount.Text));
            MessageBox.Show($"Done outputting UV anim data to:\n{outPath}", "UV Anim Data Saved Successfully");
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
