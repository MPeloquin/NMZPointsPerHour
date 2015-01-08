using System;
using System.Drawing;

namespace NmzExpHour.Utils
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
    }
}