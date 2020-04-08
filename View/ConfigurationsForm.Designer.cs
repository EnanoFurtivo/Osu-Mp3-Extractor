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
            this.outTextBox = new System.Windows.Forms.TextBox();
            this.osuTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.includeThumbnailsCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // osuBrowseButton
            // 
            this.osuBrowseButton.Location = new System.Drawing.Point(360, 33);
            this.osuBrowseButton.Name = "osuBrowseButton";
            this.osuBrowseButton.Size = new System.Drawing.Size(75, 21);
            this.osuBrowseButton.TabIndex = 0;
            this.osuBrowseButton.Text = "Browse...";
            this.osuBrowseButton.UseVisualStyleBackColor = true;
            this.osuBrowseButton.Click += new System.EventHandler(this.osuBrowseButton_Click);
            // 
            // outBrowseButton
            // 
            this.outBrowseButton.Location = new System.Drawing.Point(360, 7);
            this.outBrowseButton.Name = "outBrowseButton";
            this.outBrowseButton.Size = new System.Drawing.Size(75, 22);
            this.outBrowseButton.TabIndex = 1;
            this.outBrowseButton.Text = "Browse...";
            this.outBrowseButton.UseVisualStyleBackColor = true;
            this.outBrowseButton.Click += new System.EventHandler(this.outBrowseButton_Click);
            // 
            // outTextBox
            // 
            this.outTextBox.Location = new System.Drawing.Point(89, 8);
            this.outTextBox.Name = "outTextBox";
            this.outTextBox.ReadOnly = true;
            this.outTextBox.Size = new System.Drawing.Size(265, 20);
            this.outTextBox.TabIndex = 2;
            this.outTextBox.Text = "C:\\...\\Output";
            // 
            // osuTextBox
            // 
            this.osuTextBox.Location = new System.Drawing.Point(89, 34);
            this.osuTextBox.Name = "osuTextBox";
            this.osuTextBox.ReadOnly = true;
            this.osuTextBox.Size = new System.Drawing.Size(265, 20);
            this.osuTextBox.TabIndex = 3;
            this.osuTextBox.Text = "C:\\...\\osu!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "output Folder: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "osu! Folder:    ";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(441, 12);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(48, 33);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // includeThumbnailsCheckbox
            // 
            this.includeThumbnailsCheckbox.AutoSize = true;
            this.includeThumbnailsCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.includeThumbnailsCheckbox.Checked = true;
            this.includeThumbnailsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeThumbnailsCheckbox.Location = new System.Drawing.Point(7, 60);
            this.includeThumbnailsCheckbox.Name = "includeThumbnailsCheckbox";
            this.includeThumbnailsCheckbox.Size = new System.Drawing.Size(118, 17);
            this.includeThumbnailsCheckbox.TabIndex = 7;
            this.includeThumbnailsCheckbox.Text = "Include Thumbnails";
            this.includeThumbnailsCheckbox.UseVisualStyleBackColor = false;
            this.includeThumbnailsCheckbox.CheckedChanged += new System.EventHandler(this.includeThumbnailsCheckbox_CheckedChanged);
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::View.Properties.Resources.DefaultImg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(496, 82);
            this.Controls.Add(this.includeThumbnailsCheckbox);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.osuTextBox);
            this.Controls.Add(this.outTextBox);
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
        private System.Windows.Forms.TextBox outTextBox;
        private System.Windows.Forms.TextBox osuTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.CheckBox includeThumbnailsCheckbox;
    }
}