using System;
using NmzPointsHour.NightmareZone.Database;
using NmzPointsHour.OCR;

namespace NmzPointsHour.NightmareZone
{
    public class Dream
    {
        private bool active;
        public IPointsCalculator Calculator { get; set; }
        public INMZPointsScreenReader NMZPointsScreenReader { get; set; }
        public IMonsterDb MonsterDb { get; set; }

        private long finalDuration = 0;
        public long Duration
        {
            get { return finalDuration == 0 ? Calculator.Stopwatch.ElapsedMilliseconds : finalDuration;}
            set { finalDuration = value; }
        }

        public int Points { get; set; }
        public long Id { get; set; }

        public event MonsterKilledHandler MonsterKilled;
        public delegate void MonsterKilledHandler(Dream sender, EventArgs e);

        public Dream()
        {
            active = false;
            Points = -1;
            Calculator = new PointsCalculator();
            NMZPointsScreenReader = new NMZPointsScreenReader();
            Id = 0;
        }

        public void Start()
        {
            active = true;
            Points = Convert.ToInt32(NMZPointsScreenReader.ScreenToNMZPoints());
            Calculator.Start(Points);
        }

        public bool IsActive()
        {
            return active;
        }

        public void End()
        {
            active = false;
            Calculator.Stopwatch.Stop();
            finalDuration = Calculator.Stopwatch.ElapsedMilliseconds;
        }

        public void UpdatePoints()
        {
            int points;
            
            try
            {
                points = Convert.ToInt32(NMZPointsScreenReader.ScreenToNMZPoints());

                if (points == -1 || points == Points || points > 1048575) return;
            }
            catch (Exception)
            {
                return;
            }
            var args = new MonsterKilledArgs { Points = points - Points };

            if (MonsterKilled != null) MonsterKilled(this, args);

            Points = points;

            Calculator.UpdatePoints(Convert.ToInt32(points));
        }
    }
}