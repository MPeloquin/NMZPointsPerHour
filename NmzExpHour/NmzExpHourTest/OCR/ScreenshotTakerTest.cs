﻿using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NmzExpHour.OCR;
using NUnit.Framework;

namespace NmzExpHourTest.OCR
{
    [TestFixture]
    public class ScreenshotTakerTest
    {
        private ScreenshotTaker ssTaker;

        [SetUp]
        public void SetUp()
        {
            ssTaker = new ScreenshotTaker();
        }

        [Test]
        public void ReturnsABitMapVariable()
        {
            var img = ssTaker.TakeScreenShot();

            Assert.IsInstanceOf<Bitmap>(img);

            img.Dispose();
        }

        [Test]
        public void TheSizeOfTheBitmapIsTheSameAsTheScreens()
        {
            Bitmap img = ssTaker.TakeScreenShot();

            Assert.AreEqual(SystemInformation.VirtualScreen.Height, img.Height);
            Assert.AreEqual(SystemInformation.VirtualScreen.Width, img.Width);

            img.Dispose();
        }
    }
}
