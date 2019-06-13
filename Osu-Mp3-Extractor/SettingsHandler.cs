using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Osu_Mp3_Extractor
{
    public class SettingsHandler
    {
        //Methods
        public void New(string AppPath)
        {
            //Set everything to default values
            settings.AppPath = "";


            Settings settings = new Settings(AppPath);


            WriteToJson(settings);
        }   //Create all possible configs from apppath and default everything else
        public void ReadFromJson()
        {
            if (Exists((MainForm.AppPath + @"\settings.json")))
            {





            }
            else
                New((MainForm.AppPath + @"\settings.json"));
        }  //Parse and fill from txt json to Settings OBJ
        public void WriteToJson(Settings settings)
        {
            var jsonSettings = JsonConvert.SerializeObject(settings);
            System.IO.File.WriteAllText((settings.AppPath + @"\settings.json"), jsonSettings);
        }   //Fill txt json from Settings OBJ
        private bool Exists(string jsonpath)
        {
            if (File.Exists(jsonpath))
                return true;
            else
                return false;
        }   //Exists

        public Settings Settings { get; set; }
    }
}
