using System.Drawing;
using NmzExpHour.OCR;
using NmzExpHour.OCR.ImageProcessing;
using NmzExpHourTest.Data;
using NSubstitute;
using NUnit.Framework;

namespace NmzExpHourTest.OCR
{
    [TestFixture]
    public class NMZPointsScreenReaderTest
    {
        private NMZPointsScreenReader nmzPointsScreenReader;

        [SetUp]
        public void SetUp()
        {
            nmzPointsScreenReader = new NMZPointsScreenReader();
        }

        [Test]
        public void CallsScreenshotTaker()
        {
            var ssTaker = Substitute.For<IScreenshotTaker>();
            var pointsFinder = Substitute.For<INMZPointsPictureFinder>();

            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.InmzPointsPictureFinder = pointsFinder;

            nmzPointsScreenReader.ScreenToNMZPoints();

            ssTaker.Received().TakeScreenShot();
        }

        [Test]
        public void CallsPointsFinderWithScreenShot()
        {
            var ssTaker = Substitute.For<IScreenshotTaker>();
            var pointsFinder = Substitute.For<INMZPointsPictureFinder>();

            Bitmap img = new Bitmap(10, 10);
            ssTaker.TakeScreenShot().Returns(img);

            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.InmzPointsPictureFinder = pointsFinder;

            nmzPointsScreenReader.ScreenToNMZPoints();

            pointsFinder.Received().FindPoints(img);
        }

        [Test]
        public void CallsOCRWithScreenShot()
        {
            

        }
    }
}
