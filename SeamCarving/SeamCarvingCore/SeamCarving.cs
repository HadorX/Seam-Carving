using System.Drawing;
using System.Linq;

namespace SeamCarvingCore
{
    public static class SeamCarving
    {
        public static Bitmap FindSeam(Bitmap bitmap, int[,] m)
        {
            for (int i = 1; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    m[j, i] = m[j, i] + new[] { j - 1 < 0 ? 256 : m[j - 1, i - 1], m[j, i - 1], j + 1 >= bitmap.Width ? 256 : m[j + 1, i - 1] }.Min();
                }
            }

            var min = int.MaxValue;
            var index = 0;
            for (int i = 0; i < bitmap.Width; i++)
            {
                if (m[i, bitmap.Height - 1] < min)
                {
                    min = m[i, bitmap.Height - 1];
                    index = i;
                }
            }

            LockBitmap lockBitmapPost = new LockBitmap(bitmap);
            lockBitmapPost.LockBits();
            lockBitmapPost.SetPixel(index, bitmap.Height - 1, Color.Red);
            for (int i = bitmap.Height - 2; i >= 0; i--)
            {
                var int1 = index == 0 ? int.MaxValue : m[index - 1, i];
                var int2 = m[index, i];
                var int3 = index == bitmap.Width - 1 ? int.MaxValue : m[index + 1, i];

                var array = new[] { int1, int2, int3 };
                var minimum = array.Min();
                if (int1 == minimum && index > 0)
                    index--;
                else if (int2 != minimum && int3 == minimum && index < bitmap.Width - 1)
                    index++;

                lockBitmapPost.SetPixel(index, i, Color.Red);
            }
            lockBitmapPost.UnlockBits();
            return bitmap;
        }

        public static Bitmap FindImageEnergy(Image image, EnergyFunction energyFunction, out int[,] m)
        {
            var bitmap = new Bitmap(image, image.Width, image.Height);
            var bitmapPost = new Bitmap(image.Width, image.Height);

            var lockBitmap = new LockBitmap(bitmap);
            var lockBitmapPost = new LockBitmap(bitmapPost);

            lockBitmap.LockBits();
            lockBitmapPost.LockBits();

            m = new int[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int red, green, blue;
                    var color1 = lockBitmap.GetPixel(i, j);
                    var color2 = lockBitmap.GetPixel(i - 1, j);
                    var color3 = lockBitmap.GetPixel(i, j - 1);
                    var color4 = lockBitmap.GetPixel(i + 1, j);
                    var color5 = lockBitmap.GetPixel(i, j + 1);

                    red = 4 * color1.R - color2.R - color3.R - color4.R - color5.R;
                    green = 4 * color1.G - color2.G - color3.G - color4.G - color5.G;
                    blue = 4 * color1.B - color2.B - color3.B - color4.B - color5.B;
                    red = Clamp(red, 0, 255);
                    green = Clamp(green, 0, 255);
                    blue = Clamp(blue, 0, 255);
                    var intensity = (red + green + blue) / 3;
                    m[i, j] = intensity;
                    lockBitmapPost.SetPixel(i, j, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            lockBitmap.UnlockBits();
            lockBitmapPost.UnlockBits();
            return bitmapPost;
        }

        internal static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}