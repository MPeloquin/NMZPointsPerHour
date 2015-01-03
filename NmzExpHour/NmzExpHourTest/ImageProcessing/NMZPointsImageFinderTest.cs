﻿using System.Drawing;
using NmzExpHour.Extensions;
using NmzExpHour.ImageProcessing;
using NmzExpHourTest.Data;
using NSubstitute;
using NUnit.Framework;

namespace NmzExpHourTest.ImageProcessing
{
    [TestFixture]
    public class NMZPointsImageFinderTest
    {
        [SetUp]
        public void SetUp()
        {
            NMZPointsImageFinder = new NMZPointsImageFinder();
        }


        [Test]
        public void FindsPointsWhenPresent()
        {
            var entryImage = new Bitmap(Images.PointsFullImage);
            var expected = new Bitmap(Images.PointsSmallImage);

            var actual = NMZPointsImageFinder.FindNMZPoints(entryImage);

            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Width, actual.Width);

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    Assert.AreEqual(expected.GetPixel(i,j), actual.GetPixel(i, j));
                }
            }
        }

        [Test]
        public void ReturnsNothingWhenPointsNotPresent()
        {
            var entryImage = new Bitmap(Images.NoPoints);

            var actual = NMZPointsImageFinder.FindNMZPoints(entryImage);

            Assert.That(actual.IsEmpty());
        }

        [Test]
        public void FindsLocationOfPointsWithCallToColorFinder()
        {
            var colorFinder = Substitute.For<IColorFinder>();
            var img = new Bitmap(10, 10);

            colorFinder.FindFirstColor(img, Colors.Border).Returns(new Point(0, 0));
            colorFinder.FindLastColor(img, Colors.Border).Returns(new Point(1, 1));


            NMZPointsImageFinder.ColorFinder = colorFinder;

            NMZPointsImageFinder.FindNMZPoints(img);

            colorFinder.Received().FindFirstColor(img, Colors.Border);
            colorFinder.Received().FindLastColor(img, Colors.Border);

        }


        public NMZPointsImageFinder NMZPointsImageFinder { get; set; }

    }
}
