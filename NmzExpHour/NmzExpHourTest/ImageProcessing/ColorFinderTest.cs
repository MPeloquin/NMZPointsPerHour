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
            Img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindFirstColorLocation(Img, Color.Red);

            Assert.That(actual.IsEmpty);
        }

        [Test]
        public void ReturnEmptyPointIfLastColorNotFound()
        {
            Img = new Bitmap(10, 10);

            Point actual = ColorFinder.FindLastColorLocation(Img, Color.Red);

            Assert.That(actual.IsEmpty);
        }


        [Test]
        public void ReturnsTheLastPixelLocationOfTheColor()
        {
            Point expected = new Point(2558, 332);
            Img = new Bitmap(Images.PointsFullImage);

            Point actual = ColorFinder.FindLastColorLocation(Img, Colors.Border);

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ReturnsTheColorPresentInUnicolorImage()
        {
            Img = new Bitmap(1, 1);
            Img.SetPixel(0, 0, Color.Red);

            var actual = ColorFinder.FindColors(Img);

            Assert.That(actual.Contains(Color.FromArgb(255, 0, 0)));
        }


        [Test]
        public void ReturnsTheFirstPixelLocationOfTheColor()
        {
            Point expected = new Point(2516, 303);
            Img = new Bitmap(Images.PointsFullImage);

            Point actual = ColorFinder.FindFirstColorLocation(Img, Colors.Border);

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ReturnsTheListOfColorsPresentInAnImage()
        {
            Img = new Bitmap(Images.PointsSmallImage);
            List<Color> expected = new List<Color> { Color.FromArgb(0,0,0), Colors.Border, Colors.Font, Color.FromArgb(92, 72, 46), Color.FromArgb(92, 71, 46),  Color.FromArgb(93, 72, 47),
                                                     Color.FromArgb(92, 70, 42), Color.FromArgb(93, 71, 42), Color.FromArgb(88, 67, 41), Color.FromArgb(93, 71, 47)};

            var actual = ColorFinder.FindColors(Img);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var color in expected)
            {
                Assert.That(actual.Contains(color), "Color not found : " + color);
            }
        }

        [Test]
        public void FindsTheColorInTheLastPixel()
        {
            Img = new Bitmap(Images.LastPixel);
            List<Color> expected = new List<Color> { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };

            var actual = ColorFinder.FindColors(Img);


            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var color in expected)
            {
                Assert.That(actual.Contains(color), "Color not found : " + color);
            }

        }


        [TearDown]
        public void TearDown()
        {
            Img.Dispose();
        }




        public ColorFinder ColorFinder { get; set; }
        public Bitmap Img { get; set; }

    }
}
