using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.CharDoubler
{
    class Program
    {
        static string CharDoubler(string input1, string input2)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input1.Length; i++)
            {
                sb.Append(input1[i]);

                for (int j = 0; j < input2.Length; j++)
                {
                    if (char.ToLower(input1[i]).CompareTo(char.ToLower(input2[j])) == 0)
                    {
                        sb.Append(input1[i]);
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter first string: ");
                string input1 = Console.ReadLine();

                Console.Write("Enter second string: ");
                string input2 = Console.ReadLine();

                Console.WriteLine($"Result string: {CharDoubler(input1, input2)}");
            }
        }
    }
}
