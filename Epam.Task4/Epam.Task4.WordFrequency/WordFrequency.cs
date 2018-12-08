using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.WordFrequency
{
    public class WordFrequency
    {
        public static void FindWordFrequency(string str)
        {
            string[] wordArray = str.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var wordCounter = new Dictionary<string, int>();

            foreach (var word in wordArray)
            {
                if (wordCounter.ContainsKey(word.ToLower()))
                {
                    wordCounter[word.ToLower()]++;
                }
                else
                {
                    wordCounter.Add(word.ToLower(), 1);
                }
            }

            foreach (var item in wordCounter)
            {
                Console.WriteLine($"{item.Value} {item.Key}");
            }
        }
    }
}
