using System.Collections.Generic;

namespace NmzExpHour.Utils
{
    public class ListCleaner
    {
        public List<int> RemoveZerosFromRow(List<int> rows)
        {
            int firstElement = -1;
            int lastElement = 0;

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i] != 0 && firstElement == -1)
                    firstElement = i;
                if (rows[i] != 0)
                    lastElement = i;
            }

            for (int i = 0; i < firstElement; i++)
            {
                rows.RemoveAt(i);
                i--;
                firstElement--;
                lastElement--;
            }

            for (int i = lastElement + 1; i < rows.Count; i++)
            {
                rows.RemoveAt(i);
                i--;
            }

            return rows;
        }
    }
}