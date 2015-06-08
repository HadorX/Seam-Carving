using System.Windows.Forms;

namespace SeamCarvingGUI
{
    public partial class ImageForm : Form
    {
        private readonly ControlForm _controlForm;

        public ImageForm(ControlForm controlForm)
        {
            _controlForm = controlForm;
            InitializeComponent();
        }

        private void ImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controlForm.DisableImageControls();
        }
    }
}
