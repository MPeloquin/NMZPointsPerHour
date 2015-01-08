using System.Drawing;
using NmzExpHour.ImageProcessing;
using NmzExpHour.Utils;
using NmzExpHourTest.Data;
using NUnit.Framework;

namespace NmzExpHourTest.ImageProcessing
{
    [TestFixture]
    public class CommaRemoverTest
    {
        [Test]
        public void CanRemoveCommas()
        {
            var twoComma = new Bitmap(Images.TwoCommas);
            var expected = new Bitmap(Images.TwoCommasNoComma);

            Bitmap actual = new CommaRemover().RemoveComma(twoComma);

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    Assert.That(expected.GetPixel(i, j).IsAlmost(actual.GetPixel(i, j)));
                }
            }
        }
    }
}
