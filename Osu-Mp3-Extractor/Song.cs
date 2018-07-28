using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_Mp3_Extractor
{
    public class Song
    {
        //Constructors//
        public Song(string folderpath, string foldername, string mp3path, string mp3name, string diffpath, string title, string artist, string creator, int code)
        {
            FolderPath = folderpath;
            FolderName = foldername;
            Mp3Path = mp3path;
            Mp3Name = mp3name;
            DiffPath = diffpath;
            Title = title;
            Artist = artist;
            Creator = creator;
            Selected = false;
            Code = code;
        }

        //get; set//
        public string FolderPath { get; set; }
        public string FolderName { get; set; }
        public string Mp3Path { get; set; }
        public string Mp3Name { get; set; }
        public string DiffPath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Creator { get; set; }
        public bool Selected { get; set; }
        public int Code { get; set; }
    }
}
