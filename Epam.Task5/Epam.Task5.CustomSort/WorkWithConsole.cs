using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.CustomSort
{
    public class WorkWithConsole
    {
        public static void ShowCollectionElements<T>(IEnumerable<T> collection)
        {
            if (!collection.Any())
            {
                return;
            }

            Console.WriteLine();

            foreach (var item in collection)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
