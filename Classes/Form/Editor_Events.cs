using DarkUI.Controls;
using DarkUI.Forms;
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
        private void SavePreset_Click(object sender, EventArgs e)
        {
            var selection = WinFormsDialogs.SelectFile("Save preset file...", true, new string[] { "json (.json)" }, true);
            if (selection.Count == 0)
                return;

            string outPath = selection.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath = $"{outPath}.json";

            SaveJson(outPath);
            MessageBox.Show($"Saved preset file to:\n{outPath}", "Preset Saved Successfully");
        }

        private void LoadPreset_Click(object sender, EventArgs e)
        {
            var selection = WinFormsDialogs.SelectFile("Load preset file...", true, new string[] { "json (.json)" });
            if (selection.Count == 0 || !File.Exists(selection.First()))
                return;

            LoadJson(selection.First());
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var file in fileList.OrderBy(Path.GetFileName).Reverse().Where(x => x.ToLower().EndsWith(".dds") || x.ToLower().EndsWith(".gmd")))
                AddParticle(new Particle() { Name = Path.GetFileNameWithoutExtension(file), TexturePath = file });

            UpdateSpriteList();
        }

        private void DragDrop_Enter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void GMD_Changed(object sender, EventArgs e)
        {
            userSettings.GMD = gmds[toolStripComboBox_GMD.ComboBox.SelectedIndex];
        }


        private void FieldValue_Changed(object sender, EventArgs e)
        {
            DarkNumericUpDown numUpDwn = (DarkNumericUpDown)sender;
            UpdateOptionValue(numUpDwn.Name.Split('_')[1], numUpDwn.Name.Split('_')[2], (float)numUpDwn.Value);
        }

        private void AddSprite_Click(object sender, EventArgs e)
        {
            var files = WinFormsDialogs.SelectFile("Choose DDS Textures", true, new string[] { "DDS Texture (.DDS)", "GMD Model (.GMD)" });

            if (files.Count <= 0)
                return;

            foreach (var file in files)
                AddParticle(new Particle() { Name = Path.GetFileNameWithoutExtension(file), TexturePath = file });

            UpdateSpriteList();
        }

        private void AddParticle(Particle particle = null, int index = 0)
        {
            if (particle == null)
                particle = new Particle() { };

            int i = 1;
            string name = particle.Name.Copy();
            while (userSettings.Particles.Any(x => x.Name.Equals(particle.Name)))
            {
                i++;
                particle.Name = name + i;
            }

            userSettings.Particles.Insert(index, particle);
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
                listBox_Sprites.Items.Add(item); // Add Particle objects directly
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

        private void LoadTexturePreview(string texPath = "")
        {
            if (!String.IsNullOrEmpty(texPath) && File.Exists(texPath) && Path.GetExtension(texPath).ToLower() == ".dds")
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

        private void CopyParams_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox_Sprites.SelectedItems)
                if (userSettings.Particles.Any(x => x.Name.Equals(item.ToString())))
                    copiedParams = userSettings.Particles.First(x => x.Name.Equals(item.ToString())).Copy();
        }

        private void PasteParams_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox_Sprites.SelectedItems)
                if (userSettings.Particles.Any(x => x.Name.Equals(item.ToString())))
                {
                    var copy = copiedParams.Copy();
                    var particle = userSettings.Particles.First(x => x.Name.Equals(item.ToString()));

                    particle.SpawnChoker = copy.SpawnChoker.Copy();
                    particle.SpawnerAngles = copy.SpawnerAngles.Copy();
                    particle.Translation = copy.Translation.Copy();
                    particle.Rotation = copy.Rotation.Copy();
                    particle.Scale = copy.Scale.Copy();
                    particle.ParticleSpeed = copy.ParticleSpeed.Copy();
                    particle.RandomSpawnDelay = copy.RandomSpawnDelay.Copy();
                    particle.ParticleLife = copy.ParticleLife.Copy();
                    particle.DespawnTimer = copy.DespawnTimer.Copy();
                    particle.Field170 = copy.Field170.Copy();
                    particle.Field188 = copy.Field188.Copy();
                    particle.Field178 = copy.Field178.Copy();
                    particle.Field180 = copy.Field180.Copy();
                    particle.Field190 = copy.Field190.Copy();
                    particle.DistanceFromScreen = copy.DistanceFromScreen.Copy();
                }


            UpdateSpriteList();
        }

        private void CopyParticles_Click(object sender, EventArgs e)
        {
            copiedParticles.Clear();

            foreach (var item in listBox_Sprites.SelectedItems)
                if (userSettings.Particles.Any(x => x.Name.Equals(item.ToString())))
                    copiedParticles.Add(userSettings.Particles.First(x => x.Name.Equals(item.ToString())).Copy());

            copiedParticles.Reverse();
        }

        private void PasteParticles_Click(object sender, EventArgs e)
        {
            int index = 0;

            foreach (var item in listBox_Sprites.SelectedItems)
            {
                if (userSettings.Particles.Any(x => x.Name.Equals(item.ToString())))
                {
                    var particle = userSettings.Particles.First(x => x.Name.Equals(item.ToString()));
                    index = userSettings.Particles.IndexOf(particle);
                    userSettings.Particles.Remove(particle);
                }
            }

            foreach (var particle in copiedParticles)
                AddParticle(particle.Copy(), index);

            UpdateSpriteList();
        }

        private void PasteNew_Click(object sender, EventArgs e)
        {
            foreach (var particle in copiedParticles)
                AddParticle(particle.Copy(), 0);

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
                    settings.Rotation = new Quaternion(-0.7071068f, 0f, 0f, 0.7071068f);
                    foreach (var particle in settings.Particles)
                    {
                        particle.SpawnerAngles = new Vector2(90f, 0f);
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
                List<string> texPaths = WinFormsDialogs.SelectFile("Choose sprite texture", true, new string[] { "DDS Image (.dds)", "GMD Model (.gmd)" });
                for (int i = 0; i < texPaths.Count; i++)
                {
                    // Update texture path for model object matching selected listbox item, otherwise create new one named after file
                    if (i == 0)
                        userSettings.Particles.First(x => x.Name.Equals(listBox_Sprites.SelectedItem.ToString())).TexturePath = texPaths[i];
                    else
                    {
                        AddParticle(new Particle() { Name = Path.GetFileNameWithoutExtension(texPaths[i]), TexturePath = texPaths[i] });
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
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                CopyParticles_Click(sender, e);
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                PasteParticles_Click(sender, e);
            else if (e.Modifiers == (Keys.Control | Keys.Alt) && e.KeyCode == Keys.V)
                PasteNew_Click(sender, e);
            else if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.V)
                PasteParams_Click(sender, e);
            else if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.C)
                CopyParams_Click(sender, e);
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Up)
                MoveSelectedItem(-1);
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Down)
                MoveSelectedItem(1);
        }

        private void MoveSelectedItem(int direction)
        {
            if (listBox_Sprites.SelectedItem == null || listBox_Sprites.SelectedIndex < 0)
                return;

            // Calculate new index
            int index = listBox_Sprites.SelectedIndex;
            int newIndex = index + direction;

            // Ensure new index is valid
            if (newIndex < 0 || newIndex >= listBox_Sprites.Items.Count)
                return;

            object selected = listBox_Sprites.SelectedItem;

            userSettings.Particles.RemoveAt(listBox_Sprites.SelectedIndex);
            userSettings.Particles.Insert(newIndex, (Particle)selected);

            UpdateSpriteList();

            listBox_Sprites.SetSelected(index, true);
            listBox_Sprites.Focus();
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
