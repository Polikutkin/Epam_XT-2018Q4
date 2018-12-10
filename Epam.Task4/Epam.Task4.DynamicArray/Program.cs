using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Task4.DynamicArray
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ar1 = new DynamicArray<int>();
            var ar2 = new DynamicArray<DateTime>(12);
            var ar3 = new DynamicArray<string>
            {
                "A", "B", "C", "D"
            };

            ConsoleMethods.ShowElementsOnConsole(ar3);
            Console.WriteLine();
            Console.WriteLine(ar3.ShowInfo());

            ar3.Add("E");

            ar3.AddRange(new string[] { "F", "G" });

            ar3.ChangeCapacity(22);

            ar3.Insert(1, "AAA");

            ar3.Remove(0);

            var ar4 = ar3.Clone();

            string[] stringArray = ar3.ToArray();

            ConsoleMethods.ShowElementsOnConsole(ar3);
            Console.WriteLine();
            Console.WriteLine(ar3.ShowInfo());

            var cycleAr = new CycledDynamicArray<string>
            {
                "A", "B", "C"
            };

            foreach (var item in cycleAr)
            {
                Console.Write($"{item} ");
                Thread.Sleep(500);
            }
        }
    }
}
