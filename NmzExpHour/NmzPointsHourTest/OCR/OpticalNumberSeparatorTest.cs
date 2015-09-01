using System.Collections.Generic;
using System.Drawing;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.OCR;
using NmzPointsHourTest.Data;
using NUnit.Framework;

namespace NmzPointsHourTest.OCR
{
    [TestFixture]
    public class OpticalNumberSeparatorTest
    {
        [Test]
        public void CanSeparateTwoNumbers()
        {
            Bitmap nineFiveImage = new Bitmap(Images.OCRTwoNumbers);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(nineFiveImage);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
        }

        [Test]
        public void CanSeparateThreeNumbers()
        {
            Bitmap nineFiveHeighImage = new Bitmap(Images.OCRThreeNumbers);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(nineFiveHeighImage);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("8", ocr.RecognizeNumber(actual[2]));
        }

        [Test]
        public void CanSeparateMultipleNumbers()
        {
            Bitmap rowOfNumbers = new Bitmap(Images.OCRRow);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(new PixelFormatConverter().To24BppRGB(rowOfNumbers));

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("8", ocr.RecognizeNumber(actual[2]));
            Assert.AreEqual("0", ocr.RecognizeNumber(actual[3]));
            Assert.AreEqual("7", ocr.RecognizeNumber(actual[4]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[5]));
        }

        [Test]
        public void CanSeparateNumbersWithOnes()
        {
            Bitmap ones = new Bitmap(Images.Ones);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(ones);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("1", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("0", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("1", ocr.RecognizeNumber(actual[2]));
            Assert.AreEqual("6", ocr.RecognizeNumber(actual[3]));
            Assert.AreEqual("6", ocr.RecognizeNumber(actual[4]));
            Assert.AreEqual("9", ocr.RecognizeNumber(actual[5]));
            Assert.AreEqual("9", ocr.RecognizeNumber(actual[6]));
        }
    }
}
