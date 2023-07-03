using DarkUI.Forms;

namespace EPLGen
{
    partial class MainForm : DarkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_ParticleSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_ParticleTranslation = new DarkUI.Controls.DarkTextBox();
            this.lbl_ParticleRot = new DarkUI.Controls.DarkLabel();
            this.txt_ParticleRot = new DarkUI.Controls.DarkTextBox();
            this.lbl_ParticleScale = new DarkUI.Controls.DarkLabel();
            this.txt_ParticleScale = new DarkUI.Controls.DarkTextBox();
            this.lbl_ParticleTranslation = new DarkUI.Controls.DarkLabel();
            this.pictureBox_Tex = new System.Windows.Forms.PictureBox();
            this.listBox_Sprites = new System.Windows.Forms.ListBox();
            this.groupBox_ModelSettings = new System.Windows.Forms.GroupBox();
            this.tlp_Settings = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Rotation = new DarkUI.Controls.DarkLabel();
            this.txt_Rotation = new DarkUI.Controls.DarkTextBox();
            this.lbl_Scale = new DarkUI.Controls.DarkLabel();
            this.txt_Scale = new DarkUI.Controls.DarkTextBox();
            this.lbl_Mode = new DarkUI.Controls.DarkLabel();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.darkMenuStrip1 = new DarkUI.Controls.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_Main.SuspendLayout();
            this.groupBox_ParticleSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tex)).BeginInit();
            this.groupBox_ModelSettings.SuspendLayout();
            this.tlp_Settings.SuspendLayout();
            this.darkMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.groupBox_ParticleSettings, 1, 1);
            this.tlp_Main.Controls.Add(this.pictureBox_Tex, 1, 0);
            this.tlp_Main.Controls.Add(this.listBox_Sprites, 0, 0);
            this.tlp_Main.Controls.Add(this.groupBox_ModelSettings, 0, 1);
            this.tlp_Main.Location = new System.Drawing.Point(12, 27);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(776, 411);
            this.tlp_Main.TabIndex = 0;
            // 
            // groupBox_ParticleSettings
            // 
            this.groupBox_ParticleSettings.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_ParticleSettings.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox_ParticleSettings.Location = new System.Drawing.Point(391, 208);
            this.groupBox_ParticleSettings.Name = "groupBox_ParticleSettings";
            this.groupBox_ParticleSettings.Size = new System.Drawing.Size(382, 200);
            this.groupBox_ParticleSettings.TabIndex = 3;
            this.groupBox_ParticleSettings.TabStop = false;
            this.groupBox_ParticleSettings.Text = "Particle Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.txt_ParticleTranslation, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ParticleRot, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_ParticleRot, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ParticleScale, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_ParticleScale, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ParticleTranslation, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(376, 179);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txt_ParticleTranslation
            // 
            this.txt_ParticleTranslation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_ParticleTranslation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_ParticleTranslation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ParticleTranslation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_ParticleTranslation.Location = new System.Drawing.Point(115, 144);
            this.txt_ParticleTranslation.Name = "txt_ParticleTranslation";
            this.txt_ParticleTranslation.Size = new System.Drawing.Size(148, 22);
            this.txt_ParticleTranslation.TabIndex = 5;
            // 
            // lbl_ParticleRot
            // 
            this.lbl_ParticleRot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleRot.AutoSize = true;
            this.lbl_ParticleRot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleRot.Location = new System.Drawing.Point(49, 28);
            this.lbl_ParticleRot.Name = "lbl_ParticleRot";
            this.lbl_ParticleRot.Size = new System.Drawing.Size(60, 16);
            this.lbl_ParticleRot.TabIndex = 0;
            this.lbl_ParticleRot.Text = "Rotation:";
            // 
            // txt_ParticleRot
            // 
            this.txt_ParticleRot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_ParticleRot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_ParticleRot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ParticleRot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_ParticleRot.Location = new System.Drawing.Point(115, 25);
            this.txt_ParticleRot.Name = "txt_ParticleRot";
            this.txt_ParticleRot.Size = new System.Drawing.Size(148, 22);
            this.txt_ParticleRot.TabIndex = 1;
            // 
            // lbl_ParticleScale
            // 
            this.lbl_ParticleScale.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleScale.AutoSize = true;
            this.lbl_ParticleScale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleScale.Location = new System.Drawing.Point(64, 94);
            this.lbl_ParticleScale.Name = "lbl_ParticleScale";
            this.lbl_ParticleScale.Size = new System.Drawing.Size(45, 16);
            this.lbl_ParticleScale.TabIndex = 2;
            this.lbl_ParticleScale.Text = "Scale:";
            // 
            // txt_ParticleScale
            // 
            this.txt_ParticleScale.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_ParticleScale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_ParticleScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ParticleScale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_ParticleScale.Location = new System.Drawing.Point(115, 91);
            this.txt_ParticleScale.Name = "txt_ParticleScale";
            this.txt_ParticleScale.Size = new System.Drawing.Size(148, 22);
            this.txt_ParticleScale.TabIndex = 3;
            // 
            // lbl_ParticleTranslation
            // 
            this.lbl_ParticleTranslation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleTranslation.AutoSize = true;
            this.lbl_ParticleTranslation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleTranslation.Location = new System.Drawing.Point(32, 147);
            this.lbl_ParticleTranslation.Name = "lbl_ParticleTranslation";
            this.lbl_ParticleTranslation.Size = new System.Drawing.Size(77, 16);
            this.lbl_ParticleTranslation.TabIndex = 4;
            this.lbl_ParticleTranslation.Text = "Translation:";
            // 
            // pictureBox_Tex
            // 
            this.pictureBox_Tex.Location = new System.Drawing.Point(391, 3);
            this.pictureBox_Tex.Name = "pictureBox_Tex";
            this.pictureBox_Tex.Size = new System.Drawing.Size(382, 199);
            this.pictureBox_Tex.TabIndex = 0;
            this.pictureBox_Tex.TabStop = false;
            this.pictureBox_Tex.Click += new System.EventHandler(this.PictureBox_Clicked);
            // 
            // listBox_Sprites
            // 
            this.listBox_Sprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox_Sprites.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Sprites.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.listBox_Sprites.FormattingEnabled = true;
            this.listBox_Sprites.ItemHeight = 16;
            this.listBox_Sprites.Location = new System.Drawing.Point(3, 3);
            this.listBox_Sprites.Name = "listBox_Sprites";
            this.listBox_Sprites.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Sprites.Size = new System.Drawing.Size(382, 192);
            this.listBox_Sprites.TabIndex = 1;
            this.listBox_Sprites.SelectedIndexChanged += new System.EventHandler(this.SpriteList_IndexChanged);
            // 
            // groupBox_ModelSettings
            // 
            this.groupBox_ModelSettings.Controls.Add(this.tlp_Settings);
            this.groupBox_ModelSettings.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox_ModelSettings.Location = new System.Drawing.Point(3, 208);
            this.groupBox_ModelSettings.Name = "groupBox_ModelSettings";
            this.groupBox_ModelSettings.Size = new System.Drawing.Size(382, 200);
            this.groupBox_ModelSettings.TabIndex = 2;
            this.groupBox_ModelSettings.TabStop = false;
            this.groupBox_ModelSettings.Text = "Model Settings";
            // 
            // tlp_Settings
            // 
            this.tlp_Settings.ColumnCount = 2;
            this.tlp_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp_Settings.Controls.Add(this.lbl_Rotation, 0, 0);
            this.tlp_Settings.Controls.Add(this.txt_Rotation, 1, 0);
            this.tlp_Settings.Controls.Add(this.lbl_Scale, 0, 1);
            this.tlp_Settings.Controls.Add(this.txt_Scale, 1, 1);
            this.tlp_Settings.Controls.Add(this.lbl_Mode, 0, 2);
            this.tlp_Settings.Controls.Add(this.comboBox_Mode, 1, 2);
            this.tlp_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Settings.Location = new System.Drawing.Point(3, 18);
            this.tlp_Settings.Name = "tlp_Settings";
            this.tlp_Settings.RowCount = 3;
            this.tlp_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tlp_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tlp_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp_Settings.Size = new System.Drawing.Size(376, 179);
            this.tlp_Settings.TabIndex = 0;
            // 
            // lbl_Rotation
            // 
            this.lbl_Rotation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Rotation.AutoSize = true;
            this.lbl_Rotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Rotation.Location = new System.Drawing.Point(49, 28);
            this.lbl_Rotation.Name = "lbl_Rotation";
            this.lbl_Rotation.Size = new System.Drawing.Size(60, 16);
            this.lbl_Rotation.TabIndex = 0;
            this.lbl_Rotation.Text = "Rotation:";
            // 
            // txt_Rotation
            // 
            this.txt_Rotation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Rotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_Rotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Rotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Rotation.Location = new System.Drawing.Point(115, 25);
            this.txt_Rotation.Name = "txt_Rotation";
            this.txt_Rotation.Size = new System.Drawing.Size(148, 22);
            this.txt_Rotation.TabIndex = 1;
            this.txt_Rotation.TextChanged += new System.EventHandler(this.Rotation_Changed);
            // 
            // lbl_Scale
            // 
            this.lbl_Scale.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Scale.AutoSize = true;
            this.lbl_Scale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Scale.Location = new System.Drawing.Point(64, 94);
            this.lbl_Scale.Name = "lbl_Scale";
            this.lbl_Scale.Size = new System.Drawing.Size(45, 16);
            this.lbl_Scale.TabIndex = 2;
            this.lbl_Scale.Text = "Scale:";
            // 
            // txt_Scale
            // 
            this.txt_Scale.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Scale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Scale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Scale.Location = new System.Drawing.Point(115, 91);
            this.txt_Scale.Name = "txt_Scale";
            this.txt_Scale.Size = new System.Drawing.Size(148, 22);
            this.txt_Scale.TabIndex = 3;
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Mode.AutoSize = true;
            this.lbl_Mode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Mode.Location = new System.Drawing.Point(64, 147);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(45, 16);
            this.lbl_Mode.TabIndex = 4;
            this.lbl_Mode.Text = "Mode:";
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox_Mode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.comboBox_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Mode.ForeColor = System.Drawing.Color.Silver;
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "Cone",
            "Floor"});
            this.comboBox_Mode.Location = new System.Drawing.Point(115, 143);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(148, 24);
            this.comboBox_Mode.TabIndex = 5;
            // 
            // darkMenuStrip1
            // 
            this.darkMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.addSpriteToolStripMenuItem,
            this.removeSelectedToolStripMenuItem,
            this.renameSelectedToolStripMenuItem});
            this.darkMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.darkMenuStrip1.Name = "darkMenuStrip1";
            this.darkMenuStrip1.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.darkMenuStrip1.Size = new System.Drawing.Size(800, 28);
            this.darkMenuStrip1.TabIndex = 1;
            this.darkMenuStrip1.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportEPLToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.loadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exportEPLToolStripMenuItem
            // 
            this.exportEPLToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.exportEPLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.exportEPLToolStripMenuItem.Name = "exportEPLToolStripMenuItem";
            this.exportEPLToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.exportEPLToolStripMenuItem.Text = "Export EPL";
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.addSpriteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.addSpriteToolStripMenuItem.Text = "Add Sprite";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.AddSprite_Click);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.removeSelectedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // renameSelectedToolStripMenuItem
            // 
            this.renameSelectedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.renameSelectedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.renameSelectedToolStripMenuItem.Name = "renameSelectedToolStripMenuItem";
            this.renameSelectedToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.renameSelectedToolStripMenuItem.Text = "Rename Selected";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.darkMenuStrip1);
            this.MainMenuStrip = this.darkMenuStrip1;
            this.Name = "MainForm";
            this.Text = "EPLGen";
            this.tlp_Main.ResumeLayout(false);
            this.groupBox_ParticleSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tex)).EndInit();
            this.groupBox_ModelSettings.ResumeLayout(false);
            this.tlp_Settings.ResumeLayout(false);
            this.tlp_Settings.PerformLayout();
            this.darkMenuStrip1.ResumeLayout(false);
            this.darkMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.PictureBox pictureBox_Tex;
        private System.Windows.Forms.ListBox listBox_Sprites;
        private System.Windows.Forms.GroupBox groupBox_ModelSettings;
        private System.Windows.Forms.TableLayoutPanel tlp_Settings;
        private DarkUI.Controls.DarkLabel lbl_Rotation;
        private DarkUI.Controls.DarkTextBox txt_Rotation;
        private DarkUI.Controls.DarkLabel lbl_Scale;
        private DarkUI.Controls.DarkTextBox txt_Scale;
        private DarkUI.Controls.DarkLabel lbl_Mode;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.GroupBox groupBox_ParticleSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DarkUI.Controls.DarkLabel lbl_ParticleRot;
        private DarkUI.Controls.DarkTextBox txt_ParticleRot;
        private DarkUI.Controls.DarkLabel lbl_ParticleScale;
        private DarkUI.Controls.DarkTextBox txt_ParticleScale;
        private DarkUI.Controls.DarkLabel lbl_ParticleTranslation;
        private DarkUI.Controls.DarkTextBox txt_ParticleTranslation;
        private DarkUI.Controls.DarkMenuStrip darkMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEPLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameSelectedToolStripMenuItem;
    }
}

