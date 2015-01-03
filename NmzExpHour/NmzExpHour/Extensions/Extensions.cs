using System.Drawing;

namespace NmzExpHour.Extensions
{
    public static class Extensions
    {
        public static bool IsEmpty(this Bitmap img)
        {
            return (img.Width == 1 && img.Height == 1);
        }
    }
}