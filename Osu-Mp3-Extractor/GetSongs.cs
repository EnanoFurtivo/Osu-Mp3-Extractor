using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagLib;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;

namespace Osu_Mp3_Extractor
{
    class GetSongs
    {
        private readonly List<Song> songsList = new List<Song>();
        private List<Song> songsListTemp = new List<Song>();
        private List<Song> songsThreadIdList = new List<Song>();
        private List<Song> songsListTemp3 = new List<Song>();
        CollectionDb cdb;
        OsuDb odb;

        //RegexParameters//
        /*private string osuparam = @".*\.osu";
        private string mp3nameparam = @"AudioFilename\: (.*\.mp3)$";
        private string titleparam = @"Title\:(.*)";
        private string artistparam = @"Artist\:(.*)";
        private string creatorparam = @"Creator\:(.*)";
        private string imageparam = @"([0,9]+\,[0,9]+)\,\""([^\""]+)\"".*";*/

        //Constructors//
        /*public GetSongs(string songsPath)
        {
            ExtractSongs(songsPath);
        }*/
        public GetSongs(string songsPath, string osuDb, string collectionDb, string collectionName)
        {
            ExtractSongs(songsPath, osuDb, collectionDb, collectionName);
        }
        public GetSongs(string songsPath, string osuDb)
        {
            ExtractSongs(songsPath, osuDb);
        }

        //Methods//
        /*private void ExtractSongs(string songsPath)
        {
            int code = 0;
            foreach (string subfolder in Directory.GetDirectories(songsPath))
            {
                string diffpath = "";
                string foldername = "";
                string folderpath = "";
                string mp3name = "";
                string mp3path = "";
                string title = "";
                string artist = "";
                string creator = "";
                string imagepath = "";
                string imagename = "";

                if (subfolder != "")
                {
                    /// foldername, folderpath ///
                    string[] spstring = subfolder.Split(new string[] { "\\Songs\\" }, StringSplitOptions.None);
                    foldername = spstring[1];   //foldername
                    folderpath = subfolder;    //folderpath

                    if (foldername != "" && folderpath != "")
                    {
                        /// diffpath ///
                        List<string> osufiles = new List<string>();
                        string text = "";
                        foreach (string file in Directory.GetFiles(subfolder))
                        {
                            string[] spfile = file.Split(new string[] { foldername + "\\" }, StringSplitOptions.None);
                            text = spfile[1];

                            Regex r1 = new Regex(osuparam, RegexOptions.IgnoreCase);
                            Match m1 = r1.Match(text);
                            if (m1.Success)
                            {
                                osufiles.Add(file);
                            }
                        }//filters files that end with ".osu" inside of each song subfolder
                        if (osufiles.Count() > 0)
                            diffpath = osufiles[0];

                        if (diffpath != "")
                        {
                            /// mp3name, mp3path, title, artist, creator ///
                            int lineNumber = 0;
                            string[] lines = System.IO.File.ReadAllLines(diffpath); //takes .osu file and transform into an array of strings
                            foreach (string line in lines)
                            {
                                string text1 = lines[lineNumber];

                                /// mp3name, mp3path ///
                                Regex r1 = new Regex(mp3nameparam, RegexOptions.IgnoreCase);
                                Match m1 = r1.Match(text1);
                                if (m1.Success)
                                {
                                    mp3name = m1.Groups[1].Value;   //mp3name
                                    mp3path = subfolder + "\\" + mp3name;   //mp3path
                                }

                                /// title ///
                                Regex r2 = new Regex(titleparam, RegexOptions.IgnoreCase);
                                Match m2 = r2.Match(text1);
                                if (m2.Success)
                                {
                                    title = m2.Groups[1].Value; //title
                                }

                                /// artist ///
                                Regex r3 = new Regex(artistparam, RegexOptions.IgnoreCase);
                                Match m3 = r3.Match(text1);
                                if (m3.Success)
                                {
                                    artist = m3.Groups[1].Value;    //artist
                                }

                                /// creator ///
                                Regex r4 = new Regex(creatorparam, RegexOptions.IgnoreCase);
                                Match m4 = r4.Match(text1);
                                if (m4.Success)
                                {
                                    creator = m4.Groups[1].Value; //creator
                                }

                                /// imagepath ///
                                Regex r5 = new Regex(imageparam, RegexOptions.IgnoreCase);
                                Match m5 = r5.Match(text1);
                                if (m5.Success)
                                {
                                    imagename = m5.Groups[2].Value;
                                    imagepath = subfolder + "\\" + imagename;   //imagepath
                                    break;
                                }

                                /// exit while ///
                                if (text1 == "[TimingPoints]")
                                {
                                    break;
                                }
                                lineNumber++;
                            } //filters out the variables using regex on each line
                            
                            if (mp3path != "" && mp3name != "" && title != "" && artist != "" && creator != "")
                            {
                                Song song = new Song(folderpath, foldername, mp3path, mp3name, diffpath, title, artist, creator, imagename, imagepath, code);
                                songsList.Add(song);
                                code++;
                            }/// Create Object///
                        }/// mp3name, mp3path, title, artist ///
                    }/// diffpath ///
                }/// foldername, folderpath ///
            }/// subfolder ///
        }*/
        private void ExtractSongs(string songsPath, string osuDb, string collectionDb, string collectionName)
        {
            List<int> threadIds = new List<int>();
            List<int> threadIds2 = new List<int>();
            cdb = CollectionDb.Read(collectionDb);
            odb = OsuDb.Read(osuDb);
            int code = 0;
            int code2 = 0;
            bool band = true;
            bool band2 = true;

            foreach (BeatmapEntry bm in odb.Beatmaps)
            {
                band = true;

                foreach (int id in threadIds)
                {
                    if (id == bm.ThreadId)
                    {
                        band = false;
                    }
                }

                if (band)
                {
                    string diffpath = songsPath + "\\" + bm.FolderName + "\\" + bm.BeatmapFileName;
                    string foldername = bm.FolderName;
                    string folderpath = songsPath + "\\" + bm.FolderName;
                    string mp3name = bm.AudioFileName;
                    string mp3path = songsPath + "\\" + bm.FolderName + "\\" + bm.AudioFileName;
                    string title = bm.Title;
                    string artist = bm.Artist;
                    string creator = bm.Creator;
                    int length = bm.TotalTime;
                    string hash = bm.BeatmapChecksum;
                    int threadid = bm.ThreadId;
                    int beatmapsetid = bm.BeatmapSetId;
                    int beatmapid = bm.BeatmapId;

                    Song song = new Song(folderpath, foldername, mp3path, mp3name, diffpath, title, artist, creator, length, code, hash, threadid, beatmapsetid, beatmapid, "osu!");
                    songsListTemp3.Add(song);
                    threadIds.Add(bm.ThreadId);
                    code++;
                }
            } //Filtered map directory to SongsListTemp3
            band = true;

            foreach (BeatmapEntry bm in odb.Beatmaps)
            {
                if (band)
                {
                    string diffpath = songsPath + "\\" + bm.FolderName + "\\" + bm.BeatmapFileName;
                    string foldername = bm.FolderName;
                    string folderpath = songsPath + "\\" + bm.FolderName;
                    string mp3name = bm.AudioFileName;
                    string mp3path = songsPath + "\\" + bm.FolderName + "\\" + bm.AudioFileName;
                    string title = bm.Title;
                    string artist = bm.Artist;
                    string creator = bm.Creator;
                    int length = bm.TotalTime;
                    string hash = bm.BeatmapChecksum;
                    int threadid = bm.ThreadId;
                    int beatmapsetid = bm.BeatmapSetId;
                    int beatmapid = bm.BeatmapId;

                    Song song = new Song(folderpath, foldername, mp3path, mp3name, diffpath, title, artist, creator, length, code2, hash, threadid, beatmapsetid, beatmapid, "osu!");
                    songsListTemp.Add(song);
                    code2++;
                }
            } //Complete map directory to SongsListTemp

            foreach (Collection c in cdb.Collections)
            {
                if (c.Name == collectionName)
                {
                    foreach (string bh in c.BeatmapHashes)
                    {
                        foreach (Song song3 in songsListTemp)
                        {
                            if (song3.Hash == bh)
                            {
                                foreach (Song song2 in songsListTemp3)
                                {
                                    if (song3.ThreadId == song2.ThreadId)
                                    {
                                        band2 = true;

                                        foreach (int id in threadIds2)
                                        {
                                            if (id == song2.ThreadId)
                                            {
                                                band2 = false;
                                            }
                                        }

                                        if (band2)
                                        {
                                            string diffpath = song2.DiffPath;
                                            string foldername = song2.FolderName;
                                            string folderpath = song2.FolderPath;
                                            string mp3name = song2.Mp3Name;
                                            string mp3path = song2.Mp3Path;
                                            string title = song2.Title;
                                            string artist = song2.Artist;
                                            string creator = song2.Creator;
                                            int length = song2.Length;
                                            string hash = song2.Hash;
                                            int code3 = song2.Code;
                                            int threadid = song2.ThreadId;
                                            int beatmapsetid = song2.BeatmapSetId;
                                            int beatmapid = song2.BeatmapId;

                                            Song song = new Song(folderpath, foldername, mp3path, mp3name, diffpath, title, artist, creator, length, code3, hash, threadid, beatmapsetid, beatmapid, "osu!");
                                            songsList.Add(song);
                                            threadIds2.Add(song2.ThreadId);
                                        }
                                    }
                                }
                                band2 = true;
                            }
                        }
                    }
                }
            }
        }
        private void ExtractSongs(string songsPath, string osuDb)
        {
            List<int> threadIds = new List<int>();
            odb = OsuDb.Read(osuDb);
            int code = 0;
            bool band = true;

            foreach (BeatmapEntry bm in odb.Beatmaps)
            {
                band = true;

                foreach (int id in threadIds)
                {
                    if (id == bm.ThreadId)
                    {
                        band = false;
                    }
                }

                if (band)
                {
                    string diffpath = songsPath + "\\" + bm.FolderName + "\\" + bm.BeatmapFileName;
                    string foldername = bm.FolderName;
                    string folderpath = songsPath + "\\" + bm.FolderName;
                    string mp3name = bm.AudioFileName;
                    string mp3path = songsPath + "\\" + bm.FolderName + "\\" + bm.AudioFileName;
                    string title = bm.Title;
                    string artist = bm.Artist;
                    string creator = bm.Creator;
                    int length = bm.TotalTime;
                    string hash = bm.BeatmapChecksum;
                    int threadid = bm.ThreadId;
                    int beatmapsetid = bm.BeatmapSetId;
                    int beatmapid = bm.BeatmapId;

                    Song song = new Song(folderpath, foldername, mp3path, mp3name, diffpath, title, artist, creator, length, code, hash, threadid, beatmapsetid, beatmapid, "osu!");
                    songsList.Add(song);
                    threadIds.Add(bm.ThreadId);
                    code++;
                }
            }
            band = true;
        }

        //Get; Set;
        public List<Song> SongsList { get { return songsList; } }
    }
}
