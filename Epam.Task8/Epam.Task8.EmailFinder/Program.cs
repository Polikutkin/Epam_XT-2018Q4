using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task8.EmailFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Regex mailTemplate = new Regex(@"[0-9A-Za-z]+(\w|\.|-)*[0-9A-Za-z]+@[0-9A-Za-z]+[0-9A-Za-z-]*[0-9A-Za-z]+(\.[A-Za-z]{3,6})?\.[A-Za-z]{2,15}");

            while (true)
            {
                Console.WriteLine("Enter a string to find out if it contains Email adresses:");

                string input = Console.ReadLine();
                Console.WriteLine();

                var emailMatches = mailTemplate.Matches(input);

                if (emailMatches.Count > 0)
                {
                    Console.WriteLine("Finded Email adresses:");

                    foreach (var match in emailMatches)
                    {
                        Console.WriteLine(match.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("There are no Email adresses in the string.");
                }

                Console.WriteLine();
            }
        }
    }
}
