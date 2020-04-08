namespace Osu_Mp3_Extractor
{
    partial class CollectionSelection
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
            this.extractionProgressBar = new System.Windows.Forms.ProgressBar();
            this.optionsComboBox = new System.Windows.Forms.ComboBox();
            this.changePathsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // extractionProgressBar
            // 
            this.extractionProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.extractionProgressBar.Enabled = false;
            this.extractionProgressBar.Location = new System.Drawing.Point(140, 49);
            this.extractionProgressBar.Name = "extractionProgressBar";
            this.extractionProgressBar.Size = new System.Drawing.Size(529, 60);
            this.extractionProgressBar.Step = 1;
            this.extractionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.extractionProgressBar.TabIndex = 22;
            this.extractionProgressBar.Visible = false;
            // 
            // optionsComboBox
            // 
            this.optionsComboBox.Enabled = false;
            this.optionsComboBox.FormattingEnabled = true;
            this.optionsComboBox.Location = new System.Drawing.Point(250, 215);
            this.optionsComboBox.Name = "optionsComboBox";
            this.optionsComboBox.Size = new System.Drawing.Size(300, 21);
            this.optionsComboBox.TabIndex = 30;
            this.optionsComboBox.Text = "Mode";
            // 
            // changePathsButton
            // 
            this.changePathsButton.Location = new System.Drawing.Point(166, 259);
            this.changePathsButton.Name = "changePathsButton";
            this.changePathsButton.Size = new System.Drawing.Size(82, 23);
            this.changePathsButton.TabIndex = 31;
            this.changePathsButton.Text = "Change Paths";
            this.changePathsButton.UseVisualStyleBackColor = true;
            this.changePathsButton.Click += new System.EventHandler(this.changePaths_Click);
            // 
            // CollectionSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.changePathsButton);
            this.Controls.Add(this.optionsComboBox);
            this.Controls.Add(this.extractionProgressBar);
            this.Name = "CollectionSelection";
            this.Text = "CollectionSelection";
            this.Load += new System.EventHandler(this.CollectionSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar extractionProgressBar;
        private System.Windows.Forms.ComboBox optionsComboBox;
        private System.Windows.Forms.Button changePathsButton;
    }
}