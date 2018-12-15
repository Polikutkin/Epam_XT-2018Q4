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

            QuickSort(array, 0, array.Length - 1, compare);

            this.Sorted?.Invoke(this, EventArgs.Empty);

        }

        private static void QuickSort<T>(T[] array, int left, int right, Func<T, T, int> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            if (left > right || left < 0 || right < 0)
            {
                return;
            }

            int index = Partition(array, left, right, compare);

            if (index != -1)
            {
                QuickSort(array, left, index - 1, compare);
                QuickSort(array, index + 1, right, compare);
            }
        }

        private static int Partition<T>(T[] array, int left, int right, Func<T, T, int> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            if (left > right)
            {
                return -1;
            }

            int end = left;

            T pivot = array[right];

            for (int i = left; i < right; i++)
            {
                if (compare(array[i], pivot) < 0)
                {
                    Swap(array, i, end++);
                }
            }

            Swap(array, end, right);

            return end;
        }

        private static void Swap<T>(T[] array, int left, int right)
        {
            T temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
