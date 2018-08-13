using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using TagLib;
using System.Text.RegularExpressions;

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
            string[] spstring = Application.ExecutablePath.Split(new string[] { "\\Osu-Mp3-Extractor.exe" }, StringSplitOptions.None);
            appPath = spstring[0];
            txtPath = appPath + @"\" + "SongExtractorLog.ini";
            Check();
        }
        
        //Button Triggers//
        private void folderButton_Click(object sender, EventArgs e)
        {
            band = true;
            SetFolder();
            Check();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            SongsExtract = new List<Song>();
            foreach (Song song in songsext.SongsList)
            {
                if (song.Code == selectedIndex && song.Selected == true)
                {
                    songsext.SongsList[selectedIndex].Selected = false;
                    selectedTextBox.Text = "No";
                    addButton.Text = "Add to extract queue";
                }
                else if (song.Code != selectedIndex && song.Selected == true)
                {
                    SongsExtract.Add(songsext.SongsList[song.Code]);
                }
                else if (song.Code == selectedIndex && song.Selected == false)
                {
                    songsext.SongsList[selectedIndex].Selected = true;
                    selectedTextBox.Text = "Yes";
                    addButton.Text = "Remove From extract queue";
                    SongsExtract.Add(songsext.SongsList[selectedIndex]);
                }
            }

            if (SongsExtract.Count() != 0)
            {
                extractButton.Enabled = true;
                clearButton.Enabled = true;
            }
            else
            {
                extractButton.Enabled = false;
                clearButton.Enabled = false;
            }
            PrintExtractList();
        }
        private void extractButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exctract all of the songs within the selection?" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExtractSongs();
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your selection?" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SongsExtract = new List<Song>();
                foreach (Song song in songsext.SongsList)
                {
                    songsext.SongsList[song.Code].Selected = false;
                }
                PrintExtractList();
                PrintSongDetails();
                extractButton.Enabled = false;
                clearButton.Enabled = false;
                addallButton.Enabled = true;
            }
        }
        private void addallButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add all of the songs to the selection?" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SongsExtract = new List<Song>();
                foreach (Song song in songsext.SongsList)
                {
                    songsext.SongsList[song.Code].Selected = true;
                    SongsExtract.Add(songsext.SongsList[song.Code]);
                }
                PrintExtractList();
                PrintSongDetails();
                extractButton.Enabled = true;
                clearButton.Enabled = true;
                addallButton.Enabled = false;
            }
        }

        //Form Events//
        private void songsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = songsListBox.SelectedValue.ToString();
            if (text != "Osu_Mp3_Extractor.Song")
            {
                selectedIndex = Int32.Parse(text);
            }
            PrintSongDetails();
        }
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
        private void PrintExtractList()
        {
            extractqueueListBox.DataSource = SongsExtract;
            extractqueueListBox.DisplayMember = "Title";
            extractqueueListBox.ValueMember = "Code";
        }
        private void PrintSongsList()
        {
            songsListBox.DataSource = songsext.SongsList;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";
            searchTextBox.Enabled = true;
            addallButton.Enabled = true;
        }
        private void PrintSongsFilteredList()
        {
            songsListBox.DataSource = SongsFiltered;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";
            searchTextBox.Enabled = true;
        }
        private void PrintSongDetails()
        {
            //title, artist, creator
            titleTextBox.Text = songsext.SongsList[selectedIndex].Title;
            artistTextBox.Text = songsext.SongsList[selectedIndex].Artist;
            mapcreatorTextBox.Text = songsext.SongsList[selectedIndex].Creator;
            //selected
            if (songsext.SongsList[selectedIndex].Selected)
            {
                selectedTextBox.Text = "Yes";
                addButton.Text = "Remove From extract queue";
                addButton.Enabled = true;
            }
            else
            {
                selectedTextBox.Text = "No";
                addButton.Text = "Add to extract queue";
                addButton.Enabled = true;
            }
            //length
            TagLib.File f = TagLib.File.Create(songsext.SongsList[selectedIndex].Mp3Path, TagLib.ReadStyle.Average);
            var t = (int)f.Properties.Duration.TotalSeconds;
            int minutes = t / 60;
            int seconds = t % 60;
            string length = minutes.ToString() + ":" + seconds.ToString();
            lengthTextBox.Text = length;
            //thumbnail
            using (FrequentlyUsed fused = new FrequentlyUsed())
            {
                if (System.IO.File.Exists(songsext.SongsList[selectedIndex].ImagePath))
                    specsongsPictureBox.Image = fused.ResizeImage(songsext.SongsList[selectedIndex].ImagePath, 192, 108);
                else
                    specsongsPictureBox.Image = fused.ResizeImage(Resources.Defaultsongthumbnail, 192, 108);
            }
        }
        private void PrintSongDetails(int songcode)
        {
            //title, artist, creator
            titleTextBox.Text = songsext.SongsList[songcode].Title;
            artistTextBox.Text = songsext.SongsList[songcode].Artist;
            mapcreatorTextBox.Text = songsext.SongsList[songcode].Creator;
            //length
            TagLib.File f = TagLib.File.Create(songsext.SongsList[songcode].Mp3Path, TagLib.ReadStyle.Average);
            var t = (int)f.Properties.Duration.TotalSeconds;
            int minutes = t / 60;
            int seconds = t % 60;
            string length = minutes.ToString() + ":" + seconds.ToString();
            lengthTextBox.Text = length;
            //thumbnail
            using (FrequentlyUsed fused = new FrequentlyUsed())
            {
                if (System.IO.File.Exists(songsext.SongsList[songcode].ImagePath))
                    specsongsPictureBox.Image = fused.ResizeImage(songsext.SongsList[songcode].ImagePath, 192, 108);
                else
                    specsongsPictureBox.Image = fused.ResizeImage(Resources.Defaultsongthumbnail, 192, 108);
            }
        }
    }
}
