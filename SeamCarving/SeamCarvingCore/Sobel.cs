using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingCore
{
    public class Sobel : EnergyFunctionBase
    {
        protected override int GetPixelEnergy(int x, int y)
        {
            int[] pixels = new int[9];

            pixels[0] = GetPixelData(x - 1, y - 1);
            pixels[1] = GetPixelData(x, y - 1);
            pixels[2] = GetPixelData(x + 1, y - 1);
            pixels[3] = GetPixelData(x - 1, y);
            pixels[4] = GetPixelData(x, y);
            pixels[5] = GetPixelData(x + 1, y);
            pixels[6] = GetPixelData(x - 1, y + 1);
            pixels[7] = GetPixelData(x, y + 1);
            pixels[8] = GetPixelData(x + 1, y + 1);

            int xSobel = pixels[8] + 2 * pixels[5] + pixels[2] - pixels[0] - 2 * pixels[3] - pixels[6];
            int ySobel = pixels[8] + 2 * pixels[7] + pixels[6] - pixels[2] - 2 * pixels[1] - pixels[0];

            int sobel = Math.Abs(xSobel) + Math.Abs(ySobel);

            if (sobel > 255)
            {
                sobel = 255;
            }

            return (byte)sobel;
        }
    }
}
