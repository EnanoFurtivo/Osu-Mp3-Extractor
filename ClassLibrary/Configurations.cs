using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ClassLibrary
{
    public class Configurations
    {
        //constructor
        public Configurations(string appPathIn) 
        {
            AppPath = appPathIn; //generate useful stuff for later
            CfgPath = Path.Combine(AppPath, "Config.ini");
            LogPath = Path.Combine(AppPath, "Log.txt");

            Cfg = new Dictionary<string, string>() //Generate default settings dictionary
            {
                {"output path", string.Empty},
                {"osu! path", string.Empty},
                {"include image", string.Empty},
                {"overwrite album", string.Empty},
                {"overwrite artist", string.Empty},
                {"overwrite title", string.Empty},
                {"force album", string.Empty},
                {"force artist", string.Empty},
                {"force title", string.Empty}
            };
        }

        //Public metohds
        public bool updateConfigurations(Dictionary<string, string> cfgIn) //Receives new cfg dict and tries to update | returns true if succesful / flase if not
        {
            bool result = true;
            if (checkConfigurations(cfgIn))
            {
                Cfg = cfgIn;
                saveConfigurations();
                updatePublicProperties();
            }
            else
                result = false;
            return result;
        }
        public bool getConfigurations() //Reads configs form .ini file and stores in Cfg | returns true if succesful / false if not
        {
            bool result = true;
            if (System.IO.File.Exists(CfgPath))
            {
                Dictionary<string, string> cfgCopy = new Dictionary<string, string>(Cfg);
                string line;
                int i = 0;

                using (StreamReader sr = new StreamReader(CfgPath))
                {
                    while (!string.IsNullOrWhiteSpace(line = sr.ReadLine())) //Hasta que haya un espacio entre secciones
                    {
                        if (line.Contains('='))
                        {
                            try
                            {
                                //Exclude " = " from the dictionary
                                int j = line.IndexOf('=');
                                string key = line.Substring(0, j - 1);
                                string value = line.Substring(j + 2);

                                foreach (System.Collections.Generic.KeyValuePair<string, string> item in Cfg)
                                {
                                    if (item.Key == key && value != string.Empty && value != null)
                                    {
                                        cfgCopy[key] = value;
                                        i++;
                                    }
                                }
                            }
                            catch (Exception) { };
                        }
                    }
                }

                if (i != cfgCopy.Count) //return false
                    result = false;
                else if (!checkConfigurations(cfgCopy))
                    result = false;
                else
                {
                    Cfg = cfgCopy;
                    updatePublicProperties();
                }
            }
            else
                result = false;

            return result;
        }

        //Private metohds
        private void saveConfigurations()
        {
            if (File.Exists(CfgPath)) File.Delete(CfgPath); //delete previous file for overwriting
            using (StreamWriter sw = new StreamWriter(CfgPath)) //Create txt to writ to
                foreach (System.Collections.Generic.KeyValuePair<string, string> item in Cfg) //write all default configurations down into the file
                    sw.WriteLine(item.Key + " = " + item.Value); //write each line individually
        }
        private bool checkConfigurations(Dictionary<string, string> cfgIn) //checks if the dictionary received is OK.
        {
            return (checkOsuPath(cfgIn["osu! path"]) && checkOutPath(cfgIn["output path"]));
        }
        private bool checkOutPath(string outPath)
        {
            bool result = false;
            if (outPath == string.Empty)
                MessageBox.Show("Opps!, output path seems to be invalid, please select a new one...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                if (Directory.Exists(outPath))
                    result = true;
            return result;
        }
        private bool checkOsuPath(string osuPath)
        {
            bool result = false;
            if (osuPath == string.Empty)
                MessageBox.Show("Opps!, osu path seems to be invalid, please select a new one...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                string osuDb = string.Empty;
                string collectionDb = string.Empty;
                string songsPath = string.Empty;

                if (!Directory.Exists(osuPath)) //Error no osu! folder 
                    MessageBox.Show("It seems like your osu! folder has been renamed, moved or deleted. Please restore it or select a new one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    foreach (string subfolder in Directory.GetDirectories(osuPath))
                        if (subfolder == Path.Combine(osuPath, "Songs"))
                            songsPath = subfolder;

                    if (songsPath == "") //Eror no song folder in osu! folder
                        MessageBox.Show("That was weird. Your osu! folder doesnt contain a Songs folder, try selecting another one or getting yourself some songs if you dont have any", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        foreach (string subfile in Directory.GetFiles(osuPath))
                        {
                            if (subfile == Path.Combine(osuPath, "osu!.db"))
                                osuDb = subfile;
                            else if (subfile == Path.Combine(osuPath, "collection.db"))
                                collectionDb = subfile;
                        }

                        if (osuDb == string.Empty && collectionDb == string.Empty)
                            MessageBox.Show("That was really weird. Your osu! folder doesnt contain neither osu!.db or collections.db files, try selecting another osu! installation or getting yourself a new one. Opening the game should fix it tho LOL", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else if (osuDb == "")
                            MessageBox.Show("That was really weird. Your osu! folder doesnt contain osu!.db file, try selecting another osu! installation or getting yourself a new one. Opening the game should fix it tho LOL", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else if (collectionDb == "")
                            MessageBox.Show("That was really weird. Your osu! folder doesnt contain collections.db file, try selecting another osu! installation or getting yourself a new one. Opening the game should fix it tho LOL", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                            result = true;
                    }
                }
            }

            return result;
        }

        private void updatePublicProperties()
        {
            if (Cfg["include image"] == "true") IncludeThumbnails = true; else IncludeThumbnails = false;
            if (Cfg["overwrite album"] == "true") OverwriteAlbum = true; else OverwriteAlbum = false;
            if (Cfg["overwrite artist"] == "true") OverwriteArtist = true; else OverwriteArtist = false;
            if (Cfg["overwrite title"] == "true") OverwriteTitle = true; else OverwriteTitle = false;
            if (Cfg["force album"] == "true") ForceAlbum = true; else ForceAlbum = false;
            if (Cfg["force artist"] == "true") ForceArtist = true; else ForceArtist = false;
            if (Cfg["force title"] == "true") ForceTitle = true; else ForceTitle = false;

            OutPath = Cfg["output path"];
            OsuPath = Cfg["osu! path"];
            OsuDbPath = Path.Combine(Cfg["osu! path"], "osu!.db");
            CollectionDbPath = Path.Combine(Cfg["osu! path"], "collection.db");
            SongsPath = Path.Combine(Cfg["osu! path"], "Songs");
        }
    
        //Private management
        private string CfgPath { get; set; }
        public string AppPath { get; set; }
        public string LogPath{ get; set; }

        //Actual properties 
        public Dictionary<string, string> Cfg { get; set; }
        public string OsuDbPath { get; set; }
        public string CollectionDbPath { get; set; }
        public string SongsPath { get; set; }
        public string OsuPath { get; set; }
        public string OutPath { get; set; }

        public bool IncludeThumbnails { get; set; }
        public bool OverwriteAlbum { get; set; }
        public bool OverwriteArtist { get; set; }
        public bool OverwriteTitle { get; set; }
        public bool ForceAlbum { get; set; }
        public bool ForceArtist { get; set; }
        public bool ForceTitle { get; set; }
    }
}