using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2._2DArray
{
    class Program
    {
        static void InitialieTwodimensionalArray(int[,] array)
        {
            Random r = new Random();

            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    array[i, j] = r.Next(-10, 10);
                }
            }
        }

        static int TwoDArray(int[,] array)
        {
            int sum = 0;

            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    sum += ((i + j) % 2 == 0) ? array[i, j] : 0;
                }
            }

            return sum;
        }

        static void ShowArrayInfo(int[,] array)
        {
            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    Console.Write($"{array[j, i]}, ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int[,] array = new int[3, 3];

            InitialieTwodimensionalArray(array);

            Console.WriteLine($"Array:{Environment.NewLine}");
            ShowArrayInfo(array);

            Console.WriteLine($"Sum of even elements: {TwoDArray(array)}");
        }
    }
}
