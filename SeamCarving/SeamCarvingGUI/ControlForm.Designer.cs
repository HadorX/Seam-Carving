namespace SeamCarvingGUI
{
    partial class ControlForm
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResizeGroupBox = new System.Windows.Forms.GroupBox();
            this.ImageWidthNumeric = new System.Windows.Forms.NumericUpDown();
            this.ImageHeightNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SetWidthButton = new System.Windows.Forms.Button();
            this.SetHeightButton = new System.Windows.Forms.Button();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.AverageEnergyLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.algorithmDropDown = new System.Windows.Forms.ComboBox();
            this.ResizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeightNumeric)).BeginInit();
            this.SettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(9, 10);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(82, 28);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load image";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(194, 10);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(82, 28);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save image";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResizeGroupBox
            // 
            this.ResizeGroupBox.Controls.Add(this.ImageWidthNumeric);
            this.ResizeGroupBox.Controls.Add(this.ImageHeightNumeric);
            this.ResizeGroupBox.Controls.Add(this.label2);
            this.ResizeGroupBox.Controls.Add(this.label1);
            this.ResizeGroupBox.Controls.Add(this.SetWidthButton);
            this.ResizeGroupBox.Controls.Add(this.SetHeightButton);
            this.ResizeGroupBox.Enabled = false;
            this.ResizeGroupBox.Location = new System.Drawing.Point(10, 62);
            this.ResizeGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ResizeGroupBox.Name = "ResizeGroupBox";
            this.ResizeGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ResizeGroupBox.Size = new System.Drawing.Size(266, 108);
            this.ResizeGroupBox.TabIndex = 2;
            this.ResizeGroupBox.TabStop = false;
            this.ResizeGroupBox.Text = "Resize";
            // 
            // ImageWidthNumeric
            // 
            this.ImageWidthNumeric.Location = new System.Drawing.Point(99, 67);
            this.ImageWidthNumeric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageWidthNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ImageWidthNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ImageWidthNumeric.Name = "ImageWidthNumeric";
            this.ImageWidthNumeric.Size = new System.Drawing.Size(90, 20);
            this.ImageWidthNumeric.TabIndex = 5;
            this.ImageWidthNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ImageHeightNumeric
            // 
            this.ImageHeightNumeric.Location = new System.Drawing.Point(99, 29);
            this.ImageHeightNumeric.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageHeightNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ImageHeightNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ImageHeightNumeric.Name = "ImageHeightNumeric";
            this.ImageHeightNumeric.Size = new System.Drawing.Size(90, 20);
            this.ImageHeightNumeric.TabIndex = 4;
            this.ImageHeightNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height";
            // 
            // SetWidthButton
            // 
            this.SetWidthButton.Location = new System.Drawing.Point(205, 66);
            this.SetWidthButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SetWidthButton.Name = "SetWidthButton";
            this.SetWidthButton.Size = new System.Drawing.Size(56, 19);
            this.SetWidthButton.TabIndex = 1;
            this.SetWidthButton.Text = "Process";
            this.SetWidthButton.UseVisualStyleBackColor = true;
            this.SetWidthButton.Click += new System.EventHandler(this.SetWidthButton_Click);
            // 
            // SetHeightButton
            // 
            this.SetHeightButton.Location = new System.Drawing.Point(205, 29);
            this.SetHeightButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SetHeightButton.Name = "SetHeightButton";
            this.SetHeightButton.Size = new System.Drawing.Size(56, 19);
            this.SetHeightButton.TabIndex = 0;
            this.SetHeightButton.Text = "Process";
            this.SetHeightButton.UseVisualStyleBackColor = true;
            this.SetHeightButton.Click += new System.EventHandler(this.SetHeightButton_Click);
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.algorithmDropDown);
            this.SettingsGroupBox.Controls.Add(this.AverageEnergyLabel);
            this.SettingsGroupBox.Controls.Add(this.label3);
            this.SettingsGroupBox.Location = new System.Drawing.Point(10, 185);
            this.SettingsGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SettingsGroupBox.Size = new System.Drawing.Size(261, 299);
            this.SettingsGroupBox.TabIndex = 3;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // AverageEnergyLabel
            // 
            this.AverageEnergyLabel.AutoSize = true;
            this.AverageEnergyLabel.Location = new System.Drawing.Point(151, 280);
            this.AverageEnergyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AverageEnergyLabel.Name = "AverageEnergyLabel";
            this.AverageEnergyLabel.Size = new System.Drawing.Size(10, 13);
            this.AverageEnergyLabel.TabIndex = 1;
            this.AverageEnergyLabel.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 280);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Average image energy:";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(10, 486);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(261, 19);
            this.ProgressBar.TabIndex = 4;
            // 
            // algorithmDropDown
            // 
            this.algorithmDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmDropDown.FormattingEnabled = true;
            this.algorithmDropDown.Items.AddRange(new object[] {
            "Roberts Cross",
            "Sobel",
            "Prewitt",
            "Prewitt Slanting",
            "Default"});
            this.algorithmDropDown.Location = new System.Drawing.Point(2, 15);
            this.algorithmDropDown.Name = "algorithmDropDown";
            this.algorithmDropDown.Size = new System.Drawing.Size(121, 21);
            this.algorithmDropDown.TabIndex = 2;
            this.algorithmDropDown.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 514);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.ResizeGroupBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.ResizeGroupBox.ResumeLayout(false);
            this.ResizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeightNumeric)).EndInit();
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox ResizeGroupBox;
        private System.Windows.Forms.NumericUpDown ImageWidthNumeric;
        private System.Windows.Forms.NumericUpDown ImageHeightNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetWidthButton;
        private System.Windows.Forms.Button SetHeightButton;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label AverageEnergyLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox algorithmDropDown;
    }
}

