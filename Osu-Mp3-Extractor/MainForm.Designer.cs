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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.extractButton = new System.Windows.Forms.Button();
            this.extractqueueListBox = new System.Windows.Forms.ListBox();
            this.songsListBox = new System.Windows.Forms.ListBox();
            this.specsongsPictureBox = new System.Windows.Forms.PictureBox();
            this.folderButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.artistTextBox = new System.Windows.Forms.TextBox();
            this.mapcreatorTextBox = new System.Windows.Forms.TextBox();
            this.lengthTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.specsongsPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // extractButton
            // 
            this.extractButton.Location = new System.Drawing.Point(516, 11);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(300, 21);
            this.extractButton.TabIndex = 5;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // extractqueueListBox
            // 
            this.extractqueueListBox.FormattingEnabled = true;
            this.extractqueueListBox.Location = new System.Drawing.Point(516, 38);
            this.extractqueueListBox.Name = "extractqueueListBox";
            this.extractqueueListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.extractqueueListBox.Size = new System.Drawing.Size(300, 264);
            this.extractqueueListBox.TabIndex = 16;
            // 
            // songsListBox
            // 
            this.songsListBox.FormattingEnabled = true;
            this.songsListBox.Location = new System.Drawing.Point(12, 38);
            this.songsListBox.Name = "songsListBox";
            this.songsListBox.Size = new System.Drawing.Size(300, 264);
            this.songsListBox.TabIndex = 0;
            // 
            // specsongsPictureBox
            // 
            this.specsongsPictureBox.Location = new System.Drawing.Point(318, 11);
            this.specsongsPictureBox.Name = "specsongsPictureBox";
            this.specsongsPictureBox.Size = new System.Drawing.Size(192, 108);
            this.specsongsPictureBox.TabIndex = 2;
            this.specsongsPictureBox.TabStop = false;
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(223, 11);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(89, 21);
            this.folderButton.TabIndex = 3;
            this.folderButton.Text = "Set Folders";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Enabled = false;
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(205, 20);
            this.searchTextBox.TabIndex = 4;
            this.searchTextBox.Text = "Serach by title or artist";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Artist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Map Creator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(318, 139);
            this.titleTextBox.Multiline = true;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.Size = new System.Drawing.Size(192, 46);
            this.titleTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Length";
            // 
            // artistTextBox
            // 
            this.artistTextBox.Location = new System.Drawing.Point(318, 204);
            this.artistTextBox.Name = "artistTextBox";
            this.artistTextBox.ReadOnly = true;
            this.artistTextBox.Size = new System.Drawing.Size(93, 20);
            this.artistTextBox.TabIndex = 11;
            // 
            // mapcreatorTextBox
            // 
            this.mapcreatorTextBox.Location = new System.Drawing.Point(417, 204);
            this.mapcreatorTextBox.Name = "mapcreatorTextBox";
            this.mapcreatorTextBox.ReadOnly = true;
            this.mapcreatorTextBox.Size = new System.Drawing.Size(93, 20);
            this.mapcreatorTextBox.TabIndex = 13;
            // 
            // lengthTextBox
            // 
            this.lengthTextBox.Location = new System.Drawing.Point(318, 243);
            this.lengthTextBox.Name = "lengthTextBox";
            this.lengthTextBox.ReadOnly = true;
            this.lengthTextBox.Size = new System.Drawing.Size(93, 20);
            this.lengthTextBox.TabIndex = 14;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(318, 269);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(192, 33);
            this.addButton.TabIndex = 15;
            this.addButton.Text = "Add to extract queue";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 314);
            this.Controls.Add(this.extractqueueListBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.lengthTextBox);
            this.Controls.Add(this.mapcreatorTextBox);
            this.Controls.Add(this.artistTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.specsongsPictureBox);
            this.Controls.Add(this.songsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Osu Mp3 Extractor";
            ((System.ComponentModel.ISupportInitialize)(this.specsongsPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.ListBox extractqueueListBox;
        private System.Windows.Forms.ListBox songsListBox;
        private System.Windows.Forms.PictureBox specsongsPictureBox;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox artistTextBox;
        private System.Windows.Forms.TextBox mapcreatorTextBox;
        private System.Windows.Forms.TextBox lengthTextBox;
        private System.Windows.Forms.Button addButton;
    }
}