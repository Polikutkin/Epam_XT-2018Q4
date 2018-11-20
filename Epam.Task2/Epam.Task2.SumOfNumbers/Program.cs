using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.SumOfNumbers
{
    class Program
    {
        static int SumOfNumbers(int n)
        {
            int sum = 0;

            for (int i = 1; i < n; i++)
            {
                sum += i % 3 == 0 ? i : i % 5 == 0 ? i : 0;
            }

            return sum;
        }
        static void Main(string[] args)
        {
            Console.Write("The sum of all numbers is less than 1000, a multiple of 3 or 5: ");

            Console.WriteLine(SumOfNumbers(1000));
        }
    }
}
