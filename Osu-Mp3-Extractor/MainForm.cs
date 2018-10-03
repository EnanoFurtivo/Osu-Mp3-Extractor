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
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;

namespace Osu_Mp3_Extractor
{
    public partial class MainForm : Form
    {
        //initialize component//
        public MainForm()
        {
            InitializeComponent();
            
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        //Startup//
        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] spstring = Application.ExecutablePath.Split(new string[] { "\\Osu-Mp3-Extractor.exe" }, StringSplitOptions.None);
            appPath = spstring[0];  //appPath
            txtPath = appPath + @"\" + "SongExtractorLog.ini"; //txtPath
            imgPath = appPath + @"\" + "DefaultImg.png"; //imgPath

            comboBox1.Items.Add("Extract from in game collection");
            comboBox1.Items.Add("Extract by selecting maps");

            SetFolder();
            songs = new GetSongs(songsPath, osuDb);
            songsModify = new List<Song>(songs.SongsList);
        }
        
        //Button Triggers//
        private void folderButton_Click(object sender, EventArgs e)
        {
            fromChangeFolder = true;
            SetFolder();
        } //fixed
        private void addButton_Click(object sender, EventArgs e)
        {
            addOrRemoveSong();
        } //No need to change
        private void extractButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exctract all of the songs within the selection?" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //show the progress bar
                progressBar1.Visible = true;
                progressBar1.Maximum = songsForExtraction.Count();

                //hides the buttons
                addallButton.Enabled = false;
                clearButton.Enabled = false;
                addButton.Enabled = false;
                folderButton.Enabled = false;
                searchTextBox.Enabled = false;
                songsListBox.Enabled = false;
                extractqueueListBox.Enabled = false;

                //Extracts songs on background
                backgroundWorker1.RunWorkerAsync();
            }
        } //fixed
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your selection? " + songsForExtraction.Count.ToString() + " Songs" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (Song song in songsForExtraction)
                {
                    songs.SongsList[song.Code].Selected = false;
                }

                songsForExtraction = new List<Song>();
                PrintExtractList();

                extractButton.Enabled = false;
                clearButton.Enabled = false;
                addallButton.Enabled = true;

                songsListBox.Focus();
                PrintSongDetails(selectedValue, true);
            }
        } //fixed
        private void addallButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add all of the songs to the selection? " + currentlyDisplayed.Count.ToString() + " Songs" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bounce = true;
                foreach (Song song in currentlyDisplayed)
                {
                    if (!song.Selected)
                    {
                        songs.SongsList[song.Code].Selected = true;
                        songsForExtraction.Add(songs.SongsList[song.Code]);
                    }
                }
                PrintExtractList();
                //printSongDetails();

                extractButton.Enabled = true;
                clearButton.Enabled = true;
                addallButton.Enabled = false;
                
                extractqueueListBox.ClearSelected();
                songsListBox.Focus();
                PrintSongDetails(selectedValue, true);

                bounce = false;
            }
        } //fixed
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        } //No need to change
        private void hideButton_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        } //No need to change

        //Form Events//
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cdb = CollectionDb.Read(collectionDb);
            comboBox2.Items.Clear();

            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract from in game collection")
            {
                comboBox2.Enabled = true;
                foreach (Collection c in cdb.Collections)
                {
                    comboBox2.Items.Add(c.Name);
                }

                //hides the buttons
                addallButton.Enabled = false;
                clearButton.Enabled = false;
                addButton.Enabled = false;
                folderButton.Enabled = false;
                searchTextBox.Enabled = false;
                songsListBox.Enabled = false;
                extractqueueListBox.Enabled = false;
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract by selecting maps")
            {
                comboBox2.Enabled = false;

                //unhides the buttons
                addallButton.Enabled = true;
                clearButton.Enabled = true;
                addButton.Enabled = true;
                folderButton.Enabled = true;
                searchTextBox.Enabled = true;
                songsListBox.Enabled = true;
                extractqueueListBox.Enabled = true;

                PrintSongsList(songsModify, true);
                getSelected();
                PrintSongDetails(selectedValue, true);
                PrintExtractList();
            }
            else
            {
                //hides the buttons
                addallButton.Enabled = false;
                clearButton.Enabled = false;
                addButton.Enabled = false;
                folderButton.Enabled = false;
                searchTextBox.Enabled = false;
                songsListBox.Enabled = false;
                extractqueueListBox.Enabled = false;
            }
        } //fixed
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract from in game collection")
            {
                if (comboBox2.Text != "Collections")
                {
                    songsFromCollection = new GetSongs(songsPath, osuDb, collectionDb, comboBox1.GetItemText(comboBox2.SelectedItem));
                    songsFromCollectionModify = new List<Song>(songsFromCollection.SongsList);
                    songsFromCollectionModify.Sort();
                    
                    //unhides the buttons
                    addallButton.Enabled = true;
                    clearButton.Enabled = true;
                    addButton.Enabled = true;
                    folderButton.Enabled = true;
                    searchTextBox.Enabled = true;
                    songsListBox.Enabled = true;
                    extractqueueListBox.Enabled = true;

                    PrintSongsList(songsFromCollectionModify, true);
                    songsListBox.Focus();
                    getSelected();
                    PrintSongDetails(selectedValue, true);
                    PrintExtractList();
                }
                else
                    MessageBox.Show("Please select a collection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        } //fixed
        
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "" && searchTextBox.Text != "Serach by title, artist or Map creator")
            {
                FillSongsFilteredList(currentlyDisplayedTmp ,searchTextBox.Text);
                PrintSongsList(songsFiltered, false);
                fromFilteredList = true;
            }
            else
                PrintSongsList(songsModify, true);
        } //fixed
        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
                searchTextBox.Text = "Serach by title, artist or Map creator";
        } //fixed
        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Serach by title, artist or Map creator")
                searchTextBox.Text = "";
        } //fixed

        private void songsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (songsListBox.SelectedValue != null && !bounce)
            {
                lastListSelected = false;
                getSelected();
                PrintSongDetails(selectedValue, true);
            }
        } //fixed
        private void songsListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                addOrRemoveSong();
            }
        } //fixed
        private void extractqueueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extractqueueListBox.SelectedValue != null && !bounce)
            {
                lastListSelected = true;
                getSelectedExt();
                PrintSongDetails(selectedValueExt, true);
            }
        } //fixed
        private void extractqueueListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                addOrRemoveSong();
            }
        } //fixed

        //Print Methods//
        private void PrintExtractList()
        {
            if (songsForExtraction.Count != 0)
            {
                songsForExtraction.Sort();
                extractqueueListBox.DataSource = null;
                extractqueueListBox.DataSource = songsForExtraction;
            }else
                extractqueueListBox.DataSource = null;
            extractqueueListBox.DisplayMember = "Title";
            extractqueueListBox.ValueMember = "Code";
        } //fixed
        private void PrintSongsList(List<Song> songstmp, bool sort)
        {
            currentlyDisplayed = new List<Song>(songstmp);
            if (!fromFilteredList)
                currentlyDisplayedTmp = new List<Song>(songstmp);
            if (sort)
                songstmp.Sort();

            songsListBox.DataSource = null;
            songsListBox.DataSource = songstmp;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";

            searchTextBox.Enabled = true;
            addallButton.Enabled = true;
            fromFilteredList = false;
        } //fixed
        private void PrintSongDetails(int code, bool buttonActive)
        {
            //title, artist, creator
            titleTextBox.Text = songs.SongsList[code].Title;
            artistTextBox.Text = songs.SongsList[code].Artist;
            mapcreatorTextBox.Text = songs.SongsList[code].Creator;
            //selected
            if (buttonActive)
            {
                if (songs.SongsList[code].Selected)
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
            }else
            {
                selectedTextBox.Text = "";
                addButton.Text = "";
            }
            //length
            var t = songs.SongsList[code].Length;
            string tempmin = "";
            string tempsec = "";

            TimeSpan span = new TimeSpan(0, 0, 0, 0, t);

            if (span.Minutes.ToString().Length == 1)
                tempmin = "0" + span.Minutes.ToString();
            else
                tempmin = span.Minutes.ToString();

            if (span.Seconds.ToString().Length == 1)
                tempsec = "0" + span.Seconds.ToString();
            else
                tempsec = span.Seconds.ToString();
            
            lengthTextBox.Text = tempmin + ":" + tempsec;

            //thumbnail
            PrintThumbnail(code);
        } //fixed
        private void PrintThumbnail(int code)
        {
            string imagepath = GetImage(code);
            using (FrequentlyUsed fused = new FrequentlyUsed())
            {
                if (System.IO.File.Exists(imagepath))
                    specsongsPictureBox.Image = fused.ResizeImage(imagepath, 192, 108);
                else
                    specsongsPictureBox.Image = fused.ResizeImage(Resources.Defaultsongthumbnail, 192, 108);
            }
        } //new
    }
}
