using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace NmzPointsHour.NightmareZone.Database
{
    public interface IMonsterDb
    {
        List<Monster> RetrieveMonsters(int points);
    }

    public class MonsterDb : IMonsterDb
    {

        public List<Monster> RetrieveMonsters(int points)
        {
            List<Monster> result = new List<Monster>();

            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select * from Monster", c))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            result.Add(new Monster
                            {
                                Name = r["Name"].ToString(),
                                Points = (long)r["Points"]
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
