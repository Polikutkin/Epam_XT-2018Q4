using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.AnotherTriangle
{
    class Program
    {
        static void AnotherTriangle(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    Console.Write(' ');
                }
                for (int k = 0; k < i + (i - 1); k++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
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

                AnotherTriangle(lines);
            }
        }
    }
}
