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
            string timeFormat = "t";
            Regex timeTemplate = new Regex(@"\b[0-9]+:[0-9]+\b");

            while (true)
            {
                Console.WriteLine("Enter a text to find out how many times how many times in the text occurs time:");

                string input = Console.ReadLine();

                var timeMatches = timeTemplate.Matches(input);
                int timeCounter = 0;

                foreach (var match in timeMatches)
                {
                    if (DateTime.TryParseExact(match.ToString(), timeFormat, null, System.Globalization.DateTimeStyles.None, out var time))
                    {
                        timeCounter++;
                    }
                }

                Console.WriteLine($"Time in the text meets {timeCounter} times");
                Console.WriteLine();
            }
        }
    }
}
