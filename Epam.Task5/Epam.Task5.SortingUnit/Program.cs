using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Task5.SortingUnit
{
    public class Program
    {
        public static void ShowArrayElements<T>(T[] array)
        {
            if (!array.Any())
            {
                return;
            }

            Console.WriteLine();

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine(Environment.NewLine);
        }

        public static void Main(string[] args)
        {
            string[] stringArray = { "what", "So", "ar", "Ar", "where", "so", "when" };

            Console.WriteLine($"Array: {nameof(stringArray)}");

            ShowArrayElements(stringArray);

            SortClass sc = new SortClass();
            sc.Sorted += (o, e) => Console.WriteLine("Sorting is done.");

            Thread t = new Thread(() => sc.Sort(stringArray, SortClass.CompareString));

            t.Start();
            t.Join();

            ShowArrayElements(stringArray);
        }
    }
}
