using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Osu_Mp3_Extractor
{
    public class SettingsHandler
    {
        private Settings settings;

        public static void New(string AppPath)
        {


        }   //Create all possible configs from apppath and default everything else

        //Methods
        public static void ParseFromTxt()
        {
            //Parse and fill from txt json to Settings OBJ
            if (Exists())
            {

            }

            //settings = new Settings();
        }  //Finish
        public static void ParseToTxt()
        {
            //var jsonSettings = JsonConvert.SerializeObject(settings);
            //System.IO.File.WriteAllText((settings.AppPath + @"\settings.json"), jsonSettings);
        }   //Fill txt json from Settings OBJ
        private static bool Exists()
        {
            bool exists = false; //True for debugging



     
            return exists;
        }   //Finish check when done

        public static Settings Settings { get; set; }
    }
}
