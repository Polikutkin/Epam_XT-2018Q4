using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.NoPositive
{
    class Program
    {
        static void InitialieTridimensionalArray(int[,,] array)
        {
            Random r = new Random();

            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    for (int k = 0; k < array.GetLongLength(2); k++)
                    {
                        array[i, j, k] = r.Next(-50, 50);
                    }
                }
            }
        }

        static void NoPositive(int[,,] array)
        {
            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    for (int k = 0; k < array.GetLongLength(2); k++)
                    {
                        if (array[i, j, k] > 0)
                        {
                            array[i, j, k] = 0;
                        }
                    }
                }
            }
        }

        static void ShowArrayInfo(int[,,] array)
        {
            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    for (int k = 0; k < array.GetLongLength(2); k++)
                    {
                        Console.Write($"{array[k, j, i]}, ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int[,,] array = new int[2,2,2];

            InitialieTridimensionalArray(array);

            Console.WriteLine($"Array:{Environment.NewLine}");
            ShowArrayInfo(array);

            NoPositive(array);

            Console.WriteLine($"Array with 0 instead of positive numbers:{Environment.NewLine}");
            ShowArrayInfo(array);
        }
    }
}
