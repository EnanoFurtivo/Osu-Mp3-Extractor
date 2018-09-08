using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

        //Methods//
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
        public Bitmap ResizeImage(string fileName, int width, int height)
        {
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                Bitmap bitmap = new Bitmap(image, width, height);
                return bitmap;
            }
        }   //converts file to bitmap
        public Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap bitmap = new Bitmap(image, width, height);
            return bitmap;
        }   //converts file to bitmap
        public Bitmap ResizeImage(string fileName)
        {
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                Bitmap bitmap = new Bitmap(image);
                return bitmap;
            }
        }   //converts file to bitmap
        public Bitmap ResizeImage(Bitmap image)
        {
            Bitmap bitmap = new Bitmap(image);
            return bitmap;
        }   //converts file to bitmap
    }
}
