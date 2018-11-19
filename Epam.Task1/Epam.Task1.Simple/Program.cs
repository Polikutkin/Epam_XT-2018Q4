using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task1.Simple
{
    class Program
    {
        static bool Simple(int n)
        {
            try
            {
                if (n < 1)
                    throw new ArgumentOutOfRangeException("Number is below 0");
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

        static void Main(string[] args)
        {
            Console.WriteLine("This program will determine if the input number is simple or not");

            while (true)
            {
                Console.WriteLine("\r\nEnter a positive integer");

                bool isDigit = int.TryParse(Console.ReadLine(), out var data);

                if (!isDigit || data < 1)
                {
                    Console.WriteLine("Please, enter correct data");
                    continue;
                }

                bool result = Simple(data);

                Console.WriteLine($"{data} is simple number: {result}");
            }
        }
    }
}
