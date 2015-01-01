using NmzExpHour.ImageProcessing;

namespace NmzExpHour.OCR
{
    public class NMZPointsScreenReader
    {

        public NMZPointsScreenReader()
        {
            ScreenShotTaker = new ScreenshotTaker();
            InmzPointsImageFinder = new NMZPointsImageFinder();
        }

        public string ScreenToNMZPoints()
        {
            var screenshot = ScreenShotTaker.TakeScreenShot();
            InmzPointsImageFinder.FindNMZPoints(screenshot);

            return "";
        }


        public IScreenshotTaker ScreenShotTaker { get; set; }
        public INMZPointsImageFinder InmzPointsImageFinder { get; set; }
    }
}
