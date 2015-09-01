using System;
using System.Diagnostics;

namespace NmzPointsHour.NightmareZone
{
    public interface IPointsCalculator
    {
        void Start(int points);
        void UpdatePoints(int points);
        float CalculatePointsPerHour();
        Stopwatch Stopwatch { get; set; }
    }

    public class PointsCalculator : IPointsCalculator
    {
        private Object thisLock = new Object();


        public PointsCalculator()
        {
            Stopwatch = new Stopwatch();
        }

        public void Start(int points)
        {
            Stopwatch.Start();
            StartingPoints = points;
        }

        public void UpdatePoints(int points)
        {
            lock (thisLock)
            {
                points = points - Points;
                
                Points += points;
            }
        }

        public float CalculatePointsPerHour()
        {
            if (Points == 0) return 0;
            float pointsHour = 0;
            lock (thisLock)
            {
                pointsHour = ((float)(Points - StartingPoints) / (float)Stopwatch.ElapsedMilliseconds) * 1000*60*60;
            }

            return pointsHour;
        }

        public Stopwatch Stopwatch { get; set; }
        public int Points { get; set; }
        public int StartingPoints { get; set; }
    }
}