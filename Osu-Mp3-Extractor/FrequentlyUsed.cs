using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;

namespace Osu_Mp3_Extractor
{
    public class FrequentlyUsed : IDisposable
    {
        //IDisposable//
        public void Dispose()
        {

        }//adds Dispose Capabilities
        ~FrequentlyUsed()
        {

        }//Clean up unmanaged resources in case users of your library don't call 'Dispose'.

        //Basic//
        public string seekFolder()
        {
            FolderBrowserDialog search = new FolderBrowserDialog();
            if (search.ShowDialog() == DialogResult.OK)
            {
                string outputVar = search.SelectedPath;
                return outputVar;
            }
            else { return ""; }
        }   //Browse folder function
    }
}
