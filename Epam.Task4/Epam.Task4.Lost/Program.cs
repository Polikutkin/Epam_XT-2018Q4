using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.Lost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of people who will play the game. Every second person will be removed until one person remains.");

            while (true)
            {
                string input = Console.ReadLine();
                bool intParse = int.TryParse(input, out var number);

                if (!intParse || number < 1)
                {
                    Console.WriteLine("Enter the correct number. Must be more than 0.");
                    continue;
                }

                int result = Lost.LostMethod(number);

                Console.WriteLine($"Man number {result} won");
                Console.WriteLine();
            }
        }
    }
}
