namespace Osu_Mp3_Extractor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.folderButton = new System.Windows.Forms.Button();
            this.extractButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.songsListBox = new System.Windows.Forms.ListBox();
            this.specsongsPictureBox = new System.Windows.Forms.PictureBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.artistTextBox = new System.Windows.Forms.TextBox();
            this.mapcreatorTextBox = new System.Windows.Forms.TextBox();
            this.lengthTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.extractqueueListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.selectedTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.addallButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.hideButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.specsongsPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Osu Mp3 Extractor";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(318, 12);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(89, 21);
            this.folderButton.TabIndex = 3;
            this.folderButton.Text = "Change Folders";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // extractButton
            // 
            this.extractButton.Enabled = false;
            this.extractButton.Location = new System.Drawing.Point(413, 12);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(97, 21);
            this.extractButton.TabIndex = 5;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(804, 21);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 21;
            this.progressBar1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(300, 21);
            this.comboBox1.TabIndex = 28;
            this.comboBox1.Text = "Select extraction method";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(516, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(300, 21);
            this.comboBox2.TabIndex = 29;
            this.comboBox2.Text = "Collections";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // songsListBox
            // 
            this.songsListBox.FormattingEnabled = true;
            this.songsListBox.Location = new System.Drawing.Point(0, 27);
            this.songsListBox.Name = "songsListBox";
            this.songsListBox.Size = new System.Drawing.Size(300, 264);
            this.songsListBox.TabIndex = 0;
            this.songsListBox.SelectedIndexChanged += new System.EventHandler(this.songsListBox_SelectedIndexChanged);
            this.songsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.songsListBox_KeyPress);
            // 
            // specsongsPictureBox
            // 
            this.specsongsPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.specsongsPictureBox.Location = new System.Drawing.Point(306, 0);
            this.specsongsPictureBox.Name = "specsongsPictureBox";
            this.specsongsPictureBox.Size = new System.Drawing.Size(192, 108);
            this.specsongsPictureBox.TabIndex = 2;
            this.specsongsPictureBox.TabStop = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Enabled = false;
            this.searchTextBox.Location = new System.Drawing.Point(0, 1);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(300, 20);
            this.searchTextBox.TabIndex = 4;
            this.searchTextBox.Text = "Serach by title, artist or Map creator";
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTextBox.Enter += new System.EventHandler(this.searchTextBox_Enter);
            this.searchTextBox.Leave += new System.EventHandler(this.searchTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Artist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Map Creator";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.titleTextBox.Location = new System.Drawing.Point(306, 128);
            this.titleTextBox.Multiline = true;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.Size = new System.Drawing.Size(192, 33);
            this.titleTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(303, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estimated Length";
            // 
            // artistTextBox
            // 
            this.artistTextBox.Location = new System.Drawing.Point(306, 180);
            this.artistTextBox.Multiline = true;
            this.artistTextBox.Name = "artistTextBox";
            this.artistTextBox.ReadOnly = true;
            this.artistTextBox.Size = new System.Drawing.Size(93, 33);
            this.artistTextBox.TabIndex = 11;
            // 
            // mapcreatorTextBox
            // 
            this.mapcreatorTextBox.Location = new System.Drawing.Point(405, 180);
            this.mapcreatorTextBox.Multiline = true;
            this.mapcreatorTextBox.Name = "mapcreatorTextBox";
            this.mapcreatorTextBox.ReadOnly = true;
            this.mapcreatorTextBox.Size = new System.Drawing.Size(93, 33);
            this.mapcreatorTextBox.TabIndex = 13;
            // 
            // lengthTextBox
            // 
            this.lengthTextBox.Location = new System.Drawing.Point(306, 232);
            this.lengthTextBox.Name = "lengthTextBox";
            this.lengthTextBox.ReadOnly = true;
            this.lengthTextBox.Size = new System.Drawing.Size(93, 20);
            this.lengthTextBox.TabIndex = 14;
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(306, 258);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(192, 33);
            this.addButton.TabIndex = 15;
            this.addButton.Text = "Add to extract queue";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // extractqueueListBox
            // 
            this.extractqueueListBox.FormattingEnabled = true;
            this.extractqueueListBox.Location = new System.Drawing.Point(504, 27);
            this.extractqueueListBox.Name = "extractqueueListBox";
            this.extractqueueListBox.Size = new System.Drawing.Size(300, 264);
            this.extractqueueListBox.TabIndex = 16;
            this.extractqueueListBox.SelectedIndexChanged += new System.EventHandler(this.extractqueueListBox_SelectedIndexChanged);
            this.extractqueueListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.extractqueueListBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(402, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Selected";
            // 
            // selectedTextBox
            // 
            this.selectedTextBox.Location = new System.Drawing.Point(405, 232);
            this.selectedTextBox.Name = "selectedTextBox";
            this.selectedTextBox.ReadOnly = true;
            this.selectedTextBox.Size = new System.Drawing.Size(93, 20);
            this.selectedTextBox.TabIndex = 18;
            // 
            // clearButton
            // 
            this.clearButton.Enabled = false;
            this.clearButton.Location = new System.Drawing.Point(653, 0);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(148, 23);
            this.clearButton.TabIndex = 19;
            this.clearButton.Text = "Clear Queue";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Title";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 25;
            // 
            // addallButton
            // 
            this.addallButton.Enabled = false;
            this.addallButton.Location = new System.Drawing.Point(504, 0);
            this.addallButton.Name = "addallButton";
            this.addallButton.Size = new System.Drawing.Size(143, 22);
            this.addallButton.TabIndex = 20;
            this.addallButton.Text = "Add All";
            this.addallButton.UseVisualStyleBackColor = true;
            this.addallButton.Click += new System.EventHandler(this.addallButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 26;
            // 
            // hideButton
            // 
            this.hideButton.Enabled = false;
            this.hideButton.Location = new System.Drawing.Point(348, 44);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(103, 21);
            this.hideButton.TabIndex = 27;
            this.hideButton.Text = "Hide";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Visible = false;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hideButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.addallButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.selectedTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.extractqueueListBox);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.lengthTextBox);
            this.panel1.Controls.Add(this.mapcreatorTextBox);
            this.panel1.Controls.Add(this.artistTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.titleTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.searchTextBox);
            this.panel1.Controls.Add(this.specsongsPictureBox);
            this.panel1.Controls.Add(this.songsListBox);
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 297);
            this.panel1.TabIndex = 30;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 340);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.extractButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Osu Mp3 Extractor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.specsongsPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ListBox songsListBox;
        private System.Windows.Forms.PictureBox specsongsPictureBox;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox artistTextBox;
        private System.Windows.Forms.TextBox mapcreatorTextBox;
        private System.Windows.Forms.TextBox lengthTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox extractqueueListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox selectedTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button addallButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Panel panel1;
    }
}