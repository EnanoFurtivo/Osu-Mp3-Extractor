namespace View
{
    partial class ConfigurationsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationsForm));
            this.osuBrowseButton = new System.Windows.Forms.Button();
            this.outBrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.includeThumbnailsCheckbox = new System.Windows.Forms.CheckBox();
            this.overwriteAlbumCheckbox = new System.Windows.Forms.CheckBox();
            this.overwriteArtistCheckbox = new System.Windows.Forms.CheckBox();
            this.overwriteTitleCheckbox = new System.Windows.Forms.CheckBox();
            this.forceAlbumCheckbox = new System.Windows.Forms.CheckBox();
            this.forceArtistCheckbox = new System.Windows.Forms.CheckBox();
            this.forceTitleCheckbox = new System.Windows.Forms.CheckBox();
            this.outLabel = new System.Windows.Forms.Label();
            this.osuLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // osuBrowseButton
            // 
            this.osuBrowseButton.BackColor = System.Drawing.Color.Transparent;
            this.osuBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.osuBrowseButton.ForeColor = System.Drawing.SystemColors.Control;
            this.osuBrowseButton.Location = new System.Drawing.Point(360, 33);
            this.osuBrowseButton.Name = "osuBrowseButton";
            this.osuBrowseButton.Size = new System.Drawing.Size(75, 21);
            this.osuBrowseButton.TabIndex = 0;
            this.osuBrowseButton.Text = "Browse...";
            this.osuBrowseButton.UseVisualStyleBackColor = false;
            this.osuBrowseButton.Click += new System.EventHandler(this.osuBrowseButton_Click);
            // 
            // outBrowseButton
            // 
            this.outBrowseButton.BackColor = System.Drawing.Color.Transparent;
            this.outBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.outBrowseButton.ForeColor = System.Drawing.SystemColors.Control;
            this.outBrowseButton.Location = new System.Drawing.Point(360, 7);
            this.outBrowseButton.Name = "outBrowseButton";
            this.outBrowseButton.Size = new System.Drawing.Size(75, 22);
            this.outBrowseButton.TabIndex = 1;
            this.outBrowseButton.Text = "Browse...";
            this.outBrowseButton.UseVisualStyleBackColor = false;
            this.outBrowseButton.Click += new System.EventHandler(this.outBrowseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "output Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "osu! Folder:";
            // 
            // acceptButton
            // 
            this.acceptButton.BackColor = System.Drawing.Color.Transparent;
            this.acceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.acceptButton.ForeColor = System.Drawing.SystemColors.Control;
            this.acceptButton.Location = new System.Drawing.Point(441, 12);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(48, 33);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = false;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // includeThumbnailsCheckbox
            // 
            this.includeThumbnailsCheckbox.AutoSize = true;
            this.includeThumbnailsCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.includeThumbnailsCheckbox.Checked = true;
            this.includeThumbnailsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeThumbnailsCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.includeThumbnailsCheckbox.Location = new System.Drawing.Point(7, 60);
            this.includeThumbnailsCheckbox.Name = "includeThumbnailsCheckbox";
            this.includeThumbnailsCheckbox.Size = new System.Drawing.Size(118, 17);
            this.includeThumbnailsCheckbox.TabIndex = 7;
            this.includeThumbnailsCheckbox.Text = "Include Thumbnails";
            this.includeThumbnailsCheckbox.UseVisualStyleBackColor = false;
            this.includeThumbnailsCheckbox.CheckedChanged += new System.EventHandler(this.includeThumbnailsCheckbox_CheckedChanged);
            // 
            // overwriteAlbumCheckbox
            // 
            this.overwriteAlbumCheckbox.AutoSize = true;
            this.overwriteAlbumCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.overwriteAlbumCheckbox.Checked = true;
            this.overwriteAlbumCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwriteAlbumCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.overwriteAlbumCheckbox.Location = new System.Drawing.Point(131, 60);
            this.overwriteAlbumCheckbox.Name = "overwriteAlbumCheckbox";
            this.overwriteAlbumCheckbox.Size = new System.Drawing.Size(103, 17);
            this.overwriteAlbumCheckbox.TabIndex = 8;
            this.overwriteAlbumCheckbox.Text = "Overwrite Album";
            this.overwriteAlbumCheckbox.UseVisualStyleBackColor = false;
            this.overwriteAlbumCheckbox.CheckedChanged += new System.EventHandler(this.overwriteAlbumCheckbox_CheckedChanged);
            // 
            // overwriteArtistCheckbox
            // 
            this.overwriteArtistCheckbox.AutoSize = true;
            this.overwriteArtistCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.overwriteArtistCheckbox.Checked = true;
            this.overwriteArtistCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwriteArtistCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.overwriteArtistCheckbox.Location = new System.Drawing.Point(240, 60);
            this.overwriteArtistCheckbox.Name = "overwriteArtistCheckbox";
            this.overwriteArtistCheckbox.Size = new System.Drawing.Size(97, 17);
            this.overwriteArtistCheckbox.TabIndex = 9;
            this.overwriteArtistCheckbox.Text = "Overwrite Artist";
            this.overwriteArtistCheckbox.UseVisualStyleBackColor = false;
            this.overwriteArtistCheckbox.CheckedChanged += new System.EventHandler(this.overwriteArtistCheckbox_CheckedChanged);
            // 
            // overwriteTitleCheckbox
            // 
            this.overwriteTitleCheckbox.AutoSize = true;
            this.overwriteTitleCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.overwriteTitleCheckbox.Checked = true;
            this.overwriteTitleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwriteTitleCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.overwriteTitleCheckbox.Location = new System.Drawing.Point(343, 60);
            this.overwriteTitleCheckbox.Name = "overwriteTitleCheckbox";
            this.overwriteTitleCheckbox.Size = new System.Drawing.Size(94, 17);
            this.overwriteTitleCheckbox.TabIndex = 10;
            this.overwriteTitleCheckbox.Text = "Overwrite Title";
            this.overwriteTitleCheckbox.UseVisualStyleBackColor = false;
            this.overwriteTitleCheckbox.CheckedChanged += new System.EventHandler(this.overwriteTitleCheckbox_CheckedChanged);
            // 
            // forceAlbumCheckbox
            // 
            this.forceAlbumCheckbox.AutoSize = true;
            this.forceAlbumCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.forceAlbumCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.forceAlbumCheckbox.Location = new System.Drawing.Point(131, 83);
            this.forceAlbumCheckbox.Name = "forceAlbumCheckbox";
            this.forceAlbumCheckbox.Size = new System.Drawing.Size(85, 17);
            this.forceAlbumCheckbox.TabIndex = 11;
            this.forceAlbumCheckbox.Text = "Force Album";
            this.forceAlbumCheckbox.UseVisualStyleBackColor = false;
            this.forceAlbumCheckbox.CheckedChanged += new System.EventHandler(this.forceAlbumCheckbox_CheckedChanged);
            // 
            // forceArtistCheckbox
            // 
            this.forceArtistCheckbox.AutoSize = true;
            this.forceArtistCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.forceArtistCheckbox.Checked = true;
            this.forceArtistCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.forceArtistCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.forceArtistCheckbox.Location = new System.Drawing.Point(240, 83);
            this.forceArtistCheckbox.Name = "forceArtistCheckbox";
            this.forceArtistCheckbox.Size = new System.Drawing.Size(79, 17);
            this.forceArtistCheckbox.TabIndex = 12;
            this.forceArtistCheckbox.Text = "Force Artist";
            this.forceArtistCheckbox.UseVisualStyleBackColor = false;
            this.forceArtistCheckbox.CheckedChanged += new System.EventHandler(this.forceArtistCheckbox_CheckedChanged);
            // 
            // forceTitleCheckbox
            // 
            this.forceTitleCheckbox.AutoSize = true;
            this.forceTitleCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.forceTitleCheckbox.Checked = true;
            this.forceTitleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.forceTitleCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.forceTitleCheckbox.Location = new System.Drawing.Point(343, 83);
            this.forceTitleCheckbox.Name = "forceTitleCheckbox";
            this.forceTitleCheckbox.Size = new System.Drawing.Size(76, 17);
            this.forceTitleCheckbox.TabIndex = 13;
            this.forceTitleCheckbox.Text = "Force Title";
            this.forceTitleCheckbox.UseVisualStyleBackColor = false;
            this.forceTitleCheckbox.CheckedChanged += new System.EventHandler(this.forceTitleCheckbox_CheckedChanged);
            // 
            // outLabel
            // 
            this.outLabel.BackColor = System.Drawing.Color.Transparent;
            this.outLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.outLabel.Location = new System.Drawing.Point(84, 8);
            this.outLabel.Name = "outLabel";
            this.outLabel.Size = new System.Drawing.Size(270, 20);
            this.outLabel.TabIndex = 14;
            this.outLabel.Text = "Select output folder";
            this.outLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // osuLabel
            // 
            this.osuLabel.BackColor = System.Drawing.Color.Transparent;
            this.osuLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.osuLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.osuLabel.Location = new System.Drawing.Point(74, 34);
            this.osuLabel.Name = "osuLabel";
            this.osuLabel.Size = new System.Drawing.Size(280, 20);
            this.osuLabel.TabIndex = 15;
            this.osuLabel.Text = "C:\\...\\osu!";
            this.osuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::View.Properties.Resources.Defaultsongthumbnaildim;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(496, 105);
            this.Controls.Add(this.osuLabel);
            this.Controls.Add(this.outLabel);
            this.Controls.Add(this.forceTitleCheckbox);
            this.Controls.Add(this.forceArtistCheckbox);
            this.Controls.Add(this.forceAlbumCheckbox);
            this.Controls.Add(this.overwriteTitleCheckbox);
            this.Controls.Add(this.overwriteArtistCheckbox);
            this.Controls.Add(this.overwriteAlbumCheckbox);
            this.Controls.Add(this.includeThumbnailsCheckbox);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outBrowseButton);
            this.Controls.Add(this.osuBrowseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigurationsForm";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button osuBrowseButton;
        private System.Windows.Forms.Button outBrowseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.CheckBox includeThumbnailsCheckbox;
        private System.Windows.Forms.CheckBox overwriteAlbumCheckbox;
        private System.Windows.Forms.CheckBox overwriteArtistCheckbox;
        private System.Windows.Forms.CheckBox overwriteTitleCheckbox;
        private System.Windows.Forms.CheckBox forceAlbumCheckbox;
        private System.Windows.Forms.CheckBox forceArtistCheckbox;
        private System.Windows.Forms.CheckBox forceTitleCheckbox;
        private System.Windows.Forms.Label outLabel;
        private System.Windows.Forms.Label osuLabel;
    }
}