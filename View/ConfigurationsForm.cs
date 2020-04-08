using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class ConfigurationsForm : Form
    {
        //Constructors
        public ConfigurationsForm()
        {
            InitializeComponent();

            Cfg = new Dictionary<string, string>() //Generate default settings dictionary
            {
                {"output path", ""},
                {"osu! path", ""},
                {"include image", ""}
            };

            Cfg["include image"] = includeThumbnailsCheckbox.Checked.ToString().ToLower();
        }
        public ConfigurationsForm(Dictionary<string, string> cfgIn)
        {
            InitializeComponent();

            Cfg = new Dictionary<string, string>(); //Generate default settings dictionary
            Cfg = cfgIn;
            
            outTextBox.Text = Cfg["output path"];
            osuTextBox.Text = Cfg["osu! path"];

            if (Cfg["include image"] == "false")
                includeThumbnailsCheckbox.Checked = false;
        }

        //buttons
        private void includeThumbnailsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["include image"] = includeThumbnailsCheckbox.Checked.ToString().ToLower();
        }
        private void outBrowseButton_Click(object sender, EventArgs e)
        {
            string outPath = seekFolder();
            if (outPath != "")
            {
                Cfg["output path"] = outPath;
                outTextBox.Text = Cfg["output path"];
            }
            else if (Cfg["output path"] == "")
                outTextBox.Text = @"C:\...\Mp3output";
        }
        private void osuBrowseButton_Click(object sender, EventArgs e)
        {
            string osuPath = seekFolder();
            if (osuPath != "")
            {
                Cfg["osu! path"] = osuPath;
                osuTextBox.Text = Cfg["osu! path"];
            }
            else if (Cfg["osu! path"] == "")
                osuTextBox.Text = @"C:\...\osu!";
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (System.Collections.Generic.KeyValuePair<string, string> item in Cfg)
                if (item.Value != "")
                    i++;
            if (i == Cfg.Count)
                this.Close();
            else
                MessageBox.Show("Opps!, you didnt select some path, please select both...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //useful
        private string seekFolder()
        {
            string outputVar = "";
            FolderBrowserDialog search = new FolderBrowserDialog();
            if (search.ShowDialog() == DialogResult.OK)
                outputVar = search.SelectedPath;
            return outputVar;
        }

        //properties
        public Dictionary<string, string> Cfg { get; set; }
    }
}
