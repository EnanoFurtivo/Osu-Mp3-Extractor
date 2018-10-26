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
        //Constructors//
        public MainForm()
        {
            InitializeComponent();
            
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        #region Methods
        //Form Events//
        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] spstring = Application.ExecutablePath.Split(new string[] { "\\Osu-Mp3-Extractor.exe" }, StringSplitOptions.None);
            appPath = spstring[0];  //appPath
            txtPath = appPath + @"\" + "SongExtractorLog.ini"; //txtPath
            imgPath = appPath + @"\" + "DefaultImg.png"; //imgPath
            txtErrorPath = appPath + @"\" + "ErrorLog.txt";

            //comboBox1.Items.Add("Extract by selecting from collection");
            comboBox1.Items.Add("Extract by selecting from library");
            comboBox1.Items.Add("Extract an entire collection");
            comboBox1.Items.Add("Extract entire library");

            SetFolder();
            if (!closeApp)
            {
                songs = new GetSongs(songsPath, osuDb);
                songsModify = new List<Song>(songs.SongsList);
                songsModify.Sort();

                //hides the buttons
                addallButton.Enabled = false;
                clearButton.Enabled = false;
                addButton.Enabled = false;
                optionsButton.Enabled = false;
                searchTextBox.Enabled = false;
                songsListBox.Enabled = false;
                extractqueueListBox.Enabled = false;
                comboBox2.Enabled = false;
            }
            else
                Application.ExitThread();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = "";
            songsListBox.DataSource = null;
            extractqueueListBox.DataSource = null;

            cdb = CollectionDb.Read(collectionDb);
            comboBox2.Items.Clear();
            comboBox2.Text = "Collections";

            //clears all texts
            searchTextBox.Text = "Serach by title, artist or Map creator";
            titleTextBox.Text = "";
            artistTextBox.Text = "";
            mapcreatorTextBox.Text = "";
            lengthTextBox.Text = "";
            linkLabel1.Text = "";
            loadedMapsLabel.Text = "0";
            selectedMapsLabels.Text = "0";

            //hides the buttons
            addallButton.Enabled = false;
            clearButton.Enabled = false;
            addButton.Enabled = false;
            searchTextBox.Enabled = false;
            songsListBox.Enabled = false;
            extractqueueListBox.Enabled = false;
            comboBox2.Enabled = false;
            
            //comment option button when fully implemented
            optionsButton.Enabled = false;
            
            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract by selecting from library")
            {
                mode = "Extract by selecting from library";
                selectedCollection = "osu!";
                SelectFromOsuDb();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract an entire collection")
            {
                mode = "Extract an entire collection";
                comboBox2.Enabled = true;
                foreach (Collection c in cdb.Collections)
                {
                    comboBox2.Items.Add(c.Name);
                }
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Extract entire library")
            {
                mode = "Extract entire library";
                selectedCollection = "osu!";
                CompleteLibrary();
            }
        } 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode == "Extract an entire collection")
            {
                if (comboBox2.GetItemText(comboBox2.SelectedItem) != "Collections")
                {
                    selectedCollection = comboBox2.GetItemText(comboBox2.SelectedItem);
                    CompleteCollection();
                }
                else
                    MessageBox.Show("Please select a collection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        } 
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "" && searchTextBox.Text != "Serach by title, artist or Map creator")
            {
                fromFilteredList = true;
                FillSongsFilteredList(currentlyDisplayedTmp, searchTextBox.Text);
                PrintSongsList(songsFiltered, false);
            }
            else
            {
                fromFilteredList = false;
                PrintSongsList(currentlyDisplayed, true);
            }
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
        private void songsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (songsListBox.SelectedValue != null && !bounce)
            {
                lastListSelected = false;
                getSelected();
                PrintSongDetails(selectedValue, true);
            }
        } 
        private void songsListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !noAdd)
            {
                addOrRemoveSong();
            }
        } 
        private void extractqueueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extractqueueListBox.SelectedValue != null && !bounce)
            {
                lastListSelected = true;
                getSelectedExt();
                PrintSongDetails(selectedValueExt, true);
            }
        } 
        private void extractqueueListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !noAdd)
            {
                addOrRemoveSong();
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        //Button Triggers//
        private void optionsButton_Click(object sender, EventArgs e)
        {

        }
        private void addButton_Click(object sender, EventArgs e)
        {
            addOrRemoveSong();
        }
        private void extractButton_Click(object sender, EventArgs e)
        {
            if (mode == "Extract an entire collection" || mode == "Extract entire library")
                songsForFinalExtraction = new List<Song>(songsForCompleteExtraction);
            else if (mode == "Extract by selecting from library")
                songsForFinalExtraction = new List<Song>(songsForExtraction);

            if (MessageBox.Show("Are you sure you want to exctract all " + songsForFinalExtraction.Count + " songs you selected?" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Song> songstmp = new List<Song>();
                
                //show the progress bar
                progressBar1.Visible = true;
                progressBar1.Maximum = songsForFinalExtraction.Count();

                //hides the buttons
                addallButton.Enabled = false;
                clearButton.Enabled = false;
                addButton.Enabled = false;
                optionsButton.Enabled = false;
                searchTextBox.Enabled = false;
                songsListBox.Enabled = false;
                extractqueueListBox.Enabled = false;

                //Extracts songs on background
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your selection? " + songsForExtraction.Count.ToString() + " Songs" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (Song song in songsForExtraction)
                {
                    songs.SongsList[song.Code].Collection = "osu!";
                    songs.SongsList[song.Code].Selected = false;
                }

                songsForExtraction = new List<Song>();
                PrintExtractList(songsForExtraction);

                extractButton.Enabled = false;
                clearButton.Enabled = false;
                addallButton.Enabled = true;

                songsListBox.Focus();
                PrintSongDetails(selectedValue, true);
            }
        }
        private void addallButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add all of the songs to the selection? " + currentlyDisplayed.Count.ToString() + " Songs" + Environment.NewLine + Environment.NewLine + "This cannont be undone", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bounce = true;
                foreach (Song song in currentlyDisplayed)
                {
                    if (!songs.SongsList[song.Code].Selected)
                    {
                        songs.SongsList[song.Code].Collection = selectedCollection;
                        songs.SongsList[song.Code].Selected = true;
                        songsForExtraction.Add(songs.SongsList[song.Code]);
                    }
                }
                PrintExtractList(songsForExtraction);
                //printSongDetails();

                extractButton.Enabled = true;
                clearButton.Enabled = true;
                addallButton.Enabled = false;

                extractqueueListBox.ClearSelected();
                songsListBox.Focus();
                PrintSongDetails(selectedValue, true);

                bounce = false;
            }
        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        //Print Methods//
        private void PrintExtractList(List<Song> songstmp)
        {
            if (songstmp.Count != 0)
            {
                extractButton.Enabled = true;
                songstmp.Sort();
                extractqueueListBox.DataSource = null;
                extractqueueListBox.DataSource = songstmp;
                selectedMapsLabels.Text = songstmp.Count.ToString();
            }
            else
            {
                extractButton.Enabled = false;
                extractqueueListBox.DataSource = null;
                selectedMapsLabels.Text = "0";
            }
            extractqueueListBox.DisplayMember = "Title";
            extractqueueListBox.ValueMember = "Code";
        } 
        private void PrintSongsList(List<Song> songstmp, bool sort)
        {
            if (!fromFilteredList)
            {
                currentlyDisplayed = new List<Song>(songstmp);
                currentlyDisplayedTmp = new List<Song>(songstmp);
            }
            if (sort)
                songstmp.Sort();

            songsListBox.DataSource = null;
            songsListBox.DataSource = songstmp;
            songsListBox.DisplayMember = "Title";
            songsListBox.ValueMember = "Code";

            loadedMapsLabel.Text = songstmp.Count.ToString();
        } 
        private void PrintSongDetails(int code, bool buttonActive)
        {
            //title, artist, creator
            titleTextBox.Text = songs.SongsList[code].Title;
            artistTextBox.Text = songs.SongsList[code].Artist;
            mapcreatorTextBox.Text = songs.SongsList[code].Creator;
            //osu mapset link
            linkLabel1.Text = @"https://osu.ppy.sh/beatmapsets/" + songs.SongsList[code].BeatmapSetId;
            //selected
            if (buttonActive)
            {
                if (songs.SongsList[code].Selected)
                {
                    addButton.Text = "Remove From extract queue";
                }
                else
                {
                    addButton.Text = "Add to extract queue";
                }
            }else
                addButton.Text = "";
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
            if (thumbnailPreview)
            PrintThumbnail(code);
        } 
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
        }

        //Modes//
        private void SelectFromOsuDb()
        {
            //unhides the buttons
            addallButton.Enabled = true;
            clearButton.Enabled = true;
            addButton.Enabled = true;
            noAdd = false;
            //optionsButton.Enabled = true;
            searchTextBox.Enabled = true;
            songsListBox.Enabled = true;
            extractqueueListBox.Enabled = true;

            PrintSongsList(songsModify, true);
            getSelected();
            PrintSongDetails(selectedValue, true);
            PrintExtractList(songsForExtraction);
        }
        private void CompleteCollection()
        {
            songsFromCollection = new GetSongs(songsPath, osuDb, collectionDb, comboBox1.GetItemText(comboBox2.SelectedItem));
            songsFromCollectionModify = new List<Song>(songsFromCollection.SongsList);
            songsFromCollectionModify.Sort();

            songsForCompleteExtraction = new List<Song>(songsFromCollectionModify);

            //unhides the buttons
            //addallButton.Enabled = true;
            //clearButton.Enabled = true;
            //addButton.Enabled = true;
            //optionsButton.Enabled = true;
            noAdd = true;
            searchTextBox.Enabled = true;
            songsListBox.Enabled = true;
            extractqueueListBox.Enabled = true;
            
            PrintSongsList(songsFromCollectionModify, true);
            songsListBox.Focus();
            getSelected();
            PrintSongDetails(selectedValue, true);
            PrintExtractList(songsForCompleteExtraction);
        }
        private void CompleteLibrary()
        {
            songsForCompleteExtraction = new List<Song>(songsModify);

            //unhides the buttons
            //addallButton.Enabled = true;
            //clearButton.Enabled = true;
            //addButton.Enabled = true;
            //optionsButton.Enabled = true;
            noAdd = true;
            searchTextBox.Enabled = true;
            songsListBox.Enabled = true;
            extractqueueListBox.Enabled = true;

            PrintSongsList(songsModify, true);
            songsListBox.Focus();
            getSelected();
            PrintSongDetails(selectedValue, true);
            PrintExtractList(songsForCompleteExtraction);
        }
        #endregion

        //Dead Code//
        /*
        private void folderButton_Click(object sender, EventArgs e)
        {
            fromChangeFolder = true;
            SetFolder();
        } 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (thumbnailOnly)
                thumbnailOnly = false;
            else
                thumbnailOnly = true;
        } //working
        private void hideButton_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        } 
        */
    }
}
