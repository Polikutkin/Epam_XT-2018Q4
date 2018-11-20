﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.Rectangle
{
    class Program
    {
        static int Area(int a, int b) => a * b;

        static bool IsPositiveNumber(ref int a)
        {
            bool side = int.TryParse(Console.ReadLine(), out var x);

            if (side == true && x < 1)
            {
                try
                {
                    throw new ArgumentOutOfRangeException("The number is not positive.");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }

            a = x;
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the length of the sides of the rectangle to find the area.");

            while (true)
            {
                int a = 0;
                int b = 0;

                Console.Write($"{Environment.NewLine}Enter side \"{nameof(a)}\": ");

                bool side1 = IsPositiveNumber(ref a);

                if (!side1)
                {
                    continue;
                }

                Console.Write($"{Environment.NewLine}Enter side \"{nameof(b)}\": ");

                bool side2 = IsPositiveNumber(ref b);

                if(!side2)
                {
                    continue;
                }

                Console.WriteLine($"\nArea of Rectangle: {Area(a, b)}");
            }
        }
    }
}