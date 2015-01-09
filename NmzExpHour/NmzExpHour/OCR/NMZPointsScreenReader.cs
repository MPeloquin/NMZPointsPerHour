using System.Linq;
using NmzExpHour.ImageProcessing;

namespace NmzExpHour.OCR
{
    public class NMZPointsScreenReader
    {
        public NMZPointsScreenReader()
        {
            ScreenShotTaker = new ScreenshotTaker();
            NmzPointsImageFinder = new NMZPointsImageFinder();
            ImageFilterer = new ImageFilterer();
            Separator = new OpticalNumberSeparator();
            OCR = new OpticalNumberRecognizer();
        }

        public string ScreenToNMZPoints()
        {
            var screenshot = ScreenShotTaker.TakeScreenShot();
            var points = NmzPointsImageFinder.FindNMZPoints(screenshot);
            var filteredImage = ImageFilterer.FilterImage(points);
            var listNumbers = Separator.Separate(filteredImage);

            return listNumbers.Aggregate("", (current, number) => current + OCR.RecognizeNumber(number));
        }


        public IScreenshotTaker ScreenShotTaker { get; set; }
        public INMZPointsImageFinder NmzPointsImageFinder { get; set; }
        public IImageFilterer ImageFilterer { get; set; }
        public IOpticalNumberSeparator Separator { get; set; }
        public IOpticalNumberRecognizer OCR { get; set; }

    }
}
