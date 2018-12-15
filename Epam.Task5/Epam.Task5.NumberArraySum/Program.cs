using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.NumberArraySum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] arrayInt = { 1, 2, 3, 4, 5 };
            decimal[] arrayDec = { 1.2m, 5.45m, 0.0001m };

            Console.WriteLine("Array:");
            WorkWithConsole.ShowCollectionElements(arrayInt);
            Console.WriteLine($"Sum of elements = {arrayInt.MySum()}");

            Console.WriteLine();
            Console.WriteLine("Array:");
            WorkWithConsole.ShowCollectionElements(arrayDec);
            Console.WriteLine($"Sum of elements = {arrayDec.MySum()}");
        }
    }
}
