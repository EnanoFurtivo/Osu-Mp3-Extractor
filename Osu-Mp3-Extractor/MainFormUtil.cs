using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_Mp3_Extractor
{
    public partial class MainForm
    {
        //Definitions//
        string outputPath = "";
        string songsPath = "";
        string searchString = "";
        //bool songsPathCheckResult = false;
        bool songsPathCheckResult = true;    //DEBUG

        //Methods
        private void FillSongsList()
        {
            if (outputPath != "" && songsPath != "" && songsPathCheckResult)
            {
                SongExtract songsext = new SongExtract(songsPath, searchString);
                PrintSongsList(songsext);
            }
            else
            {
                SetFolder();
            }
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
