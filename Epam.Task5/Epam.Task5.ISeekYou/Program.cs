using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.ISeekYou
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] array = new int[1000000];
            ServantClass.FillArray(array);

            Console.WriteLine("Comparative measurements of the working time of the methods:");
            Console.WriteLine($"Average call execution time in an array that contains {array.Length} elements.");

            var simpleMethod = ServantClass.TimeTest(() => array.HowManyZeros());

            var delegateMethod = ServantClass.TimeTest(() => array.HowManyZeros(SearchMethods.NumberComparer));

            var anonDelegateMethod = ServantClass.TimeTest(() => array.HowManyZeros(delegate(int a, int b)
            {
                return a == b;
            }));

            var lambdaMethod = ServantClass.TimeTest(() => array.HowManyZeros((a, b) => a == b));

            var linqMethod = ServantClass.TimeTest(() => array.Select(ar => ar == 0).Count());

            Console.WriteLine($"By using {nameof(simpleMethod)}: {simpleMethod} milliseconds");
            Console.WriteLine($"By using {nameof(delegateMethod)}: {delegateMethod} milliseconds");
            Console.WriteLine($"By using {nameof(anonDelegateMethod)}: {anonDelegateMethod} milliseconds");
            Console.WriteLine($"By using {nameof(lambdaMethod)}: {lambdaMethod} milliseconds");
            Console.WriteLine($"By using {nameof(linqMethod)}: {linqMethod} milliseconds");
        }
    }
}
