using System;
using System.Collections.Generic;
using System.Linq;
using NmzPointsHour.NightmareZone.Database;

namespace NmzPointsHour.NightmareZone
{
    public class NMZSession
    {
        public IDreamDb DreamDb;

        public NMZSession()
        {
            Dreams = new List<Dream>();
            DreamDb = new DreamDb();
        }

        public void StartNewDream()
        {
            if (CurrentDream != null && CurrentDream.IsActive()) return;

            Dreams.Add(new Dream());
            CurrentDream.Start();
        }

        public Dream CurrentDream
        {
            get { return Dreams.Count == 0 ? null : Dreams.Last(); }
        }


        public void StopCurrentDream()
        {
            if (CurrentDream == null || !CurrentDream.IsActive()) return;
            CurrentDream.End();
            DreamDb.SaveDream(CurrentDream);
        }

        public List<Dream> Dreams { get; set; }
    }
}