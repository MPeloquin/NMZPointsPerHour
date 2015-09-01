using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using NmzPointsHour.Utils;

namespace NmzPointsHour.ImageProcessing
{

    public interface IColorFinder
    {
        Point FindFirstColorLocation(Bitmap img, Color color);
        Point FindLastColorLocation(Bitmap img, Color color);
        List<Point> FindColorsLocations(Bitmap img, Color color);
        int CountColor(Bitmap img, Color color, int tolerance = 0);
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
                int bytesPerPixel = Image.GetPixelFormatSize(img.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color currentColor = Color.FromArgb(currentLine[x + 2], currentLine[x + 1], currentLine[x]);
                        
                        if (currentColor == color)
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

            unsafe
            {
                BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, img.PixelFormat);
                int bytesPerPixel = Image.GetPixelFormatSize(img.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color currentColor = Color.FromArgb(currentLine[x + 2], currentLine[x + 1], currentLine[x]);

                        if (!colors.Contains(currentColor))
                        {
                            colors.Add(currentColor);
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }


            return colors;
        }

        public int CountColor(Bitmap img, Color color, int tolerance = 0)
        {
            int count = 0;

            unsafe
            {
                BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, img.PixelFormat);
                int bytesPerPixel = Image.GetPixelFormatSize(img.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color currentColor = Color.FromArgb(currentLine[x + 2], currentLine[x + 1], currentLine[x]);

                        if (currentColor.IsAlmost(color, tolerance))
                        {
                            count++;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            return count;
        }
    }
}