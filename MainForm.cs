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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DarkUI.Controls;
using DarkUI.Forms;
using ShrineFox.IO;
using static EPLGen.MainForm;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            AddVectorFields();
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
            public Vector3 Translation = new Vector3(0, 0, 0);
            public Vector4 Rotation = new Vector4(0, 0, 0, 0);
            public Vector3 Scale = new Vector3(1, 1, 1);
        }

        public class ModelSettings
        {
            public string Name = "Untitled";
            public string TexturePath = "";
            public Vector4 Rotation = new Vector4(0, 0, 0, 0);
            public Vector3 Scale = new Vector3(1, 1, 1);
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

        private void PictureBox_Clicked(object sender, EventArgs e)
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
    }



    public static class StringVector4Extensions
    {
        public static Vector4 ToVector4(this string str, params string[] delimiters)
        {
            if (str == null) throw new ArgumentNullException("string is null");
            if (string.IsNullOrEmpty(str)) throw new FormatException("string is empty");
            if (string.IsNullOrWhiteSpace(str)) throw new FormatException("string is just white space");

            if (delimiters == null) throw new ArgumentNullException("delimiters are null");
            if (delimiters.Length <= 0) throw new InvalidOperationException("missing delimiters");

            try
            {
                var rslt = str
            .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse)
            .ToArray()
            ;
                if (rslt.Length != 4)
                    return new Vector4(0, 0, 0, 0);
                else
                    return new Vector4(rslt[0], rslt[1], rslt[2], rslt[3]);
            } catch
            {
                return new Vector4(0, 0, 0, 0);
            }
            
        }
    }

    public static class StringVector3Extensions
    {
        public static Vector3 ToVector3(this string str, params string[] delimiters)
        {
            if (str == null) throw new ArgumentNullException("string is null");
            if (string.IsNullOrEmpty(str)) throw new FormatException("string is empty");
            if (string.IsNullOrWhiteSpace(str)) throw new FormatException("string is just white space");

            if (delimiters == null) throw new ArgumentNullException("delimiters are null");
            if (delimiters.Length <= 0) throw new InvalidOperationException("missing delimiters");

            try
            {
                var rslt = str
            .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse)
            .ToArray()
            ;
                if (rslt.Length != 3)
                    return new Vector3(1, 1, 1);
                return new Vector3(rslt[0], rslt[1], rslt[2]);
            }
            catch
            {
                return new Vector3(1, 1, 1);
            }
            
        }
    }
}
