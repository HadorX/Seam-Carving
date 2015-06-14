using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingCore
{
    public class RobertsCross : EnergyFunctionBase
    {
        protected override int GetPixelEnergy(int x, int y)
        {
            int[] pixels = new int[4];

            pixels[0] = GetPixelData(x, y);
            pixels[1] = GetPixelData(x + 1, y);
            pixels[2] = GetPixelData(x, y + 1);
            pixels[3] = GetPixelData(x + 1, y + 1);

            int xRoberts = pixels[0] - pixels[3];
            int yRoberts = pixels[1] - pixels[2];

            int roberts = Math.Abs(xRoberts) + Math.Abs(yRoberts);

            if (roberts > 255)
            {
                roberts = 255;
            }

            return roberts;
        }
    }
}
