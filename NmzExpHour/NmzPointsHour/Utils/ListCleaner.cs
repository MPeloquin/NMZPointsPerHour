using System;
using System.Collections.Generic;
using System.Linq;

namespace NmzPointsHour.Utils
{
    public class ListCleaner
    {
        public List<int> TrimZeros(List<int> list)
        {
            RemoveLeadingZeros(list);
            RemoveTrailingZeros(list);

            return list;
        }

        private void RemoveLeadingZeros(List<int> list)
        {
            for (int i = 0; i < list.FindIndex(j => j != 0); i++)
            {
                list.RemoveAt(i);
                i--;
            }
        }

        private void RemoveTrailingZeros(List<int> list)
        {
            for (int i = list.FindLastIndex(j => j != 0) + 1; i < list.Count; i++)
            {
                list.RemoveAt(i);
                i--;
            }
        }

        public List<Tuple<bool, int>> RemoveBackToBackTrueColumnAndFalseColumns(List<Tuple<bool, int>> entry)
        {
            for (int i = 1; i < entry.Count; i++)
            {
                if ((entry[i].Item1 && entry[i - 1].Item1))
                {
                    entry.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < entry.Count; i++)
            {
                if (!entry[i].Item1)
                {
                    entry.RemoveAt(i);
                    i--;
                }
            }
            
            return entry;
        }
    }
}