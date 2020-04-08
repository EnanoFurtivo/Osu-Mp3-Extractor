using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osu_Mp3_Extractor
{
    public partial class CollectionSelection : Form
    {
        /*public struct Config
        {
            public string appPath;
            public string cfgPath;
            public string errPath;
            public string imgPath;
            public string expPath;
            public string osuPath;
            public bool includeThumbnails;
        }*/

        public CollectionSelection()
        {
            InitializeComponent();

            mybox.Items.Add(230);


        }

        private void CollectionSelection_Load(object sender, EventArgs e)
        {
            optionsComboBox.Enabled = true;
            foreach (Collection c in cdb.Collections)
            {
                optionsComboBox.Items.Add(c.Name);

            }

            if (mode == "Extract an entire collection")
            {
                if (optionsComboBox.GetItemText(optionsComboBox.SelectedItem) != "Collections")
                {
                    selectedCollection = optionsComboBox.GetItemText(optionsComboBox.SelectedItem);
                    CompleteCollection();
                }
                else
                    MessageBox.Show("Please select a collection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void changePaths_Click(object sender, EventArgs e)
        {

        }
    }
}
