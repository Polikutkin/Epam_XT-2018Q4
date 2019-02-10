using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.WordFrequency
{
    public class WordFrequency
    {
        public static IDictionary<string, int> FindWordFrequency(string str)
        {
            string[] wordArray = GetWords(str).ToArray();
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

            return wordCounter;
        }

        private static IEnumerable<string> GetWords(string str)
        {
            List<string> wordCollection = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < wordCollection.Count; i++)
            {
                StringBuilder sb = new StringBuilder();

                for (int j = 0; j < wordCollection[i].Length; j++)
                {
                    char c = wordCollection[i][j];
                    bool writed = false;

                    if (char.IsLetter(c)
                        || c == '-'
                        || c == '\'')
                    {
                        sb.Append(c);
                        writed = true;
                    }
                    else if (writed)
                    {
                        break;
                    }
                }

                if (!char.IsLetter(sb[0]))
                {
                    sb.Remove(0, 1);
                }

                if (!char.IsLetter(sb[sb.Length - 1]))
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                wordCollection[i] = sb.ToString();
            }

            return wordCollection;
        }
    }
}
