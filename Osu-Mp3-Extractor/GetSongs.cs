using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagLib;

namespace Osu_Mp3_Extractor
{
    class GetSongs
    {
        private readonly List<Song> songsList = new List<Song>();

        //RegexParameters//
        private string osuparam = @".*\.osu";
        private string mp3nameparam = @"AudioFilename\: (.*\.mp3)$";
        private string titleparam = @"Title\:(.*)";
        private string artistparam = @"Artist\:(.*)";
        private string creatorparam = @"Creator\:(.*)";
        private string imageparam = @"([0,9]+\,[0,9]+)\,\""([^\""]+)\"".*";

        //Constructors//
        public GetSongs(string songsPath)
        {
            ExtractSongs(songsPath);
        }

        //Methods//
        private void ExtractSongs(string songsPath)
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
        }

        //Get; Set;
        public IList<Song> SongsList { get { return songsList; } }
    }
}
