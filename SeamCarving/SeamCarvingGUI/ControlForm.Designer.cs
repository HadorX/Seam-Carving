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
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ResizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeightNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 12);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(110, 35);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load image";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(258, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(109, 35);
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
            this.ResizeGroupBox.Location = new System.Drawing.Point(13, 76);
            this.ResizeGroupBox.Name = "ResizeGroupBox";
            this.ResizeGroupBox.Size = new System.Drawing.Size(354, 133);
            this.ResizeGroupBox.TabIndex = 2;
            this.ResizeGroupBox.TabStop = false;
            this.ResizeGroupBox.Text = "Resize";
            // 
            // ImageWidthNumeric
            // 
            this.ImageWidthNumeric.Location = new System.Drawing.Point(132, 82);
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
            this.ImageWidthNumeric.Size = new System.Drawing.Size(120, 22);
            this.ImageWidthNumeric.TabIndex = 5;
            this.ImageWidthNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ImageHeightNumeric
            // 
            this.ImageHeightNumeric.Location = new System.Drawing.Point(132, 36);
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
            this.ImageHeightNumeric.Size = new System.Drawing.Size(120, 22);
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
            this.label2.Location = new System.Drawing.Point(60, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height";
            // 
            // SetWidthButton
            // 
            this.SetWidthButton.Location = new System.Drawing.Point(273, 81);
            this.SetWidthButton.Name = "SetWidthButton";
            this.SetWidthButton.Size = new System.Drawing.Size(75, 23);
            this.SetWidthButton.TabIndex = 1;
            this.SetWidthButton.Text = "Process";
            this.SetWidthButton.UseVisualStyleBackColor = true;
            // 
            // SetHeightButton
            // 
            this.SetHeightButton.Location = new System.Drawing.Point(273, 36);
            this.SetHeightButton.Name = "SetHeightButton";
            this.SetHeightButton.Size = new System.Drawing.Size(75, 23);
            this.SetHeightButton.TabIndex = 0;
            this.SetHeightButton.Text = "Process";
            this.SetHeightButton.UseVisualStyleBackColor = true;
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Location = new System.Drawing.Point(13, 228);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(348, 393);
            this.SettingsGroupBox.TabIndex = 3;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 633);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.ResizeGroupBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.ResizeGroupBox.ResumeLayout(false);
            this.ResizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeightNumeric)).EndInit();
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
    }
}

