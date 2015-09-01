using NmzPointsHour.Utils;
using NUnit.Framework;

namespace NmzPointsHourTest.Utils
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void KiloFormatInt()
        {
            Assert.AreEqual("1", 1.KiloFormat());
            Assert.AreEqual("10", 10.KiloFormat());
            Assert.AreEqual("1 000", 1000.KiloFormat());
            Assert.AreEqual("9 999", 9999.KiloFormat());
            Assert.AreEqual("10K", 10000.KiloFormat());
            Assert.AreEqual("10K", 10001.KiloFormat());
            Assert.AreEqual("9 999K", 9999000.KiloFormat());
            Assert.AreEqual("10M", 10000000.KiloFormat());
            Assert.AreEqual("999M", 999000000.KiloFormat());
            Assert.AreEqual("1 000M", 1000000000.KiloFormat());
        }

        [Test]
        public void KiloFormatFloat()
        {
            Assert.AreEqual("1", ((float)1).KiloFormat());
            Assert.AreEqual("10", ((float)10).KiloFormat());
            Assert.AreEqual("1 000", ((float)1000).KiloFormat());
            Assert.AreEqual("9 999", ((float)9999).KiloFormat());
            Assert.AreEqual("10K", ((float)10000).KiloFormat());
            Assert.AreEqual("10K", ((float)10001).KiloFormat());
            Assert.AreEqual("9 999K", ((float)9999000).KiloFormat());
            Assert.AreEqual("10M", ((float)10000000).KiloFormat());
            Assert.AreEqual("999M", ((float)999000000).KiloFormat());
            Assert.AreEqual("1 000M", ((float)1000000000).KiloFormat());
        }
    }
}
