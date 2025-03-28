using DarkUI.Controls;
using DarkUI.Forms;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
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
            numUpDwn.ValueChanged += FieldValue_Changed;
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
                numUpDwn.ValueChanged += FieldValue_Changed;
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
                    case "DistanceFromScreen":
                        particle.DistanceFromScreen = value;
                        break;
                }

            }
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
                case "DistanceFromScreen":
                    numUpDwn.Value = Convert.ToDecimal(particle.DistanceFromScreen);
                    break;
            }
        }

    }
}
