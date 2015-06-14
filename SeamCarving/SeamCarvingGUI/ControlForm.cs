using System;
using System.Collections.Generic;
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
            algorithmDropDown.SelectedIndex = 0;
            SeamCarving.Progress += new SeamCarving.ProgressDelegate(this.UpdateProgressBar);
        }

        private void UpdateProgressBar(int progress)
        {
            ProgressBar.Value = progress;
        }

        //MOZNA DODAC WARTOSC SREDNIA ENERGII W OBRAZIE I POKAZAC ZE Z USUWANIE SEAMOW SIE PODNOSI
        private void LoadButton_Click(object sender, EventArgs e)
        {
            var result = OpenFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            if (_imageForm != null)
                _imageForm.Close();

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

            double avgEnergy;
            Bitmap bmp;
            SeamCarving.FindImageEnergy(GetEnergyAlgorithm(), out avgEnergy, out bmp);
            AverageEnergyLabel.Text = avgEnergy.ToString("F2");
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

        public void DrawSeam(int[] seam)
        {
            var bmp = new Bitmap(_imageForm.imageBox.Image);
            for (int i = 0; i < seam.Length; i++)
            {
                bmp.SetPixel(seam[i], i, Color.Red);
            }
            _imageForm.imageBox.Image = bmp;
        }


        private void SetWidthButton_Click(object sender, EventArgs e)
        {
            var widthDiff = _imageForm.imageBox.Width - ImageWidthNumeric.Value;
            var energyFunction = GetEnergyAlgorithm();

            SeamCarving.ResizeWidth(energyFunction, widthDiff);

            var newBitmap = SeamCarving.ToImage();
            Bitmap bmp;
            double avgEnergy;
            SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
            AverageEnergyLabel.Text = avgEnergy.ToString("F2");

            _imageForm.imageBox.Image = newBitmap;
            _imageForm.imageBox.Width = newBitmap.Width;
            _imageForm.imageBox.Height = newBitmap.Height;
            ImageHeightNumeric.Value = newBitmap.Height;
            ImageWidthNumeric.Value = newBitmap.Width;

            ProgressBar.Value = 100;
        }

        private void SetHeightButton_Click(object sender, EventArgs e)
        {
            var heightDiff = _imageForm.imageBox.Height - ImageHeightNumeric.Value;
            var energyFunction = GetEnergyAlgorithm();

            SeamCarving.ResizeHeight(energyFunction, heightDiff);

            var newBitmap = SeamCarving.ToImage();

            Bitmap bmp;
            double avgEnergy;
            SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
            AverageEnergyLabel.Text = avgEnergy.ToString("F2");

            _imageForm.imageBox.Image = newBitmap;
            _imageForm.imageBox.Width = newBitmap.Width;
            _imageForm.imageBox.Height = newBitmap.Height;
            ImageHeightNumeric.Value = newBitmap.Height;
            ImageWidthNumeric.Value = newBitmap.Width;
            ProgressBar.Value = 100;
        }

        private EnergyFunctionBase GetEnergyAlgorithm()
        {
            EnergyFunctionBase function;
            switch (algorithmDropDown.SelectedIndex)
            {
                case 1:
                    function = new Sobel();
                    break;
                case 2:
                    function = new Prewitt();
                    break;
                case 3:
                    function = new PrewittSlanting();
                    break;
                case 4:
                    function = new Laplacian();
                    break;
                default:
                    function = new RobertsCross();
                    break;
            }
            return function;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
