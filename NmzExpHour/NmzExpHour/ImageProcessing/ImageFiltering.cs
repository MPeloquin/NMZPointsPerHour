using System.Collections.Generic;
using System.Drawing;

namespace NmzExpHour.ImageProcessing
{
    public class ImageFiltering
    {

        public Bitmap FilterImage(Bitmap img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (img.GetPixel(x, y) != Colors.Font)
                        img.SetPixel(x, y, Color.White);
                    else
                        img.SetPixel(x, y, Color.Black);
                }
            }
            return img;
        }
    }
}