using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TagLib;
using System.Windows.Forms;
using System.IO;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;
using osu_database_reader.TextFiles;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClassLibrary
{
    public class Extractor
    {
        public Extractor(ProgressBar progressBar, Button buttonExtract, Button buttonOptions, Button buttonCancel)
        {
            ViewButtonOptions = buttonOptions;
            ViewButtonExtract = buttonExtract;
            ViewButtonCancel = buttonCancel;
            ViewPrBar = progressBar;

            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            BackgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            BackgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
        }

        public void extract(string mode, Configurations configs,  OsuDb odb, CollectionDb cdb)
        {
            Configs = configs;
            Mode = mode;
            ValidMode = validChars(mode);
            Odb = odb;
            Cdb = cdb;
            Result = false;

            if (Odb.Beatmaps.Count != 0)
            {
                Log = new List<string>();

                if (!System.IO.File.Exists(ImagePath))
                {
                    try
                    {
                        Properties.Resources.Defaultsongthumbnail.Save(ImagePath);
                        PicDefault.Data = TagLib.ByteVector.FromPath(ImagePath);
                    }
                    catch (Exception) { ; }
                }

                try
                {
                    Directory.CreateDirectory(Path.Combine(Configs.OutPath, ValidMode));
                    OutputSubFolder = Path.Combine(Configs.OutPath, ValidMode);

                    if (Mode == "Complete library")
                    {
                        PrBarMax = 0;
                        BeatmapEntry oldBeatmap = Odb.Beatmaps[0];
                        foreach (BeatmapEntry beatmap in Odb.Beatmaps)
                        {
                            if (oldBeatmap.BeatmapSetId != beatmap.BeatmapSetId)
                                PrBarMax++;
                            oldBeatmap = beatmap;
                        }
                        PrBarMax++;
                    }
                    else
                    {
                        PrBarMax = 0;

                        //Select collection
                        foreach (Collection col in Cdb.Collections)
                            if (col.Name == Mode) SelectedCollection = col;

                        foreach (string hash in SelectedCollection.BeatmapHashes)
                            PrBarMax++;
                    }
                    
                    viewUpdate(true, PrBarMax);
                    BackgroundWorker1.RunWorkerAsync();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to create directory:" + Path.Combine(Configs.OutPath, ValidMode) + Environment.NewLine + Environment.NewLine + "Please check if the fonder is read only or protected...", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else
                MessageBox.Show("Your osu seems to have 0 songs thats weird..." + Environment.NewLine + Environment.NewLine + "Please put some maps in it or try another installation...", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void cancel()
        {
            Cancel = true;
            BackgroundWorker1.CancelAsync();
            viewUpdate(false);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Mode == "Complete library")
            {
                int i = 0;
                int j = 1;

                BeatmapEntry oldBeatmap = Odb.Beatmaps[0];
                BeatmapEntry beatmap = Odb.Beatmaps[0];

                while (!Cancel && j < Odb.Beatmaps.Count)
                {
                    if (oldBeatmap.BeatmapSetId != beatmap.BeatmapSetId)
                    {
                        i++;
                        extractBeatmap(oldBeatmap);
                        BackgroundWorker1.ReportProgress(i);
                    }

                    oldBeatmap = beatmap;
                    beatmap = Odb.Beatmaps[j];
                    j++;
                }

                if (!Cancel)
                {
                    i++;
                    extractBeatmap(oldBeatmap);
                    BackgroundWorker1.ReportProgress(i);
                    Result = true;
                }
                else
                    Cancel = false;
            }
            else
            {
                int i = 0;
                int j = 0;

                //Iterate though maps
                BeatmapEntry beatmap = Odb.Beatmaps[0];
                while (!Cancel && j < Odb.Beatmaps.Count && i < SelectedCollection.BeatmapHashes.Count)
                {
                    foreach (string hash in SelectedCollection.BeatmapHashes)
                    {
                        if (beatmap.BeatmapChecksum == hash)
                        {
                            i++;
                            extractBeatmap(beatmap);
                            BackgroundWorker1.ReportProgress(i);
                        }
                    }
                    
                    beatmap = Odb.Beatmaps[j];
                    j++;
                }
                
                if (!Cancel) Result = true;
                else Cancel = false;
            }
        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ViewButtonExtract.Text = e.ProgressPercentage.ToString() + " / " + PrBarMax.ToString();
            ViewPrBar.PerformStep();
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            viewUpdate(false);

            if (System.IO.File.Exists(Configs.LogPath)) System.IO.File.Delete(Configs.LogPath); //delete previous file for overwriting
            using (StreamWriter sw = new StreamWriter(Configs.LogPath)) //Create txt to writ to
                foreach (string item in Log) 
                    sw.WriteLine(item); //write each line individually
        }

        private void extractBeatmap(BeatmapEntry bmp)
        {
            try
            {
                string inFile = Path.Combine(Configs.SongsPath, bmp.FolderName, bmp.AudioFileName);
                string entryName = validChars(bmp.Title) + " - " + validChars(bmp.Artist) + " [" + validChars(bmp.Version) + "] (" + validChars(bmp.Creator) + ")";
                string outFile = Path.Combine(OutputSubFolder, entryName + @".mp3");

                if (!System.IO.File.Exists(outFile))
                    copyAudioFile(inFile, outFile, entryName, bmp);
                else
                    Log.Add("[Info] For beatmap: " + entryName + " = " + "Beatmap was previously exported");
            }
            catch (Exception e)
            {
                Log.Add("[Error] While reading map data = " + e.Message);
            }
        }
        private void copyAudioFile(string input, string output, string entryName, BeatmapEntry bmp)
        {
            try
            {
                System.IO.File.Copy(input, output, true);
                
                if (Configs.OverwriteAlbum || Configs.OverwriteArtist || Configs.OverwriteTitle)
                {
                    File = TagLib.File.Create(output);
                    TagLib.File fileAux = null;

                    if (useAuxFile())
                        fileAux = File;

                    if (forceTitle()) //ask title
                        File.Tag.Title = bmp.Title;
                    else if (notForceTitle())
                        if (fileAux.Tag.Title == null) File.Tag.Title = bmp.Title;

                    if (forceAlbum()) //ask album
                        File.Tag.Album = ValidMode;
                    else if (notForceTitle())
                        if (fileAux.Tag.Album == null) File.Tag.Album = ValidMode;
                    
                    if (forceArtist())
                        updateArtist(bmp.Artist);
                    else if (notForceArtist())
                    {
                        if (fileAux.Tag.AlbumArtists.Count() == 0 && fileAux.Tag.Performers.Count() == 0)
                            updateArtist(bmp.Artist);
                        else if (fileAux.Tag.AlbumArtists.Count() != 0 && fileAux.Tag.Performers.Count() == 0)
                            updateArtist(File.Tag.AlbumArtists[0]);
                        else if (fileAux.Tag.Performers.Count() != 0 && fileAux.Tag.AlbumArtists.Count() == 0)
                            updateArtist(File.Tag.Performers[0]);
                    }
                    
                    if (Configs.IncludeThumbnails)
                    {
                        TagLib.Picture Pic = new TagLib.Picture();
                        Pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                        Pic.Description = "Cover";

                        BeatmapFile beatmapFile = BeatmapFile.Read(Path.Combine(Configs.SongsPath, bmp.FolderName, bmp.BeatmapFileName), true);
                        string imagePath;

                        foreach (PictureType pType in Enum.GetValues(typeof(PictureType)))
                        {
                            Pic.Type = pType;
                            Pic.Data = null;
                        }

                        if (beatmapFile.BackgroundImageFile != null && System.IO.File.Exists(imagePath = Path.Combine(Configs.SongsPath, bmp.FolderName, beatmapFile.BackgroundImageFile)))
                        {
                            Pic.Type = TagLib.PictureType.FrontCover;

                            try
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    Image png = Image.FromFile(imagePath);

                                    png.Save(ms, ImageFormat.Jpeg);
                                    ms.Position = 0;
                                    png.Dispose();
                                    
                                    Pic.Data = TagLib.ByteVector.FromStream(ms);
                                }
                            }
                            catch (Exception)
                            {
                                Pic.Data = PicDefault.Data;
                            }
                        }
                        else
                            Pic.Data = PicDefault.Data;

                        File.Tag.Pictures = new TagLib.IPicture[] { Pic };
                    }

                    File.Save();

                    if (useAuxFile())
                        fileAux.Dispose();
                }
            }
            catch (FileNotFoundException fileMissing)
            {
                Log.Add("[Error] For beatmap: " + entryName + " = " + fileMissing.Message);
            }
            catch (UnauthorizedAccessException noAccess)
            {
                Log.Add("[Error] For beatmap: " + entryName + " = " + noAccess.Message);
            }
            catch (TagLib.UnsupportedFormatException unsupportedFile)
            {
                Log.Add("[Error] For beatmap: " + entryName + " = " + unsupportedFile.Message);
            }
            catch (TagLib.CorruptFileException corrupt)
            {
                Log.Add("[Error] For beatmap: " + entryName + " = " + corrupt.Message);
            }
            catch (Exception exeption)
            {
                Log.Add("[Error] For beatmap: " + entryName + " = " + exeption.Message);
            }
        }
        
        private bool notForceTitle()
        {
            return (Configs.OverwriteTitle && !Configs.ForceTitle);
        }
        private bool forceTitle()
        {
            return (Configs.OverwriteTitle && Configs.ForceTitle);
        }
        private bool notForceAlbum()
        {
            return (Configs.OverwriteAlbum && !Configs.ForceAlbum);
        }
        private bool forceAlbum()
        {
            return (Configs.OverwriteAlbum && Configs.ForceAlbum);
        }
        private bool notForceArtist()
        {
            return (Configs.OverwriteArtist && !Configs.ForceArtist);
        }
        private bool forceArtist()
        {
            return (Configs.OverwriteArtist && Configs.ForceArtist);
        }
        private bool useAuxFile()
        {
            return ((Configs.OverwriteAlbum && !Configs.ForceAlbum) || (Configs.OverwriteArtist && !Configs.ForceArtist) || (Configs.OverwriteTitle && !Configs.ForceTitle));
        }
        
        private void updateArtist(string artist)
        {
            File.Tag.AlbumArtists = null;
            File.Tag.Performers = null;
            File.Tag.AlbumArtists = new String[1] { artist };
            File.Tag.Performers = new String[1] { artist };
        }
        private void viewUpdate(bool isExtracting, int max = 0)
        {
            ViewButtonCancel.Enabled = isExtracting;
            ViewButtonExtract.Enabled = !isExtracting;
            ViewButtonOptions.Enabled = !isExtracting;
            ViewPrBar.Visible = isExtracting;
            ViewPrBar.Maximum = max;
            ViewPrBar.Step = 1;
            if (!isExtracting) ViewButtonExtract.Text = "Extract";
        }
        private string validChars(string str)
        {
            str = str.Replace('/', '-');
            str = str.Replace('\\', '-');
            str = str.Replace(':', ' ');
            str = str.Replace('*', ' ');
            str = str.Replace('?', ' ');
            str = str.Replace('"', '-');
            str = str.Replace('<', '[');
            str = str.Replace('>', ']');
            str = str.Replace('|', '-');
            return str;
        }

        private Button ViewButtonCancel { get; set; }
        private Button ViewButtonExtract { get; set; }
        private Button ViewButtonOptions { get; set; }
        private ProgressBar ViewPrBar { get; set; }
        private OsuDb Odb { get; set; }
        private CollectionDb Cdb { get; set; }

        private string ImagePath { get; set; }
        private Collection SelectedCollection { get; set; } 
        private int PrBarMax { get; set; }
        private bool Cancel { get; set; }
        private string Mode { get; set; }
        private string ValidMode { get; set; }
        private string OutputSubFolder { get; set; }
        private Configurations Configs { get; set; }
        private TagLib.File File { get; set; }
        //private TagLib.Picture Pic { get; set; }
        private TagLib.Picture PicDefault { get; set; }
        private BackgroundWorker BackgroundWorker1 { get; set; }

        public List<string> Log { get; set; }
        public bool Result { get; set; }
    }
}
