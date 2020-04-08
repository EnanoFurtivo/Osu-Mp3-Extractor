﻿namespace View
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.extractButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.osuLabel = new System.Windows.Forms.Label();
            this.outputLabelData = new System.Windows.Forms.Label();
            this.osuLabelData = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.optionsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modeComboBox
            // 
            this.modeComboBox.BackColor = System.Drawing.Color.White;
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.modeComboBox.Location = new System.Drawing.Point(12, 12);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(242, 21);
            this.modeComboBox.TabIndex = 0;
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
            // 
            // extractButton
            // 
            this.extractButton.Enabled = false;
            this.extractButton.Location = new System.Drawing.Point(12, 39);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(242, 21);
            this.extractButton.TabIndex = 1;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(12, 67);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(84, 21);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.BackColor = System.Drawing.SystemColors.Control;
            this.outputLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputLabel.Location = new System.Drawing.Point(14, 97);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(45, 15);
            this.outputLabel.TabIndex = 4;
            this.outputLabel.Text = "output: ";
            // 
            // osuLabel
            // 
            this.osuLabel.AutoSize = true;
            this.osuLabel.BackColor = System.Drawing.SystemColors.Control;
            this.osuLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.osuLabel.Location = new System.Drawing.Point(13, 121);
            this.osuLabel.Name = "osuLabel";
            this.osuLabel.Size = new System.Drawing.Size(47, 15);
            this.osuLabel.TabIndex = 5;
            this.osuLabel.Text = "osu!:     ";
            // 
            // outputLabelData
            // 
            this.outputLabelData.BackColor = System.Drawing.SystemColors.Control;
            this.outputLabelData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputLabelData.Location = new System.Drawing.Point(66, 97);
            this.outputLabelData.Name = "outputLabelData";
            this.outputLabelData.Size = new System.Drawing.Size(188, 15);
            this.outputLabelData.TabIndex = 6;
            this.outputLabelData.Text = "Path";
            // 
            // osuLabelData
            // 
            this.osuLabelData.BackColor = System.Drawing.SystemColors.Control;
            this.osuLabelData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.osuLabelData.Location = new System.Drawing.Point(66, 121);
            this.osuLabelData.Name = "osuLabelData";
            this.osuLabelData.Size = new System.Drawing.Size(188, 15);
            this.osuLabelData.TabIndex = 7;
            this.osuLabelData.Text = "Path";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 10);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(242, 23);
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;
            // 
            // optionsButton
            // 
            this.optionsButton.Location = new System.Drawing.Point(102, 67);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(152, 21);
            this.optionsButton.TabIndex = 9;
            this.optionsButton.Text = "Options";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::View.Properties.Resources.DefaultImg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(264, 149);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.osuLabelData);
            this.Controls.Add(this.outputLabelData);
            this.Controls.Add(this.osuLabel);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.modeComboBox);
            this.ForeColor = System.Drawing.SystemColors.MenuText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "osu! Mp3 Extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label osuLabel;
        private System.Windows.Forms.Label outputLabelData;
        private System.Windows.Forms.Label osuLabelData;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button optionsButton;
    }
}

