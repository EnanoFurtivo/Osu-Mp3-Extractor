using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_Mp3_Extractor
{
    public class Song : IComparable
    {
        //IComparer Implementation
        private class sortSongsAlphabetDescendingHelper : IComparer
        {
            public int Compare(object a, object b)
            {
                Song s1 = (Song)a;
                Song s2 = (Song)b;
                if (s1.Title.CompareTo(s2.Title) > 0)
                {
                    return -1;
                }
                if (s1.Title.CompareTo(s2.Title) < 0)
                {
                    return 1;
                }
                return 0;
            }
        }
        public static IComparer sortSongsAlphabetDescending()
        {
            return (IComparer) new sortSongsAlphabetDescendingHelper();
        }
        int IComparable.CompareTo(object obj)
        {
            Song s = (Song)obj;
            return String.Compare(this.Title, s.Title);
        }

        //Constructors//
        public Song(string folderpath, string foldername, string mp3path, string mp3name, string diffpath, string title, string artist, string creator, int length, int code, string hash, int threadid, int beatmapsetid, int beatmapid, string collection)
        {
            FolderPath = folderpath;
            FolderName = foldername;
            Mp3Path = mp3path;
            Mp3Name = mp3name;
            DiffPath = diffpath;
            Title = title;
            Artist = artist;
            Creator = creator;
            Length = length;
            Selected = false;
            Code = code;
            Hash = hash;
            ThreadId = threadid;
            BeatmapSetId = beatmapsetid;
            BeatmapId = beatmapid;
            Collection = collection;
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
        public int Length { get; set; }
        public bool Selected { get; set; }
        public int Code { get; set; }
        public string Hash { get; set; }
        public int ThreadId { get; set; }
        public int BeatmapSetId { get; set; }
        public int BeatmapId { get; set; }
        public string Collection { get; set; }
    }
}
