using System.Drawing;
using System.Drawing.Imaging;
using NmzExpHour.ImageProcessing;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.ImageProcessing
{
    [TestFixture]
    public class PixelFormatConverterTest
    {
        [Test]
        public void Converts32BppRbGto24BppRbg()
        {
            var img32Bpp = new Bitmap(Images.OCRRow);

            Assert.AreEqual(PixelFormat.Format32bppArgb, img32Bpp.PixelFormat);

            var actual = new PixelFormatConverter().To24BppRGB(img32Bpp);

            Assert.AreEqual(PixelFormat.Format24bppRgb, actual.PixelFormat);
        }
    }
}
