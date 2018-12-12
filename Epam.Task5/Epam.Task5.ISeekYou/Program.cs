using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.ISeekYou
{
    public class Program
    {
        public static readonly Random random = new Random();

        public static void FillArray(int[] array)
        {
            if (!array.Any())
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-5, 5);
            }
        }

        public static void ShowInfo(double t)
        {
            Console.WriteLine();
            Console.WriteLine($"By using {nameof(t)}: {t}");
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Comparative measurements of the working time of the methods:");
            Console.WriteLine("Average time for 1000 invokes of method that contains 1M of int elements.");

            int[] array = new int[1000000];
            FillArray(array);

            var simpleMethod = TimeTestClass.TimeTest(array, SearchMethods.HowManyZeros);

            var delegateMethod = TimeTestClass.TimeTest(array, SearchMethods.NumberComparer, SearchMethods.HowManyZeros);

            var anonDelegateMethod = TimeTestClass.TimeTest(array,
                delegate (int a, int b)
                {
                    return a == b;
                },
                SearchMethods.HowManyZeros);

            var lambdaMethod = TimeTestClass.TimeTest(array, (a, b) => a == b, SearchMethods.HowManyZeros);

            var linqMethod = TimeTestClass.TimeTest(() => array.Select(ar => ar == 0).Count());

            Console.WriteLine($"By using {nameof(simpleMethod)}: {simpleMethod} milliseconds");
            Console.WriteLine($"By using {nameof(delegateMethod)}: {delegateMethod} milliseconds");
            Console.WriteLine($"By using {nameof(anonDelegateMethod)}: {anonDelegateMethod} milliseconds");
            Console.WriteLine($"By using {nameof(lambdaMethod)}: {lambdaMethod} milliseconds");
            Console.WriteLine($"By using {nameof(linqMethod)}: {linqMethod} milliseconds");
        }
    }
}
