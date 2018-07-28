using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

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
            //songsext.SongsList[1].
        }
        private void extractButton_Click(object sender, EventArgs e)
        {

        }

        //Form Events//
        private void songsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = songsListBox.SelectedIndex;
            if (filtered == true)
                PrintSongDetails(selectedIndex);
            else
                PrintSongFilteredDetails(selectedIndex);
        }
        ///search Box Events///
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "" && searchTextBox.Text != "Serach by title, artist or Map creator")
                FillSongsFilteredList(searchTextBox.Text);
            else
                PrintSongsList();
        }
        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
                searchTextBox.Text = "Serach by title, artist or Map creator";
        }
        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Serach by title, artist or Map creator")
                searchTextBox.Text = "";
        }


        //Print Methods//
        private void PrintSongsList()
        {
            songsListBox.DataSource = songsext.SongsList;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";
            searchTextBox.Enabled = true;
        }
        private void PrintSongsFilteredList()
        {
            songsListBox.DataSource = SongsFiltered;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";
            searchTextBox.Enabled = true;
        }
        private void PrintSongDetails(int selectedIndex)
        {
            titleTextBox.Text = songsext.SongsList[selectedIndex].Title;
            artistTextBox.Text = songsext.SongsList[selectedIndex].Artist;
            mapcreatorTextBox.Text = songsext.SongsList[selectedIndex].Creator;
        }
        private void PrintSongFilteredDetails(int selectedIndex)
        {
            titleTextBox.Text = SongsFiltered[selectedIndex].Title;
            artistTextBox.Text = SongsFiltered[selectedIndex].Artist;
            mapcreatorTextBox.Text = SongsFiltered[selectedIndex].Creator;
        }
    }
}
