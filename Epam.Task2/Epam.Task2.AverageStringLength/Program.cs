using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task2.AverageStringLength
{
    class Program
    {
        static double AverageStringLength(string s)
        {
            var charArray = s.ToCharArray();

            double letterCounter = 0;
            int wordCounter = 0;
            bool previousIsLetter = false;

            for (int i = 0; i < charArray.Length; i++)
            {
                if (char.IsLetter(charArray[i]))
                {
                    letterCounter++;
                    previousIsLetter = true;
                    continue;
                }
                else if (previousIsLetter)
                {
                    wordCounter++;
                    previousIsLetter = false;
                }
            }

            return letterCounter / wordCounter;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter some text to find out the average word length:");

                string input = Console.ReadLine();

                Console.WriteLine($"{Environment.NewLine}Average word length is: {AverageStringLength(input)}{Environment.NewLine}"); 
            }
        }
    }
}
