using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.Non_NegativeSum
{
    internal class Program
    {
        internal static int NonNegativeSum(int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i] >= 0 ? array[i] : 0;
            }

            return sum;
        }

        internal static void Main(string[] args)
        {
            int[] array = new int[5];

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Fill array elements to find out the sum of its positive elements.{Environment.NewLine}");

                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"array[{i}] = ");

                    bool result = int.TryParse(Console.ReadLine(), out var num);

                    if (!result)
                    {
                        Console.WriteLine("Please, enter the correct number.");
                        i--;
                        continue;
                    }

                    array[i] = num;
                }

                Console.WriteLine($"Sum of Non-negative numbers of the Array: {NonNegativeSum(array)}");
            }
        }
    }
}
