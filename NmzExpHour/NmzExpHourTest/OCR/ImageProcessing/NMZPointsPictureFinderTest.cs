using System.Drawing;
using NmzExpHour.OCR.ImageProcessing;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.OCR.ImageProcessing
{
    [TestFixture]
    public class NMZPointsPictureFinderTest
    {
        [SetUp]
        public void SetUp()
        {
            NMZInmzPointsPicturePictureFinder = new NMZInmzPointsPicturePictureFinder();
        }


        [Test]
        public void FindsPointsWhenPresent()
        {
            var entryImage = new Bitmap(Images.TestPointsPresent0);
            var expected = new Bitmap(Images.TestPointsPresent0Result);

            var actual = NMZInmzPointsPicturePictureFinder.FindPoints(entryImage);

            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Width, actual.Width);

            for (int i = 0; i < expected.Height; i++)
            {
                for (int j = 0; j < expected.Width; j++)
                {
                    Assert.AreEqual(expected.GetPixel(i,j), actual.GetPixel(i, j));
                }
            }
        }


        public NMZInmzPointsPicturePictureFinder NMZInmzPointsPicturePictureFinder { get; set; }

    }
}
