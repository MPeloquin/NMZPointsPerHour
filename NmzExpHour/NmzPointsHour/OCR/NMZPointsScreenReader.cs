using System;
using System.Linq;
using NmzPointsHour.Utils;
using NmzPointsHour.ImageProcessing;

namespace NmzPointsHour.OCR
{
    public interface INMZPointsScreenReader
    {
        string ScreenToNMZPoints();
    }

    public class NMZPointsScreenReader : INMZPointsScreenReader
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
            var screenshot = ScreenShotTaker.TakeNmzScreenShot();
            var points = NmzPointsImageFinder.FindNMZPoints(screenshot);

            if (points.IsEmpty())
                return "-1";

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
