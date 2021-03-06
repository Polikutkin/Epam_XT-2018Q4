﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2._2DArray
{
    internal class Program
    {
        internal static void InitializeArray(int[,] array)
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

        internal static int TwoDArray(int[,] array)
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

        internal static void ShowArrayInfo(int[,] array)
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

        internal static void Main(string[] args)
        {
            int[,] array = new int[3, 3];

            InitializeArray(array);

            Console.WriteLine($"Array:{Environment.NewLine}");
            ShowArrayInfo(array);

            Console.WriteLine($"{Environment.NewLine}Sum of even elements: {TwoDArray(array)}");
        }
    }
}
