using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.ISeekYou
{
    public static class TimeTestClass
    {
        public static double TimeTest<T>(T[] array, Func<T[], int> method)
        {
            Stopwatch sw = new Stopwatch();
            var millisecondsList = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                int count = method(array);
                sw.Stop();

                millisecondsList.Add(sw.ElapsedMilliseconds);
                sw.Reset();
            }

            return millisecondsList.Average();
        }

        public static double TimeTest<T>(T[] array, Func<int, int, bool> argumentMethod, Func<T[], Func<int, int, bool>, int> method)
        {
            Stopwatch sw = new Stopwatch();
            var millisecondsList = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                int count = method(array, argumentMethod);
                sw.Stop();

                millisecondsList.Add(sw.ElapsedMilliseconds);
                sw.Reset();
            }

            return millisecondsList.Average();
        }

        public static double TimeTest(Func<int> method)
        {
            Stopwatch sw = new Stopwatch();
            var millisecondsList = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                int count = method();
                sw.Stop();

                millisecondsList.Add(sw.ElapsedMilliseconds);
                sw.Reset();
            }

            return millisecondsList.Average();
        }
    }
}
