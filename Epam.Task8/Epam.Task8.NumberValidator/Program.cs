using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task8.NumberValidator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Regex simpleForm = new Regex(@"^-?[0-9]+(\.[0-9]+)?$");
            Regex scienceForm = new Regex(@"^(-|\+)?[0-9]+(\.[0-9]+)?[Ee]{1}(-|\+)?[0-9]+$");

            while (true)
            {
                Console.WriteLine("Enter a number to find out in which notation it was written:");

                string input = Console.ReadLine();

                if (scienceForm.IsMatch(input.Trim()))
                {
                    Console.WriteLine("This number in science notation");
                }
                else if (simpleForm.IsMatch(input.Trim()))
                {
                    Console.WriteLine("This number in simple notation");
                }
                else
                {
                    Console.WriteLine("This is not a number");
                }

                Console.WriteLine();
            }
        }
    }
}
