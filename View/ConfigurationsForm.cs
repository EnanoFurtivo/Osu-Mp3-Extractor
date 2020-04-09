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
            Cfg["overwrite album"] = overwriteAlbumCheckbox.Checked.ToString().ToLower();
            Cfg["overwrite artist"] = overwriteArtistCheckbox.Checked.ToString().ToLower();
            Cfg["overwrite title"] = overwriteTitleCheckbox.Checked.ToString().ToLower();
            Cfg["force album"] = forceAlbumCheckbox.Checked.ToString().ToLower();
            Cfg["force artist"] = forceArtistCheckbox.Checked.ToString().ToLower();
            Cfg["force title"] = forceTitleCheckbox.Checked.ToString().ToLower();

            ShouldClose = true;
        }
        public ConfigurationsForm(Dictionary<string, string> cfgIn)
        {
            InitializeComponent();

            Cfg = new Dictionary<string, string>(); //Generate default settings dictionary
            Cfg = cfgIn;
            
            outLabel.Text = Cfg["output path"];
            osuLabel.Text = Cfg["osu! path"];

            if (Cfg["include image"] == "false") includeThumbnailsCheckbox.Checked = false;
            if (Cfg["overwrite album"] == "false") { overwriteAlbumCheckbox.Checked = false; forceAlbumCheckbox.Enabled = false; }
            if (Cfg["overwrite artist"] == "false") { overwriteArtistCheckbox.Checked = false; forceArtistCheckbox.Enabled = false; }
            if (Cfg["overwrite title"] == "false") { overwriteTitleCheckbox.Checked = false; forceTitleCheckbox.Enabled = false; }
            if (Cfg["force album"] == "false") forceAlbumCheckbox.Checked = false;
            if (Cfg["force artist"] == "false") forceArtistCheckbox.Checked = false;
            if (Cfg["force title"] == "false") forceTitleCheckbox.Checked = false;

            ShouldClose = false;
        }

        //checkBoxes
        private void includeThumbnailsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["include image"] = includeThumbnailsCheckbox.Checked.ToString().ToLower();
        }
        private void overwriteAlbumCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["overwrite album"] = overwriteAlbumCheckbox.Checked.ToString().ToLower();
            if (overwriteAlbumCheckbox.Checked) forceAlbumCheckbox.Enabled = true;
            else forceAlbumCheckbox.Enabled = false;
        }
        private void overwriteArtistCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["overwrite artist"] = overwriteArtistCheckbox.Checked.ToString().ToLower();
            if (overwriteArtistCheckbox.Checked) forceArtistCheckbox.Enabled = true;
            else forceArtistCheckbox.Enabled = false;
        }
        private void overwriteTitleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["overwrite title"] = overwriteTitleCheckbox.Checked.ToString().ToLower();
            if (overwriteTitleCheckbox.Checked) forceTitleCheckbox.Enabled = true;
            else forceTitleCheckbox.Enabled = false;
        }
        private void forceAlbumCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["force album"] = forceAlbumCheckbox.Checked.ToString().ToLower();
        }
        private void forceArtistCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["force artist"] = forceArtistCheckbox.Checked.ToString().ToLower();
        }
        private void forceTitleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Cfg["force title"] = forceTitleCheckbox.Checked.ToString().ToLower();
        }
        
        //buttons
        private void outBrowseButton_Click(object sender, EventArgs e)
        {
            string outPath = seekFolder();
            if (outPath != string.Empty)
            {
                Cfg["output path"] = outPath;
                outLabel.Text = Cfg["output path"];
            }
            else if (Cfg["output path"] == "")
                outLabel.Text = @"C:\...\Mp3output";
        }
        private void osuBrowseButton_Click(object sender, EventArgs e)
        {
            string osuPath = seekFolder();
            if (osuPath != string.Empty)
            {
                Cfg["osu! path"] = osuPath;
                osuLabel.Text = Cfg["osu! path"];
            }
            else if (Cfg["osu! path"] == "")
                osuLabel.Text = @"C:\...\osu!";
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (System.Collections.Generic.KeyValuePair<string, string> item in Cfg)
                if (item.Value != string.Empty)
                    i++;
            if (i == Cfg.Count)
            {
                ShouldClose = false;
                this.Close();
            }
            else
                MessageBox.Show("Opps!, you didnt select some path, please select both...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //useful
        private string seekFolder()
        {
            string outputVar = string.Empty;
            FolderBrowserDialog search = new FolderBrowserDialog();
            if (search.ShowDialog() == DialogResult.OK)
                outputVar = search.SelectedPath;
            return outputVar;
        }

        //properties
        public bool ShouldClose { get; set; }
        public Dictionary<string, string> Cfg { get; set; }
    }
}
