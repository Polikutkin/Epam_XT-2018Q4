using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.ArrayProcessing
{
    internal class Program
    {
        internal static int Max(int[] array)
        {
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                max = max < array[i] ? array[i] : max;
            }

            return max;
        }

        internal static int Min(int[] array)
        {
            int min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                min = min > array[i] ? array[i] : min;
            }

            return min;
        }

        internal static int[] MergeSort(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            int middle = array.Length / 2;

            int[] leftArray = new int[middle];
            int[] rightArray = new int[array.Length - middle];

            Array.Copy(array, leftArray, middle);
            Array.Copy(array, middle, rightArray, 0, array.Length - middle);

            return Merge(MergeSort(leftArray), MergeSort(rightArray));

            int[] Merge(int[] leftAr, int[] rightAr)
            {
                int left = 0;
                int right = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    if (left < leftAr.Length && right < rightAr.Length)
                    {
                        if (leftAr[left] > rightAr[right])
                        {
                            array[i] = rightAr[right++];
                        }
                        else
                        {
                            array[i] = leftAr[left++];
                        }
                    }
                    else if (left < leftAr.Length)
                    {
                        array[i] = leftAr[left++];
                    }
                    else
                    {
                        array[i] = rightAr[right++];
                    }
                }

                return array;
            }
        }

        internal static void ArrayElements(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }

            Console.WriteLine(array[array.Length - 1]);
        }

        internal static void Main(string[] args)
        {
            Random r = new Random();

            int[] array = new int[11];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(-50, 50);
            }

            Console.WriteLine("Array: ");
            ArrayElements(array);

            Console.WriteLine($"Max value: {Max(array)}{Environment.NewLine}Min value: {Min(array)}");

            MergeSort(array);

            Console.WriteLine("Sorted array: ");
            ArrayElements(array);
        }
    }
}
