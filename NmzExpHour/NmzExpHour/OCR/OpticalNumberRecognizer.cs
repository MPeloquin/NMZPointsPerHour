using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using NmzExpHour.ImageProcessing;

namespace NmzExpHour.OCR
{
    public class OpticalNumberRecognizer
    {
        private const int FiveOrTwoOrNine = 15;
        private const int OneOrSeven = 11;
        private const int ThreeOrFour = 13;
        private const int SixOrZero = 18;
        private const int Heigh = 19;
        private readonly ColorFinder finder;

        private readonly List<Tuple<string, List<int>>> signatures;


        public OpticalNumberRecognizer()
        {
            finder = new ColorFinder();

            signatures = new List<Tuple<string, List<int>>>
            {		
                new Tuple<string, List<int>>("0", new List<int> {3, 2, 2, 2, 2, 2, 2, 3}),
                new Tuple<string, List<int>>("1", new List<int> {1, 2, 1, 1, 1, 1, 1, 3}),
                new Tuple<string, List<int>>("3", new List<int> {2, 2, 1, 2, 1, 1, 2, 2}),
                new Tuple<string, List<int>>("5", new List<int> {4, 1, 1, 3, 1, 1, 2, 2}),
                new Tuple<string, List<int>>("9", new List<int> {3, 2, 2, 2, 3, 1, 1, 1}),
            };
        }

        public string RecognizeNumber(Bitmap img)
        {
            var count = finder.CountColor(img, Color.FromArgb(0, 0, 0), 3);

            switch (count)
            {
                case OneOrSeven:
                    return Differenciate(new List<string> { "1", "7" }, img);
                case FiveOrTwoOrNine:
                    return Differenciate(new List<string> {"9","5", "2"}, img);
                case ThreeOrFour:
                    return Differenciate(new List<string>{"3","4"}, img);
                case SixOrZero:
                    return Differenciate(new List<string>{"0","6"}, img);;
                case Heigh:
                    return "8";
            }

            return "0";
        }

        private string Differenciate(List<string> numbers, Bitmap img)
        {
            List<int> rows = new List<int>(new int[img.Height]);

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

                        if ((red <= 3) && (green <= 3) && (blue <= 3))
                        {
                            rows[y]++;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }
            
            rows = RemoveZerosFromRow(rows);

            foreach (var number in numbers)
            {
                if (signatures.Any(signature => signature.Item1 == number && IsSignature(rows, signature.Item2)))
                {
                    return number;
                }
            }

            return numbers.Last();
        }

        private static List<int> RemoveZerosFromRow(List<int> rows)
        {
            int firstElement = -1;
            int lastElement = 0;

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i] != 0 && firstElement == -1)
                    firstElement = i;
                if (rows[i] != 0)
                    lastElement = i;
            }

            for (int i = 0; i < firstElement; i++)
            {
                rows.RemoveAt(i);
                i--;
                firstElement--;
                lastElement--;
            }

            for (int i = lastElement + 1; i < rows.Count; i++)
            {
                rows.RemoveAt(i);
                i--;
            }

            return rows;
        }

        private bool IsSignature(List<int> rows, List<int> signature)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i] != signature[i])
                    return false;
            }
            return true;
        }
    }
}