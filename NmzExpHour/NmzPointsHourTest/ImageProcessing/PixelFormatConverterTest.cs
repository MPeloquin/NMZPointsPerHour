using System.Drawing;
using System.Drawing.Imaging;
using NmzPointsHour.ImageProcessing;
using NmzPointsHourTest.Data;
using NUnit.Framework;

namespace NmzPointsHourTest.ImageProcessing
{
    [TestFixture]
    public class PixelFormatConverterTest
    {
        [Test]
        public void Converts32BppRbGto24BppRbg()
        {
            var img32Bpp = new Bitmap(Images.Image32Bpp);

            Assert.AreEqual(PixelFormat.Format32bppArgb, img32Bpp.PixelFormat);

            var actual = new PixelFormatConverter().To24BppRGB(img32Bpp);

            Assert.AreEqual(PixelFormat.Format24bppRgb, actual.PixelFormat);
        }
    }
}
