using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task1.Square
{
    class Program
    {
        static void Square(int n)
        {
            try
            {
                if (n <= 0)
                {
                    throw new ArgumentOutOfRangeException("The number less than 0.");
                }
                if (n % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException("The number is even.");
                }
            }

            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i == n / 2 && j == n / 2)
                        Console.Write(' ');
                    else
                        Console.Write('*');
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("This program will show you the beauty of symmetry!");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter a positive odd integer.");

                bool isDigit = int.TryParse(Console.ReadLine(), out var data);

                if (!isDigit || data < 1 || data % 2 == 0)
                {
                    Console.WriteLine("Please, enter correct data.");
                    continue;
                }

                Square(data);
            }
        }
    }
}
