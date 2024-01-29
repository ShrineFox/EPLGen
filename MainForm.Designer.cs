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
            tlp_Main = new TableLayoutPanel();
            groupBox_ParticleSettings = new GroupBox();
            panel_ParticleSettings = new Panel();
            tlp_ParticleSettings = new TableLayoutPanel();
            listBox_Sprites = new ListBox();
            darkContextMenu_Sprites = new DarkUI.Controls.DarkContextMenu();
            addSpriteToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedToolStripMenuItem = new ToolStripMenuItem();
            renameSelectedToolStripMenuItem = new ToolStripMenuItem();
            groupBox_ModelSettings = new GroupBox();
            panel_ModelSettings = new Panel();
            tlp_ModelSettings = new TableLayoutPanel();
            lbl_Mode = new DarkUI.Controls.DarkLabel();
            comboBox_Mode = new ComboBox();
            groupBox_Texture = new DarkUI.Controls.DarkGroupBox();
            pictureBox_Tex = new PictureBox();
            darkContextMenu_Texture = new DarkUI.Controls.DarkContextMenu();
            chooseImageFileToolStripMenuItem = new ToolStripMenuItem();
            darkMenuStrip_MainMenu = new DarkUI.Controls.DarkMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exportEPLToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            setImageToolStripMenuItem = new ToolStripMenuItem();
            createEPTsToolStripMenuItem = new ToolStripMenuItem();
            tlp_Main.SuspendLayout();
            groupBox_ParticleSettings.SuspendLayout();
            panel_ParticleSettings.SuspendLayout();
            darkContextMenu_Sprites.SuspendLayout();
            groupBox_ModelSettings.SuspendLayout();
            panel_ModelSettings.SuspendLayout();
            tlp_ModelSettings.SuspendLayout();
            groupBox_Texture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Tex).BeginInit();
            darkContextMenu_Texture.SuspendLayout();
            darkMenuStrip_MainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 2;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Main.Controls.Add(groupBox_ParticleSettings, 1, 1);
            tlp_Main.Controls.Add(listBox_Sprites, 0, 0);
            tlp_Main.Controls.Add(groupBox_ModelSettings, 0, 1);
            tlp_Main.Controls.Add(groupBox_Texture, 1, 0);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 28);
            tlp_Main.Margin = new Padding(3, 4, 3, 4);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 2;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlp_Main.Size = new Size(982, 663);
            tlp_Main.TabIndex = 0;
            // 
            // groupBox_ParticleSettings
            // 
            groupBox_ParticleSettings.Controls.Add(panel_ParticleSettings);
            groupBox_ParticleSettings.Dock = DockStyle.Fill;
            groupBox_ParticleSettings.ForeColor = SystemColors.ScrollBar;
            groupBox_ParticleSettings.Location = new Point(494, 335);
            groupBox_ParticleSettings.Margin = new Padding(3, 4, 3, 4);
            groupBox_ParticleSettings.Name = "groupBox_ParticleSettings";
            groupBox_ParticleSettings.Padding = new Padding(3, 4, 3, 4);
            groupBox_ParticleSettings.Size = new Size(485, 324);
            groupBox_ParticleSettings.TabIndex = 3;
            groupBox_ParticleSettings.TabStop = false;
            groupBox_ParticleSettings.Text = "Sprite Settings";
            // 
            // panel_ParticleSettings
            // 
            panel_ParticleSettings.AutoScroll = true;
            panel_ParticleSettings.AutoScrollMinSize = new Size(1, 1);
            panel_ParticleSettings.Controls.Add(tlp_ParticleSettings);
            panel_ParticleSettings.Dock = DockStyle.Fill;
            panel_ParticleSettings.Location = new Point(3, 24);
            panel_ParticleSettings.Margin = new Padding(3, 4, 3, 4);
            panel_ParticleSettings.Name = "panel_ParticleSettings";
            panel_ParticleSettings.Size = new Size(479, 296);
            panel_ParticleSettings.TabIndex = 0;
            // 
            // tlp_ParticleSettings
            // 
            tlp_ParticleSettings.AutoSize = true;
            tlp_ParticleSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlp_ParticleSettings.ColumnCount = 2;
            tlp_ParticleSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlp_ParticleSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78F));
            tlp_ParticleSettings.Dock = DockStyle.Top;
            tlp_ParticleSettings.Location = new Point(0, 0);
            tlp_ParticleSettings.Margin = new Padding(3, 4, 3, 4);
            tlp_ParticleSettings.Name = "tlp_ParticleSettings";
            tlp_ParticleSettings.RowCount = 14;
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ParticleSettings.Size = new Size(458, 1050);
            tlp_ParticleSettings.TabIndex = 2;
            // 
            // listBox_Sprites
            // 
            listBox_Sprites.BackColor = Color.FromArgb(60, 63, 65);
            listBox_Sprites.BorderStyle = BorderStyle.None;
            listBox_Sprites.ContextMenuStrip = darkContextMenu_Sprites;
            listBox_Sprites.Dock = DockStyle.Fill;
            listBox_Sprites.ForeColor = SystemColors.ScrollBar;
            listBox_Sprites.FormattingEnabled = true;
            listBox_Sprites.ItemHeight = 20;
            listBox_Sprites.Location = new Point(3, 4);
            listBox_Sprites.Margin = new Padding(3, 4, 3, 4);
            listBox_Sprites.Name = "listBox_Sprites";
            listBox_Sprites.SelectionMode = SelectionMode.MultiExtended;
            listBox_Sprites.Size = new Size(485, 323);
            listBox_Sprites.TabIndex = 1;
            listBox_Sprites.SelectedIndexChanged += SpriteList_IndexChanged;
            listBox_Sprites.KeyDown += SpritesList_KeyDown;
            // 
            // darkContextMenu_Sprites
            // 
            darkContextMenu_Sprites.BackColor = Color.FromArgb(60, 63, 65);
            darkContextMenu_Sprites.ForeColor = Color.FromArgb(220, 220, 220);
            darkContextMenu_Sprites.ImageScalingSize = new Size(20, 20);
            darkContextMenu_Sprites.Items.AddRange(new ToolStripItem[] { addSpriteToolStripMenuItem, removeSelectedToolStripMenuItem, renameSelectedToolStripMenuItem });
            darkContextMenu_Sprites.Name = "darkContextMenu_Sprites";
            darkContextMenu_Sprites.Size = new Size(194, 76);
            // 
            // addSpriteToolStripMenuItem
            // 
            addSpriteToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addSpriteToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            addSpriteToolStripMenuItem.Size = new Size(193, 24);
            addSpriteToolStripMenuItem.Text = "Add Sprite";
            addSpriteToolStripMenuItem.Click += AddSprite_Click;
            // 
            // removeSelectedToolStripMenuItem
            // 
            removeSelectedToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            removeSelectedToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            removeSelectedToolStripMenuItem.Size = new Size(193, 24);
            removeSelectedToolStripMenuItem.Text = "Remove Selected";
            removeSelectedToolStripMenuItem.Click += RemoveSelected_Click;
            // 
            // renameSelectedToolStripMenuItem
            // 
            renameSelectedToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            renameSelectedToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            renameSelectedToolStripMenuItem.Name = "renameSelectedToolStripMenuItem";
            renameSelectedToolStripMenuItem.Size = new Size(193, 24);
            renameSelectedToolStripMenuItem.Text = "Rename Selected";
            renameSelectedToolStripMenuItem.Click += Rename_Click;
            // 
            // groupBox_ModelSettings
            // 
            groupBox_ModelSettings.Controls.Add(panel_ModelSettings);
            groupBox_ModelSettings.Dock = DockStyle.Fill;
            groupBox_ModelSettings.ForeColor = SystemColors.ScrollBar;
            groupBox_ModelSettings.Location = new Point(3, 335);
            groupBox_ModelSettings.Margin = new Padding(3, 4, 3, 4);
            groupBox_ModelSettings.Name = "groupBox_ModelSettings";
            groupBox_ModelSettings.Padding = new Padding(3, 4, 3, 4);
            groupBox_ModelSettings.Size = new Size(485, 324);
            groupBox_ModelSettings.TabIndex = 2;
            groupBox_ModelSettings.TabStop = false;
            groupBox_ModelSettings.Text = "Effect Settings";
            // 
            // panel_ModelSettings
            // 
            panel_ModelSettings.AutoScroll = true;
            panel_ModelSettings.AutoScrollMinSize = new Size(1, 1);
            panel_ModelSettings.Controls.Add(tlp_ModelSettings);
            panel_ModelSettings.Dock = DockStyle.Fill;
            panel_ModelSettings.Location = new Point(3, 24);
            panel_ModelSettings.Margin = new Padding(3, 4, 3, 4);
            panel_ModelSettings.Name = "panel_ModelSettings";
            panel_ModelSettings.Size = new Size(479, 296);
            panel_ModelSettings.TabIndex = 0;
            // 
            // tlp_ModelSettings
            // 
            tlp_ModelSettings.AutoSize = true;
            tlp_ModelSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlp_ModelSettings.ColumnCount = 2;
            tlp_ModelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp_ModelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tlp_ModelSettings.Controls.Add(lbl_Mode, 0, 0);
            tlp_ModelSettings.Controls.Add(comboBox_Mode, 1, 0);
            tlp_ModelSettings.Dock = DockStyle.Top;
            tlp_ModelSettings.Location = new Point(0, 0);
            tlp_ModelSettings.Margin = new Padding(3, 4, 3, 4);
            tlp_ModelSettings.Name = "tlp_ModelSettings";
            tlp_ModelSettings.RowCount = 4;
            tlp_ModelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlp_ModelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ModelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlp_ModelSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_ModelSettings.Size = new Size(479, 200);
            tlp_ModelSettings.TabIndex = 1;
            // 
            // lbl_Mode
            // 
            lbl_Mode.Anchor = AnchorStyles.Right;
            lbl_Mode.AutoSize = true;
            lbl_Mode.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_Mode.Location = new Point(41, 15);
            lbl_Mode.Name = "lbl_Mode";
            lbl_Mode.Size = new Size(51, 20);
            lbl_Mode.TabIndex = 4;
            lbl_Mode.Text = "Mode:";
            // 
            // comboBox_Mode
            // 
            comboBox_Mode.Anchor = AnchorStyles.Left;
            comboBox_Mode.BackColor = Color.FromArgb(60, 60, 60);
            comboBox_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Mode.FlatStyle = FlatStyle.Flat;
            comboBox_Mode.ForeColor = Color.Silver;
            comboBox_Mode.FormattingEnabled = true;
            comboBox_Mode.Items.AddRange(new object[] { "Cone", "Floor" });
            comboBox_Mode.Location = new Point(98, 11);
            comboBox_Mode.Margin = new Padding(3, 4, 3, 4);
            comboBox_Mode.Name = "comboBox_Mode";
            comboBox_Mode.Size = new Size(199, 28);
            comboBox_Mode.TabIndex = 5;
            comboBox_Mode.SelectedIndexChanged += Mode_Changed;
            // 
            // groupBox_Texture
            // 
            groupBox_Texture.BorderColor = Color.FromArgb(60, 63, 65);
            groupBox_Texture.Controls.Add(pictureBox_Tex);
            groupBox_Texture.Dock = DockStyle.Fill;
            groupBox_Texture.Location = new Point(494, 4);
            groupBox_Texture.Margin = new Padding(3, 4, 3, 4);
            groupBox_Texture.Name = "groupBox_Texture";
            groupBox_Texture.Padding = new Padding(3, 4, 3, 4);
            groupBox_Texture.Size = new Size(485, 323);
            groupBox_Texture.TabIndex = 4;
            groupBox_Texture.TabStop = false;
            groupBox_Texture.Text = "Sprite Texture";
            // 
            // pictureBox_Tex
            // 
            pictureBox_Tex.BackColor = Color.FromArgb(50, 53, 55);
            pictureBox_Tex.ContextMenuStrip = darkContextMenu_Texture;
            pictureBox_Tex.Dock = DockStyle.Fill;
            pictureBox_Tex.Location = new Point(3, 24);
            pictureBox_Tex.Margin = new Padding(3, 4, 3, 4);
            pictureBox_Tex.Name = "pictureBox_Tex";
            pictureBox_Tex.Size = new Size(479, 295);
            pictureBox_Tex.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Tex.TabIndex = 1;
            pictureBox_Tex.TabStop = false;
            // 
            // darkContextMenu_Texture
            // 
            darkContextMenu_Texture.BackColor = Color.FromArgb(60, 63, 65);
            darkContextMenu_Texture.ForeColor = Color.FromArgb(220, 220, 220);
            darkContextMenu_Texture.ImageScalingSize = new Size(20, 20);
            darkContextMenu_Texture.Items.AddRange(new ToolStripItem[] { chooseImageFileToolStripMenuItem });
            darkContextMenu_Texture.Name = "darkContextMenu_Texture";
            darkContextMenu_Texture.Size = new Size(210, 28);
            // 
            // chooseImageFileToolStripMenuItem
            // 
            chooseImageFileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            chooseImageFileToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            chooseImageFileToolStripMenuItem.Name = "chooseImageFileToolStripMenuItem";
            chooseImageFileToolStripMenuItem.Size = new Size(209, 24);
            chooseImageFileToolStripMenuItem.Text = "Choose Image File...";
            chooseImageFileToolStripMenuItem.Click += ChooseImageFile_Click;
            // 
            // darkMenuStrip_MainMenu
            // 
            darkMenuStrip_MainMenu.BackColor = Color.FromArgb(60, 63, 65);
            darkMenuStrip_MainMenu.ForeColor = Color.FromArgb(220, 220, 220);
            darkMenuStrip_MainMenu.ImageScalingSize = new Size(20, 20);
            darkMenuStrip_MainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, addToolStripMenuItem, removeToolStripMenuItem, renameToolStripMenuItem, setImageToolStripMenuItem, createEPTsToolStripMenuItem });
            darkMenuStrip_MainMenu.Location = new Point(0, 0);
            darkMenuStrip_MainMenu.Name = "darkMenuStrip_MainMenu";
            darkMenuStrip_MainMenu.Padding = new Padding(3, 2, 0, 2);
            darkMenuStrip_MainMenu.Size = new Size(982, 28);
            darkMenuStrip_MainMenu.TabIndex = 1;
            darkMenuStrip_MainMenu.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadToolStripMenuItem, saveToolStripMenuItem, exportEPLToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            loadToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(169, 26);
            loadToolStripMenuItem.Text = "Load Preset";
            loadToolStripMenuItem.Click += LoadPreset_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            saveToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(169, 26);
            saveToolStripMenuItem.Text = "Save Preset";
            saveToolStripMenuItem.Click += SavePreset_Click;
            // 
            // exportEPLToolStripMenuItem
            // 
            exportEPLToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            exportEPLToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            exportEPLToolStripMenuItem.Name = "exportEPLToolStripMenuItem";
            exportEPLToolStripMenuItem.Size = new Size(169, 26);
            exportEPLToolStripMenuItem.Text = "Export EPL";
            exportEPLToolStripMenuItem.Click += ExportEPL_Click;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(51, 24);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += AddSprite_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            removeToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(77, 24);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += RemoveSelected_Click;
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            renameToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(77, 24);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += Rename_Click;
            // 
            // setImageToolStripMenuItem
            // 
            setImageToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            setImageToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            setImageToolStripMenuItem.Name = "setImageToolStripMenuItem";
            setImageToolStripMenuItem.Size = new Size(90, 24);
            setImageToolStripMenuItem.Text = "Set Image";
            setImageToolStripMenuItem.Click += ChooseImageFile_Click;
            // 
            // createEPTsToolStripMenuItem
            // 
            createEPTsToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            createEPTsToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            createEPTsToolStripMenuItem.Name = "createEPTsToolStripMenuItem";
            createEPTsToolStripMenuItem.Size = new Size(99, 24);
            createEPTsToolStripMenuItem.Text = "Create EPTs";
            createEPTsToolStripMenuItem.Click += CreateEPTs_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 691);
            Controls.Add(tlp_Main);
            Controls.Add(darkMenuStrip_MainMenu);
            MainMenuStrip = darkMenuStrip_MainMenu;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(650, 582);
            Name = "MainForm";
            Text = "EPLGen";
            tlp_Main.ResumeLayout(false);
            groupBox_ParticleSettings.ResumeLayout(false);
            panel_ParticleSettings.ResumeLayout(false);
            panel_ParticleSettings.PerformLayout();
            darkContextMenu_Sprites.ResumeLayout(false);
            groupBox_ModelSettings.ResumeLayout(false);
            panel_ModelSettings.ResumeLayout(false);
            panel_ModelSettings.PerformLayout();
            tlp_ModelSettings.ResumeLayout(false);
            tlp_ModelSettings.PerformLayout();
            groupBox_Texture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_Tex).EndInit();
            darkContextMenu_Texture.ResumeLayout(false);
            darkMenuStrip_MainMenu.ResumeLayout(false);
            darkMenuStrip_MainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.ListBox listBox_Sprites;
        private System.Windows.Forms.GroupBox groupBox_ModelSettings;
        private System.Windows.Forms.GroupBox groupBox_ParticleSettings;
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
        private System.Windows.Forms.Panel panel_ModelSettings;
        private System.Windows.Forms.TableLayoutPanel tlp_ModelSettings;
        private DarkUI.Controls.DarkLabel lbl_Mode;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.Panel panel_ParticleSettings;
        private System.Windows.Forms.TableLayoutPanel tlp_ParticleSettings;
        private ToolStripMenuItem createEPTsToolStripMenuItem;
    }
}

