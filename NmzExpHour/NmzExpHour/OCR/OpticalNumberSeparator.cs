using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace NmzExpHour.OCR
{
    public class OpticalNumberSeparator
    {
        public List<Bitmap> Separate(Bitmap img)
        {
            Point topLimit = new Point(-1,-1);
            Point bottomLimit = new Point(-1,-1);

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
                            if (TopLimitNotYetSet(topLimit))
                                topLimit.Y = y;
                            if (CurrentXIsMoreToTheLeftThanLimit(topLimit, actualX))
                                topLimit.X = actualX;
                            if (CurrentXIsMoreToTheRightThenLimitButNotTooFar(actualX, bottomLimit, topLimit))
                                bottomLimit.X = actualX;
                            if (CurrentYIsMoreToTheBottomThanLimitButNotTooFar(y, bottomLimit, actualX, topLimit))
                                bottomLimit.Y = y;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            Size rectSize = new Size((bottomLimit.X - topLimit.X + 1), (bottomLimit.Y - topLimit.Y + 1));

            var result =  new List<Bitmap>
            {
                img.Clone(new Rectangle(new Point(topLimit.X, topLimit.Y), rectSize), img.PixelFormat),
            };


            if (ImageHasAnOtherNumber(img, bottomLimit))
                result.AddRange(Separate(img.Clone(new Rectangle(new Point(bottomLimit.X + 1, 0), new Size(img.Width - bottomLimit.X - 1, img.Height)),
                                        img.PixelFormat)));

            return result;
        }

        private static bool ImageHasAnOtherNumber(Bitmap img, Point bottomLimit)
        {
            return (img.Width - 1 - bottomLimit.X) > 5;
        }

        private static bool CurrentYIsMoreToTheBottomThanLimitButNotTooFar(int y, Point bottomLimit, int actualX, Point topLimit)
        {
            return y > bottomLimit.Y && actualX < (topLimit.X + 5);
        }

        private static bool CurrentXIsMoreToTheRightThenLimitButNotTooFar(int actualX, Point bottomLimit, Point topLimit)
        {
            return actualX > bottomLimit.X && actualX < (topLimit.X + 5);
        }

        private static bool CurrentXIsMoreToTheLeftThanLimit(Point topLimit, int actualX)
        {
            return topLimit.X == -1 || actualX < topLimit.X;
        }

        private static bool TopLimitNotYetSet(Point topLimit)
        {
            return topLimit.Y == -1;
        }
    }
}
