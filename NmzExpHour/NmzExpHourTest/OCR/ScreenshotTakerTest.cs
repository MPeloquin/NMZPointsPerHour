using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NmzExpHour.OCR;
using NUnit.Framework;

namespace NmzExpHourTest.OCR
{
    [TestFixture]
    public class ScreenshotTakerTest
    {
        private const string FileName = "temp.jpg";
        private ScreenshotTaker ssTaker;

        [SetUp]
        public void SetUp()
        {
            ssTaker = new ScreenshotTaker();
        }

        [Test]
        public void AFileIsSavedOnTheDiskWithTheCorrectName()
        {
            ssTaker.TakeScreenShot(FileName);

            Assert.That(File.Exists(FileName));
        }

        [Test]
        public void TheSizeOfTheSavedPictureIsTheSameAsTheScreens()
        {
            ssTaker.TakeScreenShot(FileName);

            Bitmap img = new Bitmap(FileName);

            Assert.AreEqual(SystemInformation.VirtualScreen.Height, img.Height);
            Assert.AreEqual(SystemInformation.VirtualScreen.Width, img.Width);

            img.Dispose();
        }

        [TearDown]
        public void RemoveTestFile()
        {
            //File.Delete(FileName);
        }
    }
}
