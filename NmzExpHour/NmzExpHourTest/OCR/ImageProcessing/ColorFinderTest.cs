using System.Drawing;
using NmzExpHour.OCR.ImageProcessing;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.OCR.ImageProcessing
{
    [TestFixture]
    public class ColorFinderTest
    {
        [SetUp]
        public void SetUp()
        {
            ColorFinder = new ColorFinder();
        }

        [Test]
        public void ReturnsMinus1IfFirstColorNotFound()
        {
            Bitmap img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindFirstColor(img, Color.Red);

            Assert.AreEqual(new Point(-1, -1), actual);
        }

        [Test]
        public void ReturnMinus1IfLastColorNotFound()
        {
            Bitmap img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindLastColor(img, Color.Red);

            Assert.AreEqual(new Point(-1, -1), actual);
        }

        [Test]
        public void ReturnsTheLastPixelLocationOfTheColor()
        {
            Point expected = new Point(2558, 332);
            Bitmap img = new Bitmap(Images.TestPointsPresent0);
            Color color = Color.FromArgb(127, 70, 15);

            Point actual = ColorFinder.FindLastColor(img, color);

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ReturnsTheFirstPixelLocationOfTheColor()
        {
            Point expected = new Point(2516, 303);
            Bitmap img = new Bitmap(Images.TestPointsPresent0);
            Color color = Color.FromArgb(127, 70, 15);
           
            Point actual = ColorFinder.FindFirstColor(img, color);

            Assert.AreEqual(expected, actual);
        }

        public ColorFinder ColorFinder { get; set; }

    }
}
