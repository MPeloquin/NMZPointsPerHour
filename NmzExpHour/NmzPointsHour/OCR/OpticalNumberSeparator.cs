using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.Utils;

namespace NmzPointsHour.OCR
{
    public interface IOpticalNumberSeparator
    {
        List<Bitmap> Separate(Bitmap img);
    }

    public class OpticalNumberSeparator : IOpticalNumberSeparator
    {
        public List<Bitmap> Separate(Bitmap img32)
        {
            Bitmap img = new PixelFormatConverter().To24BppRGB(img32);

            List<Tuple<bool, int>> whiteColumns = FindWhiteColumnsInImage(img);

            new ListCleaner().RemoveBackToBackTrueColumnAndFalseColumns(whiteColumns);

            return SeparateNumbersWithWhiteColumnsAsDelimiter(img, whiteColumns);
        }

        private static List<Tuple<bool, int>> InitiateListWithTrueAndColumnIndex(int imgWidth)
        {
            List<Tuple<bool, int>> whiteColumns = new List<Tuple<bool, int>>(imgWidth);

            for (int i = 0; i < whiteColumns.Capacity; i++)
            {
                whiteColumns.Add(new Tuple<bool, int>(true, i));
            }
            return whiteColumns;
        }

        private static List<Bitmap> SeparateNumbersWithWhiteColumnsAsDelimiter(Bitmap img, List<Tuple<bool, int>> whiteColumns)
        {
            List<Bitmap> result = new List<Bitmap>
            {
                img.Clone(Rectangle.FromLTRB(0, 0, whiteColumns[1].Item2, img.Height), img.PixelFormat)
            };

            for (int i = 1; i < whiteColumns.Count - 1; i++)
            {
                result.Add(img.Clone(Rectangle.FromLTRB(whiteColumns[i].Item2, 0, whiteColumns[i + 1].Item2, img.Height),
                    img.PixelFormat));
            }
            return result;
        }

        private static List<Tuple<bool, int>> FindWhiteColumnsInImage(Bitmap img)
        {
            List<Tuple<bool, int>> whiteColumns = InitiateListWithTrueAndColumnIndex(img.Width);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (img.GetPixel(x, y).IsAlmost(Color.Black))
                    {
                        whiteColumns[x] = new Tuple<bool, int>(false, x);
                        break;
                    }
                }
            }

            return whiteColumns;
        }
    }
}
