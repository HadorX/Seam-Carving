using SeamCarvingCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingGUI
{
    class Generator
    {

        private const string SCALE_METHOD = "Scale";
        private const string CROP_METHOD = "Crop";
        private List<string> enerfyFunctions = new List<string>(){
            "Laplacian",
            "Prewitt",
            "RobertsCross",
            "PrewittSlanting",
            "Sobel"
        };

        private string inputDirectory = @"D:\\Kopia\\mgr\\ppo\\images\\selected\\";
        private string outputDirectory = @"D:\\Kopia\\mgr\\ppo\\images\\selected\\res\\";

        private List<GeneratorElementConfig> GeneratorElements = new List<GeneratorElementConfig>();

        static void Main()
        {
            SeamCarving.Progress += new SeamCarving.ProgressDelegate(LogProgress);
            //new Generator().Generate();
           //new Generator().GenerateEnergyMaps();
        }

        public void GenerateEnergyMaps()
        {
            CreateOutputDirectory();
            string[] files = GetFiles();
            foreach (var f in files)
            {
                Image image = Image.FromFile(f);
                double energy;
                Bitmap bmp;
                string filename = Path.GetFileNameWithoutExtension(f);
                string fileExtension = Path.GetExtension(f);

                foreach(var ef in enerfyFunctions){
                    SeamCarving.LoadImage(new Bitmap(image));
                    SeamCarving.FindImageEnergy(GetEnergyFunction(ef),out energy,out bmp);
                    bmp.Save(outputDirectory+"\\"+ filename + "_"+ ef + "_energyMap" + fileExtension);
                    //int [,] m = SeamCarving.ResizeWidth(GetEnergyFunction(ef),50);
                    //SeamCarving.LoadImage(bmp);
                    //Image im2 = SeamCarving.ToImage(m);
                    //im2.Save(outputDirectory + "\\" + filename + "_" + ef + "_test" + fileExtension);
                }
            }
        }
        



        public void Generate()
        {  
            CreateOutputDirectory();
            CreateGeneratorElements();
            Trace.WriteLine("\n\nSTART\n\n");

            foreach (var element in GeneratorElements)
            {
                if(SCALE_METHOD.Equals(element.MethodName))
                    GenerateScale(element);
                else if (CROP_METHOD.Equals(element.MethodName))
                    GenerateCrop(element);
                else
                    GenerateSeamCarving(element);
            }

            Trace.WriteLine("\n\nEND\n\n");
        }

        private void CreateOutputDirectory()
        {
            DateTime dt = DateTime.Now;
            outputDirectory += dt.ToString("dd-MM-yyyy-HH-mm-ss");
            Directory.CreateDirectory(outputDirectory);
        }

        private string[] GetFiles()
        {
            return Directory.GetFiles(inputDirectory);
        }

        private void CreateGeneratorElements()
        {
            string[] files = GetFiles();
            foreach (var f in files)
            {
                Image img = Image.FromFile(f);
                //zmiana szerokosci
               // AddElementsForSize(f, img.Size.Width - 20, img.Size.Height, true);
                AddElementsForSize(f, img.Size.Width - 50, img.Size.Height, true);
              //  AddElementsForSize(f, img.Size.Width - 100, img.Size.Height, true);
             //   AddElementsForSize(f, img.Size.Width + 20, img.Size.Height, true);
                AddElementsForSize(f, img.Size.Width + 50, img.Size.Height, true);
             //   AddElementsForSize(f, img.Size.Width + 100, img.Size.Height, true);
             //zmiana wysokosci
             //  AddElementsForSize(f, img.Size.Width, img.Size.Height - 20, false);
             //   AddElementsForSize(f, img.Size.Width, img.Size.Height - 50, false);
             //   AddElementsForSize(f, img.Size.Width, img.Size.Height - 100, false);
             //   AddElementsForSize(f, img.Size.Width, img.Size.Height + 20, false);
             //   AddElementsForSize(f, img.Size.Width, img.Size.Height + 50, false);
             //   AddElementsForSize(f, img.Size.Width, img.Size.Height + 100, false);
            }
        }

        private void AddElementsForSize(string f, int w, int h,bool changeWidth)
        {
            GeneratorElements.Add(new GeneratorElementConfig(f, SCALE_METHOD, outputDirectory, w, h, changeWidth));
            GeneratorElements.Add(new GeneratorElementConfig(f, CROP_METHOD, outputDirectory, w, h, changeWidth));
            foreach (var ef in enerfyFunctions)
            {
                GeneratorElements.Add(new GeneratorElementConfig(f, ef, outputDirectory, w, h, changeWidth));
            }
        }

        private void GenerateSeamCarving(GeneratorElementConfig config)
        {
            Console.WriteLine("from: " + config.InputFileName() + "\nto: " + config.OutputFileName() + "\nmethod: "+config.MethodName);
            var image = Image.FromFile(config.InputFilePath);
            SeamCarving.LoadImage(new Bitmap(image));
            EnergyFunctionBase energyFunction = GetEnergyFunction(config.MethodName);
            if(config.ChangeWidth)
                SeamCarving.ResizeWidth(energyFunction, image.Size.Width - config.ToWidth);
            else
                SeamCarving.ResizeHeight(energyFunction,image.Size.Height - config.ToHeight);
            Image res = SeamCarving.ToImage();
            res.Save(config.OutputFilePath(), getImageFormat(config.FileExtension()));
        }

        private void GenerateScale(GeneratorElementConfig config)
        {
            Console.WriteLine("from: " + config.InputFileName() + "\nto: " + config.OutputFileName() + "\nmethod: " + config.MethodName);
            var image = Image.FromFile(config.InputFilePath);
            Image res = SeamCarving.ResizeImage(image, config.ToWidth, config.ToHeight);
            res.Save(config.OutputFilePath(), getImageFormat(config.FileExtension()));
        }

        private void GenerateCrop(GeneratorElementConfig config)
        {
            Console.WriteLine("from: " + config.InputFileName() + "\nto: " + config.OutputFileName() + "\nmethod: " + config.MethodName);
            var image = Image.FromFile(config.InputFilePath);
            Image res = SeamCarving.CropImage(image, config.ToWidth, config.ToHeight);
            res.Save(config.OutputFilePath(), getImageFormat(config.FileExtension()));
        }

        private EnergyFunctionBase GetEnergyFunction(string methodName)
        {
            if ("Laplacian".Equals(methodName))
                return new Laplacian();
            if ("Prewitt".Equals(methodName))
                return new Prewitt();
            if ("PrewittSlanting".Equals(methodName))
                return new PrewittSlanting();
            if ("RobertsCross".Equals(methodName))
                return new RobertsCross();
            if ("Sobel".Equals(methodName))
                return new Sobel();
            throw new ArgumentException("Wrong method");
        }
     
        private ImageFormat getImageFormat(string extension)
        {
            if (".jpg".Equals(extension))
                return ImageFormat.Jpeg;
            if (".png".Equals(extension))
                return ImageFormat.Png;
            if (".bmp".Equals(extension))
                return ImageFormat.Bmp;
            return ImageFormat.Bmp;
        }


        private static void LogProgress(int progress)
        {
            Trace.Write("[");
            Trace.Write(progress);
            Trace.Write("]");
        }

    }


    //Konfiguracja pojedynczej generacji
    class GeneratorElementConfig
    {
        public string InputFilePath { get; private set; }
        public string MethodName { get; private set; }
        public string ParentDirectory { get; private set; }

        public int ToWidth { get; private set; }

        public int ToHeight { get; private set; }

        public bool ChangeWidth { get; private set; }

        public GeneratorElementConfig(string InputFilePath, string MethodName, string ParentDirectory, int ToWidth, int ToHeight, bool ChangeWidth)
        {
            this.InputFilePath = InputFilePath;
            this.MethodName = MethodName;
            this.ParentDirectory = ParentDirectory;
            this.ToWidth = ToWidth;
            this.ToHeight = ToHeight;
            this.ChangeWidth = ChangeWidth;
        }



        public string InputFileName()
        {
            return Path.GetFileNameWithoutExtension(InputFilePath);
        }

        public string OutputFileName()
        {
            return InputFileName() + "_" + ToWidth + "x" + ToHeight + "_" + MethodName + FileExtension(); 
        }

        public string FileExtension()
        {
            return Path.GetExtension(InputFilePath);
        }

        public string OutputFilePath()
        {
            return ParentDirectory + "\\" + OutputFileName();
        }
    }

}
