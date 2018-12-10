using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.DynamicArray
{
    public class ConsoleMethods
    {
        public static void ShowElementsOnConsole<T>(IEnumerable<T> ar)
        {
            Console.WriteLine($"Elements of {ar.GetType().Name}:");

            foreach (var item in ar)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
