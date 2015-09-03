using System.Drawing;
using NUnit.Framework;
using NmzPointsHour.OCR;
using NmzPointsHourTest.Data;

namespace NmzPointsHourTest.OCR
{
    [TestFixture]
    public class OpticalNumberRecognizerTest
    {
        [Test]
        public void CanRecognize1()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCROne);
            
            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("1", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize2()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRTwo);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("2", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize3()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRThree);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("3", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize4()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRFour);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("4", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize5()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRFive);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("5", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize6()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRSix);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("6", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize7()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRSeven);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("7", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize8()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRHeigh);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("8", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize9()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRNine);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("9", actual);

            img.Dispose();
        }
        [Test]
        public void CanRecognize0()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRZero);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("0", actual);

            img.Dispose();
        }


        [Test]
        public void CanRecognize9WithExtraWhiteSpaces()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRLarge9);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("9", actual);

            img.Dispose();
        }

        [Test]
        public void UnrecognizableNumberReturns0()
        {
            OpticalNumberRecognizer ocr = new OpticalNumberRecognizer();
            Bitmap img = new Bitmap(Images.OCRNoNumber);

            var actual = ocr.RecognizeNumber(img);

            Assert.AreEqual("0", actual);

            img.Dispose();
        }
    }
}
