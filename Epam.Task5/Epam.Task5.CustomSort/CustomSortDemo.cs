using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.CustomSort
{
    public class CustomSortDemo
    {
        public static int CompareString(string first, string second)
        {
            if (first == second)
            {
                return 0;
            }

            if (first == null)
            {
                return -1;
            }

            if (second == null)
            {
                return 1;
            }

            if (first.Length < second.Length)
            {
                return -1;
            }

            if (first.Length > second.Length)
            {
                return 1;
            }

            return first.CompareTo(second);
        }
    }
}
