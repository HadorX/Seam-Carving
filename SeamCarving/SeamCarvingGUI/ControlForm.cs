using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeamCarvingGUI
{
    public partial class ControlForm : Form
    {
        private ImageForm _imageForm;

        private string _imageFileName;

        public ControlForm()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var result = OpenFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            _imageForm = new ImageForm(this);
            var image = Image.FromFile(OpenFileDialog.FileName);
            _imageFileName = OpenFileDialog.SafeFileName;
            _imageForm.imageBox.Image = image;
            _imageForm.imageBox.Width = image.Width;
            _imageForm.imageBox.Height = image.Height;
            _imageForm.Show();

            ImageHeightNumeric.Value = image.Height;
            ImageWidthNumeric.Value = image.Width;
            EnableImageControls();
        }

        public void EnableImageControls()
        {
            ResizeGroupBox.Enabled = true;
            SaveButton.Enabled = true;
        }

        public void DisableImageControls()
        {
            ResizeGroupBox.Enabled = false;
            SaveButton.Enabled = false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog.FileName = _imageFileName;
            var result = SaveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            _imageForm.imageBox.Image.Save(SaveFileDialog.FileName);
        }
    }
}
