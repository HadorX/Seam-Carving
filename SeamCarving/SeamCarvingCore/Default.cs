using System;

namespace SeamCarvingCore
{
    public class Default :EnergyFunctionBase
    {

        protected override int GetPixelEnergy(int x, int y)
        {
            int[] pixels = new int[5];

            pixels[0] = GetPixelData(x, y);
            pixels[1] = GetPixelData(x + 1, y);
            pixels[2] = GetPixelData(x, y + 1);
            pixels[3] = GetPixelData(x, y - 1);
            pixels[4] = GetPixelData(x - 1, y);

            int val = Math.Abs(4*pixels[0] - pixels[1] - pixels[2] - pixels[3] - pixels[4]);

            if (val > 255)
            {
                val = 255;
            }

            return val;
        }
    }
}