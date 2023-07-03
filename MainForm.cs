using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using ShrineFox.IO;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
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
            UpdateOptionValues();
        }

        private void UpdateOptionValues()
        {
            if (listBox_Sprites.SelectedIndex != -1 && listBox_Sprites.Enabled && modelSettings.Count - 1 >= listBox_Sprites.SelectedIndex)
            {
                var selectedModel = modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString()));

                txt_Rotation.Text = selectedModel.Rotation.ToString();
                txt_Scale.Text = selectedModel.Scale.ToString();
                txt_ParticleTranslation.Text = selectedModel.Particle.Translation.ToString();
                txt_ParticleRot.Text = selectedModel.Particle.Rotation.ToString();
                txt_ParticleScale.Text = selectedModel.Particle.Scale.ToString();
                comboBox_Mode.SelectedIndex = (int)selectedModel.EplType;

                LoadTexturePreview(selectedModel.TexturePath);
            }
            else
                LoadTexturePreview();
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

        private void Rotation_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1)
                return;

            foreach (var item in listBox_Sprites.SelectedItems)
                if (modelSettings.Any(x => x.Name.Equals(item.ToString())))
                    modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).Rotation = txt_Rotation.Text.ToVector4(",", " ");
        }

        private void Scale_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1)
                return;

            foreach (var item in listBox_Sprites.SelectedItems)
                if (modelSettings.Any(x => x.Name.Equals(item.ToString())))
                    modelSettings.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).Scale = txt_Scale.Text.ToVector3(",", " ");
        }

        private void Mode_Changed(object sender, EventArgs e)
        {
            if (listBox_Sprites.SelectedIndex == -1)
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

            var rslt = str
            .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse)
            .ToArray()
            ;

            if (rslt.Length != 4)
                throw new FormatException("The input string does not follow" +
                                            "the required format for the string." +
                                            "There has to be four floats inside" +
                                            "the string delimited by one of the" +
                                            "requested delimiters. input string: " +
                                            str);
            return new Vector4(rslt[0], rslt[1], rslt[2], rslt[3]);
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

            var rslt = str
            .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse)
            .ToArray()
            ;

            if (rslt.Length != 3)
                throw new FormatException("The input string does not follow" +
                                            "the required format for the string." +
                                            "There has to be three floats inside" +
                                            "the string delimited by one of the" +
                                            "requested delimiters. input string: " +
                                            str);
            return new Vector3(rslt[0], rslt[1], rslt[2]);
        }
    }
}
