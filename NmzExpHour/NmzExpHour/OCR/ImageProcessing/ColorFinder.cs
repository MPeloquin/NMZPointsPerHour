using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NmzExpHour.OCR.ImageProcessing
{
    public class ColorFinder
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

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    if (img.GetPixel(i, j).Equals(color))
                    {
                        listPoints.Add(new Point(i,j));
                    }
                }
            }

            return listPoints;
        }
    }
}