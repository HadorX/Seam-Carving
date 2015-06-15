using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeamCarvingGUI
{
    public partial class TestForm : Form
    {
        private string originalDirectory = @"D:\\Kopia\\mgr\\ppo\\images\\selected";
        private string processedDirectory = @"D:\\Kopia\\mgr\\ppo\\images\\selected\\test";
        private string resultsDir = @"D:\\Kopia\\mgr\\ppo\\images\\selected\\test\\res";
        private List<TestCase> testCases;
        private IEnumerator enumerator;
        private TestCase current;
        public TestForm()
        {
            InitializeComponent();
            testCases = createTestCases();
            enumerator = testCases.GetEnumerator();
            Next();
        }

        private List<TestCase> createTestCases()
        {
            string [] originals = Directory.GetFiles(originalDirectory);
            string [] processed = Directory.GetFiles(processedDirectory);
            List<TestCase> testCases = new List<TestCase>();
            foreach (var o in originals)
            {
                var oname=  Path.GetFileNameWithoutExtension(o);
                foreach(var p in processed)
                {
                    var pname = Path.GetFileNameWithoutExtension(p);
                    if(pname.StartsWith(oname))
                        testCases.Add(new TestCase(o,p));
                }
            }
            Console.WriteLine("TEST CASES: " + testCases.Count);
            return testCases;
        }

        private void SaveResults()
        {
            var csv = new StringBuilder();

            foreach(var t in testCases){
                var first = t.GetFileName();
                var second = t.value;
                var newLine = string.Format("{0},{1}{2}", first, second, Environment.NewLine);
                csv.Append(newLine);
            }

            File.WriteAllText(resultsDir + "\\" +DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")+".csv", csv.ToString());
        }

        public void Next()
        {
            if(!enumerator.MoveNext())
            {
                SaveResults();
                MessageBox.Show("To koniec. Dziękujemy za udział w tescie.");
                this.Close();
                return;
            }
            current = (TestCase)enumerator.Current;
            updatePictureBoxes();
        }

        private void updatePictureBoxes()
        {
            Image image1 = Image.FromFile(current.Original);
            originalPictureBox.Image = image1;
            originalPictureBox.Width = image1.Width;
            originalPictureBox.Height = image1.Height;
            originalPictureBox.Refresh();
            image1 = Image.FromFile(current.Evaluated);
            evaluatedPictureBox.Image = image1;
            evaluatedPictureBox.Width = image1.Width;
            evaluatedPictureBox.Height = image1.Height;
            evaluatedPictureBox.Refresh();
        }


        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            var checkedButton = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (checkedButton == null)
            {
                MessageBox.Show("Musisz ocenić obrazek aby przejść dalej!");
                return;
            }
            current.value = int.Parse(checkedButton.Tag.ToString());
            checkedButton.Checked = false;
            Next();
        }

    }

    class TestCase
    {
        public string Original { get; private set; }
        public string Evaluated { get; private set; }
        public int value { get; set; }

        public TestCase(string original, string evaluated)
        {
            this.Original = original;
            this.Evaluated = evaluated;
            value = -1000;
        }

        public string GetFileName()
        {
            return Path.GetFileNameWithoutExtension(Evaluated);
        }

    }

}
