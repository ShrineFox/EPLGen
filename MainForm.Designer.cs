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
            this.tlp_ParticleSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ParticleRot = new DarkUI.Controls.DarkLabel();
            this.lbl_ParticleScale = new DarkUI.Controls.DarkLabel();
            this.lbl_ParticleTranslation = new DarkUI.Controls.DarkLabel();
            this.listBox_Sprites = new System.Windows.Forms.ListBox();
            this.darkContextMenu_Sprites = new DarkUI.Controls.DarkContextMenu();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_ModelSettings = new System.Windows.Forms.GroupBox();
            this.tlp_ModelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Rotation = new DarkUI.Controls.DarkLabel();
            this.lbl_Scale = new DarkUI.Controls.DarkLabel();
            this.lbl_Mode = new DarkUI.Controls.DarkLabel();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.groupBox_Texture = new DarkUI.Controls.DarkGroupBox();
            this.pictureBox_Tex = new System.Windows.Forms.PictureBox();
            this.darkContextMenu_Texture = new DarkUI.Controls.DarkContextMenu();
            this.chooseImageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkMenuStrip_MainMenu = new DarkUI.Controls.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_Main.SuspendLayout();
            this.groupBox_ParticleSettings.SuspendLayout();
            this.tlp_ParticleSettings.SuspendLayout();
            this.darkContextMenu_Sprites.SuspendLayout();
            this.groupBox_ModelSettings.SuspendLayout();
            this.tlp_ModelSettings.SuspendLayout();
            this.groupBox_Texture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tex)).BeginInit();
            this.darkContextMenu_Texture.SuspendLayout();
            this.darkMenuStrip_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.groupBox_ParticleSettings, 1, 1);
            this.tlp_Main.Controls.Add(this.listBox_Sprites, 0, 0);
            this.tlp_Main.Controls.Add(this.groupBox_ModelSettings, 0, 1);
            this.tlp_Main.Controls.Add(this.groupBox_Texture, 1, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 28);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(800, 422);
            this.tlp_Main.TabIndex = 0;
            // 
            // groupBox_ParticleSettings
            // 
            this.groupBox_ParticleSettings.Controls.Add(this.tlp_ParticleSettings);
            this.groupBox_ParticleSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ParticleSettings.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox_ParticleSettings.Location = new System.Drawing.Point(403, 214);
            this.groupBox_ParticleSettings.Name = "groupBox_ParticleSettings";
            this.groupBox_ParticleSettings.Size = new System.Drawing.Size(394, 205);
            this.groupBox_ParticleSettings.TabIndex = 3;
            this.groupBox_ParticleSettings.TabStop = false;
            this.groupBox_ParticleSettings.Text = "Particle Settings";
            // 
            // tlp_ParticleSettings
            // 
            this.tlp_ParticleSettings.ColumnCount = 2;
            this.tlp_ParticleSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_ParticleSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp_ParticleSettings.Controls.Add(this.lbl_ParticleRot, 0, 0);
            this.tlp_ParticleSettings.Controls.Add(this.lbl_ParticleScale, 0, 1);
            this.tlp_ParticleSettings.Controls.Add(this.lbl_ParticleTranslation, 0, 2);
            this.tlp_ParticleSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_ParticleSettings.Location = new System.Drawing.Point(3, 18);
            this.tlp_ParticleSettings.Name = "tlp_ParticleSettings";
            this.tlp_ParticleSettings.RowCount = 4;
            this.tlp_ParticleSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_ParticleSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_ParticleSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_ParticleSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_ParticleSettings.Size = new System.Drawing.Size(388, 184);
            this.tlp_ParticleSettings.TabIndex = 0;
            // 
            // lbl_ParticleRot
            // 
            this.lbl_ParticleRot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleRot.AutoSize = true;
            this.lbl_ParticleRot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleRot.Location = new System.Drawing.Point(5, 19);
            this.lbl_ParticleRot.Name = "lbl_ParticleRot";
            this.lbl_ParticleRot.Size = new System.Drawing.Size(108, 16);
            this.lbl_ParticleRot.TabIndex = 0;
            this.lbl_ParticleRot.Text = "Particle Rotation:";
            // 
            // lbl_ParticleScale
            // 
            this.lbl_ParticleScale.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleScale.AutoSize = true;
            this.lbl_ParticleScale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleScale.Location = new System.Drawing.Point(20, 74);
            this.lbl_ParticleScale.Name = "lbl_ParticleScale";
            this.lbl_ParticleScale.Size = new System.Drawing.Size(93, 16);
            this.lbl_ParticleScale.TabIndex = 2;
            this.lbl_ParticleScale.Text = "Particle Scale:";
            // 
            // lbl_ParticleTranslation
            // 
            this.lbl_ParticleTranslation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ParticleTranslation.AutoSize = true;
            this.lbl_ParticleTranslation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_ParticleTranslation.Location = new System.Drawing.Point(36, 121);
            this.lbl_ParticleTranslation.Name = "lbl_ParticleTranslation";
            this.lbl_ParticleTranslation.Size = new System.Drawing.Size(77, 32);
            this.lbl_ParticleTranslation.TabIndex = 4;
            this.lbl_ParticleTranslation.Text = "Particle Translation:";
            // 
            // listBox_Sprites
            // 
            this.listBox_Sprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox_Sprites.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Sprites.ContextMenuStrip = this.darkContextMenu_Sprites;
            this.listBox_Sprites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Sprites.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.listBox_Sprites.FormattingEnabled = true;
            this.listBox_Sprites.ItemHeight = 16;
            this.listBox_Sprites.Location = new System.Drawing.Point(3, 3);
            this.listBox_Sprites.Name = "listBox_Sprites";
            this.listBox_Sprites.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Sprites.Size = new System.Drawing.Size(394, 205);
            this.listBox_Sprites.TabIndex = 1;
            this.listBox_Sprites.SelectedIndexChanged += new System.EventHandler(this.SpriteList_IndexChanged);
            // 
            // darkContextMenu_Sprites
            // 
            this.darkContextMenu_Sprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkContextMenu_Sprites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkContextMenu_Sprites.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.darkContextMenu_Sprites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem,
            this.removeSelectedToolStripMenuItem,
            this.renameSelectedToolStripMenuItem});
            this.darkContextMenu_Sprites.Name = "darkContextMenu_Sprites";
            this.darkContextMenu_Sprites.Size = new System.Drawing.Size(194, 76);
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.addSpriteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.addSpriteToolStripMenuItem.Text = "Add Sprite";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.AddSprite_Click);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.removeSelectedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // renameSelectedToolStripMenuItem
            // 
            this.renameSelectedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.renameSelectedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.renameSelectedToolStripMenuItem.Name = "renameSelectedToolStripMenuItem";
            this.renameSelectedToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.renameSelectedToolStripMenuItem.Text = "Rename Selected";
            this.renameSelectedToolStripMenuItem.Click += new System.EventHandler(this.Rename_Click);
            // 
            // groupBox_ModelSettings
            // 
            this.groupBox_ModelSettings.Controls.Add(this.tlp_ModelSettings);
            this.groupBox_ModelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ModelSettings.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox_ModelSettings.Location = new System.Drawing.Point(3, 214);
            this.groupBox_ModelSettings.Name = "groupBox_ModelSettings";
            this.groupBox_ModelSettings.Size = new System.Drawing.Size(394, 205);
            this.groupBox_ModelSettings.TabIndex = 2;
            this.groupBox_ModelSettings.TabStop = false;
            this.groupBox_ModelSettings.Text = "Model Settings";
            // 
            // tlp_ModelSettings
            // 
            this.tlp_ModelSettings.ColumnCount = 2;
            this.tlp_ModelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_ModelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp_ModelSettings.Controls.Add(this.lbl_Rotation, 0, 0);
            this.tlp_ModelSettings.Controls.Add(this.lbl_Scale, 0, 1);
            this.tlp_ModelSettings.Controls.Add(this.lbl_Mode, 0, 2);
            this.tlp_ModelSettings.Controls.Add(this.comboBox_Mode, 1, 2);
            this.tlp_ModelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_ModelSettings.Location = new System.Drawing.Point(3, 18);
            this.tlp_ModelSettings.Name = "tlp_ModelSettings";
            this.tlp_ModelSettings.RowCount = 3;
            this.tlp_ModelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tlp_ModelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tlp_ModelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp_ModelSettings.Size = new System.Drawing.Size(388, 184);
            this.tlp_ModelSettings.TabIndex = 0;
            // 
            // lbl_Rotation
            // 
            this.lbl_Rotation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Rotation.AutoSize = true;
            this.lbl_Rotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Rotation.Location = new System.Drawing.Point(12, 30);
            this.lbl_Rotation.Name = "lbl_Rotation";
            this.lbl_Rotation.Size = new System.Drawing.Size(101, 16);
            this.lbl_Rotation.TabIndex = 0;
            this.lbl_Rotation.Text = "Model Rotation:";
            // 
            // lbl_Scale
            // 
            this.lbl_Scale.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Scale.AutoSize = true;
            this.lbl_Scale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Scale.Location = new System.Drawing.Point(27, 98);
            this.lbl_Scale.Name = "lbl_Scale";
            this.lbl_Scale.Size = new System.Drawing.Size(86, 16);
            this.lbl_Scale.TabIndex = 2;
            this.lbl_Scale.Text = "Model Scale:";
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Mode.AutoSize = true;
            this.lbl_Mode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Mode.Location = new System.Drawing.Point(20, 152);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(93, 16);
            this.lbl_Mode.TabIndex = 4;
            this.lbl_Mode.Text = "Particle Mode:";
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
            this.comboBox_Mode.Location = new System.Drawing.Point(119, 148);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(148, 24);
            this.comboBox_Mode.TabIndex = 5;
            // 
            // groupBox_Texture
            // 
            this.groupBox_Texture.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.groupBox_Texture.Controls.Add(this.pictureBox_Tex);
            this.groupBox_Texture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Texture.Location = new System.Drawing.Point(403, 3);
            this.groupBox_Texture.Name = "groupBox_Texture";
            this.groupBox_Texture.Size = new System.Drawing.Size(394, 205);
            this.groupBox_Texture.TabIndex = 4;
            this.groupBox_Texture.TabStop = false;
            this.groupBox_Texture.Text = "Sprite Texture";
            // 
            // pictureBox_Tex
            // 
            this.pictureBox_Tex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.pictureBox_Tex.ContextMenuStrip = this.darkContextMenu_Texture;
            this.pictureBox_Tex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Tex.Location = new System.Drawing.Point(3, 18);
            this.pictureBox_Tex.Name = "pictureBox_Tex";
            this.pictureBox_Tex.Size = new System.Drawing.Size(388, 184);
            this.pictureBox_Tex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Tex.TabIndex = 1;
            this.pictureBox_Tex.TabStop = false;
            // 
            // darkContextMenu_Texture
            // 
            this.darkContextMenu_Texture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkContextMenu_Texture.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkContextMenu_Texture.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.darkContextMenu_Texture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseImageFileToolStripMenuItem});
            this.darkContextMenu_Texture.Name = "darkContextMenu_Texture";
            this.darkContextMenu_Texture.Size = new System.Drawing.Size(210, 28);
            // 
            // chooseImageFileToolStripMenuItem
            // 
            this.chooseImageFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chooseImageFileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.chooseImageFileToolStripMenuItem.Name = "chooseImageFileToolStripMenuItem";
            this.chooseImageFileToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.chooseImageFileToolStripMenuItem.Text = "Choose Image File...";
            this.chooseImageFileToolStripMenuItem.Click += new System.EventHandler(this.ChooseImageFile_Click);
            // 
            // darkMenuStrip_MainMenu
            // 
            this.darkMenuStrip_MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkMenuStrip_MainMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkMenuStrip_MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.darkMenuStrip_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.setImageToolStripMenuItem});
            this.darkMenuStrip_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.darkMenuStrip_MainMenu.Name = "darkMenuStrip_MainMenu";
            this.darkMenuStrip_MainMenu.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.darkMenuStrip_MainMenu.Size = new System.Drawing.Size(800, 28);
            this.darkMenuStrip_MainMenu.TabIndex = 1;
            this.darkMenuStrip_MainMenu.Text = "darkMenuStrip1";
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
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.loadToolStripMenuItem.Text = "Load Preset";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.saveToolStripMenuItem.Text = "Save Preset";
            // 
            // exportEPLToolStripMenuItem
            // 
            this.exportEPLToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.exportEPLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.exportEPLToolStripMenuItem.Name = "exportEPLToolStripMenuItem";
            this.exportEPLToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.exportEPLToolStripMenuItem.Text = "Export EPL";
            this.exportEPLToolStripMenuItem.Click += new System.EventHandler(this.ExportEPL_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.addToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddSprite_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.removeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.renameToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.Rename_Click);
            // 
            // setImageToolStripMenuItem
            // 
            this.setImageToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.setImageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.setImageToolStripMenuItem.Name = "setImageToolStripMenuItem";
            this.setImageToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.setImageToolStripMenuItem.Text = "Set Image";
            this.setImageToolStripMenuItem.Click += new System.EventHandler(this.ChooseImageFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.darkMenuStrip_MainMenu);
            this.MainMenuStrip = this.darkMenuStrip_MainMenu;
            this.Name = "MainForm";
            this.Text = "EPLGen";
            this.tlp_Main.ResumeLayout(false);
            this.groupBox_ParticleSettings.ResumeLayout(false);
            this.tlp_ParticleSettings.ResumeLayout(false);
            this.tlp_ParticleSettings.PerformLayout();
            this.darkContextMenu_Sprites.ResumeLayout(false);
            this.groupBox_ModelSettings.ResumeLayout(false);
            this.tlp_ModelSettings.ResumeLayout(false);
            this.tlp_ModelSettings.PerformLayout();
            this.groupBox_Texture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tex)).EndInit();
            this.darkContextMenu_Texture.ResumeLayout(false);
            this.darkMenuStrip_MainMenu.ResumeLayout(false);
            this.darkMenuStrip_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.ListBox listBox_Sprites;
        private System.Windows.Forms.GroupBox groupBox_ModelSettings;
        private System.Windows.Forms.TableLayoutPanel tlp_ModelSettings;
        private DarkUI.Controls.DarkLabel lbl_Rotation;
        private DarkUI.Controls.DarkLabel lbl_Scale;
        private DarkUI.Controls.DarkLabel lbl_Mode;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.GroupBox groupBox_ParticleSettings;
        private System.Windows.Forms.TableLayoutPanel tlp_ParticleSettings;
        private DarkUI.Controls.DarkLabel lbl_ParticleRot;
        private DarkUI.Controls.DarkLabel lbl_ParticleScale;
        private DarkUI.Controls.DarkLabel lbl_ParticleTranslation;
        private DarkUI.Controls.DarkMenuStrip darkMenuStrip_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEPLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameSelectedToolStripMenuItem;
        private DarkUI.Controls.DarkContextMenu darkContextMenu_Sprites;
        private DarkUI.Controls.DarkGroupBox groupBox_Texture;
        private System.Windows.Forms.PictureBox pictureBox_Tex;
        private DarkUI.Controls.DarkContextMenu darkContextMenu_Texture;
        private System.Windows.Forms.ToolStripMenuItem chooseImageFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setImageToolStripMenuItem;
    }
}

