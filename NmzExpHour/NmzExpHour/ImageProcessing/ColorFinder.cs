using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NmzExpHour.ImageProcessing
{

    public interface IColorFinder
    {
        Point FindFirstColorLocation(Bitmap img, Color color);
        Point FindLastColorLocation(Bitmap img, Color color);
        List<Point> FindColorsLocations(Bitmap img, Color color);
    }

    public class ColorFinder : IColorFinder
    {
        public Point FindFirstColorLocation(Bitmap img, Color color)
        {
            List<Point> listPoints = FindColorsLocations(img, color);

            return listPoints.Count != 0 ? listPoints.First() : new Point();
        }

        public Point FindLastColorLocation(Bitmap img, Color color)
        {
            List<Point> listPoints = FindColorsLocations(img, color);

            return listPoints.Count != 0 ? listPoints.Last() : new Point();
        }

        public List<Point> FindColorsLocations(Bitmap img, Color color)
        {
            List<Point> listPoints = new List<Point>();

            unsafe
            {
                BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, img.PixelFormat);
                int bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(img.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];

                        if (Color.FromArgb(red, green, blue) == color)
                        {
                            listPoints.Add(new Point(x / 4, y));
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            return listPoints;
        }

        public List<Color> FindColors(Bitmap img)
        {
            List<Color> colors = new List<Color>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (!colors.Contains(img.GetPixel(x, y)))
                    {
                        colors.Add(img.GetPixel(x, y));
                    }
                }
            }

            return colors;
        }
    }
}