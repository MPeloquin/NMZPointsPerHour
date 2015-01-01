using System.Drawing;
using System.Windows.Forms;

namespace NmzExpHour.ImageProcessing
{
    public interface IScreenshotTaker
    {
        Bitmap TakeScreenShot();
    }

    public class ScreenshotTaker : IScreenshotTaker
    {
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

            return bmpScreenshot;
        }
    }
}
