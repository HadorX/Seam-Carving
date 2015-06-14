using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingCore
{
    abstract public class EnergyFunctionBase
    {
        public int[,] Energy;

        public int[, ,] Pixels;

        public int Width;

        public int Height;

        public double AvgEnergy = 0;

        protected abstract int GetPixelEnergy(int x, int y);

        public void ComputeEnergy(int width, int height, int[, ,] pixels)
        {
            Width = width;
            Height = height;
            Pixels = (int[, ,])pixels.Clone();

            Energy = new int[width, height];

            Parallel.For(0, width, x =>
            {
                for (int y = 0; y < height; ++y)
                {
                    Energy[x, y] = GetPixelEnergy(x, y);
                }
            });
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    AvgEnergy += Energy[x, y];
                }
            AvgEnergy /= Width * Height;
        }

        public void UpdateEnergyVerticalSeam(int width, int height, int[, ,] pixels, int[] seam)
        {
            Width = width;
            Height = height;
            Pixels = (int[, ,])pixels.Clone();

            for (var j = 0; j < Height; j++)
            {
                for (var i = Math.Max(0, seam[j] - 3); i < seam[j] + 3 && i < Width; i++)
                {
                    Energy[i, j] = GetPixelEnergy(i, j);
                }
            }

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    AvgEnergy += Energy[x, y];
                }
            AvgEnergy /= Width * Height;
        }

        public void UpdateEnergyHorizontalSeam(int width, int height, int[, ,] pixels, int[] seam)
        {
            Width = width;
            Height = height;
            Pixels = (int[, ,])pixels.Clone();

            for (var i = 0; i < Width; i++)
            {
                for (var j = Math.Max(0, seam[i] - 3); j < seam[i] + 3 && j < Height; j++)
                {
                    Energy[i, j] = GetPixelEnergy(i, j);
                }
            }

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    AvgEnergy += Energy[x, y];
                }
            AvgEnergy /= Width * Height;
        }

        protected int GetPixelData(int x, int y)
        {
            var kara = 0;
            //if (x < 0 || y < 0 || x >= Width || y >= Height)
            //{
            //    kara = 50;
            //    return 255;
            //}
            if (x < 0)
                x = 0;
            if (x >= Width)
                x = Width - 1;
            if (y < 0)
                y = 0;
            if (y >= Height)
                y = Height - 1;
            var val = (0.2126 * Pixels[x, y, 0]) + (0.7152 * Pixels[x, y, 1]) + (0.0722 * Pixels[x, y, 2]) + kara;
            if (val > 255)
                val = 255;
            return (int)(val);
        }

    }
}
