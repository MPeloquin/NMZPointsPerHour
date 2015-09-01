using System.Drawing;
using System.Windows.Forms;

namespace NmzPointsHour.ImageProcessing
{
    public interface IScreenshotTaker
    {
        Bitmap TakeScreenShot();
        Bitmap TakeNmzScreenShot();
    }

    public class ScreenshotTaker : IScreenshotTaker
    {
        public IOsBuddyFinder OsBuddyFinder { get; set; }

        public ScreenshotTaker()
        {
            OsBuddyFinder = new OsBuddyFinder();
        }

        public Bitmap TakeScreenShot()
        {
            var bmpScreenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
                                           SystemInformation.VirtualScreen.Height);

            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            gfxScreenshot.CopyFromScreen(SystemInformation.VirtualScreen.X,
                                        SystemInformation.VirtualScreen.Y,
                                        0,
                                        0,
                                        SystemInformation.VirtualScreen.Size);

            gfxScreenshot.Dispose();

            return bmpScreenshot;
        }

        public Bitmap TakeNmzScreenShot()
        {
            var rect = OsBuddyFinder.FindOsBuddy();

            var bmpScreenshot = new Bitmap((rect.Right-rect.Left), (rect.Bottom-rect.Top));
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            gfxScreenshot.CopyFromScreen(rect.Left,
                            rect.Top,
                            0,
                            0,
                            SystemInformation.VirtualScreen.Size);

            gfxScreenshot.Dispose();

            return bmpScreenshot;
        }
    }
}
