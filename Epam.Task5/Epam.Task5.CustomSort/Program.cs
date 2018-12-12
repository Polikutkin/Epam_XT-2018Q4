using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.CustomSort
{
    public class Program
    {
        public static void ShowArrayElements<T>(T[] array)
        {
            Console.WriteLine();

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            string[] stringArray = { "what", "So", "ar", "Ar", "where", "so", "when" };

            Console.WriteLine($"Array: {nameof(stringArray)}");

            ShowArrayElements(stringArray);

            stringArray.QuickSort(CustomSortDemo.CompareString);

            Console.WriteLine();
            Console.WriteLine($"Sorted array: {nameof(stringArray)}");

            ShowArrayElements(stringArray);
        }
    }
}
