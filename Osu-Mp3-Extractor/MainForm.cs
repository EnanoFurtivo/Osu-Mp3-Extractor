using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osu_Mp3_Extractor
{
    public partial class MainForm : Form
    {
        //initialize component//
        public MainForm()
        {
            InitializeComponent();
        }

        //Definitions//
        List<Song> songs = new List<Song>();

        string outputPath = "";
        string songsPath = "";
        string searchString = "";

        //Startup//
        private void MainForm_Load(object sender, EventArgs e)
        {
            //chequear en txt
            if (outputPath != "" && songsPath != "")
            {

            }
            else
            {
                Folders folders = new Folders();
                folders.ShowDialog();
                songsPath = folders.songsPath;
                outputPath = folders.outputPath;
            }
        }

        //Button Triggers//
        private void folderButton_Click(object sender, EventArgs e)
        {
            Folders folders = new Folders(outputPath, songsPath);
            folders.ShowDialog();
            songsPath = folders.songsPath;
            outputPath = folders.outputPath;
        }
        private void addButton_Click(object sender, EventArgs e)
        {

        }
        private void extractButton_Click(object sender, EventArgs e)
        {

        }
    }
}
