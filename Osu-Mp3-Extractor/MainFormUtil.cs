using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;

namespace Osu_Mp3_Extractor
{
    public partial class MainForm
    {
        #region Definitions
        //DEBUG//
        private bool thumbnailPreview = true;
        private bool thumbnailOnExtractFile = true;

        //DEFINITIONS//
        private string imageparam = @"([0,9]+\,[0,9]+)\,\""([^\""]+)\"".*";
        private string outputparam = @"OutputPath\=(.*)";
        private string songsparam = @"OsuPath\=(.*)";
        private string extractparam = @"Extracts\=(.*)";
        private bool fromChangeFolder = false;
        private bool fromFilteredList = false;
        private bool lastListSelected = false; //false = list | true = extractlist
        private bool bounce = false;
        private bool newTxt = false;
        private string txtErrorPath = "";
        private string appPath = "";
        private string txtPath = "";
        private string imgPath = "";
        private string outputPath = "";
        private string songsPath = "";
        private string osuPath = "";
        private string collectionDb = "";
        private string osuDb = "";
        private int errorInt = 0;
        private int selectedValue = 0;
        private int selectedIndex = 0;
        private int selectedIndexExt = 0;
        private int selectedValueExt = 0;
        private int extractions = 0;
        private List<Song> songsFiltered = new List<Song>();
        private List<Song> songsModify = new List<Song>();
        private List<Song> songsFromCollectionModify = new List<Song>();
        private List<Song> currentlyDisplayed = new List<Song>();
        private List<Song> currentlyDisplayedTmp = new List<Song>();
        private List<Song> songsForExtraction = new List<Song>();
        private List<Song> songsForExtractionEmpty = new List<Song>();
        private List<String> errorString;
        private GetSongs songs;
        private GetSongs songsFromCollection;
        private BackgroundWorker backgroundWorker1;
        private CollectionDb cdb;
        #endregion

        #region Methods
        private void SetFolder()
        {
            if (System.IO.File.Exists(txtPath))
            {
                checkTxt();
                openFolderForm();
            }
            else
            {
                CreateSettings();
                openFolderForm();
            }
        } //fixed
        private void openFolderForm()
        {
            Folders folders = new Folders(outputPath, osuPath, txtPath, extractions, fromChangeFolder, newTxt);
            folders.ShowDialog();
            if (!folders.Abort)
            {
                osuPath = folders.OsuPath;
                songsPath = folders.SongsPath;
                outputPath = folders.OutputPath;
                collectionDb = folders.CollectionDb;
                osuDb = folders.OsuDb;
                newTxt = false;
            }
            else if (fromChangeFolder)
            {
                fromChangeFolder = false;
            }
            else
                this.Close();
        } //fixed

        private void FillSongsFilteredList(List<Song> songstmp, string searchString)
        {
            List<int> usedCodes = new List<int>();
            songsFiltered = new List<Song>();
            bool band = true;

            foreach (Song song in songstmp)
            {
                band = true;

                foreach (int code in usedCodes)
                {
                    if (code == song.Code)
                    {
                        band = false;
                    }
                }

                if (band)
                {
                    Regex r1 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                    Match m1 = r1.Match(song.Title);
                    if (m1.Success)
                    {
                        songsFiltered.Add(songs.SongsList[song.Code]);
                        usedCodes.Add(song.Code);
                    }
                }
            }
            foreach (Song song in songstmp)
            {
                band = true;

                foreach (int code in usedCodes)
                {
                    if (code == song.Code)
                    {
                        band = false;
                    }
                }

                if (band)
                {
                    Regex r2 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                    Match m2 = r2.Match(song.Artist);
                    if (m2.Success)
                    {
                        songsFiltered.Add(songs.SongsList[song.Code]);
                        usedCodes.Add(song.Code);
                    }
                }
            }
            foreach (Song song in songstmp)
            {
                band = true;

                foreach (int code in usedCodes)
                {
                    if (code == song.Code)
                    {
                        band = false;
                    }
                }

                if (band)
                {
                    Regex r3 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                    Match m3 = r3.Match(song.Creator);
                    if (m3.Success)
                    {
                        songsFiltered.Add(songs.SongsList[song.Code]);
                        usedCodes.Add(song.Code);
                    }
                }
            }
        } //fixed
        private void addOrRemoveSong()
        {
            bounce = true;
            songsForExtraction = new List<Song>();
            int tmp = selectedValue;
            int tmpi = selectedIndex;
            if (lastListSelected)
            {
                tmp = selectedValueExt;
                tmpi = selectedIndexExt;
            }

            foreach (Song song in songs.SongsList)
            {
                if (song.Code == tmp)
                {
                    if (!song.Selected)
                    {
                        songs.SongsList[song.Code].Selected = true;
                        addButton.Text = "Remove From extract queue";
                        songsForExtraction.Add(songs.SongsList[song.Code]);
                    }
                    else
                    {
                        songs.SongsList[song.Code].Selected = false;
                        addButton.Text = "Add to extract queue";
                    }
                }else if (song.Selected)
                {
                    songsForExtraction.Add(songs.SongsList[song.Code]);
                }
            }

            if (songsForExtraction.Count() != 0)
            {
                extractButton.Enabled = true;
                clearButton.Enabled = true;
                PrintExtractList();
            }
            else
            {
                extractButton.Enabled = false;
                clearButton.Enabled = false;
                PrintExtractList();
            }

            if (!lastListSelected)
            {
                extractqueueListBox.ClearSelected();
                songsListBox.Focus();
                PrintSongDetails(selectedValue, true);
            }else
            {
                if (songsForExtraction.Count == 0)
                {
                    songsListBox.Focus();
                }else if (selectedIndexExt == songsForExtraction.Count)
                {
                    extractqueueListBox.SetSelected(selectedIndexExt - 1, true);
                }
            }
            bounce = false;
        } //fixed
        
        private void getSelected()
        {
            if (songsListBox.SelectedValue != null)
            {
                string text = songsListBox.SelectedValue.ToString();
                if (text != "Osu_Mp3_Extractor.Song" && text != "")
                    selectedValue = Int32.Parse(text);
                selectedIndex = songsListBox.SelectedIndex;
            }
        } //Renamed
        private void getSelectedExt()
        {
            if (extractqueueListBox.SelectedValue != null)
            {
                string text = extractqueueListBox.SelectedValue.ToString();
                if (text != "Osu_Mp3_Extractor.Song" && text != "")
                    selectedValueExt = Int32.Parse(text);
                selectedIndexExt = extractqueueListBox.SelectedIndex;
            }
        } //Renamed
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TagLib.Picture pic = new TagLib.Picture();
            pic.Type = TagLib.PictureType.FrontCover;
            pic.Description = "Cover";
            pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

            int i = 0;
            string mp3CopyPath = "";
            errorInt = 0;
            errorString = new List<String>();

            foreach (Song songn in songsForExtraction)
            {
                if (songn.Selected == true)
                {
                    //Delete invalid chars of the songs title and artist
                    string mp3validname = songCharReplace(songn.Title, songn.Artist);

                    //Copy themp3
                    mp3CopyPath = outputPath + "\\" + mp3validname + ".mp3";
                    while (System.IO.File.Exists(mp3CopyPath))
                    {
                        i++;
                        mp3CopyPath = outputPath + "\\" + mp3validname + " (" + i + ")" + ".mp3";
                    }

                    if (File.Exists(songn.Mp3Path))
                    {
                        System.IO.File.Copy(songn.Mp3Path, mp3CopyPath, true);
                            
                        //Create Song Object
                        var file = TagLib.File.Create(mp3CopyPath);

                        //Applying the tags
                        file.Tag.Title = songn.Title;                                                    //title
                        file.Tag.AlbumArtists = songn.Artist.Split(new char[] { ';' });                 //Artist
                        file.Tag.Album = "osu!";                                                       //Album

                        //Applying the cover
                        if (thumbnailOnExtractFile)
                        {
                            string ImagePath = GetImage(songn.Code);

                            if (ImagePath != "" && System.IO.File.Exists(ImagePath))
                            {
                                try
                                {
                                    pic.Data = TagLib.ByteVector.FromPath(ImagePath);
                                }
                                catch (Exception)
                                {
                                    if (System.IO.File.Exists(imgPath))
                                    {
                                        pic.Data = TagLib.ByteVector.FromPath(imgPath);
                                    }
                                    else
                                    {
                                        Resources.Defaultsongthumbnail.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                                        pic.Data = TagLib.ByteVector.FromPath(imgPath);
                                    }
                                }
                            }
                            else
                            {
                                if (System.IO.File.Exists(imgPath))
                                {
                                    pic.Data = TagLib.ByteVector.FromPath(imgPath);
                                }
                                else
                                {
                                    Resources.Defaultsongthumbnail.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                                    pic.Data = TagLib.ByteVector.FromPath(imgPath);
                                }
                            }
                            file.Tag.Pictures = new TagLib.IPicture[] { pic };
                        }
                        //Save the mp3
                        extractions++;
                        file.Save();
                    }
                    else
                    {
                        errorString.Add(errorInt.ToString() + " - " + "Mp3 File Could Not Be Found On Song Folder: " + songn.Title + " [Artist:" + songn.Artist + " |Creator:" + songn.Creator + "] " + "- " + @"https://osu.ppy.sh/b/" + songn.BeatmapId + " {Artist: " + songn.Artist + ", BeatmapSetId: " + songn.BeatmapSetId + ", Code: " + songn.Code + ", Creator: " + songn.Creator + ", DiffPath: " + songn.DiffPath + ", FolderName: " + songn.FolderName + ", FolderPath: " + songn.FolderPath + ", Hash: " + songn.Hash + ", Length: " + songn.Length + ", mapId: " + songn.BeatmapId + ", Mp3Name: " + songn.Mp3Name + ", Mp3Path: " + songn.Mp3Path + ", Selected: " + songn.Selected + ", ThreadId: " + songn.ThreadId + ", Title: " + songn.Title + "}");
                        errorInt++;
                    }
                    //Update Prograss Bar
                    backgroundWorker1.ReportProgress(songn.Code);
                }
            }
        } //fixed
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PrintSongDetails(e.ProgressPercentage, false);
            progressBar1.PerformStep();
        } //fixed
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Finishes the process
            progressBar1.Visible = false;
            System.IO.File.Delete(txtPath);
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=" + outputPath);
                sw.WriteLine("OsuPath=" + osuPath);
                sw.WriteLine("Extracts=" + extractions);
            }

            addallButton.Enabled = true;
            clearButton.Enabled = true;
            addButton.Enabled = true;
            optionsButton.Enabled = true;
            searchTextBox.Enabled = true;
            songsListBox.Enabled = true;
            extractqueueListBox.Enabled = true;

            if (errorInt != 0)
            {
                if (!File.Exists(txtErrorPath))
                {
                    using (StreamWriter sw = System.IO.File.CreateText(txtErrorPath))
                    {
                        foreach (string str in errorString)
                        {
                            sw.WriteLine(str);
                        }
                    }
                }
                else
                {
                    System.IO.File.Delete(txtErrorPath);
                    using (StreamWriter sw = System.IO.File.CreateText(txtErrorPath))
                    {
                        foreach (string str in errorString)
                        {
                            sw.WriteLine(str);
                        }
                    }
                }

                MessageBoxManager.Cancel = "Error log";
                MessageBoxManager.Register();
                if (errorInt == 1)
                {
                    if (MessageBox.Show("Most songs have been extracted succesfully in exception of: " + errorInt + " map" + Environment.NewLine + Environment.NewLine + "For further details click Error log", "File System Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel);
                    System.Diagnostics.Process.Start(txtErrorPath);
                }
                else
                {
                    if (MessageBox.Show("Most songs have been extracted succesfully in exception of: " + errorInt + " maps" + Environment.NewLine + Environment.NewLine + "For further details click Error log", "File System Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel);
                    System.Diagnostics.Process.Start(txtErrorPath);
                }
                MessageBoxManager.Unregister();
            }
            else
                MessageBox.Show("All songs have been extracted succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }//fixed
        
        private void checkTxt()
        {
            bool txterror = false;
            int lineNumber = 0;
            string[] lines = System.IO.File.ReadAllLines(txtPath);
            foreach (string line in lines)
            {
                string text1 = lines[lineNumber];
                if (lineNumber == 0)
                {
                    Regex r1 = new Regex(outputparam, RegexOptions.IgnoreCase);
                    Match m1 = r1.Match(text1);
                    if (m1.Success)
                    {
                        outputPath = m1.Groups[1].Value;
                    }
                    else
                        txterror = true;
                }
                else if (lineNumber == 1)
                {
                    Regex r2 = new Regex(songsparam, RegexOptions.IgnoreCase);
                    Match m2 = r2.Match(text1);
                    if (m2.Success)
                    {
                        osuPath = m2.Groups[1].Value;
                    }
                    else
                        txterror = true;
                }
                else if (lineNumber == 2)
                {
                    Regex r3 = new Regex(extractparam, RegexOptions.IgnoreCase);
                    Match m3 = r3.Match(text1);
                    if (m3.Success && m3.Groups[1].Value != "")
                    {
                        extractions = Int32.Parse(m3.Groups[1].Value);
                    }
                    else
                        txterror = true;
                }
                lineNumber++;
            }       //reads txt
            if (txterror == true)
            {
                ResetSettings();
                txterror = false;
            } //check path and set variables
        } //new
        private void ResetSettings()
        {
            System.IO.File.Delete(txtPath);
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=");
                sw.WriteLine("OsuPath=");
                sw.WriteLine("Extracts=");
            }
        } //new
        private void CreateSettings()
        {
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=");
                sw.WriteLine("OsuPath=");
                sw.WriteLine("Extracts=");
            }
            newTxt = true;
        } //new

        private string GetImage(int code)
        {
            string imagepath = "";
            using (FrequentlyUsed fused = new FrequentlyUsed())
            {
                int lineNumber = 0;
                string[] lines = System.IO.File.ReadAllLines(songs.SongsList[code].DiffPath); //takes .osu file and transform into an array of strings
                foreach (string line in lines)
                {
                    string text1 = lines[lineNumber];

                    /// imagepath ///
                    Regex r5 = new Regex(imageparam, RegexOptions.IgnoreCase);
                    Match m5 = r5.Match(text1);
                    if (m5.Success)
                    {
                        string imagename = m5.Groups[2].Value;
                        imagepath = songs.SongsList[code].FolderPath + "\\" + imagename;   //imagepath
                        break;
                    }
                    lineNumber++;
                }
            }//new
            return imagepath;
        }
        private string songCharReplace(string title, string artist)
        {
            string songTitletemp = title;
            string songArtisttemp = artist;

            var t1 = songTitletemp.Replace('/', '-');
            var t2 = t1.Replace('\\', '-');
            var t3 = t2.Replace(':', ' ');
            var t4 = t3.Replace('*', ' ');
            var t5 = t4.Replace('?', ' ');
            var t6 = t5.Replace('"', '-');
            var t7 = t6.Replace('<', '[');
            var t8 = t7.Replace('>', ']');
            var t9 = t8.Replace('|', '-');

            var a1 = songArtisttemp.Replace('/', '-');
            var a2 = a1.Replace('\\', '-');
            var a3 = a2.Replace(':', ' ');
            var a4 = a3.Replace('*', ' ');
            var a5 = a4.Replace('?', ' ');
            var a6 = a5.Replace('"', '-');
            var a7 = a6.Replace('<', '[');
            var a8 = a7.Replace('>', ']');
            var a9 = a8.Replace('|', '-');

            return t9 + " - " + a9;
        } //No need to change
        #endregion
    }
}