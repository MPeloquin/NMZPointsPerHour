using System.Collections.Generic;
using System.Drawing;
using NmzExpHour.ImageProcessing;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.ImageProcessing
{
    [TestFixture]
    public class ImageFilteringTest
    {
        private ImageFiltering imageFiltering;

        [SetUp]
        public void SetUp()
        {
            imageFiltering = new ImageFiltering();
        }

        [Test]
        public void ChangesTheFontColorToBlack()
        {
            var colorFinder = new ColorFinder();
            var img = new Bitmap(Images.PointsSmallImage);

            var blackBefore = colorFinder.FindFirstColorLocation(img, Color.FromArgb(0, 0, 0));
            var fontColorBefore = colorFinder.FindFirstColorLocation(img, Colors.Font);

            var newImage = imageFiltering.FilterImage(img);

            var blackAfter = colorFinder.FindFirstColorLocation(img, Color.FromArgb(0, 0, 0));
            var fontColorAfter = colorFinder.FindFirstColorLocation(img, Colors.Font);

            img.Dispose();

            Assert.AreNotEqual(blackBefore, blackAfter);
            Assert.AreNotEqual(fontColorBefore, fontColorAfter);
        }


        [Test]
        public void RemovesAllColorsExpectFontColor()
        {
            var img = new Bitmap("test2.png");

            var expected = new List<Color> { Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0) };

            var imgNoColor = imageFiltering.FilterImage(img);

            var actual = new ColorFinder().FindColors(img);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var color in expected)
            {
                Assert.That(actual.Contains(color), "Color not found : " + color);
            }

            imgNoColor.Save("hello.png");

            img.Dispose();
            imgNoColor.Dispose();
        }



    }
}
