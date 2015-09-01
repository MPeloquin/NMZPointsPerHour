using System.Drawing;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.Utils;
using NmzPointsHourTest.Data;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.ImageProcessing
{
    [TestFixture]
    public class NMZPointsImageFinderTest
    {
        public NMZPointsImageFinder NMZPointsImageFinder { get; set; }


        [SetUp]
        public void SetUp()
        {
            NMZPointsImageFinder = new NMZPointsImageFinder();
        }


        [Test]
        public void FindsPointsWhenPresent()
        {
            var entryImage = new Bitmap(Images.PointsFullImage);
            var expected = new Bitmap(Images.PointsSmallImage);

            var actual = NMZPointsImageFinder.FindNMZPoints(entryImage);

            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Width, actual.Width);

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    Assert.AreEqual(expected.GetPixel(i,j), actual.GetPixel(i, j));
                }
            }

            entryImage.Dispose();
            expected.Dispose();
        }

        [Test]
        public void FindsPointsWhenPresentInResizable()
        {
            var entryImage = new Bitmap(Images.PointsFullImageResizable);
            var expected = new Bitmap(Images.PointsFullImageResizableResult);

            var actual = NMZPointsImageFinder.FindNMZPoints(entryImage);

            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Width, actual.Width);

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    Assert.AreEqual(expected.GetPixel(i, j), actual.GetPixel(i, j));
                }
            }

            entryImage.Dispose();
            expected.Dispose();
        }

        [Test]
        public void ReturnsNothingWhenPointsNotPresent()
        {
            var entryImage = new Bitmap(Images.NoPoints);

            var actual = NMZPointsImageFinder.FindNMZPoints(entryImage);

            Assert.That(actual.IsEmpty());

            entryImage.Dispose();
        }

        [Test]
        public void FindsLocationOfPointsWithCallToColorFinder()
        {
            var colorFinder = Substitute.For<IColorFinder>();
            var img = new Bitmap(2, 2);

            NMZPointsImageFinder.ColorFinder = colorFinder;

            NMZPointsImageFinder.FindNMZPoints(img);

            colorFinder.Received().FindFirstColorLocation(img, NMZColors.Border);
            colorFinder.Received().FindLastColorLocation(img, NMZColors.Border);

            img.Dispose();
        }
     }
}
