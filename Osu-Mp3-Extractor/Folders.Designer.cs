namespace Osu_Mp3_Extractor
{
    partial class Folders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Folders));
            this.browseButton1 = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.outputfolderTextBox = new System.Windows.Forms.TextBox();
            this.songsfolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browseButton1
            // 
            this.browseButton1.Location = new System.Drawing.Point(354, 38);
            this.browseButton1.Name = "browseButton1";
            this.browseButton1.Size = new System.Drawing.Size(75, 20);
            this.browseButton1.TabIndex = 0;
            this.browseButton1.Text = "Browse...";
            this.browseButton1.UseVisualStyleBackColor = true;
            this.browseButton1.Click += new System.EventHandler(this.browseButton1_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(354, 12);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 20);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outputfolderTextBox
            // 
            this.outputfolderTextBox.Location = new System.Drawing.Point(83, 12);
            this.outputfolderTextBox.Name = "outputfolderTextBox";
            this.outputfolderTextBox.ReadOnly = true;
            this.outputfolderTextBox.Size = new System.Drawing.Size(265, 20);
            this.outputfolderTextBox.TabIndex = 2;
            this.outputfolderTextBox.Text = "C:\\...\\Mp3output";
            // 
            // songsfolderTextBox
            // 
            this.songsfolderTextBox.Location = new System.Drawing.Point(83, 38);
            this.songsfolderTextBox.Name = "songsfolderTextBox";
            this.songsfolderTextBox.ReadOnly = true;
            this.songsfolderTextBox.Size = new System.Drawing.Size(265, 20);
            this.songsfolderTextBox.TabIndex = 3;
            this.songsfolderTextBox.Text = "C:\\...\\osu!\\Songs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Output Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Songs Folder";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(435, 16);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(48, 33);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // Folders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 67);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.songsfolderTextBox);
            this.Controls.Add(this.outputfolderTextBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.browseButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Folders";
            this.Text = "Directories";
            this.Load += new System.EventHandler(this.Folders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton1;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox outputfolderTextBox;
        private System.Windows.Forms.TextBox songsfolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceptButton;
    }
}