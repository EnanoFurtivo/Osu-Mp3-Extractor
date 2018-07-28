using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Osu_Mp3_Extractor
{
    public partial class MainForm
    {
        //Definitions//
        //string outputPath = "";
        //string songsPath = "";
        //bool songsPathCheckResult = false;
        bool filtered = true;
        List<Song> SongsFiltered;
        GetSongs songsext;

        //DEBUG
        bool songsPathCheckResult = true;
        string outputPath = @"C:\Users\Larcho\Desktop\Mp3Output";
        string songsPath = @"C:\Users\Larcho\Documents\osu!\Songs";

        //Methods
        private void FillSongsList()
        {
            songsext = new GetSongs(songsPath);
            if (outputPath != "" && songsPath != "" && songsPathCheckResult)
            {
                PrintSongsList();
            }
            else if (outputPath == "" || songsPath == "")
            {
                SetFolder();
                PrintSongsList();
            }
            else if (songsPathCheckResult == false)
            {
                MessageBox.Show("The program was unable to find any Songs inside the provided folder: " + songsPath + Environment.NewLine + Environment.NewLine + "Please select a valid songs folder or add some songs to your osu game if its empty", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetFolder();
                PrintSongsList();
            }
            filtered = true;
        }
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
            filtered = false;
        }
        private void SetFolder()
        {
            Folders folders = new Folders(outputPath, songsPath);
            folders.ShowDialog();
            songsPath = folders.SongsPath;
            outputPath = folders.OutputPath;
        }
    }
}
