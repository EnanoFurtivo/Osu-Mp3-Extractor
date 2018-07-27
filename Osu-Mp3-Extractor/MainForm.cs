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

        

        //Startup//
        private void MainForm_Load(object sender, EventArgs e)
        {
            //chequear en txt------------------------
            //chequear volidez  de outputPathsongpath -------------------
            FillSongsList();
        }

        //Button Triggers//
        private void folderButton_Click(object sender, EventArgs e)
        {
            SetFolder();
        }
        private void addButton_Click(object sender, EventArgs e)
        {

        }
        private void extractButton_Click(object sender, EventArgs e)
        {

        }

        //Print Methods//
        private void PrintSongsList(SongExtract songsext)
        {
            searchTextBox.Enabled = true;
        }
    }
}
