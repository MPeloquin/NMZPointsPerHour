using System.Collections.Generic;
using System.Drawing;
using NmzExpHour.ImageProcessing;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.ImageProcessing
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
        public void ReturnsEmptyPointIfFirstColorNotFound()
        {
            Bitmap img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindFirstColor(img, Color.Red);

            Assert.That(actual.IsEmpty);
        }

        [Test]
        public void ReturnEmptyPointIfLastColorNotFound()
        {
            Bitmap img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindLastColor(img, Color.Red);

            Assert.That(actual.IsEmpty);
        }



        [Test]
        public void ReturnsTheLastPixelLocationOfTheColor()
        {
            Point expected = new Point(2558, 332);
            Bitmap img = new Bitmap(Images.PointsFullImage);
 
            Point actual = ColorFinder.FindLastColor(img, Colors.Border);

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ReturnsTheFirstPixelLocationOfTheColor()
        {
            Point expected = new Point(2516, 303);
            Bitmap img = new Bitmap(Images.PointsFullImage);

            Point actual = ColorFinder.FindFirstColor(img, Colors.Border);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReturnsTheListOfColorsPresentInAnImage()
        {
            Bitmap img = new Bitmap(Images.PointsSmallImage);
            List<Color> expected = new List<Color>();
            expected.Add(Color.Black);
            expected.Add(Colors.Border);
            
        }

        public ColorFinder ColorFinder { get; set; }

    }
}
