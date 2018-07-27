using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Osu_Mp3_Extractor
{
    public partial class Folders : Form
    {
        //constructors//
        public Folders()
        {
            InitializeComponent();
            OutputPath = "";
            SongsPath = "";
        }
        public Folders(string outputPath, string songsPath)
        {
            InitializeComponent();
            OutputPath = outputPath;
            SongsPath = songsPath;
        }

        // Window Button Triggers//
        private void browseButton_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
                OutputPath = fused.seekFolder();
            if (OutputPath != "")
                outputfolderTextBox.Text = OutputPath;
            else
                outputfolderTextBox.Text = @"C:\...\Mp3output";
        }
        private void browseButton1_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
               SongsPath = fused.seekFolder();
            if (SongsPath != "")
                songsfolderTextBox.Text = SongsPath;
            else
                songsfolderTextBox.Text = @"C:\...\osu!\Songs";
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Form Events//
        private void Folders_FormClosing(object sender, FormClosingEventArgs e)
        {
            string param = @"osu\!\\Songs$";
            Regex r1 = new Regex(param, RegexOptions.IgnoreCase);
            Match m1 = r1.Match(SongsPath);

            if (SongsPath == "" && OutputPath == "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select both output and songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if (SongsPath == "" && OutputPath != "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select a songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (OutputPath == "" && SongsPath != "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select an output folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!m1.Success)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a valid osu!/songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (OutputPath == SongsPath)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a different output folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Folders_Load(object sender, EventArgs e)
        {
            if (OutputPath != "" && SongsPath != "")
            {
                outputfolderTextBox.Text = OutputPath;
                songsfolderTextBox.Text = SongsPath;
            }
            else
            {
                outputfolderTextBox.Text = @"C:\...\Mp3output";
                songsfolderTextBox.Text = @"C:\...\osu!\Songs";
            }
        }

        //get; set//
        public string OutputPath { get; set; }
        public string SongsPath { get; set; }
    }
}
