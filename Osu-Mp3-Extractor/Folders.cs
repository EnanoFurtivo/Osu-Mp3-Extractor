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
using System.IO;

namespace Osu_Mp3_Extractor
{
    public partial class Folders : Form
    {
        private string txtPath = "";
        private int extractions = 0;
        private GetSongs songsext2;

        //constructors//
        public Folders()
        {
            InitializeComponent();
            OutputPath = "";
            SongsPath = "";
        }
        public Folders(string outputPath, string songsPath, string txtPathtemp, int extractionstemp)
        {
            InitializeComponent();
            OutputPath = outputPath;
            SongsPath = songsPath;
            txtPath = txtPathtemp;
            extractions = extractionstemp;
            Abort = true;
        }

        //Startup//
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
            string param = @"osu\!\\Songs$";
            Regex r1 = new Regex(param, RegexOptions.IgnoreCase);
            Match m1 = r1.Match(SongsPath);

            if (SongsPath == "" && OutputPath == "")
            {
                MessageBox.Show("Please select both output and songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (SongsPath == "" && OutputPath != "")
            {
                MessageBox.Show("Please select a songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (OutputPath == "" && SongsPath != "")
            {
                MessageBox.Show("Please select an output folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (OutputPath == SongsPath)
            {
                MessageBox.Show("Please select a different output folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!m1.Success)
            {
                MessageBox.Show("Please select a valid osu!/songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                songsext2 = new GetSongs(SongsPath);
                if (songsext2.SongsList.Count == 0)
                {
                    MessageBox.Show("The program was unable to find any Songs inside the provided folder: " + SongsPath + Environment.NewLine + Environment.NewLine + "Please select a valid songs folder or add some songs to your osu game if its empty", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    System.IO.File.Delete(txtPath);
                    using (StreamWriter sw = System.IO.File.CreateText(txtPath))
                    {
                        sw.WriteLine("OutputPath=" + OutputPath);
                        sw.WriteLine("SongsPath=" + SongsPath);
                        sw.WriteLine("Extracts=" + extractions);
                    }
                    Abort = false;
                    this.Close();
                }
            }
        }

        //get; set//
        public string OutputPath { get; set; }
        public string SongsPath { get; set; }
        public bool Abort { get; set; }
    }
}
