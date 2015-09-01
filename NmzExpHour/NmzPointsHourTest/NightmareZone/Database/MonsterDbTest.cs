using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using NmzPointsHour.NightmareZone;
using NmzPointsHour.NightmareZone.Database;
using NUnit.Framework;

namespace NmzPointsHourTest.NightmareZone.Database
{
    [TestFixture]
    public class MonsterDbTest
    {

        [Test]
        public void CanRetrieveAListOfMonsterFromPoints()
        {
            InsertMonster("testMonster1", 12345);
            InsertMonster("testMonster2", 12345);

            List<Monster> actual = new MonsterDb().RetrieveMonsters(12345);

            Assert.AreEqual(1, actual.Count(q => q.Points == 12345 && q.Name == "testMonster1"));
            Assert.AreEqual(1, actual.Count(q => q.Points == 12345 && q.Name == "testMonster2"));
            Assert.AreEqual(2, actual.Count);
        }

        private void InsertMonster(string name, int points)
        {
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Monster (Name, Points) VALUES (@name,@points);", c))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@points", points);
                    command.ExecuteNonQuery();
                }
            }
        }

        [TearDown]
        public void DeleteRecords()
        {
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("Delete from Monster;", c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
