using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.WordFrequency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter some English text to know frequency of each word.");

            string input = Console.ReadLine();

            WordFrequency.FindWordFrequency(input);
        }
    }
}
