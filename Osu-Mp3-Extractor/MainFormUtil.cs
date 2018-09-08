﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

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
        private string outputPath = "";
        private string songsPath = "";
        private int selectedIndex = 0;
        private int extractions = 0;
        private List<Song> SongsFiltered;
        private List<Song> SongsExtract;
        private GetSongs songsext;

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
        private void ExtractSongs()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = songsext.SongsList.Count();

            foreach (Song songn in songsext.SongsList)
            {
                if (songn.Selected == true)
                {
                    //Copy the mp3
                    extractions++;
                    string mp3CopyPath = outputPath + "\\" + extractions.ToString() + "- " + songn.Mp3Name;
                    System.IO.File.Copy(songn.Mp3Path, mp3CopyPath, true);

                    //Create Song Object
                    var file = TagLib.File.Create(mp3CopyPath);

                    //Applying the modifications
                    file.Tag.Title = songn.Title;                                           //title
                    file.Tag.AlbumArtists = songn.Artist.Split(new char[] { ';' });         //Artist
                    file.Tag.Album = "osu!";                                                //album

                    //apply thumbnail
                    TagLib.Picture pic = new TagLib.Picture();
                    pic.Type = TagLib.PictureType.FrontCover;
                    pic.Description = "Cover";
                    pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                    MemoryStream ms = new MemoryStream();
                    using (FrequentlyUsed fused = new FrequentlyUsed())
                    {
                        if (songn.ImagePath != "" && System.IO.File.Exists(songn.ImagePath))
                        {
                            fused.ResizeImage(songn.ImagePath).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            fused.ResizeImage(Resources.Defaultsongthumbnail).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    ms.Position = 0;
                    pic.Data = TagLib.ByteVector.FromStream(ms);
                    file.Tag.Pictures = new TagLib.IPicture[] { pic };

                    //Save the mp3
                    file.Save();
                    ms.Close();

                    //increment progressbar
                    progressBar1.PerformStep();
                }
            }
            progressBar1.Visible = false;
            System.IO.File.Delete(txtPath);
            using (StreamWriter sw = System.IO.File.CreateText(txtPath))
            {
                sw.WriteLine("OutputPath=" + outputPath);
                sw.WriteLine("SongsPath=" + songsPath);
                sw.WriteLine("Extracts=" + extractions);
            }
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

                string param = @"osu\!\\Songs$";
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
                    MessageBox.Show("Please select a valid osu!/songs folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFolder();
                }
                else
                {
                    songsext = new GetSongs(songsPath);
                    if (songsext.SongsList.Count == 0)
                    {
                        MessageBox.Show("The program was unable to find any Songs inside the provided folder: " + songsPath + Environment.NewLine + Environment.NewLine + "Please select a valid songs folder or add some songs to your osu game if its empty", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SetFolder();
                    }
                    else
                    {
                        PrintSongsList();
                        PrintSongDetails();
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
    }
}