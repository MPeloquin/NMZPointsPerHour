using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace NmzExpHour.OCR
{
    public class ScreenshotTaker
    {
        public void TakeScreenShot(String name)
        {
            var bmpScreenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
                                           SystemInformation.VirtualScreen.Height);

            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            gfxScreenshot.CopyFromScreen(SystemInformation.VirtualScreen.X,
                                        SystemInformation.VirtualScreen.Y,
                                        0,
                                        0,
                                        SystemInformation.VirtualScreen.Size);

            bmpScreenshot.Save(name);
        }
    }
}
