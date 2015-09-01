using System;
using System.Configuration;
using System.Data.SQLite;

namespace NmzPointsHour.NightmareZone.Database
{
    public interface IDreamDb
    {
        void SaveDream(Dream dream);
    }

    public class DreamDb : IDreamDb
    {
        public void SaveDream(Dream dream)
        {
            using (SQLiteConnection c = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqllite"].ConnectionString))
            {
                c.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Dream (DurationMs, Points) VALUES (@durationms,@points);", c))
                {
                    command.Parameters.AddWithValue("@durationms", dream.Duration);
                    command.Parameters.AddWithValue("@points", dream.Points);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}