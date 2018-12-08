using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.DynamicArray
{
    public class CycledDynamicArray<T> : DynamicArray<T>
    {
        public CycledDynamicArray() : base()
        {
        }

        public CycledDynamicArray(int n) : base(n)
        {
        }

        public CycledDynamicArray(IEnumerable<T> collection) : base(collection)
        {
        }

        public override IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    yield return this.Array[i];
                }
            }
        }
    }
}
