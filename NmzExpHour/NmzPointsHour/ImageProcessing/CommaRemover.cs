using System.Drawing;
using NmzPointsHour.Utils;

namespace NmzPointsHour.ImageProcessing
{
    public interface ICommaRemover
    {
        Bitmap RemoveComma(Bitmap img);
    }

    public class CommaRemover : ICommaRemover
    {
        public Bitmap RemoveComma(Bitmap img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (x == img.Width - 1 || y >= img.Height - 2) continue;
                    Color color = img.GetPixel(x, y);
                    if (!color.IsAlmost(Color.Black)) continue;

                    if (img.GetPixel(x + 1, y).IsAlmost(Color.Black) && img.GetPixel(x + 1, y + 1).IsAlmost(Color.Black) &&
                        img.GetPixel(x, y + 2).IsAlmost(Color.Black) && img.GetPixel(x - 1, y).IsAlmost(Color.White))
                    {
                        img.SetPixel(x, y, Color.White);
                        img.SetPixel(x + 1, y, Color.White);
                        img.SetPixel(x+1, y+1, Color.White);
                        img.SetPixel(x, y+2, Color.White);
                    }
                }
            }

            return img;
        }
    }
}