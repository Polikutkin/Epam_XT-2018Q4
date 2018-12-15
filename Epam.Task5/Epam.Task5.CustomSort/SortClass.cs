using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.CustomSort
{
    public static class SortClass
    {
        public static void BubbleSort<T>(this T[] array, Func<T, T, int> compare)
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
                        Swap(array, i, j);
                    }
                }
            }
        }

        public static void MergeSort<T>(this T[] array, Func<T, T, int> compare)
        {
            if (!array.Any() || array.Length == 1)
            {
                return;
            }

            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            MergeSort(array, 0, array.Length - 1, compare);
        }

        public static void QuickSort<T>(this T[] array, Func<T, T, int> compare)
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
        }

        private static void MergeSort<T>(T[] array, int low, int high, Func<T, T, int> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            if (low < high)
            {
                int middle = (low / 2) + (high / 2);

                MergeSort(array, low, middle, compare);
                MergeSort(array, middle + 1, high, compare);
                MergeSort(array, low, middle, high, compare);
            }
        }

        private static void MergeSort<T>(T[] array, int low, int middle, int high, Func<T, T, int> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            int left = low;
            int right = middle + 1;
            int tmpIndex = 0;

            T[] tmp = new T[(high - low) + 1];

            while ((left <= middle) && (right <= high))
            {
                if (compare(array[left], array[right]) < 0)
                {
                    tmp[tmpIndex] = array[left++];
                }
                else
                {
                    tmp[tmpIndex] = array[right++];
                }

                tmpIndex = tmpIndex + 1;
            }

            if (left <= middle)
            {
                while (left <= middle)
                {
                    tmp[tmpIndex++] = array[left++];
                }
            }

            if (right <= high)
            {
                while (right <= high)
                {
                    tmp[tmpIndex++] = array[right++];
                }
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                array[low + i] = tmp[i];
            }
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
