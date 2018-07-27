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
            outputPath = "";
            songsPath = "";
        }
        public Folders(string OutputPath, string SongsPath)
        {
            InitializeComponent();
            outputPath = OutputPath;
            songsPath = SongsPath;
        }

        // Window Button Triggers//
        private void browseButton_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
                outputPath = fused.seekFolder();
            if (outputPath != "")
                outputfolderTextBox.Text = outputPath;
            else
                outputfolderTextBox.Text = @"C:\...\Mp3output";
        }
        private void browseButton1_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
               songsPath = fused.seekFolder();
            if (songsPath != "")
                songsfolderTextBox.Text = songsPath;
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
            Match m1 = r1.Match(songsPath);

            if (songsPath == "" && outputPath == "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select both output and songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if (songsPath == "" && outputPath != "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select a songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (outputPath == "" && songsPath != "")
            {
                e.Cancel = true;
                MessageBox.Show("Please select an output folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!m1.Success)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a valid osu!/songs folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (outputPath == songsPath)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a different output folder", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Folders_Load(object sender, EventArgs e)
        {
            if (outputPath != "" && songsPath != "")
            {
                outputfolderTextBox.Text = outputPath;
                songsfolderTextBox.Text = songsPath;
            }
            else
            {
                outputfolderTextBox.Text = @"C:\...\Mp3output";
                songsfolderTextBox.Text = @"C:\...\osu!\Songs";
            }
        }

        //get; set//
        public string outputPath { get; set; }
        public string songsPath { get; set; }
    }
}
