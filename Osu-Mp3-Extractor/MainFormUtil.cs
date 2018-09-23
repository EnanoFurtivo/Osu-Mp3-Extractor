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

namespace Osu_Mp3_Extractor
{
    public partial class MainForm
    {
        //Definitions//
        private string outputparam = @"OutputPath\=(.*)";
        private string songsparam = @"SongsPath\=(.*)";
        private string extractparam = @"Extracts\=(.*)";
        private bool txterror = false;
        private bool band = false;
        private string appPath = "";
        private string txtPath = "";
        private string imgPath = "";
        private string outputPath = "";
        private string songsPath = "";
        private string songsPathOld = "";
        private int selectedIndex = 0;
        private int selectedValue = 0;
        private int extractions = 0;
        private List<Song> SongsFiltered;
        private List<Song> SongsExtract;
        private List<Song> songsextSorted;
        private GetSongs songsext;
        private BackgroundWorker backgroundWorker1;

        //Methods//
        private void FillSongsFilteredList(string searchString)
        {
            SongsFiltered = new List<Song>();
            foreach (Song song in songsext.SongsList)
            {
                Regex r1 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                Match m1 = r1.Match(song.Title);
                Regex r2 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                Match m2 = r2.Match(song.Artist);
                Regex r3 = new Regex(Regex.Escape(searchString), RegexOptions.IgnoreCase);
                Match m3 = r3.Match(song.Creator);
                if (m1.Success)
                {
                    SongsFiltered.Add(songsext.SongsList[song.Code]);
                }
                else if (m2.Success)
                {
                    SongsFiltered.Add(songsext.SongsList[song.Code]);
                }
                else if (m3.Success)
                {
                    SongsFiltered.Add(songsext.SongsList[song.Code]);
                }
            }
            PrintSongsFilteredList();
        }
        private void SetFolder()
        {
            Folders folders = new Folders(outputPath, songsPath, txtPath, extractions);
            folders.ShowDialog();
            if (!folders.Abort)
            {
                songsPath = folders.SongsPath;
                outputPath = folders.OutputPath;
                Check();
            }
            else if (folders.Abort && band == true)
            {
                songsPath = folders.SongsPath;
                outputPath = folders.OutputPath;
                band = false;
                Check();
            }
            else
                this.Close();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Create Cover Object
            TagLib.Picture pic = new TagLib.Picture();
            pic.Type = TagLib.PictureType.FrontCover;
            pic.Description = "Cover";
            pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
            string oldMp3ValidName = "";
            int i = 1;
            string mp3CopyPath = "";

            foreach (Song songn in songsextSorted)
            {
                if (songn.Selected == true)
                {
                    //Correct the song name
                    string mp3validname = songCharReplace(songn.Title, songn.Artist);

                    //Copy the mp3
                    extractions++;
                    if (mp3validname == oldMp3ValidName)
                    {
                        mp3CopyPath = outputPath + "\\" + mp3validname + " (" + i + ")" + ".mp3";
                        i++;
                    }
                    else
                    {
                        mp3CopyPath = outputPath + "\\" + mp3validname + ".mp3";
                    }

                    oldMp3ValidName = mp3validname;
                    System.IO.File.Copy(songn.Mp3Path, mp3CopyPath, true);

                    //Create Song Object
                    var file = TagLib.File.Create(mp3CopyPath);

                    //Applying the modifications
                    file.Tag.Title = songn.Title;                                                    //title
                    file.Tag.AlbumArtists = songn.Artist.Split(new char[] { ';' });                 //Artist
                    file.Tag.Album = "osu!";                                                       //Album

                    //Applying the cover
                    if (songn.ImagePath != "" && System.IO.File.Exists(songn.ImagePath))
                    {
                        pic.Data = TagLib.ByteVector.FromPath(songn.ImagePath);
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

                    //Save the mp3
                    file.Save();

                    //Update Prograss Bar
                    backgroundWorker1.ReportProgress(songn.Code);
                }
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PrintSongDetails(e.ProgressPercentage);
            progressBar1.PerformStep();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Finishes the process
            progressBar1.Visible = false;
            System.IO.File.Delete(txtPath);
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=" + outputPath);
                sw.WriteLine("SongsPath=" + songsPath);
                sw.WriteLine("Extracts=" + extractions);
            }

            addallButton.Enabled = true;
            clearButton.Enabled = true;
            addButton.Enabled = true;
            folderButton.Enabled = true;
            searchTextBox.Enabled = true;
            songsListBox.Enabled = true;
            extractqueueListBox.Enabled = true;

            MessageBox.Show("All songs have been extracted succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            PrintSongDetails();
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
        }
        private void Check()
        {
            if (System.IO.File.Exists(txtPath))
            {
                int lineNumber = 0;
                string[] lines = System.IO.File.ReadAllLines(txtPath); //takes .osu file and transform into an array of strings
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
                            songsPath = m2.Groups[1].Value;
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
                }

                if (txterror == true)
                {
                    MessageBox.Show("Please select both output and songs and make sure they are valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    System.IO.File.Delete(txtPath);
                    using (StreamWriter sw = System.IO.File.CreateText(txtPath))
                    {
                        sw.WriteLine("OutputPath=");
                        sw.WriteLine("SongsPath=");
                        sw.WriteLine("Extracts=");
                    }
                    txterror = false;
                    SetFolder();
                }
                
                string param = @"\\Songs$"; //fix??
                Regex r4 = new Regex(param, RegexOptions.IgnoreCase);
                Match m4 = r4.Match(songsPath);

                if (songsPath == "" && outputPath == "")
                {
                    MessageBox.Show("Please select both output and songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else if (songsPath == "" && outputPath != "")
                {
                    MessageBox.Show("Please select a songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else if (outputPath == "" && songsPath != "")
                {
                    MessageBox.Show("Please select an output folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else if (outputPath == songsPath)
                {
                    MessageBox.Show("Please select a different output folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else if (!m4.Success)
                {
                    MessageBox.Show("Please select a valid songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else
                {
                    songsext = new GetSongs(songsPath);
                    songsextSorted = new List<Song>(songsext.SongsList);
                    songsextSorted.Sort();

                    if (songsext.SongsList.Count == 0)
                    {
                        MessageBox.Show("The program was unable to find any Songs inside the provided folder: " + songsPath + Environment.NewLine + Environment.NewLine + "Please select a valid songs folder or add some songs to your osu game if its empty", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SetFolder();
                    }
                    else
                    {
                        PrintSongsList();
                        PrintSongDetails();
                        getSelectedValue();

                        if (songsPathOld != songsPath)
                        {
                            SongsExtract = new List<Song>();
                            foreach (Song song in songsext.SongsList)
                            {
                                songsext.SongsList[song.Code].Selected = false;
                            }
                            PrintExtractList();
                            PrintSongDetails();
                            getSelectedValue();
                            extractButton.Enabled = false;
                            clearButton.Enabled = false;
                            addallButton.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.CreateText(txtPath))
                {
                    sw.WriteLine("OutputPath=");
                    sw.WriteLine("SongsPath=");
                    sw.WriteLine("Extracts=");
                }
                SetFolder();
            }
        }
        private void getSelectedValue()
        {
            string text = songsListBox.SelectedValue.ToString();
            if (text != "Osu_Mp3_Extractor.Song")
            {
                selectedValue = Int32.Parse(text);
            }
        }
    }
}