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
                if (n <= 0 || n % 2 == 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                if (n < 0)
                    Console.WriteLine($"Значение {n} не соответствует условию: меньше 0.");
                else
                    Console.WriteLine($"Значение {n} не соответствует условию: четное.");

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
            Square(9);

            Square(6);

            Square(-2);

            Console.ReadKey();
        }
    }
}
