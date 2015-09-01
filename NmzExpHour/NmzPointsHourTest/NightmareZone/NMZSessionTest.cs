using NmzPointsHour.NightmareZone;
using NmzPointsHour.NightmareZone.Database;
using NSubstitute;
using NUnit.Framework;

namespace NmzPointsHourTest.NightmareZone
{
    public class NMZSessionTest
    {
        private NMZSession nmzSession;

        [SetUp]
        public void SetUp()
        {
            nmzSession = new NMZSession();
            nmzSession.DreamDb = Substitute.For<IDreamDb>();

        }

        [Test]
        public void CanAddNewDreams()
        {
            nmzSession.StartNewDream();

            Assert.AreEqual(nmzSession.Dreams.Count, 1);
        }

        [Test]
        public void StartedDreamBecomesActiveDream()
        {
            nmzSession.StartNewDream();

            Assert.That(nmzSession.CurrentDream.IsActive());
        }

        [Test]
        public void CannotStartNewDreamWhenDreamAlreadyActive()
        {
            nmzSession.StartNewDream();
            nmzSession.StartNewDream();

            Assert.AreEqual(1, nmzSession.Dreams.Count);
        }

        [Test]
        public void StoppingADreamMarksItAsInactive()
        {
            nmzSession.StartNewDream();
            nmzSession.StopCurrentDream();

            Assert.That(!nmzSession.CurrentDream.IsActive());
        }

        [Test]
        public void CanStartASecondDreamAfterFirstOneIsOver()
        {
            nmzSession.StartNewDream();
            nmzSession.StopCurrentDream();
            nmzSession.StartNewDream();

            Assert.AreEqual(nmzSession.Dreams.Count, 2);
        }

        [Test]
        public void StoppingADreamSavesItToTheDb()
        {
            var dreamDb = Substitute.For<IDreamDb>();
            nmzSession.DreamDb = dreamDb;

            nmzSession.StartNewDream();
            nmzSession.StopCurrentDream();


            dreamDb.Received().SaveDream(nmzSession.Dreams[0]);
        }

        [Test]
        public void StoppingWhenNoDreamActiveDoesNothing()
        {
            nmzSession.StopCurrentDream();
        }

        [Test]
        public void StoppingWhenDreamIsInnactiveDoesNothing()
        {
            var dreamDb = Substitute.For<IDreamDb>();

            nmzSession.StartNewDream();
            nmzSession.StopCurrentDream();

            nmzSession.DreamDb = dreamDb;
            nmzSession.StopCurrentDream();

            dreamDb.DidNotReceive().SaveDream(nmzSession.Dreams[0]);
        }
    }
}
