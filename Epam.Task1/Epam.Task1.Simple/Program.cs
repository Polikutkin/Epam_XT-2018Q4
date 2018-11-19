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
                if (n <= 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            for (var i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        static void ShowIsSimple(int n)
        {
            Console.Write($"Число {n} простое: ");
            Console.WriteLine(Simple(n));
        }

        static void Main(string[] args)
        {
            int a = 7;
            int b = 18;
            int c = 0;

            ShowIsSimple(a);

            ShowIsSimple(b);

            ShowIsSimple(c);
        }
    }
}
