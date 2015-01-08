using System.Drawing;

namespace NmzExpHour.ImageProcessing
{
    public class PixelFormatConverter
    {
        public Bitmap To24BppRGB(Bitmap other)
        {
            Bitmap clone = new Bitmap(other.Width, other.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(other, new Rectangle(0, 0, clone.Width, clone.Height));
            }
            return clone;
        }
    }
}