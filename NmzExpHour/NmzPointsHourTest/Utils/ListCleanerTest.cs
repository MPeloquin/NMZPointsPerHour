using System;
using System.Collections.Generic;
using System.Linq;
using NmzPointsHour.Utils;
using NUnit.Framework;

namespace NmzPointsHourTest.Utils
{
    [TestFixture]
    public class ListCleanerTest
    {
        [Test]
        public void RemovesLeadingZeros()
        {
            var entry = new List<int>
            {
                0,0,0,0,0,0,1,3,5,4,6,7
            };

            var expected = new List<int>
            {
                1,3,5,4,6,7
            };

            var actual = new ListCleaner().TrimZeros(entry);

            Assert.That(!actual.Except(expected).ToList().Any());
        }

        [Test]
        public void RemovesTrailingZeros()
        {
            var entry = new List<int>
            {
                1,3,5,4,6,7,0,0,0,0,0,0
            };

            var expected = new List<int>
            {
                1,3,5,4,6,7
            };

            var actual = new ListCleaner().TrimZeros(entry);

            Assert.That(!actual.Except(expected).ToList().Any());
        }

        [Test]
        public void EmptyListReturnsEmptyList()
        {
            var actual = new ListCleaner().TrimZeros(new List<int>());

            Assert.That(actual.Count == 0);
        }

        [Test]
        public void ListWithOneNonZeroElementStaysTheSame()
        {
            var entry = new List<int> { 1 };
            var expected = new List<int> { 1 };

            var actual = new ListCleaner().TrimZeros(entry);

            Assert.That(!actual.Except(expected).ToList().Any());
        }

        [Test]
        public void KeepsMiddleZeros()
        {
            var entry = new List<int>
            {
                1,3,0,0,6,7
            };

            var expected = new List<int>
            {
                1,3,0,0,6,7
            };

            var actual = new ListCleaner().TrimZeros(entry);

            Assert.That(!actual.Except(expected).ToList().Any());
        }

        [Test]
        public void RemovesBackToBackTrueColumsInMiddle()
        {
            List<Tuple<bool, int>> entry = new List<Tuple<bool, int>>
            {
                new Tuple<bool, int>(false, 0),
                new Tuple<bool, int>(true, 1),
                new Tuple<bool, int>(true, 2),
                new Tuple<bool, int>(false, 3), 
            };
            List<Tuple<bool, int>> expected = new List<Tuple<bool, int>>
            {
                new Tuple<bool, int>(true, 1),
            };

            var actual = new ListCleaner().RemoveBackToBackTrueColumnAndFalseColumns(entry);

            Assert.AreEqual(expected.Count ,actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, actual[i].Item1, i + " element Item1 not equal");
                Assert.AreEqual(expected[i].Item2, actual[i].Item2, i + " element Item2 not equal");
            }
        }

        [Test]
        public void RemovesBackToBackTrueColumsAtTheBegining()
        {
            List<Tuple<bool, int>> entry = new List<Tuple<bool, int>>
            {
                new Tuple<bool, int>(true, 0),
                new Tuple<bool, int>(true, 1),
                new Tuple<bool, int>(true, 2),
                new Tuple<bool, int>(false, 3), 
            };

            List<Tuple<bool, int>> expected = new List<Tuple<bool, int>>
            {
                new Tuple<bool, int>(true, 0)
            };

            var actual = new ListCleaner().RemoveBackToBackTrueColumnAndFalseColumns(entry);

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, actual[i].Item1, i + " element Item1 not equal");
                Assert.AreEqual(expected[i].Item2, actual[i].Item2, i + " element Item2 not equal");
            }
        }
    }
}
