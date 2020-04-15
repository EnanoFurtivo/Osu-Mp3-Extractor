using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;

namespace View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            string[] spstring = Application.ExecutablePath.Split(new string[] { "\\osu! mp3 extractor.exe" }, StringSplitOptions.None);
            AppPath = spstring[0];  //appPath

            Configs = new Configurations(AppPath); //Initialize configurations
            if (!Configs.getConfigurations())
            {
                ConfigurationsForm configForm = new ConfigurationsForm();
                configForm.ShowDialog();
                if (configForm.ShouldClose) Environment.Exit(0);

                while (!Configs.updateConfigurations(configForm.Cfg))
                {
                    configForm.Dispose();

                    configForm = new ConfigurationsForm(configForm.Cfg);
                    configForm.ShowDialog();
                    if (configForm.ShouldClose) Environment.Exit(0);
                }
            }
            
            outputLabelData.Text = Configs.OutPath;
            osuLabelData.Text = Configs.OsuPath;

            extractor = new Extractor(this.progressBar, this.extractButton, this.optionsButton, this.cancelButton);

            Cdb = new CollectionDb();
            Odb = new OsuDb();

            updateComboBox(false);
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            string oldOsuPath = Configs.OsuPath;
            
            ConfigurationsForm configForm = new ConfigurationsForm(Configs.Cfg);
            configForm.ShowDialog();

            while (!Configs.updateConfigurations(configForm.Cfg))
            {
                configForm.Dispose();

                configForm = new ConfigurationsForm(Configs.Cfg);
                configForm.ShowDialog();
            }

            outputLabelData.Text = Configs.OutPath;
            osuLabelData.Text = Configs.OsuPath;

            if (Configs.OsuPath != oldOsuPath)
            {
                extractButton.Enabled = false;
                updateComboBox(true);
            }
        }
        private void extractButton_Click(object sender, EventArgs e)
        {
            extractor.extract(SelectedCollection, Configs, Odb, Cdb);
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            extractor.cancel();
        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            defaultComboboxLabel.Visible = false;
            SelectedCollection = modeComboBox.GetItemText(modeComboBox.SelectedItem);
            extractButton.Enabled = true;
        }

        private void updateComboBox(bool reset)
        {
            readDatabase();
            if (reset) { modeComboBox.Items.Clear(); defaultComboboxLabel.Visible = true; }
            modeComboBox.Items.Add("Complete library");
            foreach (Collection coll in Cdb.Collections)
                if (coll.BeatmapHashes.Count != 0) modeComboBox.Items.Add(coll.Name); //Prevent empty collections from apearing in the combo box
        }
        private void readDatabase()
        {
            Cdb = CollectionDb.Read(Configs.CollectionDbPath);
            Odb = OsuDb.Read(Configs.OsuDbPath);
        }

        private Extractor extractor { get; set; }
        private string SelectedCollection { get; set; }
        private OsuDb Odb { get; set; }
        private CollectionDb Cdb { get; set; }
        private Configurations Configs { get; set; }
        private string AppPath { get; set; }
    }
}
