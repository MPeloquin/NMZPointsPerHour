using System.Drawing;

namespace NmzExpHour.OCR.ImageProcessing
{
    public interface INMZPointsPictureFinder
    {
        Bitmap FindPoints(Bitmap img);
    }

    public class NMZInmzPointsPicturePictureFinder : INMZPointsPictureFinder
    {
        public NMZInmzPointsPicturePictureFinder()
        {
            ColorFinder = new ColorFinder();
        }

        public Bitmap FindPoints(Bitmap img)
        {
            Point firstPoint = ColorFinder.FindFirstColor(img, Color.FromArgb(127, 70, 15));
            Point lastPoint = ColorFinder.FindLastColor(img, Color.FromArgb(127, 70, 15));

            return ExtractNMZPointsImageFrom(img, lastPoint, firstPoint);
        }

        private static Bitmap ExtractNMZPointsImageFrom(Bitmap img, Point lastPoint, Point firstPoint)
        {
            Size rectSize = new Size((lastPoint.X - firstPoint.X + 1), (lastPoint.Y - firstPoint.Y + 1));

            return img.Clone(new Rectangle(firstPoint, rectSize), img.PixelFormat);
        }


        public IColorFinder ColorFinder { get; set; }
    }
}