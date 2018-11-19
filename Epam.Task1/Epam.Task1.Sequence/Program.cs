using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task1.Sequence
{
    class Program
    {
        static string Sequence(int n)
        {
            try
            {
                if (n <= 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var sb = new StringBuilder();

            for (var i = 1; i < n; i++)
            {
                sb.Append(i + ", ");
            }

            sb.Append(n);

            return sb.ToString();
        }

        static void ShowIsSequence(int n)
        {
            Console.WriteLine($"Перечисление до {n}:");
            Console.WriteLine(Sequence(n));
        }

        static void Main(string[] args)
        {
            int a = 13;
            int b = -1;

            ShowIsSequence(a);

            ShowIsSequence(b);
        }
    }
}
