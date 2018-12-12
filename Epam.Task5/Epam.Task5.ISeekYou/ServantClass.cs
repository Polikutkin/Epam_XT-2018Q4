using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.ISeekYou
{
    public static class ServantClass
    {
        public static readonly Random Random = new Random();

        public static void FillArray(int[] array)
        {
            if (!array.Any())
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Random.Next(-5, 5);
            }
        }

        public static double TimeTest(Action method)
        {
            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                method();
                sw.Stop();
            }

            return sw.Elapsed.TotalMilliseconds / 100;
        }
    }
}
