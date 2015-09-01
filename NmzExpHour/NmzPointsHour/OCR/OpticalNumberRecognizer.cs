using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.Utils;

namespace NmzPointsHour.OCR
{
    public interface IOpticalNumberRecognizer
    {
        string RecognizeNumber(Bitmap img);
    }

    public class OpticalNumberRecognizer : IOpticalNumberRecognizer
    {
        private const int TwoOrFiveOrNine = 15;
        private const int OneOrSeven = 11;
        private const int ThreeOrFour = 13;
        private const int SixOrZero = 18;
        private const int Heigh = 19;
        private readonly ColorFinder finder;
        private readonly ListCleaner listCleaner;
        private readonly NumberSignatureRecognizer numberSignatureRecognizer;

        public OpticalNumberRecognizer()
        {
            numberSignatureRecognizer = new NumberSignatureRecognizer();
            finder = new ColorFinder();
            listCleaner = new ListCleaner();
        }

        public string RecognizeNumber(Bitmap img)
        {
            var count = finder.CountColor(img, Color.FromArgb(0, 0, 0), 10);

            switch (count)
            {
                case OneOrSeven:
                    return Differenciate(new List<string> { "1", "7" }, img);
                case TwoOrFiveOrNine:
                    return Differenciate(new List<string> {"9","5", "2"}, img);
                case ThreeOrFour:
                    return Differenciate(new List<string>{"3","4"}, img);
                case SixOrZero:
                    return Differenciate(new List<string>{"0","6"}, img);;
            }
            return "8";
        }

        private string Differenciate(List<string> numbers, Bitmap img)
        {
            List<int> signature = ExtractNumberSignature(img);

            return numberSignatureRecognizer.RecognizeSignature(signature, numbers);
        }

        private unsafe List<int> ExtractNumberSignature(Bitmap img)
        {
            List<int> signature = new List<int>(new int[img.Height]);
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
                        int red = currentLine[x + 2];
                        int green = currentLine[x + 1];
                        int blue = currentLine[x];

                        if ((red <= 10) && (green <= 10) && (blue <= 10))
                        {
                            signature[y]++;
                        }
                    }
                }
                img.UnlockBits(bitmapData);
            }

            return listCleaner.TrimZeros(signature);
        }
    }
}