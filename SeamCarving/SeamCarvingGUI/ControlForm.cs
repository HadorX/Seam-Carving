using System;
using System.ComponentModel;
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

        //MOZNA DODAC WARTOSC SREDNIA ENERGII W OBRAZIE I POKAZAC ZE Z USUWANIE SEAMOW SIE PODNOSI
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

            //double avgEnergy;
            //Bitmap bmp;
            //SeamCarving.FindImageEnergy(EnergyFunction.Default, out avgEnergy, out bmp);
            //_imageForm.imageBox.Image = bmp;
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

        private void SetWidthButton_Click(object sender, EventArgs e)
        {
            var widthDiff = _imageForm.imageBox.Width - ImageWidthNumeric.Value;

            int[,] m = new int[Width,Height];
            double avgEnergy;

            Bitmap bmp;


            for (int i = 0; i < widthDiff; i++)
            {
                m = SeamCarving.FindImageEnergy(EnergyFunction.Default, out avgEnergy, out bmp);
                AverageEnergyLabel.Text = avgEnergy.ToString("F2");
                ProgressBar.Value = (int)((i*100)/widthDiff);
                
                var seam = SeamCarving.FindSeamVertical(m);

                m = SeamCarving.RemoveVerticalSeam(seam, m);
            }

            var newBitmap = SeamCarving.ToImage();

            SeamCarving.FindImageEnergy(EnergyFunction.Default, out avgEnergy, out bmp);
            AverageEnergyLabel.Text = avgEnergy.ToString("F2");

            _imageForm.imageBox.Image = newBitmap;
            _imageForm.imageBox.Width = newBitmap.Width;
            _imageForm.imageBox.Height = newBitmap.Height;
            ImageHeightNumeric.Value = newBitmap.Height;
            ImageWidthNumeric.Value = newBitmap.Width;
            ProgressBar.Value = 100;
        }
    }
}
