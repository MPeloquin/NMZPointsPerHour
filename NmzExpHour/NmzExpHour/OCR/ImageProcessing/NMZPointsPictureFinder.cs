using System.Drawing;

namespace NmzExpHour.OCR.ImageProcessing
{
    public interface INMZPointsPictureFinder
    {
        Bitmap FindPoints(Bitmap img);
    }

    public class NMZInmzPointsPicturePictureFinder : INMZPointsPictureFinder
    {
        public Bitmap FindPoints(Bitmap img)
        {
            return new Bitmap(1,1);
        }


    }
}