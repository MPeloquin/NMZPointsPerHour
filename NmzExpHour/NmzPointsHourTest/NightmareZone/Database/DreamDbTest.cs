using System;
using System.Configuration;
using System.Data.SQLite;
using NmzPointsHour.NightmareZone;
using NmzPointsHour.NightmareZone.Database;
using NUnit.Framework;

namespace NmzPointsHourTest.NightmareZone.Database
{
    [TestFixture]
    public class DreamDbTest
    {
        [Test]
        public void CanSaveDream()
        {
            int pointsActual;
            int durationActual;

            var dream = new Dream {Duration = 12345, Points = 12312};

            new DreamDb().SaveDream(dream);

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select * from Dream", c))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        r.Read();
                        pointsActual = Convert.ToInt32(r["Points"]);
                        durationActual = Convert.ToInt32(r["DurationMs"]);
                    }
                }
            }

            Assert.AreEqual(dream.Points, pointsActual);
            Assert.AreEqual(dream.Duration, durationActual);
        }

        [TearDown]
        public void DeleteRecords()
        {
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("Delete from Dream;", c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
