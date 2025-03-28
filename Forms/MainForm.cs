using System.Data;
using DarkUI.Forms;
using EPLGen.Classes;
using GFDLibrary.Models;
using GFDLibrary;
using ShrineFox.IO;

namespace EPLGen
{
    public partial class MainForm : DarkForm
    {
        List<Particle> copiedParticles = new List<Particle>();
        Particle copiedParams = new Particle();
        List<string> gmds = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            MenuStripHelper.SetMenuStripIcons(MenuStripHelper.GetMenuStripIconPairs("./Dependencies/Icons.txt"), this);
            AddInputFields();

            gmds = Directory.GetFiles("./Dependencies/GMD", "*.gmd").ToList();
            toolStripComboBox_GMD.ComboBox.Items.AddRange(gmds.Select(x => Path.GetFileName(x)).ToArray());
            toolStripComboBox_GMD.ComboBox.SelectedIndex = 0;
        }
    }
}
