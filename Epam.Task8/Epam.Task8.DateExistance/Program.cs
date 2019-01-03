using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task8.DateExistance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dateFormat = "dd-MM-yyyy";
            Regex r = new Regex(@"\b[0-9]{2}-[0-9]{2}-[0-9]{4}\b");

            while (true)
            {
                Console.WriteLine($"Enter a text that contains data in \"{dateFormat}\" format:");

                string input = Console.ReadLine();

                var dateMatches = r.Matches(input);
                bool dateParse = false;

                foreach (var match in dateMatches)
                {
                    if (DateTime.TryParseExact(match.ToString(), dateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        dateParse = true;
                        break;
                    } 
                }

                if (dateParse)
                {
                    Console.WriteLine($"Text contains data in \"{dateFormat}\" format.");
                }
                else
                {
                    Console.WriteLine($"Text doesnt contains data in \"{dateFormat}\" format.");
                }

                Console.WriteLine();
            }
        }
    }
}
