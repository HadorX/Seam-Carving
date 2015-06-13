using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

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
            for (int i = 0; i < bits.Width; i++)
            {
                for (int j = 0; j < bits.Height; j++)
                {
                    var color = bits.GetPixel(i, j);
                    Pixels[i, j, 0] = color.R;
                    Pixels[i, j, 1] = color.G;
                    Pixels[i, j, 2] = color.B;
                }
            }
        }

        public static int[] FindSeamVertical(int[,] m)
        {
            var height = m.GetLength(1);
            var width = m.GetLength(0);
            var result = new int[height];
            for (int i = 1; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    m[j, i] = m[j, i] + new[] { j - 1 < 0 ? 256 : m[j - 1, i - 1], m[j, i - 1], j + 1 >= width ? 256 : m[j + 1, i - 1] }.Min();
                }
            }

            var min = int.MaxValue;
            var index = 0;

            for (int i = 0; i < width; i++)
            {
                if (m[i, height - 1] < min)
                {
                    min = m[i, height - 1];
                    index = i;
                }
            }
            result[height - 1] = index;

            for (int i = height - 2; i >= 0; i--)
            {
                var int1 = index == 0 ? int.MaxValue : m[index - 1, i];
                var int2 = m[index, i];
                var int3 = index == width - 1 ? int.MaxValue : m[index + 1, i];

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

        public static List<int[]> FindSeamsVertical(int[,] m, int n)
        {
            var seamList = new List<int[]>(n);
            var height = m.GetLength(1);
            var width = m.GetLength(0);
            
           


            for (int k = 0; k < n; k++)
            {

                var result = new int[height];
                var min = int.MaxValue;
                var index = 0;

                for (int i = 0; i < width; i++)
                {
                    if (m[i, height - 1] < min)
                    {
                        min = m[i, height - 1];
                        index = i;
                    }
                }
                result[height - 1] = index;
                m[index, height - 1] = int.MaxValue;

                for (int i = height - 2; i >= 0; i--)
                {
                    var int1 = index == 0 ? int.MaxValue : m[index - 1, i];
                    var int2 = m[index, i];
                    var int3 = index == width - 1 ? int.MaxValue : m[index + 1, i];

                    var array = new[] { int1, int2, int3 };
                    var minimum = array.Min();
                    if (int1 == minimum && index > 0)
                        index--;
                    else if (int2 != minimum && int3 == minimum && index < width - 1)
                        index++;

                    result[i] = index;
                    m[index, i] = int.MaxValue;
                }
                seamList.Add(result);
            }

            return seamList;
        }

        public static int[,] FindImageEnergy(EnergyFunction energyFunction, out double avgEnergy, out Bitmap bmp)
        {
            bmp = new Bitmap(Width, Height);

            var lockBitmapPost = new LockBitmap(bmp);

            lockBitmapPost.LockBits();

            int red, green, blue;
            var m = new int[Width, Height];
            avgEnergy = 0;

            var maxIntensity = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    red = Math.Abs(4 * GetPixelData(i, j, 0) - GetPixelData(i - 1, j, 0) - GetPixelData(i, j - 1, 0) - GetPixelData(i + 1, j, 0) - GetPixelData(i, j + 1, 0));
                    green = Math.Abs(4 * GetPixelData(i, j, 1) - GetPixelData(i - 1, j, 1) - GetPixelData(i, j - 1, 1) - GetPixelData(i + 1, j, 1) - GetPixelData(i, j + 1, 1));
                    blue = Math.Abs(4 * GetPixelData(i, j, 2) - GetPixelData(i - 1, j, 2) - GetPixelData(i, j - 1, 2) - GetPixelData(i + 1, j, 2) - GetPixelData(i, j + 1, 2));

                    var intensity = red * red + green * green + blue * blue;
                    m[i, j] = intensity;
                    avgEnergy += intensity;
                    if (intensity > maxIntensity)
                        maxIntensity = intensity;
                }
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var intensity = (int)(((double)m[i, j] / maxIntensity) * 255);
                    lockBitmapPost.SetPixel(i, j, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            lockBitmapPost.UnlockBits();

            //return bitmapPost;
            avgEnergy = avgEnergy / (Width * Height);
            return m;
        }

        private static int GetPixelData(int x, int y, int color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return 0;
            return Pixels[x, y, color];
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
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
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

        public static int[,] AddVerticalSeam(int[] seam, int[,] m)
        {
            Width++;
            var newPixelData = new int[Width, Height, 3];
            var m2 = new int[Width, Height];
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
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

            Bitmap bmp = new Bitmap(Width, Height);
            LockBitmap bmpData = new LockBitmap(bmp);
            bmpData.LockBits();

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    bmpData.SetPixel(i, j, Color.FromArgb(Pixels[i, j, 0], Pixels[i, j, 1], Pixels[i, j, 2]));
                }
            }
            bmpData.UnlockBits();
            return bmp;
        }
    }
}