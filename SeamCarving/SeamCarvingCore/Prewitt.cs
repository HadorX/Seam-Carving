using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamCarvingCore
{
    class Prewitt : EnergyFunctionBase
    {

        protected override int GetPixelEnergy(int x, int y)
        {
            int[] pixels = new int[8];

            pixels[0] = GetPixelData( x - 1, y - 1);
            pixels[1] = GetPixelData( x, y - 1);
            pixels[2] = GetPixelData( x + 1, y - 1);
            pixels[3] = GetPixelData( x - 1, y);
            pixels[4] = GetPixelData( x + 1, y);
            pixels[5] = GetPixelData( x - 1, y + 1);
            pixels[6] = GetPixelData( x, y + 1);
            pixels[7] = GetPixelData( x + 1, y + 1);

            int xPrewitt = pixels[2] + pixels[4] + pixels[7] - pixels[0] - pixels[3] - pixels[5];
            int yPrewitt = pixels[5] + pixels[6] + pixels[7] - pixels[0] - pixels[1] - pixels[2];

            int prewitt = Math.Abs(xPrewitt) + Math.Abs(yPrewitt);


            if (prewitt > 255)
            {
                prewitt = 255;
            }

            return prewitt;
        }

}
}
