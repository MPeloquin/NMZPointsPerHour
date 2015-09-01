using System;
using System.Threading.Tasks;
using NmzPointsHour.NightmareZone;
using NmzPointsHour.NightmareZone.Database;
using NmzPointsHour.OCR;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.NightmareZone
{
    [TestFixture]
    public class DreamTest
    {
        private Dream dream;

        [SetUp]
        public void SetUp()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("1231");

            dream = new Dream {
                Calculator = Substitute.For<IPointsCalculator>(), 
                NMZPointsScreenReader = screenReader,
            };
        }

        [Test]
        public void StartingADreamStartsCalculator()
        {
            var calculator = Substitute.For<IPointsCalculator>();

            dream.Calculator = calculator;
            dream.Start();

            calculator.ReceivedWithAnyArgs().Start(3);
        }

        [Test]
        public void StartingADreamStartsTheCalculatorWithTheInitialPoints()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("1231");

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;

            dream.Start();

            calculator.ReceivedWithAnyArgs().Start(1231);
        }

        [Test]
        public void UpdatePointsCallsScreenReader()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("1231");
            dream.NMZPointsScreenReader = screenReader;

            dream.UpdatePoints();

            screenReader.Received().ScreenToNMZPoints();
        }

        [Test]
        public void IfPointsAreWrongCalculatorIsntUpdated()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("-1");

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;

            dream.UpdatePoints();


            calculator.DidNotReceiveWithAnyArgs().UpdatePoints(123);
        }

        [Test]
        public void IfPointsAreTheSameAsLastReadingCalculatorIsntUpdated()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("12345");

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;

            dream.UpdatePoints();

            calculator.ClearReceivedCalls();

            dream.UpdatePoints();

            calculator.DidNotReceiveWithAnyArgs().UpdatePoints(123);
        }

        [Test]
        public void IfPointsAreLargerThanMaxAmountCalculatorIsntUpdated()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("1048576");

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;

            dream.UpdatePoints();


            calculator.DidNotReceiveWithAnyArgs().UpdatePoints(123);
        }

        [Test]
        public async void EndingADreamSavesItsDuration()
        {
            dream = new Dream();
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("1048576");
            dream.NMZPointsScreenReader = screenReader;

            dream.Start();
            await Task.Delay(100);
            dream.End();

            Assert.That(Math.Abs(dream.Duration - 100) < 50, "Expected 100, was actually " + dream.Duration);
        }

        [Test]
        public void Reading0PointsAtTheBeginingDoesntEndTheDream()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("0");

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;

            dream.Start();
            dream.UpdatePoints();

            Assert.That(dream.IsActive());
        }

        [Test]
        public void EventIsRaisedWhenPointsAreGained()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            dream.NMZPointsScreenReader = screenReader;

            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("10");

            var pointsReceived = 0;

            dream.MonsterKilled += (sender, args) => pointsReceived = ((MonsterKilledArgs)args).Points;

            dream.Start();
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("12312");
            dream.UpdatePoints();

            Assert.AreEqual(12302, pointsReceived);
        }

        [Test]
        public void InvalidPointsReturnsInUpdatePoints()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();
            var calculator = Substitute.For<IPointsCalculator>();

            dream.Calculator = calculator;
            dream.NMZPointsScreenReader = screenReader;
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs("10");

            dream.Start();
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs(x => { throw new Exception(); });
            dream.UpdatePoints();

            calculator.DidNotReceiveWithAnyArgs().UpdatePoints(123);
        }

        [Test]
        public void InvalidPointsSetsPointsAsZeroInStart()
        {
            var screenReader = Substitute.For<INMZPointsScreenReader>();

            dream.NMZPointsScreenReader = screenReader;
            screenReader.ScreenToNMZPoints().ReturnsForAnyArgs(x => { throw new Exception(); });

            dream.Start();

            Assert.AreEqual(0, dream.Points);
        }
    }
}
