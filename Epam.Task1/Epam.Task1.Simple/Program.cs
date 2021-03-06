﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task1.Simple
{
    internal class Program
    {
        internal static bool Simple(int n)
        {
            try
            {
                if (n < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(n), "The number less than 0.");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            for (var i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        internal static void Main(string[] args)
        {
            Console.WriteLine("This program will determine if the input number is simple or not.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter a positive integer.");

                bool isDigit = int.TryParse(Console.ReadLine(), out var data);

                if (!isDigit || data < 1)
                {
                    Console.WriteLine("Please, enter the correct data.");
                    continue;
                }

                bool result = Simple(data);

                Console.WriteLine($"{data} is a simple number: {result}");
            }
        }
    }
}
