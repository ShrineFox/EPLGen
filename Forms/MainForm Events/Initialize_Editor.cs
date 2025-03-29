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
            AddNumControls(tlp_ParticleSettings, "Distance From Screen:", "DistanceFromScreen", 14, false);
        }

        private void SetOptionValue(string name, string axis, float value, Particle particle)
        {
            switch (name)
            {
                case "ModelRot":
                    userSettings.Rotation = UpdateQuaternion(userSettings.Rotation, axis, value);
                    break;
                case "ParticleRot":
                    particle.Rotation = UpdateQuaternion(particle.Rotation, axis, value);
                    break;
                case "ModelScale":
                    userSettings.Scale = UpdateVector3(userSettings.Scale, axis, value);
                    break;
                case "ParticleScale":
                    particle.Scale = UpdateVector3(particle.Scale, axis, value);
                    break;
                case "ModelTranslation":
                    userSettings.Translation = UpdateVector3(userSettings.Translation, axis, value);
                    break;
                case "ParticleTranslation":
                    particle.Translation = UpdateVector3(particle.Translation, axis, value);
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
                    particle.SpawnChoker = UpdateVector2(particle.SpawnChoker, axis, value);
                    break;
                case "SpawnerAngles":
                    particle.SpawnerAngles = UpdateVector2(particle.SpawnerAngles, axis, value);
                    break;
                case "Field170":
                    particle.Field170 = UpdateVector2(particle.Field170, axis, value);
                    break;
                case "Field188":
                    particle.Field188 = UpdateVector2(particle.Field188, axis, value);
                    break;
                case "Field178":
                    particle.Field178 = UpdateVector2(particle.Field178, axis, value);
                    break;
                case "Field180":
                    particle.Field180 = UpdateVector2(particle.Field180, axis, value);
                    break;
                case "Field190":
                    particle.Field190 = UpdateVector2(particle.Field190, axis, value);
                    break;
                case "DistanceFromScreen":
                    particle.DistanceFromScreen = value;
                    break;
            }
        }

        private Quaternion UpdateQuaternion(Quaternion q, string axis, float value)
        {
            return axis switch
            {
                "x" => new Quaternion(value, q.Y, q.Z, q.W),
                "y" => new Quaternion(q.X, value, q.Z, q.W),
                "z" => new Quaternion(q.X, q.Y, value, q.W),
                "w" => new Quaternion(q.X, q.Y, q.Z, value),
                _ => q,
            };
        }


        private Vector3 UpdateVector3(Vector3 v, string axis, float value)
        {
            return axis switch
            {
                "x" => new Vector3(value, v.Y, v.Z),
                "y" => new Vector3(v.X, value, v.Z),
                "z" => new Vector3(v.X, v.Y, value),
                _ => v,
            };
        }

        private Vector2 UpdateVector2(Vector2 v, string axis, float value)
        {
            return axis switch
            {
                "x" => new Vector2(value, v.Y),
                "y" => new Vector2(v.X, value),
                _ => v,
            };
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

                foreach (var ctrl in new string[] { "ParticleSpeed", "RandomSpawnDelay", "ParticleLife", "DespawnTimer", "DistanceFromScreen" })
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
                    SetNumericUpDownValue(numUpDwn, userSettings.Rotation, axis);
                    break;
                case "ParticleRot":
                    SetNumericUpDownValue(numUpDwn, particle.Rotation, axis);
                    break;
                case "ModelScale":
                    SetNumericUpDownValue(numUpDwn, userSettings.Scale, axis);
                    break;
                case "ParticleScale":
                    SetNumericUpDownValue(numUpDwn, particle.Scale, axis);
                    break;
                case "ModelTranslation":
                    SetNumericUpDownValue(numUpDwn, userSettings.Translation, axis);
                    break;
                case "ParticleTranslation":
                    SetNumericUpDownValue(numUpDwn, particle.Translation, axis);
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
                    SetNumericUpDownValue(numUpDwn, particle.SpawnChoker, axis);
                    break;
                case "SpawnerAngles":
                    SetNumericUpDownValue(numUpDwn, particle.SpawnerAngles, axis);
                    break;
                case "Field170":
                    SetNumericUpDownValue(numUpDwn, particle.Field170, axis);
                    break;
                case "Field188":
                    SetNumericUpDownValue(numUpDwn, particle.Field188, axis);
                    break;
                case "Field178":
                    SetNumericUpDownValue(numUpDwn, particle.Field178, axis);
                    break;
                case "Field180":
                    SetNumericUpDownValue(numUpDwn, particle.Field180, axis);
                    break;
                case "Field190":
                    SetNumericUpDownValue(numUpDwn, particle.Field190, axis);
                    break;
                case "DistanceFromScreen":
                    numUpDwn.Value = Convert.ToDecimal(particle.DistanceFromScreen);
                    break;
            }
        }

        private DarkNumericUpDown CreateNumericUpDown(string name, decimal min, decimal max, bool decimalPlaces = true)
        {
            var numUpDwn = new DarkNumericUpDown()
            {
                Minimum = min,
                Maximum = max,
                Name = name,
                Anchor = (AnchorStyles.Left | AnchorStyles.Right),
                AutoSize = true
            };
            numUpDwn.ValueChanged += FieldValue_Changed;
            if (!decimalPlaces)
                numUpDwn.DecimalPlaces = 7;
            return numUpDwn;
        }

        private void AddNumControls(TableLayoutPanel parentTlp, string labelText, string fieldPrefix, int row, bool decimalPlaces = true)
        {
            var numUpDwn = CreateNumericUpDown($"num_{fieldPrefix}_x", -999999, 999999, decimalPlaces);
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

                var numUpDwn = CreateNumericUpDown($"num_{fieldPrefix}_{axis}", maxMin * -1, maxMin);
                tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Percent, Height = 50f });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Percent, Width = columnWidth });
                tlp.Controls.Add(new DarkLabel() { Text = axis.ToUpper(), Dock = DockStyle.Bottom }, i, 0);
                tlp.Controls.Add(numUpDwn, i, 1);
            }
            parentTlp.Controls.Add(new DarkLabel() { Text = labelText, Anchor = AnchorStyles.Right, AutoSize = true }, 0, row);
            parentTlp.Controls.Add(tlp, 1, row);
        }

        private void UpdateOptionValue(string name, string axis, float value)
        {
            foreach (var item in listBox_Sprites.SelectedItems)
            {
                var particle = userSettings.Particles.First(x => x.Name.Equals(item.ToString()));
                SetOptionValue(name, axis, value, particle);
            }
        }

        private void SetNumericUpDownValue(DarkNumericUpDown numUpDwn, Quaternion rotation, string axis)
        {
            switch (axis)
            {
                case "x":
                    numUpDwn.Value = Convert.ToDecimal(rotation.X);
                    break;
                case "y":
                    numUpDwn.Value = Convert.ToDecimal(rotation.Y);
                    break;
                case "z":
                    numUpDwn.Value = Convert.ToDecimal(rotation.Z);
                    break;
                case "w":
                    numUpDwn.Value = Convert.ToDecimal(rotation.W);
                    break;
            }
        }

        private void SetNumericUpDownValue(DarkNumericUpDown numUpDwn, Vector3 vector, string axis)
        {
            switch (axis)
            {
                case "x":
                    numUpDwn.Value = Convert.ToDecimal(vector.X);
                    break;
                case "y":
                    numUpDwn.Value = Convert.ToDecimal(vector.Y);
                    break;
                case "z":
                    numUpDwn.Value = Convert.ToDecimal(vector.Z);
                    break;
            }
        }

        private void SetNumericUpDownValue(DarkNumericUpDown numUpDwn, Vector2 vector, string axis)
        {
            switch (axis)
            {
                case "x":
                    numUpDwn.Value = Convert.ToDecimal(vector.X);
                    break;
                case "y":
                    numUpDwn.Value = Convert.ToDecimal(vector.Y);
                    break;
            }
        }
    }
}
