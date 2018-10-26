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
        private bool NewTxt = false;
        private bool FromChangeFolder = false;
        private GetSongs songsextTest;

        //constructors//
        public Folders(string outputPath, string osuPath, string txtPathtemp, int extractionstemp, bool fromChangeFolder, bool newtxt)
        {
            InitializeComponent();

            OutputPath = outputPath;
            OsuPath = osuPath;
            txtPath = txtPathtemp;
            extractions = extractionstemp;
            FromChangeFolder = fromChangeFolder;
            NewTxt = newtxt;
            Abort = true;
        }

        //Startup//
        private void Folders_Load(object sender, EventArgs e)
        {
            if (OutputPath != "" && OsuPath != "")
            {
                outputfolderTextBox.Text = OutputPath;
                songsfolderTextBox.Text = OsuPath;
            }
            else
            {
                outputfolderTextBox.Text = @"C:\...\Mp3output";
                songsfolderTextBox.Text = @"C:\...\osu!";
            }
            check();
        }

        //Window Button Triggers//
        private void browseButton_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
                OutputPath = fused.seekFolder();
            if (OutputPath != "")
                outputfolderTextBox.Text = OutputPath;
            else
                outputfolderTextBox.Text = @"C:\...\Mp3output";
            FromChangeFolder = false; 
        }
        private void browseButton1_Click(object sender, EventArgs e)
        {
            using (FrequentlyUsed fused = new FrequentlyUsed())
               OsuPath = fused.seekFolder();
            if (OsuPath != "")
                songsfolderTextBox.Text = OsuPath;
            else
                songsfolderTextBox.Text = @"C:\...\osu!";
            FromChangeFolder = false;
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            FromChangeFolder = false;
            check();
        }

        //Methods//
        private void check()
        {
            if (!NewTxt)
            {
                if (!FromChangeFolder)
                {
                    if (OsuPath == "" && OutputPath == "")
                        MessageBox.Show("Please select both output and osu! folder", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (OsuPath == "" && OutputPath != "")
                        MessageBox.Show("Please select a osu! folder", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (OutputPath == "" && OsuPath != "")
                        MessageBox.Show("Please select an output folder", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (OutputPath == OsuPath)
                        MessageBox.Show("Please select a different output or osu! folder", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (!Directory.Exists(OsuPath))
                            MessageBox.Show("It seems like your osu folder has been renamed, moved or deleted. Please restore it or select a new one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else if (!Directory.Exists(OutputPath))
                            MessageBox.Show("It seems like your output folder has been renamed, moved or deleted. Please restore it or select a new one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            foreach (string subfolder in Directory.GetDirectories(OsuPath))
                            {
                                if (subfolder == OsuPath + "\\" + "Songs")
                                {
                                    SongsPath = subfolder;
                                    foreach (string subfile in Directory.GetFiles(OsuPath))
                                    {
                                        if (subfile == OsuPath + "\\" + "osu!.db")
                                        {
                                            OsuDb = subfile;
                                        }
                                        else if (subfile == OsuPath + "\\" + "collection.db")
                                        {
                                            CollectionDb = subfile;
                                        }
                                    }
                                }
                            }

                            if (CollectionDb != "" || OsuDb != "")
                            {
                                if (OsuDb != "")
                                {
                                    if (CollectionDb != "")
                                    {
                                        if (SongsPath != "")
                                        {
                                            songsextTest = new GetSongs(SongsPath, OsuDb);
                                            if (songsextTest.SongsList.Count == 0)
                                                MessageBox.Show("The program was unable to find any Songs inside the provided folder: " + SongsPath + Environment.NewLine + Environment.NewLine + "Please make sure that the folder is correct or add some songs to your osu game if its empty", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            else
                                            {
                                                System.IO.File.Delete(txtPath);
                                                using (StreamWriter sw = System.IO.File.CreateText(txtPath))
                                                {
                                                    sw.WriteLine("OutputPath=" + OutputPath);
                                                    sw.WriteLine("OsuPath=" + OsuPath);
                                                    sw.WriteLine("Extracts=" + extractions);
                                                }
                                                Abort = false;
                                                this.Close();
                                            }
                                        }
                                        else
                                            MessageBox.Show("No Songs folder was found inside your osu! folder" + Environment.NewLine + "Please make sure it exists, it is automatically created by osu", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("It seems like your osu is missing this files:" + Environment.NewLine + "collection.db" + Environment.NewLine + Environment.NewLine + "Please make sure it exists, it is automatically created by osu", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show("It seems like your osu is missing this files:" + Environment.NewLine + "osu!.db" + Environment.NewLine + Environment.NewLine + "Please make sure it exists, it is automatically created by osu", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("It seems like your osu is missing both of these files:" + Environment.NewLine + "collection.db" + Environment.NewLine + "osu.db" + Environment.NewLine + Environment.NewLine + "Please make sure they exist, they are automatically created by osu", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            NewTxt = false;
        }

        //get; set;//
        public string OsuDb { get; set; }
        public string CollectionDb { get; set; }
        public string OutputPath { get; set; }
        public string SongsPath { get; set; }
        public string OsuPath { get; set; }
        public bool Abort { get; set; }
    }
}
