using System.Drawing;
using NmzExpHour.OCR.ImageProcessing;
using NmzExpHourTest.Data;
using NSubstitute;
using NUnit.Framework;

namespace NmzExpHourTest.OCR.ImageProcessing
{
    [TestFixture]
    public class NMZPointsImageFinderTest
    {
        [SetUp]
        public void SetUp()
        {
            NMZPointsImageFinder = new NMZPointsImageFinder();
        }


        [Test]
        public void FindsPointsWhenPresent()
        {
            var entryImage = new Bitmap(Images.TestPointsPresent0);
            var expected = new Bitmap(Images.TestPointsPresent0Result);

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
        }



        [Test]
        public void FindsLocationOfPointsWithCallToColorFinder()
        {
            var colorFinder = Substitute.For<IColorFinder>();
            var img = new Bitmap(10, 10);
            
            colorFinder.FindFirstColor(img, Color.FromArgb(127, 70, 15)).Returns(new Point(0, 0));
            colorFinder.FindLastColor(img, Color.FromArgb(127, 70, 15)).Returns(new Point(1, 1));


            NMZPointsImageFinder.ColorFinder = colorFinder;

            NMZPointsImageFinder.FindNMZPoints(img);

            colorFinder.Received().FindFirstColor(img, Color.FromArgb(127, 70, 15));
            colorFinder.Received().FindLastColor(img, Color.FromArgb(127, 70, 15));

        }


        public NMZPointsImageFinder NMZPointsImageFinder { get; set; }

    }
}
