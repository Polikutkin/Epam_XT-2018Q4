using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.IntOrNot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a string to check whether this is a positive integer.");

                string input = Console.ReadLine();

                Console.Write("Result: ");
                Console.WriteLine(input.IsEvenNumber());
            }
        }
    }
}
