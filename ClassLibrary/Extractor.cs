using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TagLib;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;
using System.Windows.Forms;
using System.IO;

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

            Pic = new TagLib.Picture();
            Pic.Type = TagLib.PictureType.FrontCover;
            Pic.Description = "Cover";
            Pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
            
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            BackgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            BackgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            BackgroundWorker1.WorkerReportsProgress = true;
        }

        public void extract(string mode, Configurations configs,  OsuDb odb, CollectionDb cdb)
        {
            Configs = configs;
            Mode = mode;
            ValidMode = validChars(mode);
            Odb = odb;
            Cdb = cdb;

            if (Odb.Beatmaps.Count != 0)
            {
                Log = new List<string>();

                Result = false;
                viewUpdate(true);

                if (!Directory.Exists(Path.Combine(Configs.OutPath, Mode)))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(Configs.OutPath, Mode));
                        OutputSubFolder = Path.Combine(Configs.OutPath, Mode);
                        BackgroundWorker1.RunWorkerAsync();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to create directory:" + Path.Combine(Configs.OutPath, Mode) + Environment.NewLine + Environment.NewLine + "Please check if the fonder is read only or protected...", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    OutputSubFolder = Path.Combine(Configs.OutPath, Mode);
                    BackgroundWorker1.RunWorkerAsync();
                }
            }else
                MessageBox.Show("Your osu seems to have 0 songs thats weird..." + Environment.NewLine + Environment.NewLine + "Please put some maps in it or try another installation...", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void cancel()
        {
            Cancel = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Mode == "Complete library")
            {
                BeatmapEntry oldBeatmap = Odb.Beatmaps[0];
                foreach (BeatmapEntry beatmap in Odb.Beatmaps)
                {
                    if (oldBeatmap.BeatmapSetId != beatmap.BeatmapSetId)
                    {
                        extractBeatmap(oldBeatmap);
                    }

                    if (Cancel)
                    {
                        BackgroundWorker1.CancelAsync();
                        viewUpdate(false);
                    }

                    oldBeatmap = beatmap;
                }
                extractBeatmap(oldBeatmap);
            }
            else
            {
                ; //ADDSUPPORT FOR COLLECTIONS
            }
        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ViewPrBar.PerformStep();
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Result = true;
            viewUpdate(false);
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

                File = TagLib.File.Create(output);
                File.Tag.Title = bmp.Title;
                File.Tag.AlbumArtists = bmp.Artist.Split(new char[] { ';' });
                File.Tag.Performers = bmp.Artist.Split(new char[] { ';' });
                File.Tag.Album = ValidMode;
                File.Save();

                if (Configs.IncludeThumbnails)
                {
                    ; //ADD SUPPORT FOR THUMBNAILS
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

        private void viewUpdate(bool isExtracting)
        {
            ViewButtonCancel.Enabled = isExtracting;
            ViewButtonExtract.Enabled = !isExtracting;
            ViewButtonOptions.Enabled = !isExtracting;
            ViewPrBar.Visible = isExtracting;
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

        private bool Cancel { get; set; }
        private string Mode { get; set; }
        private string ValidMode { get; set; }
        private string OutputSubFolder { get; set; }
        private Configurations Configs { get; set; }
        private TagLib.File File { get; set; }
        private TagLib.Picture Pic { get; set; }
        private BackgroundWorker BackgroundWorker1 { get; set; }

        public List<string> Log { get; set; }
        public bool Result { get; set; }
    }
}
