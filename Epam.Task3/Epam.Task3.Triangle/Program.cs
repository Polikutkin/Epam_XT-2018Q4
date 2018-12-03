using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.Triangle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program allows you to create a triangle.");

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}Enter sides:");

                Console.Write("A: ");
                string s1 = Console.ReadLine();

                Console.Write("B: ");
                string s2 = Console.ReadLine();

                Console.Write("C: ");
                string s3 = Console.ReadLine();

                bool aside = double.TryParse(s1, out double a);
                bool bside = double.TryParse(s2, out double b);
                bool cside = double.TryParse(s3, out double c);

                if (!aside || !bside || !cside)
                {
                    Console.WriteLine("Triangle parameters are not correct. Please enter the valid parameters.");
                    continue;
                }

                if (a >= b + c || b >= a + c || c >= a + b)
                {
                    Console.WriteLine("Triangle with these side lengths cannot exist. The sum of the two sides cannot be equal to or less than the third side.");
                    continue;
                }

                Triangle triangle = new Triangle(a, b, c);
                triangle.ShowInfo(); 
            }
        }
    }
}
