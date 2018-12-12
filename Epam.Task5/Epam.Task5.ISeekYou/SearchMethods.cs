using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.ISeekYou
{
    public static class SearchMethods
    {
        public static int HowManyZeros(this int[] array)
        {
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static int HowManyZeros(this int[] array, Func<int, int, bool> condition)
        {
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i], 0))
                {
                    counter++;
                }
            }

            return counter;
        }

        public static bool NumberComparer(int a, int b)
        {
            return a == b;
        }
    }
}
