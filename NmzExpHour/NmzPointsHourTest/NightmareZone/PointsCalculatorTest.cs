using System;
using System.Threading.Tasks;
using NmzPointsHour.NightmareZone;
using NUnit.Framework;

namespace NmzPointsHourTest.NightmareZone
{
    [TestFixture]
    public class PointsCalculatorTest
    {
        private PointsCalculator pointsCalculator;

        [SetUp]
        public void SetUp()
        {
            pointsCalculator = new PointsCalculator();
        }
        
        [Test]
        public void StartingCalculatorSetsStartTheTimeWatch()
        {
            pointsCalculator.Start(0);

            Assert.That(pointsCalculator.Stopwatch.IsRunning);
        }

        [Test]
        public void StartingSetPointsToValuePassed()
        {
            const int points = 12345;
            pointsCalculator.Start(points);


            Assert.AreEqual(points, pointsCalculator.StartingPoints);
        }

        [Test]
        public void UpdatedPointsAreNewTotal()
        {
            const int points = 123;
            pointsCalculator.Start(0);
            pointsCalculator.UpdatePoints(points);

            Assert.AreEqual(points, pointsCalculator.Points);
        }

        [Test]
        public async void CalculatesPointsPerHour()
        {
            pointsCalculator.Start(0);
            pointsCalculator.UpdatePoints(10);
            await Task.Delay(100);

            Assert.That(Math.Abs(360000 - pointsCalculator.CalculatePointsPerHour()) < 50000, "Was actually " + pointsCalculator.CalculatePointsPerHour() + ", expecting " + 360000);
        }

        [Test]
        public void ZeroPointsMeansZeroPointsPerHour()
        {
            pointsCalculator.Start(0);

            Assert.AreEqual(0, pointsCalculator.CalculatePointsPerHour());
        }
           
    }
}
