using System.Drawing;
using System.Linq;

namespace SeamCarvingCore
{
    public static class SeamCarving
    {
        private static int Width { get; set; }
        private static int Height { get; set; }
        private static int[, ,] Pixels { get; set; }

        public static void LoadImage(Bitmap bmp)
        {
            var bits = new LockBitmap(bmp);
            bits.LockBits();

            Width = bits.Width;
            Height = bits.Height;
            Pixels = new int[Width, Height, 3];
            for (var i = 0; i < bits.Width; i++)
            {
                for (var j = 0; j < bits.Height; j++)
                {
                    var color = bits.GetPixel(i, j);
                    Pixels[i, j, 0] = color.R;
                    Pixels[i, j, 1] = color.G;
                    Pixels[i, j, 2] = color.B;
                }
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            return new Bitmap(image, width, height);
        }

        public static Bitmap CropImage(Image image, int width, int height)
        {
            var cropRect = new Rectangle(0, 0, width, height);
            var src = image as Bitmap;
            var target = new Bitmap(cropRect.Width, cropRect.Height);

            using (var g = Graphics.FromImage(target))
            {
                if (src != null)
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
            }
            return target;
        }

        public static int[] FindSeamVertical(int[,] m)
        {
            var height = m.GetLength(1);
            var width = m.GetLength(0);
            var result = new int[height];

            var mCopy = new int[width, height];

            for (var i = 0; i < width; i++)
            {
                mCopy[i, 0] = m[i, 0];
            }

            for (var i = 1; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    mCopy[j, i] = m[j, i] + new[] { j - 1 < 0 ? int.MaxValue : mCopy[j - 1, i - 1], mCopy[j, i - 1], j + 1 >= width ? int.MaxValue : mCopy[j + 1, i - 1] }.Min();
                }
            }

            var min = int.MaxValue;
            var index = -1;

            for (var i = 0; i < width; i++)
            {
                if (mCopy[i, height - 1] < min)
                {
                    min = mCopy[i, height - 1];
                    index = i;
                }
            }
            result[height - 1] = index;

            for (var i = height - 2; i >= 0; i--)
            {
                var int1 = index == 0 ? int.MaxValue : mCopy[index - 1, i];
                var int2 = mCopy[index, i];
                var int3 = index == width - 1 ? int.MaxValue : mCopy[index + 1, i];

                var array = new[] { int1, int2, int3 };
                var minimum = array.Min();
                if (int1 == minimum && index > 0)
                    index--;
                else if (int2 != minimum && int3 == minimum && index < width - 1)
                    index++;

                result[i] = index;
            }

            return result;
        }

        public static int[] FindSeamHorizontal(int[,] m)
        {
            var height = m.GetLength(1);
            var width = m.GetLength(0);
            var result = new int[width];
            var mCopy = new int[width, height];

            for (var i = 0; i < height; i++)
            {
                mCopy[0, i] = m[0, i];
            }

            for (var i = 1; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    mCopy[i, j] = m[i, j] + new[] { j - 1 < 0 ? int.MaxValue : mCopy[i - 1, j - 1], mCopy[i - 1, j], j + 1 >= height ? int.MaxValue : mCopy[i - 1, j + 1] }.Min();
                }
            }

            var min = int.MaxValue;
            var index = 0;

            for (var i = 0; i < height; i++)
            {
                if (mCopy[width - 1, i] < min)
                {
                    min = mCopy[width - 1, i];
                    index = i;
                }
            }
            result[width - 1] = index;

            for (var i = width - 2; i >= 0; i--)
            {
                var int1 = index == 0 ? int.MaxValue : mCopy[i, index - 1];
                var int2 = mCopy[i, index];
                var int3 = index == height - 1 ? int.MaxValue : mCopy[i, index + 1];

                var array = new[] { int1, int2, int3 };
                var minimum = array.Min();
                if (int1 == minimum && index > 0)
                    index--;
                else if (int2 != minimum && int3 == minimum && index < height - 1)
                    index++;

                result[i] = index;
            }

            return result;
        }

        public static int[,] UpdateImageEnergyVerticalSeam(EnergyFunctionBase energyFunction, int[,] m, int[] seam,
            out double avgEnergy, out Bitmap bmp)
        {
            energyFunction.Energy = m;
            energyFunction.UpdateEnergyVerticalSeam(Width, Height, Pixels, seam);
            bmp = ToImage(energyFunction.Energy);
            avgEnergy = energyFunction.AvgEnergy;
            return energyFunction.Energy;
        }

        public static int[,] UpdateImageEnergyHorizontalSeam(EnergyFunctionBase energyFunction, int[,] m, int[] seam,
            out double avgEnergy, out Bitmap bmp)
        {
            energyFunction.Energy = m;
            energyFunction.UpdateEnergyHorizontalSeam(Width, Height, Pixels, seam);
            bmp = ToImage(energyFunction.Energy);
            avgEnergy = energyFunction.AvgEnergy;
            return energyFunction.Energy;
        }

        public static int[,] FindImageEnergy(EnergyFunctionBase energyFunction, out double avgEnergy, out Bitmap bmp)
        {
            energyFunction.ComputeEnergy(Width, Height, Pixels);
            bmp = ToImage(energyFunction.Energy);
            avgEnergy = energyFunction.AvgEnergy;
            return energyFunction.Energy;
        }

        internal static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }


        public static int[,] RemoveVerticalSeam(int[] seam, int[,] m)
        {
            Width--;
            var newPixelData = new int[Width, Height, 3];
            var m2 = new int[Width, Height];
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    if (i >= seam[j])
                    {
                        newPixelData[i, j, 0] = Pixels[i + 1, j, 0];
                        newPixelData[i, j, 1] = Pixels[i + 1, j, 1];
                        newPixelData[i, j, 2] = Pixels[i + 1, j, 2];
                        m2[i, j] = m[i + 1, j];
                    }
                    else
                    {
                        newPixelData[i, j, 0] = Pixels[i, j, 0];
                        newPixelData[i, j, 1] = Pixels[i, j, 1];
                        newPixelData[i, j, 2] = Pixels[i, j, 2];
                        m2[i, j] = m[i, j];
                    }
                }
            }
            Pixels = newPixelData;
            return m2;
        }

        public static int[,] RemoveHorizontalSeam(int[] seam, int[,] m)
        {
            Height--;
            var newPixelData = new int[Width, Height, 3];
            var m2 = new int[Width, Height];
            for (var j = 0; j < Width; j++)
            {
                for (var i = 0; i < Height; i++)
                {
                    if (i >= seam[j])
                    {
                        newPixelData[j, i, 0] = Pixels[j, i + 1, 0];
                        newPixelData[j, i, 1] = Pixels[j, i + 1, 1];
                        newPixelData[j, i, 2] = Pixels[j, i + 1, 2];
                        m2[j, i] = m[j, i + 1];
                    }
                    else
                    {
                        newPixelData[j, i, 0] = Pixels[j, i, 0];
                        newPixelData[j, i, 1] = Pixels[j, i, 1];
                        newPixelData[j, i, 2] = Pixels[j, i, 2];
                        m2[j, i] = m[j, i];
                    }
                }
            }
            Pixels = newPixelData;
            return m2;
        }

        public static int[,] AddVerticalSeam(int[] seam, int[,] m)
        {
            Width++;
            var newPixelData = new int[Width, Height, 3];
            var m2 = new int[Width, Height];
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    if (i > seam[j])
                    {
                        newPixelData[i, j, 0] = Pixels[i - 1, j, 0];
                        newPixelData[i, j, 1] = Pixels[i - 1, j, 1];
                        newPixelData[i, j, 2] = Pixels[i - 1, j, 2];
                        m2[i, j] = m[i - 1, j];
                    }
                    else if (i == seam[j])
                    {
                        if (i == 0)
                        {
                            newPixelData[i, j, 0] = (Pixels[i, j, 0] + Pixels[i + 1, j, 0]) / 2;
                            newPixelData[i, j, 1] = (Pixels[i, j, 1] + Pixels[i + 1, j, 1]) / 2;
                            newPixelData[i, j, 2] = (Pixels[i, j, 2] + Pixels[i + 1, j, 2]) / 2;
                            m2[i, j] = m[i, j];
                        }
                        else if (i == Width - 1 || i == Width - 2)
                        {
                            newPixelData[i, j, 0] = (Pixels[i, j, 0] + Pixels[i - 1, j, 0]) / 2;
                            newPixelData[i, j, 1] = (Pixels[i, j, 1] + Pixels[i - 1, j, 1]) / 2;
                            newPixelData[i, j, 2] = (Pixels[i, j, 2] + Pixels[i - 1, j, 2]) / 2;
                            m2[i, j] = m[i, j];
                        }
                        else
                        {
                            newPixelData[i, j, 0] = (Pixels[i, j, 0] + Pixels[i + 1, j, 0] + Pixels[i - 1, j, 0]) / 3;
                            newPixelData[i, j, 1] = (Pixels[i, j, 1] + Pixels[i + 1, j, 1] + Pixels[i - 1, j, 1]) / 3;
                            newPixelData[i, j, 2] = (Pixels[i, j, 2] + Pixels[i + 1, j, 2] + Pixels[i - 1, j, 2]) / 3;
                            m2[i, j] = m[i, j];
                        }

                    }
                    else
                    {
                        newPixelData[i, j, 0] = Pixels[i, j, 0];
                        newPixelData[i, j, 1] = Pixels[i, j, 1];
                        newPixelData[i, j, 2] = Pixels[i, j, 2];
                        m2[i, j] = m[i, j];
                    }
                }
            }
            Pixels = newPixelData;
            return m2;
        }

        public static int[,] AddHorizontalSeam(int[] seam, int[,] m)
        {
            Height++;
            var newPixelData = new int[Width, Height, 3];
            var m2 = new int[Width, Height];
            for (var j = 0; j < Width; j++)
            {
                for (var i = 0; i < Height; i++)
                {
                    if (i > seam[j])
                    {
                        newPixelData[j, i, 0] = Pixels[j, i - 1, 0];
                        newPixelData[j, i, 1] = Pixels[j, i - 1, 1];
                        newPixelData[j, i, 2] = Pixels[j, i - 1, 2];
                        m2[j, i] = m[j, i - 1];
                    }
                    else if (i == seam[j])
                    {
                        if (i == 0)
                        {
                            newPixelData[j, i, 0] = (Pixels[j, i, 0] + Pixels[j, i + 1, 0]) / 2;
                            newPixelData[j, i, 1] = (Pixels[j, i, 1] + Pixels[j, i + 1, 1]) / 2;
                            newPixelData[j, i, 2] = (Pixels[j, i, 2] + Pixels[j, i + 1, 2]) / 2;
                            m2[j, i] = m[j, i];
                        }
                        else if (i == Width - 1 || i == Width - 2)
                        {
                            newPixelData[j, i, 0] = (Pixels[j, i, 0] + Pixels[j, i - 1, 0]) / 2;
                            newPixelData[j, i, 1] = (Pixels[j, i, 1] + Pixels[j, i - 1, 1]) / 2;
                            newPixelData[j, i, 2] = (Pixels[j, i, 2] + Pixels[j, i - 1, 2]) / 2;
                            m2[j, i] = m[j, i];
                        }
                        else
                        {
                            newPixelData[j, i, 0] = (Pixels[j, i, 0] + Pixels[j, i - 1, 0] + Pixels[j, i + 1, 0]) / 3;
                            newPixelData[j, i, 1] = (Pixels[j, i, 1] + Pixels[j, i - 1, 1] + Pixels[j, i + 1, 1]) / 3;
                            newPixelData[j, i, 2] = (Pixels[j, i, 2] + Pixels[j, i - 1, 2] + Pixels[j, i + 1, 2]) / 3;
                            m2[j, i] = m[j, i];
                        }

                    }
                    else
                    {
                        newPixelData[j, i, 0] = Pixels[j, i, 0];
                        newPixelData[j, i, 1] = Pixels[j, i, 1];
                        newPixelData[j, i, 2] = Pixels[j, i, 2];
                        m2[j, i] = m[j, i];
                    }
                }
            }
            Pixels = newPixelData;
            return m2;
        }

        public static Bitmap ToImage()
        {
            //throw new NotImplementedException();
            //int[] bytes = new int[Width * Height * 3];

            //for (int i = 0; i < Height; i++)
            //{
            //    for (int j = 0; j < Width; j++)
            //    {
            //        bytes[3 * j + i * Width] = Pixels[j, i, 0];
            //        bytes[3 * j + i * Width + 1] = Pixels[j, i, 1];
            //        bytes[3 * j + i * Width + 2] = Pixels[j, i, 2];
            //    }
            //}

            //var img = Image.FromStream(new MemoryStream());


            //return null;

            var bmp = new Bitmap(Width, Height);
            var bmpData = new LockBitmap(bmp);
            bmpData.LockBits();

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    bmpData.SetPixel(i, j, Color.FromArgb(Pixels[i, j, 0], Pixels[i, j, 1], Pixels[i, j, 2]));
                }
            }
            bmpData.UnlockBits();
            return bmp;
        }

        public static Bitmap ToImage(int[,] energy)
        {
            //throw new NotImplementedException();
            //int[] bytes = new int[Width * Height * 3];

            //for (int i = 0; i < Height; i++)
            //{
            //    for (int j = 0; j < Width; j++)
            //    {
            //        bytes[3 * j + i * Width] = Pixels[j, i, 0];
            //        bytes[3 * j + i * Width + 1] = Pixels[j, i, 1];
            //        bytes[3 * j + i * Width + 2] = Pixels[j, i, 2];
            //    }
            //}

            //var img = Image.FromStream(new MemoryStream());


            //return null;

            var bmp = new Bitmap(Width, Height);
            var bmpData = new LockBitmap(bmp);
            bmpData.LockBits();

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    bmpData.SetPixel(i, j, Color.FromArgb(energy[i, j], energy[i, j], energy[i, j]));
                }
            }
            bmpData.UnlockBits();
            return bmp;
        }
    }
}