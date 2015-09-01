using System.Collections.Generic;
using System.Drawing;
using NmzPointsHour.ImageProcessing;
using NmzPointsHourTest.Data;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.ImageProcessing
{
    [TestFixture]
    public class ImageFilteringTest
    {
        private ImageFilterer imageFilterer;

        [SetUp]
        public void SetUp()
        {
            imageFilterer = new ImageFilterer();
        }

        [Test]
        public void ChangesTheFontColorToBlack()
        {
            var colorFinder = new ColorFinder();
            var img = new Bitmap(Images.PointsSmallImage);

            var blackBefore = colorFinder.FindFirstColorLocation(img, Color.FromArgb(0, 0, 0));
            var fontColorBefore = colorFinder.FindFirstColorLocation(img, NMZColors.Font);

            var newImage = imageFilterer.FilterImage(img);

            var blackAfter = colorFinder.FindFirstColorLocation(img, Color.FromArgb(0, 0, 0));
            var fontColorAfter = colorFinder.FindFirstColorLocation(img, NMZColors.Font);

            img.Dispose();

            Assert.AreNotEqual(blackBefore, blackAfter);
            Assert.AreNotEqual(fontColorBefore, fontColorAfter);
        }


        [Test]
        public void RemovesAllColorsExpectFontColor()
        {
            var img = new Bitmap(Images.PointsSmallImage);

            var expected = new List<Color> { Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0) };

            var imgNoColor = imageFilterer.FilterImage(img);

            var actual = new ColorFinder().FindColors(img);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var color in expected)
            {
                Assert.That(actual.Contains(color), "Color not found : " + color);
            }

            img.Dispose();
            imgNoColor.Dispose();
        }

        [Test]
        public void CropsThePointsOut()
        {
            var entry = new Bitmap(Images.PointsSmallImage);
            var expected = new Bitmap(Images.NumbersNoPoints);

            var actual = imageFilterer.FilterImage(entry);

            for (int i = 0; i < actual.Width; i++)
            {
                for (int j = 0; j < actual.Height; j++)
                {
                    Assert.AreEqual(expected.GetPixel(i, j), actual.GetPixel(i, j));
                }
            }

            entry.Dispose();
            expected.Dispose();
            actual.Dispose();
        }

        [Test]
        public void CallsCommaRemover()
        {
            var commaRemover = Substitute.For<ICommaRemover>();
            imageFilterer.CommaRemover = commaRemover;
            
            Bitmap img= new Bitmap(3,3);
            Bitmap entryImage = new Bitmap(2,2);
            commaRemover.RemoveComma(entryImage).ReturnsForAnyArgs(img);

            imageFilterer.FilterImage(entryImage);

            commaRemover.ReceivedCalls();
        }

    }
}
