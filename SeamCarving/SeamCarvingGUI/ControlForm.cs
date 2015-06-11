using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SeamCarvingCore;

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

            //for testing
            //MemoryStream ms = new MemoryStream();
            //image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            SeamCarving.LoadImage(new Bitmap(image));
            int[,] m;

            SeamCarving.FindImageEnergy(EnergyFunction.Default, out m);

            var seam = SeamCarving.FindSeamVertical(m);

            SeamCarving.RemoveVerticalSeam(seam);
            //var newBitmap = SeamCarving.ToImage();
            //_imageForm.imageBox.Image = newBitmap;
            //_imageForm.imageBox.Width = newBitmap.Width;
            //_imageForm.imageBox.Height = newBitmap.Height;
            //ImageHeightNumeric.Value = newBitmap.Height;
            //ImageWidthNumeric.Value = newBitmap.Width;
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
