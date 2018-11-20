using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.ArrayProcessing
{
    class Program
    {
        static int Max(int[] array)
        {
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                max = max < array[i] ? array[i] : max;
            }

            return max;
        }

        static int Min(int[] array)
        {
            int min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                min = min > array[i] ? array[i] : min;
            }

            return min;
        }

        static int[] BubbleSort(int[] array)
        {
            if (array.Length < 1)
                try
                {
                    throw new ArgumentNullException("Array is empty.");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                    return array;
                }

            if (array.Length == 1)
                return array;

            int temp;

            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

        static void ArrayElements(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine(array[array.Length - 1]);
        }

        static void Main(string[] args)
        {
            Random r = new Random();

            int[] array = new int[11];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(-50, 50);
            }

            int max = Max(array);
            int min = Min(array);

            Console.WriteLine("Array: ");
            ArrayElements(array);

            Console.WriteLine($"Max value: {max}{Environment.NewLine}Min value: {min}");

            BubbleSort(array);

            Console.WriteLine("Sorted array: ");
            ArrayElements(array);
        }
    }
}
