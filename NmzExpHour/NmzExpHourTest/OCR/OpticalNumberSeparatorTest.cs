using System.Collections.Generic;
using System.Drawing;
using NmzExpHour.OCR;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.OCR
{
    [TestFixture]
    public class OpticalNumberSeparatorTest
    {
        [Test]
        public void CanSeperateTwoNumbers()
        {
            Bitmap nineFiveImage = new Bitmap(Images.OCRTwoNumbers);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(nineFiveImage);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
        }

        [Test]
        public void CanSeperateThreeNumbers()
        {
            Bitmap nineFiveHeighImage = new Bitmap(Images.OCRThreeNumbers);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(nineFiveHeighImage);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("8", ocr.RecognizeNumber(actual[2]));
        }

        [Test]
        public void CanSeperateMultipleNumbers()
        {
            Bitmap rowOfNumbers = new Bitmap(Images.OCRRow);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(rowOfNumbers);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("8", ocr.RecognizeNumber(actual[2]));
            Assert.AreEqual("0", ocr.RecognizeNumber(actual[3]));
            Assert.AreEqual("7", ocr.RecognizeNumber(actual[4]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[5]));
        }

        [Test]
        public void CanSeperateMultipleNumbersWithAComma()
        {
            Bitmap rowOfNumbersWithComma = new Bitmap(Images.OCRRowComma);

            List<Bitmap> actual = new OpticalNumberSeparator().Separate(rowOfNumbersWithComma);

            var ocr = new OpticalNumberRecognizer();

            Assert.AreEqual("9", ocr.RecognizeNumber(actual[0]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[1]));
            Assert.AreEqual("8", ocr.RecognizeNumber(actual[2]));
            Assert.AreEqual("0", ocr.RecognizeNumber(actual[3]));
            Assert.AreEqual("7", ocr.RecognizeNumber(actual[4]));
            Assert.AreEqual("5", ocr.RecognizeNumber(actual[5]));
        }
    }
}
