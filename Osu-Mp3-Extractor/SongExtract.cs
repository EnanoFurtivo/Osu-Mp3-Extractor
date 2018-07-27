using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_Mp3_Extractor
{
    class SongExtract
    {
        private readonly List<Song> songsList = new List<Song>();

        public SongExtract(string outputPath, string searchFilter)
        {
            if(searchFilter == "Serach by title or artist")
            {

            }
            else
            {

            }
        }
        
        public IList<Song> SongsList { get { return songsList; } }
    }
}
