using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.SortingUnit
{
    public class SortClass
    {
        public event EventHandler<EventArgs> Sorted;

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

        public void Sort<T>(T[] array, Func<T, T, int> compare)
        {
            if (!array.Any() || array.Length == 1)
            {
                return;
            }

            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (compare(array[j], array[i]) < 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            this.Sorted?.Invoke(this, EventArgs.Empty);
        }
    }
}
