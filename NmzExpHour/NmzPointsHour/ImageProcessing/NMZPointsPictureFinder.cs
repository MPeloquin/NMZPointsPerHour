using System;
using System.Drawing;

namespace NmzPointsHour.ImageProcessing
{
    public interface INMZPointsImageFinder
    {
        Bitmap FindNMZPoints(Bitmap img);
    }

    public class NMZPointsImageFinder : INMZPointsImageFinder
    {
        public IColorFinder ColorFinder { get; set; }

        public NMZPointsImageFinder()
        {
            ColorFinder = new ColorFinder();
        }

        public Bitmap FindNMZPoints(Bitmap img)
        {
            Point firstPoint = ColorFinder.FindFirstColorLocation(img, NMZColors.Border);
            Point lastPoint = ColorFinder.FindLastColorLocation(img, NMZColors.Border);

            if (firstPoint.IsEmpty || lastPoint.IsEmpty)
                return new Bitmap(1, 1);

            return ExtractNMZPointsImageFrom(img, lastPoint, firstPoint);
        }

        private static Bitmap ExtractNMZPointsImageFrom(Bitmap img, Point lastPoint, Point firstPoint)
        {
            Size rectSize = new Size((lastPoint.X - firstPoint.X + 1), (lastPoint.Y - firstPoint.Y + 1));

            if(rectSize.Height <= 0 || rectSize.Width <= 0)
                return new Bitmap(1,1);

            return img.Clone(new Rectangle(firstPoint, rectSize), img.PixelFormat);
        }
    }
}