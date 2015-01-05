using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace NmzExpHour.ImageProcessing
{
    public class ImageFiltering
    {

        public Bitmap FilterImage(Bitmap img)
        {
            unsafe
            {
                BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite,
                    img.PixelFormat);
                int bytesPerPixel = Image.GetPixelFormatSize(img.PixelFormat)/8;
                int widthInBytes = bitmapData.Width*bytesPerPixel;
                byte* ptrFirstPixel = (byte*) bitmapData.Scan0;

                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y*bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Color currentColor = Color.FromArgb(currentLine[x + 2], currentLine[x + 1], currentLine[x]);

                        if (currentColor != Colors.Font)
                        {
                            currentLine[x] = 255;
                            currentLine[x + 1] = 255;
                            currentLine[x + 2] = 255;
                        }
                        else
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 0;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            return img;
        }
    }
}