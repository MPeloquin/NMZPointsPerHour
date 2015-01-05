using System.Collections.Generic;
using System.Linq;
using NmzExpHour.Utils;
using NUnit.Framework;

namespace NmzExpHourTest.Utils
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

            var actual = new ListCleaner().RemoveZerosFromRow(entry);

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

            var actual = new ListCleaner().RemoveZerosFromRow(entry);

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

            var actual = new ListCleaner().RemoveZerosFromRow(entry);

            Assert.That(!actual.Except(expected).ToList().Any());
        }
    }
}
