using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NmzExpHour.OCR.ImageProcessing
{

    public interface IColorFinder
    {
        Point FindFirstColor(Bitmap img, Color color);
        Point FindLastColor(Bitmap img, Color color);
        List<Point> FindPoints(Bitmap img, Color color);
    }

    public class ColorFinder : IColorFinder
    {
        public Point FindFirstColor(Bitmap img, Color color)
        {
            List<Point> listPoints = FindPoints(img, color);

            return listPoints.Count != 0 ? listPoints.First() : new Point(-1, -1);
        }

        public Point FindLastColor(Bitmap img, Color color)
        {
            List<Point> listPoints = FindPoints(img, color);

            return listPoints.Count != 0 ? listPoints.Last() : new Point(-1, -1);
        }

        public List<Point> FindPoints(Bitmap img, Color color)
        {
            List<Point> listPoints = new List<Point>();

            LockBitmap lockBitmap = new LockBitmap(img);
            lockBitmap.LockBits();

            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    if (lockBitmap.GetPixel(x, y) == color)
                    {
                        listPoints.Add(new Point(x, y));
                    }
                }
            }
            lockBitmap.UnlockBits();

            return listPoints;
        }
    }
}