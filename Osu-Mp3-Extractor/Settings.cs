using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_Mp3_Extractor
{
    public class Settings
    {
        //settings
        public Settings(string apppath)
        {
            AppPath = apppath;
        }

        //get; set//
        public string AppPath { get; set; }
    }
}
