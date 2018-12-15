using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.NumberArraySum
{
    public static class NumberArraySum
    {
        public static int MySum(this IEnumerable<int> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            if (sum > int.MaxValue)
            {
                throw new OverflowException();
            }

            return sum;
        }

        public static long MySum(this IEnumerable<long> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            long sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            if (sum > int.MaxValue)
            {
                throw new OverflowException();
            }

            return sum;
        }

        public static float MySum(this IEnumerable<float> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            float sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            if (sum > int.MaxValue)
            {
                throw new OverflowException();
            }

            return sum;
        }

        public static double MySum(this IEnumerable<double> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            double sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            if (sum > int.MaxValue)
            {
                throw new OverflowException();
            }

            return sum;
        }

        public static decimal MySum(this IEnumerable<decimal> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            decimal sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            if (sum > int.MaxValue)
            {
                throw new OverflowException();
            }

            return sum;
        }
    }
}
