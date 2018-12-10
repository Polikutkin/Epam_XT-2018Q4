using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.Triangle
{
    internal class Program
    {
        internal const char Star = '*';

        internal static void Triangle(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write(Star);
                }

                Console.WriteLine();
            }
        }

        internal static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter the number of the lines of the Triangle.");

                bool result = int.TryParse(Console.ReadLine(), out var lines);
                
                if (!result || lines < 1)
                {
                    Console.WriteLine("Please, enter the correct number.");
                    continue;
                }

                Triangle(lines);
            }
        }
    }
}
