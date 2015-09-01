using System.Drawing;
using System.Windows.Forms;
using NmzPointsHour.ImageProcessing;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.ImageProcessing
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

        [Test]
        public void FindsTheOsBuddyClientWithACallToOsBuddyFinder()
        {
            var buddyFinder = Substitute.For<IOsBuddyFinder>();
            buddyFinder.FindOsBuddy().ReturnsForAnyArgs(new OsBuddyFinder.Rect{Bottom = 6, Left = 5, Right = 6, Top = 5});

            ssTaker.OsBuddyFinder = buddyFinder;

            ssTaker.TakeNmzScreenShot();

            buddyFinder.Received().FindOsBuddy();
        }

        [Test]
        public void CanTakeSmallScreenShotOfNmzPoints()
        {
            var buddyFinder = Substitute.For<IOsBuddyFinder>();
            buddyFinder.OsBuddyOpened().Returns(true);
            var rect = new OsBuddyFinder.Rect
            {
                Bottom = 952,
                Left = 1928,
                Right = 3186,
                Top = 175
            };
            buddyFinder.FindOsBuddy().Returns(rect);

            ssTaker.OsBuddyFinder = buddyFinder;

            var actual = ssTaker.TakeNmzScreenShot();

            Assert.AreEqual((rect.Right-rect.Left), actual.Width);
            Assert.AreEqual((rect.Bottom - rect.Top), actual.Height);
        }
    }
}
