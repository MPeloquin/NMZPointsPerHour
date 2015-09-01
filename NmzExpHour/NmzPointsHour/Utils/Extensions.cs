using System;
using System.Drawing;

namespace NmzPointsHour.Utils
{
    public static class Extensions
    {
        public static bool IsEmpty(this Bitmap img)
        {
            return (img.Width == 1 && img.Height == 1);
        }

        public static bool IsAlmost(this Color color, Color comparedColor, int tolerance = 10)
        {
            return (Math.Abs(color.R - comparedColor.R) <= tolerance) && (Math.Abs(color.G - comparedColor.G) <= tolerance) &&
                   (Math.Abs(color.B - comparedColor.B) <= tolerance);
        }

        public static string KiloFormat(this float num)
        {
            if (num >= 100000000)
                return (num / 1000000).ToString("#,0M");

            if (num >= 10000000)
                return (num / 1000000).ToString("0.#") + "M";

            if (num >= 100000)
                return (num / 1000).ToString("#,0K");

            if (num >= 10000)
                return (num / 1000).ToString("0.#") + "K";

            return num.ToString("#,0");
        }

        public static string KiloFormat(this int num)
        {
            return ((float) num).KiloFormat();
        } 
    }
}