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

            for (int i = 0; i < charArray.Length; i++)
            {
                if (char.IsLetter(charArray[i]))
                {
                    continue;
                }
                else
                {
                    charArray[i] = ' ';
                }
            }

            string[] str = new string(charArray).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;

            foreach(var word in str)
            {
                count += word.Length;
            }

            return (double)count / str.Length;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter some text to find out the average word length:");

                string input = Console.ReadLine();

                Console.WriteLine($"Average word length is: {AverageStringLength(input)}"); 
            }
        }
    }
}
