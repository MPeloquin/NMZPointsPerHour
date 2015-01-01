using System.Drawing;
using NmzExpHour.ImageProcessing;
using NmzExpHour.OCR;
using NmzExpHourTest.ImageProcessing;
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
            var pointsFinder = Substitute.For<INMZPointsImageFinder>();

            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.InmzPointsImageFinder = pointsFinder;

            nmzPointsScreenReader.ScreenToNMZPoints();

            ssTaker.Received().TakeScreenShot();
        }

        [Test]
        public void CallsPointsFinderWithScreenShot()
        {
            var ssTaker = Substitute.For<IScreenshotTaker>();
            var pointsFinder = Substitute.For<INMZPointsImageFinder>();

            Bitmap img = new Bitmap(10, 10);
            ssTaker.TakeScreenShot().Returns(img);

            nmzPointsScreenReader.ScreenShotTaker = ssTaker;
            nmzPointsScreenReader.InmzPointsImageFinder = pointsFinder;

            nmzPointsScreenReader.ScreenToNMZPoints();

            pointsFinder.Received().FindNMZPoints(img);
        }

        [Test]
        public void CallsOCRWithScreenShot()
        {
            

        }
    }
}
