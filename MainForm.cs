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
using System.Xml.Linq;
using DarkUI.Controls;
using DarkUI.Forms;
using ShrineFox.IO;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            AddVectorFields();
            SetMenuStripIcons();
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

        private void AddVectorFields()
        {
            AddNumericControls(tlp_ModelSettings, "ModelRot", 4, 1, 0);
            AddNumericControls(tlp_ParticleSettings, "ParticleRot", 4, 1, 0);
            AddNumericControls(tlp_ModelSettings, "ModelScale", 3, 1, 1);
            AddNumericControls(tlp_ParticleSettings, "ParticleScale", 3, 1, 1);
            AddNumericControls(tlp_ParticleSettings, "ParticleTranslation", 3, 1, 2);
        }

        private void AddNumericControls(TableLayoutPanel parentTlp, string fieldPrefix, int numOfOptions, int column, int row)
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

                var numUpDwn = new DarkNumericUpDown() { Minimum = maxMin * -1, Maximum = maxMin, Dock = DockStyle.Top, Name = $"num_{fieldPrefix}_{axis}" };
                numUpDwn.ValueChanged += FieldValueChanged;
                tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Percent, Height = 50f });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.Controls.Add(new DarkLabel() { Text = axis.ToUpper(), Dock = DockStyle.Bottom }, i, 0);
                tlp.Controls.Add(numUpDwn, i, 1);
            }
            parentTlp.Controls.Add(tlp, column, row);
        }

        private void FieldValueChanged(object sender, EventArgs e)
        {
            DarkNumericUpDown numUpDwn = (DarkNumericUpDown)sender;
            UpdateOptionValue(numUpDwn.Name.Split('_')[1], numUpDwn.Name.Split('_')[2], (float)numUpDwn.Value);
        }

        private void UpdateOptionValue(string name, string axis, float value)
        {
            if (listBox_Sprites.SelectedIndex != -1 && listBox_Sprites.Enabled
                && modelSettings.Count - 1 >= listBox_Sprites.SelectedIndex)
            {
                var modelSetting = modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString()));
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
                }
                
            }
        }

        List<ModelSettings> modelSettings = new List<ModelSettings>();

        public class ParticleSettings
        {
            public Vector3 Translation = new Vector3(0f, 0f, 0f);
            public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 1f);
            public Vector3 Scale = new Vector3(1f, 1f, 1f);
        }

        public class ModelSettings
        {
            public string Name = "Untitled";
            public string TexturePath = "";
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
                foreach (var ctrl in new string[] { "ModelScale", "ParticleScale", "ParticleTranslation" })
                    foreach (var axis in new string[] { "x", "y", "z", })
                    {
                        DarkNumericUpDown numUpDwn = WinForms.GetControl(this, $"num_{ctrl}_{axis}");
                        LoadOptionValue(selectedModel, numUpDwn, ctrl, axis);
                    }

                LoadTexturePreview(selectedModel.TexturePath);
            }
            else
                LoadTexturePreview();
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
            }
        }

        private void LoadTexturePreview(string texPath = "")
        {
            // TODO: Convert DDS to Bitmap

            pictureBox_Tex.ImageLocation = texPath;
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex != -1 && modelSettings.Any(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())))
            {
                string modelName = listBox_Sprites.SelectedItem.ToString();
                modelSettings.Remove(modelSettings.First(x => x.Name.Equals(modelName)));

                UpdateSpriteList();
            }
        }

        private void Mode_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1 || !comboBox_Mode.Enabled)
                return;

            foreach (var item in listBox_Sprites.SelectedItems)
                if (modelSettings.Any(x => x.Name.Equals(item.ToString())))
                    ChangeMode(modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())), 
                        (ModelType)Enum.Parse(typeof(ModelType), comboBox_Mode.SelectedItem.ToString()));
        }

        private void ChangeMode(ModelSettings model, ModelType eplType)
        {
            model.EplType = eplType;

            // TODO: Change values based on selected type
        }

        private void ExportEPL_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("./Output"))
                Directory.Delete("./Output", true);
            Directory.CreateDirectory("./Output");

            foreach (var model in modelSettings)
                EPL.Build(model);

            List<byte> combinedEpl = new List<byte>();
            foreach (var eplFile in Directory.GetFiles("./Output", "*.epl", SearchOption.AllDirectories))
                combinedEpl.Concat(File.ReadAllBytes(eplFile));
            File.WriteAllBytes("combined.epl", combinedEpl.ToArray());
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
                        AddModel(Path.GetFileNameWithoutExtension(texPaths[i]), texPaths[i]);
                }

                UpdateSpriteList();
            }
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
