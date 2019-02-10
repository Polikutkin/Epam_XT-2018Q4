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
            Regex r = new Regex(@"\b(((31-(0[13578]|1[02]))|((29|30)-(0[1,3-9]|1[0-2]))" +
                @"|((0[1-9]|1[0-9]|2[0-8])-(0[1-9]|1[0-2])))-([0-9][0-9][0-9][0-9]))|" +
                @"(29-02-([0-9][0-9]([02468][048])|([13579][26])))\b");

            while (true)
            {
                Console.WriteLine($"Enter a text that contains data in \"{dateFormat}\" format:");

                string input = Console.ReadLine();

                var dateMatches = r.Matches(input);

                if (dateMatches.Count > 0)
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
