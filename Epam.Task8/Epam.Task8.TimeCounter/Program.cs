using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task8.TimeCounter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Regex timeTemplate = new Regex(@"\b(1[0-9]|2[0-3]|0?[0-9]):[0-5][0-9]\b");

            while (true)
            {
                Console.WriteLine("Enter a text to find out how many times how many times in the text occurs time:");

                string input = Console.ReadLine();

                var timeMatches = timeTemplate.Matches(input);
                int timeCounter = 0;

                foreach (var match in timeMatches)
                {
                    timeCounter++;
                }

                Console.WriteLine($"Time in the text meets {timeCounter} times");
                Console.WriteLine();
            }
        }
    }
}
