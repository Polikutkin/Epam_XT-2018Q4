using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task8.HtmlReplacer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"This program will swap all tags in your text to \"_\" symbol.{Environment.NewLine}Example: <b>Some text</b> --> _Some text_");

            Regex r = new Regex(@"<[^>]*>");

            while (true)
            {
                Console.WriteLine("Enter some text with tags:");

                string input = Console.ReadLine();

                input = r.Replace(input, "_");

                Console.WriteLine("Swap result:");
                Console.WriteLine(input);
                Console.WriteLine();
            }
        }
    }
}
