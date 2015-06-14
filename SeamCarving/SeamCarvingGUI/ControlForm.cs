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

            int[,] m;
            double avgEnergy;
            Bitmap bmp;

            var energyFunction = GetEnergyAlgorithm();
            if (widthDiff < 0)
            {
                var seamList = new List<int[]>();

                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < -widthDiff; i++)
                {
                    ProgressBar.Value = (int)((i * 100) / -widthDiff);
                    var seam = SeamCarving.FindSeamVertical(m);
                    seamList.Add(seam);

                    m = SeamCarving.RemoveVerticalSeam(seam, m);
                    m = SeamCarving.UpdateImageEnergyVerticalSeam(energyFunction, m, seam, out avgEnergy, out bmp);
                }

                SeamCarving.LoadImage(new Bitmap(_imageForm.imageBox.Image));
                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < seamList.Count; i++)
                {
                    var seam = seamList[i];
                    m = SeamCarving.AddVerticalSeam(seam, m);

                    for (int j = 1; j < seamList.Count; j++)
                    {
                        for (int k = 0; k < seam.Length; k++)
                        {
                            if (seam[k] <= seamList[j][k])
                            {
                                seamList[j][k]++;
                            }
                        }
                    }
                }
            }
            else
            {
                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < widthDiff; i++)
                {

                    ProgressBar.Value = (int)((i * 100) / widthDiff);
                    var seam = SeamCarving.FindSeamVertical(m);

                    m = SeamCarving.RemoveVerticalSeam(seam, m);
                    m = SeamCarving.UpdateImageEnergyVerticalSeam(energyFunction, m, seam, out avgEnergy, out bmp);
                }
            }


            var newBitmap = SeamCarving.ToImage();

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

            int[,] m;
            double avgEnergy;

            Bitmap bmp;

            var energyFunction = GetEnergyAlgorithm();
            if (heightDiff < 0)
            {
                var seamList = new List<int[]>();

                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < -heightDiff; i++)
                {

                    ProgressBar.Value = (int)((i * 100) / -heightDiff);

                    var seam = SeamCarving.FindSeamHorizontal(m);
                    seamList.Add(seam);

                    m = SeamCarving.RemoveHorizontalSeam(seam, m);
                    m = SeamCarving.UpdateImageEnergyHorizontalSeam(energyFunction, m, seam, out avgEnergy, out bmp);
                }

                SeamCarving.LoadImage(new Bitmap(_imageForm.imageBox.Image));
                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < seamList.Count; i++)
                {
                    var seam = seamList[i];
                    m = SeamCarving.AddHorizontalSeam(seam, m);

                    for (int j = 1; j < seamList.Count; j++)
                    {
                        for (int k = 0; k < seam.Length; k++)
                        {
                            if (seam[k] <= seamList[j][k])
                            {
                                seamList[j][k]++;
                            }
                        }
                    }
                }
            }
            else
            {
                m = SeamCarving.FindImageEnergy(energyFunction, out avgEnergy, out bmp);
                for (int i = 0; i < heightDiff; i++)
                {

                    ProgressBar.Value = (int)((i * 100) / heightDiff);

                    var seam = SeamCarving.FindSeamHorizontal(m);

                    m = SeamCarving.RemoveHorizontalSeam(seam, m);
                    m = SeamCarving.UpdateImageEnergyHorizontalSeam(energyFunction, m, seam, out avgEnergy, out bmp);
                }
            }


            var newBitmap = SeamCarving.ToImage();

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
