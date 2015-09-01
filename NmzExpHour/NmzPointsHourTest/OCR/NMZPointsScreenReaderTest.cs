using System.Collections.Generic;
using System.Drawing;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.OCR;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.OCR
{
    [TestFixture]
    public class NMZPointsScreenReaderTest
    {
        private NMZPointsScreenReader nmzPointsScreenReader;

        [SetUp]
        public void SetUp()
        {
            nmzPointsScreenReader = new NMZPointsScreenReader();

            var ssTaker = Substitute.For<IScreenshotTaker>();
            var pointsFinder = Substitute.For<INMZPointsImageFinder>();
            var ocr = Substitute.For<IOpticalNumberRecognizer>();
            var imageFilterer = Substitute.For<IImageFilterer>();
            var separator = Substitute.For<IOpticalNumberSeparator>();

            separator.Separate(new Bitmap(1, 1)).ReturnsForAnyArgs(new List<Bitmap>());
            pointsFinder.FindNMZPoints(new Bitmap(1, 1)).ReturnsForAnyArgs(new Bitmap(10, 10));

            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.NmzPointsImageFinder = pointsFinder;
            nmzPointsScreenReader.OCR = ocr;
            nmzPointsScreenReader.ImageFilterer = imageFilterer;
            nmzPointsScreenReader.Separator = separator;
        }

        [Test]
        public void CallsScreenshotTaker()
        {
            var ssTaker = Substitute.For<IScreenshotTaker>();
            nmzPointsScreenReader.ScreenShotTaker = ssTaker;

            nmzPointsScreenReader.ScreenToNMZPoints();

            ssTaker.Received().TakeNmzScreenShot();
        }

        [Test]
        public void CallsPointsFinderWithScreenShot()
        {
            var ssTaker = Substitute.For<IScreenshotTaker>();
            var pointsFinder = Substitute.For<INMZPointsImageFinder>();
            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.NmzPointsImageFinder = pointsFinder;

            pointsFinder.FindNMZPoints(new Bitmap(1, 1)).ReturnsForAnyArgs(new Bitmap(10, 10));

            Bitmap img = new Bitmap(10, 10);
            ssTaker.TakeNmzScreenShot().Returns(img);

            nmzPointsScreenReader.ScreenToNMZPoints();

            pointsFinder.Received().FindNMZPoints(img);
        }

        [Test]
        public void CallsImageFiltererWithPoints()
        {
            var pointsFinder = Substitute.For<INMZPointsImageFinder>();
            var imageFilterer = Substitute.For<IImageFilterer>();
            nmzPointsScreenReader.NmzPointsImageFinder = pointsFinder;
            nmzPointsScreenReader.ImageFilterer = imageFilterer;

            Bitmap img = new Bitmap(10, 10);
            pointsFinder.FindNMZPoints(new Bitmap(1, 1)).ReturnsForAnyArgs(img);
            
            nmzPointsScreenReader.ScreenToNMZPoints();

            imageFilterer.Received().FilterImage(img);
        }
        
        [Test]
        public void CallsNumberSeparatorWithFilteredImage()
        {
            var separator = Substitute.For<IOpticalNumberSeparator>();
            var imageFilterer = Substitute.For<IImageFilterer>();
            nmzPointsScreenReader.Separator = separator;
            nmzPointsScreenReader.ImageFilterer = imageFilterer;

            Bitmap img = new Bitmap(10, 10);
            imageFilterer.FilterImage(new Bitmap(1, 1)).ReturnsForAnyArgs(img);
            separator.Separate(new Bitmap(1, 1)).ReturnsForAnyArgs(new List<Bitmap>());
            
            nmzPointsScreenReader.ScreenToNMZPoints();

            separator.Received().Separate(img);
        }

        [Test]
        public void CallsOCROnEachNumbers()
        {
            var separator = Substitute.For<IOpticalNumberSeparator>();
            var ocr = Substitute.For<IOpticalNumberRecognizer>(); 
            nmzPointsScreenReader.Separator = separator;
            nmzPointsScreenReader.OCR = ocr;

            Bitmap img1 = new Bitmap(2,2);
            Bitmap img2 = new Bitmap(2,2);
            List<Bitmap> imgs = new List<Bitmap> { img1, img2 };
            separator.Separate(new Bitmap(1, 1)).ReturnsForAnyArgs(imgs);

            nmzPointsScreenReader.ScreenToNMZPoints();

            ocr.Received().RecognizeNumber(img1);
            ocr.Received().RecognizeNumber(img2);
        }

        [Test]
        public void ReturnsNumbersFoundByOCR()
        {
            var separator = Substitute.For<IOpticalNumberSeparator>();
            var ocr = Substitute.For<IOpticalNumberRecognizer>();
            nmzPointsScreenReader.Separator = separator;
            nmzPointsScreenReader.OCR = ocr;

            Bitmap img1 = new Bitmap(2, 2);
            Bitmap img2 = new Bitmap(2, 2);
            List<Bitmap> imgs = new List<Bitmap> { img1, img2 };
            separator.Separate(new Bitmap(1, 1)).ReturnsForAnyArgs(imgs);

            ocr.RecognizeNumber(img1).Returns("1");
            ocr.RecognizeNumber(img2).Returns("2");

            var actual = nmzPointsScreenReader.ScreenToNMZPoints();

            Assert.AreEqual("12", actual);
        }
    }
}
