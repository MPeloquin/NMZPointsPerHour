using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using NmzExpHour.ImageProcessing;
using NmzExpHour.Utils;

namespace NmzExpHour.OCR
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
            
            var whiteColumns = InitiateListWithTrueAndColumnIndex(img);

            FindWhiteColumnsInImage(img, whiteColumns);

            new ListCleaner().RemoveBackToBackTrueColumnAndFalseColumns(whiteColumns);

            return SeparateNumbersWithWhiteColumnsAsDelimiter(img, whiteColumns);
        }

        private static List<Tuple<bool, int>> InitiateListWithTrueAndColumnIndex(Bitmap img)
        {
            List<Tuple<bool, int>> whiteColumns = new List<Tuple<bool, int>>(img.Width);

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

        private static void FindWhiteColumnsInImage(Bitmap img, List<Tuple<bool, int>> emptyColumn)
        {
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (img.GetPixel(x, y).IsAlmost(Color.Black))
                    {
                        emptyColumn[x] = new Tuple<bool, int>(false, x);
                        break;
                    }
                }
            }
        }
    }
}
