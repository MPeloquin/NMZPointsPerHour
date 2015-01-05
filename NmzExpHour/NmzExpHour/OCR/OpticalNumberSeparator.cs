using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NmzExpHour.ImageProcessing;

namespace NmzExpHour.OCR
{
    public class OpticalNumberSeparator
    {
        public List<Bitmap> Separate(Bitmap img)
        {
            if(new ColorFinder().CountColor(img, Color.FromArgb(0,0,0), 10) == 0)
                return new List<Bitmap>();


            int leftX = -1;
            int rightX = -1;
            Point topLimit = new Point(-1,-1);
            Point bottomLimit = new Point(-1,-1);
            int topY = -1;
            int bottomY = -1;

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
                        int red = currentLine[x + 2];
                        int green = currentLine[x + 1];
                        int blue = currentLine[x];
                        int actualX = x/3;

                        if ((red <= 10) && (green <= 10) && (blue <= 10))
                        {
                            if (topY == -1)
                                topY = y;
                            if (leftX == -1 || actualX < leftX)
                                leftX = actualX;
                            if (actualX > rightX && actualX < (leftX + 5))
                                rightX = actualX;
                            if (y > bottomY && actualX < (leftX + 5))
                                bottomY = y;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            Size rectSize = new Size((rightX - leftX+1), (bottomY - topY+1));

            var result =  new List<Bitmap>
            {
                img.Clone(new Rectangle(new Point(leftX, topY), rectSize), img.PixelFormat),
            };


            if ((img.Width - 1 - rectSize.Width) > 5)
                result.AddRange(Separate(img.Clone(new Rectangle(new Point(rightX + 1, 0), new Size(img.Width - rightX - 1, img.Height)),
                                        img.PixelFormat)));

            return result;
        }
    }
}
