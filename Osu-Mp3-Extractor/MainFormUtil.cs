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
        //private string imageparam = @"([0,9]+\,[0,9]+)\,\""([^\""]+)\"".*";
        private string outputparam = @"OutputPath\=(.*)";
        private string songsparam = @"OsuPath\=(.*)";
        private string extractparam = @"Extracts\=(.*)";
        private bool skip = false;
        private bool noAdd = false;
        private bool closeApp = false;
        private bool fromChangeFolder = false;
        private bool fromFilteredList = false;
        private bool lastListSelected = false; //false = list | true = extractlist
        private bool bounce = false;
        private bool newTxt = false;
        private bool isJpeg = false;
        private string mode = "";
        private string selectedCollection = "";
        private string txtErrorPath = "";
        private string txtPath = "";
        private string imgPath = "";
        private string imgPathTemp = "";
        private string outputPath = "";
        private string songsPath = "";
        private string osuPath = "";
        private string collectionDb = "";
        private string osuDb = "";
        private int extractionsTmp = 0;
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
        private List<Song> songsForCompleteExtraction = new List<Song>();
        private List<Song> songsForFinalExtraction = new List<Song>();
        private List<Song> songsForExtractionEmpty = new List<Song>();
        private List<String> formats = new List<String>();
        private List<String> errorString;
        private GetSongs songs;
        private GetSongs songsFromCollection;
        private BackgroundWorker backgroundWorker1;
        private CollectionDb cdb;
        #endregion

        #region Methods
        private void SetFolder()
        {
            //Replace with json implementation


            




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
        }   //Remove and replace for json
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
                closeApp = true;
        }   //Remove and replace for json

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
        }   //recode and organize (also agrupate in FILL ALL LISTS)
        private void addOrRemoveSong()
        {
            bool bandtemp = true; //true = add //false = remove
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
                        songs.SongsList[song.Code].Collection = selectedCollection;
                        songs.SongsList[song.Code].Selected = true;
                        addButton.Text = "Remove From extract queue";
                        songsForExtraction.Add(songs.SongsList[song.Code]);
                        bandtemp = true;
                    }
                    else
                    {
                        songs.SongsList[song.Code].Collection = "osu!";
                        songs.SongsList[song.Code].Selected = false;
                        addButton.Text = "Add to extract queue";
                        bandtemp = false;
                    }
                }else if (song.Selected)
                {
                    songsForExtraction.Add(songs.SongsList[song.Code]);
                }
            }
            PrintExtractList(songsForExtraction);

            if (songsForExtraction.Count == 0)
            {
                extractButton.Enabled = false;
                clearButton.Enabled = false;
            }
            else
            {
                extractButton.Enabled = true;
                clearButton.Enabled = true;
            }

            if (!bandtemp)
            {
                if (lastListSelected)//from extract list removing
                {
                    if (songsForExtraction.Count == 0)//if last item
                    {
                        songsListBox.Focus();
                        PrintSongDetails(selectedValue, true, true);
                    }
                    else if (selectedIndexExt == songsForExtraction.Count)//if item from the bottom
                    {
                        extractqueueListBox.SetSelected(selectedIndexExt - 1, true);
                        extractqueueListBox.Focus();
                        getSelectedExt();
                        PrintSongDetails(selectedValueExt, true, true);
                    }else
                    {
                        extractqueueListBox.SetSelected(selectedIndexExt, true);
                        extractqueueListBox.Focus();
                        getSelectedExt();
                        PrintSongDetails(selectedValueExt, true, true);
                    }
                }else //from normal list removing
                {
                    extractqueueListBox.ClearSelected();
                    songsListBox.Focus();
                }
            }else //add
            {
                songsListBox.Focus();
            }
            bounce = false;
        }   //Remove or agrupate in visual part
        
        private void getSelected()
        {
            if (songsListBox.SelectedValue != null)
            {
                string text = songsListBox.SelectedValue.ToString();
                if (text != "Osu_Mp3_Extractor.Song" && text != "")
                    selectedValue = Int32.Parse(text);
                selectedIndex = songsListBox.SelectedIndex;
            }
        }   //Remove or agrupate in visual part
        private void getSelectedExt()
        {
            if (extractqueueListBox.SelectedValue != null)
            {
                string text = extractqueueListBox.SelectedValue.ToString();
                if (text != "Osu_Mp3_Extractor.Song" && text != "")
                    selectedValueExt = Int32.Parse(text);
                selectedIndexExt = extractqueueListBox.SelectedIndex;
            }
        }   //Remove or agrupate in visual part

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TagLib.Picture pic = new TagLib.Picture();
            pic.Type = TagLib.PictureType.FrontCover;
            pic.Description = "Cover";
            pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

            TagLib.File file;
            TagLib.File file2;
            TagLib.File file3;

            extractionsTmp = 0;
            int i = 0;
            string subfoldersPath = "";
            errorInt = 0;
            errorString = new List<String>();
            
            string temp = "";
            bool bandtemp = true;
            string mp3validname = "";
            string mp3CopyPath = "";
            string mp3CopyPathTemp = "";
            string temp2 = "";
            string ImagePath = "";

            string collectionvalidname = songCharReplace(selectedCollection);

            if (!Directory.Exists(outputPath + "\\" + collectionvalidname))
            {
                try
                {
                    Directory.CreateDirectory(outputPath + "\\" + collectionvalidname);
                    subfoldersPath = outputPath + "\\" + collectionvalidname;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to create directory:" + outputPath + "\\" + collectionvalidname + Environment.NewLine + Environment.NewLine + "Instead using:" + outputPath, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    subfoldersPath = outputPath;
                }
            }
            else
                subfoldersPath = outputPath + "\\" + collectionvalidname;
            
            foreach (Song songn in songsForFinalExtraction)
            {
                skip = false;

                //Delete invalid chars of the songs title and artist
                mp3validname = songCharReplace(songn.Title, songn.Artist);
                mp3CopyPath = subfoldersPath + "\\" + mp3validname;
                
                //Copy the mp3
                if (File.Exists(songn.Mp3Path))
                {
                    bandtemp = true;

                    if (File.Exists(mp3CopyPath + ".mp3"))
                    {
                        temp = "";

                        try
                        {
                            file2 = TagLib.File.Create(mp3CopyPath + ".mp3");
                            temp = file2.Tag.Comment;
                            file2.Dispose();
                        }
                        catch (TagLib.CorruptFileException)
                        {
                            errorString.Add(errorInt.ToString() + " - " + "Mp3 File Was Corrupt at destination folder: " + songn.Title + " [Artist:" + songn.Artist + " |Creator:" + songn.Creator + "] " + "- " + @"https://osu.ppy.sh/b/" + songn.BeatmapId + " {Artist: " + songn.Artist + ", BeatmapSetId: " + songn.BeatmapSetId + ", Code: " + songn.Code + ", Creator: " + songn.Creator + ", DiffPath: " + songn.DiffPath + ", FolderName: " + songn.FolderName + ", FolderPath: " + songn.FolderPath + ", Hash: " + songn.Hash + ", Length: " + songn.Length + ", mapId: " + songn.BeatmapId + ", Mp3Name: " + songn.Mp3Name + ", Mp3Path: " + songn.Mp3Path + ", Selected: " + songn.Selected + ", ThreadId: " + songn.ThreadId + ", Title: " + songn.Title + "}");
                            errorInt++;
                            skip = true;
                        }

                        if (temp == songn.Hash)
                            bandtemp = false;
                        else
                        {
                            i = 0;
                            mp3CopyPathTemp = mp3CopyPath;

                            while (System.IO.File.Exists(mp3CopyPathTemp + ".mp3"))
                            {
                                temp2 = "";

                                try
                                {
                                    file3 = TagLib.File.Create(mp3CopyPathTemp + ".mp3");
                                    temp2 = file3.Tag.Comment;
                                    file3.Dispose();
                                }
                                catch (TagLib.CorruptFileException)
                                {
                                    errorString.Add(errorInt.ToString() + " - " + "Mp3 File Was Corrupt at destination folder: " + songn.Title + " [Artist:" + songn.Artist + " |Creator:" + songn.Creator + "] " + "- " + @"https://osu.ppy.sh/b/" + songn.BeatmapId + " {Artist: " + songn.Artist + ", BeatmapSetId: " + songn.BeatmapSetId + ", Code: " + songn.Code + ", Creator: " + songn.Creator + ", DiffPath: " + songn.DiffPath + ", FolderName: " + songn.FolderName + ", FolderPath: " + songn.FolderPath + ", Hash: " + songn.Hash + ", Length: " + songn.Length + ", mapId: " + songn.BeatmapId + ", Mp3Name: " + songn.Mp3Name + ", Mp3Path: " + songn.Mp3Path + ", Selected: " + songn.Selected + ", ThreadId: " + songn.ThreadId + ", Title: " + songn.Title + "}");
                                    errorInt++;
                                    skip = true;
                                }

                                if (temp2 == songn.Hash)
                                    bandtemp = false;

                                i++;
                                mp3CopyPathTemp = mp3CopyPath + " (" + i + ")";
                            }
                            mp3CopyPath = mp3CopyPathTemp;
                        }
                    }
                    mp3CopyPath += ".mp3";

                    if (bandtemp && !skip)
                    {
                        //Create Song Object and copy
                        System.IO.File.Copy(songn.Mp3Path, mp3CopyPath, true);
                        file = TagLib.File.Create(mp3CopyPath);

                        //Applying the tags
                        file.Tag.Title = songn.Title;
                        //file.Tag.AlbumArtists = songn.Artist.Split(new char[] { ';' });
                        file.Tag.Performers = songn.Artist.Split(new char[] { ';' });


                        //if (mode == "Extract an entire collection")
                            //file.Tag.Album = selectedCollection;
                        //else
                            //file.Tag.Album = "osu!";
                        file.Tag.Album = extractions.ToString();

                        file.Tag.Comment = songn.Hash;

                        //Applying the cover
                        if (thumbnailOnExtractFile)
                        {
                            ImagePath = GetImage(songn.Code);
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
                }
                else
                {
                    errorString.Add(errorInt.ToString() + " - " + "Mp3 File Could Not Be Found On Song Folder: " + songn.Title + " [Artist:" + songn.Artist + " |Creator:" + songn.Creator + "] " + "- " + @"https://osu.ppy.sh/b/" + songn.BeatmapId + " {Artist: " + songn.Artist + ", BeatmapSetId: " + songn.BeatmapSetId + ", Code: " + songn.Code + ", Creator: " + songn.Creator + ", DiffPath: " + songn.DiffPath + ", FolderName: " + songn.FolderName + ", FolderPath: " + songn.FolderPath + ", Hash: " + songn.Hash + ", Length: " + songn.Length + ", mapId: " + songn.BeatmapId + ", Mp3Name: " + songn.Mp3Name + ", Mp3Path: " + songn.Mp3Path + ", Selected: " + songn.Selected + ", ThreadId: " + songn.ThreadId + ", Title: " + songn.Title + "}");
                    errorInt++;
                }
                //Update Prograss Bar
                backgroundWorker1.ReportProgress(songn.Code);
            }
        }    //Agrupate in backpart also recode
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!skip)
                //PrintSongDetails(e.ProgressPercentage, false, false);
            progressBar1.PerformStep();
            extractingLabel.Text = extractionsTmp + "/" + songsForFinalExtraction.Count;
            extractionsTmp++;
        }   //Agrupate in backpart also recode
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Finishes the process
            progressBar1.Visible = false;
            progressBar1.Value = 0;
            extractionsTmp = 0;
            extractingLabel.Visible = false;
            label6.Visible = false;

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

            PrintSongDetails(selectedValue, true, true);

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
        }   //Agrupate in backpart also recode

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
        }   //Remove and replace for json
        private void ResetSettings()
        {
            System.IO.File.Delete(txtPath);
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=");
                sw.WriteLine("OsuPath=");
                sw.WriteLine("Extracts=");
            }
        }   //Remove and replace for json
        private void CreateSettings()
        {
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=");
                sw.WriteLine("OsuPath=");
                sw.WriteLine("Extracts=");
            }
            newTxt = true;
        }   //Remove and replace for json

        private string GetImage(int code)
        {
            string imagepath = "";
            imagepath = readTxt(songs.SongsList[code]);
            if (imagepath == "")
            {
                List<Song> songsthreadid = new List<Song>();
                foreach (Song songn in songs.SongsCompleteList)
                {
                    if (songn.ThreadId == songs.SongsList[code].ThreadId)
                        imagepath = readTxt(songn);
                    if (imagepath != "")
                        break;
                }
            }
            return imagepath;
        }   //Remove or agrupate in visual part also recode
        private string readTxt(Song song)
        {
            string imagePath = "";
            int lineNumber = 0;
            isJpeg = true;
            string[] lines = System.IO.File.ReadAllLines(song.DiffPath); //takes .osu file and transform into an array of strings
            foreach (string line in lines)
            {
                if (lineNumber <= lines.Count() - 3)
                {
                    if (line == @"//Background and Video events" && lines[lineNumber + 1].Contains(".png") || lines[lineNumber + 1].Contains(".jpeg") || lines[lineNumber + 1].Contains(".jpg"))
                    {
                        string[] strtmp = lines[lineNumber + 1].Split((char)34);
                        imagePath = song.FolderPath + "\\" + strtmp[1];
                        if (lines[lineNumber + 1].Contains(".png") || lines[lineNumber + 1].Contains(".jpg"))
                            imagePath = jpgConvert(imagePath);
                        break;
                    }
                    else if (line == @"//Background and Video events" && lines[lineNumber + 2].Contains(".png") || lines[lineNumber + 2].Contains(".jpeg") || lines[lineNumber + 2].Contains(".jpg"))
                    {
                        string[] strtmp = lines[lineNumber + 2].Split((char)34);
                        imagePath = song.FolderPath + "\\" + strtmp[1];
                        if (lines[lineNumber + 1].Contains(".png") || lines[lineNumber + 1].Contains(".jpg"))
                            imagePath = jpgConvert(imagePath);
                        break;
                    }
                    else if (line == "[TimingPoints]" || line == "[Colours]" || line == "[HitObjects]")
                        break;
                }else
                    break;
                lineNumber++;
            }
            return imagePath;
        }   //Remove or agrupate in visual part also recode
        private string songCharReplace(string str)
        {
            string strtemp = str;

            var t1 = strtemp.Replace('/', '-');
            var t2 = t1.Replace('\\', '-');
            var t3 = t2.Replace(':', ' ');
            var t4 = t3.Replace('*', ' ');
            var t5 = t4.Replace('?', ' ');
            var t6 = t5.Replace('"', '-');
            var t7 = t6.Replace('<', '[');
            var t8 = t7.Replace('>', ']');
            var t9 = t8.Replace('|', '-');

            return t9;
        }   //work with song 
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
        }   //work with song 
        private string jpgConvert (string path)
        {
            string newPath = path;
            Bitmap bmp;

            using (FrequentlyUsed fused = new FrequentlyUsed())
            {
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        bmp = fused.ResizeImage(path);
                    }
                    catch (Exception)
                    {
                        bmp = Resources.Defaultsongthumbnail;
                    }
                }
                else
                    bmp = Resources.Defaultsongthumbnail;
            }

            newPath = imgPathTemp;

            try
            {
                bmp.Save(imgPathTemp, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                newPath = path;
            }
            
            return newPath;
        }   //work with song 
        #endregion
    }
}