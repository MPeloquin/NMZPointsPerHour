using NmzExpHour.OCR.ImageProcessing;

namespace NmzExpHour.OCR
{
    public class NMZPointsScreenReader
    {

        public NMZPointsScreenReader()
        {
            ScreenShotTaker = new ScreenshotTaker();
            InmzPointsPictureFinder = new NMZInmzPointsPicturePictureFinder();
        }

        public string ScreenToNMZPoints()
        {
            var screenshot = ScreenShotTaker.TakeScreenShot();
            InmzPointsPictureFinder.FindPoints(screenshot);

            return "";
        }


        public IScreenshotTaker ScreenShotTaker { get; set; }
        public INMZPointsPictureFinder InmzPointsPictureFinder { get; set; }
    }
}
