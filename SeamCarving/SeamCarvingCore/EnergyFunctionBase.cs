using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingCore
{
    abstract class EnergyFunctionBase
    {
        public int[,] Energy;

        public int[,,] Pixels;

        public int Width;

        public int Height;

        protected abstract int GetPixelEnergy(int x, int y);
        public void ComputeEnergy(int width, int height, int [,,] pixels)
        {
            Width = width;
            Height = height;
            Pixels = pixels;

            Energy = new int[width, height];

            Parallel.For(0, width, x =>
            {
                for (int y = 0; y < height; ++y)
                {
                    Energy[x, y] = GetPixelEnergy(x, y);
                }
            });
        }

        protected int GetPixelData(int x, int y, int color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return 0;
            return Pixels[x, y, color];
        }
    }
}
