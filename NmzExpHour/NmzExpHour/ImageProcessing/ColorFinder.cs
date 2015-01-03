using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (img.GetPixel(x, y) == color)
                    {
                        listPoints.Add(new Point(x, y));
                    }
                }
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