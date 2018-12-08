using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task4.DynamicArray
{
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>, ICloneable
    {
        private T[] array;
        private int capacity;

        public DynamicArray()
        {
            this.Array = new T[8];
            this.Capacity = this.Array.Length;
            this.Length = 0;
        }

        public DynamicArray(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("Dynamic Array cannot have capacity with less than 1 element.", nameof(n));
            }

            this.Array = new T[n];
            this.Capacity = this.Array.Length;
            this.Length = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            this.Array = new T[collection.Count()];

            for (int i = 0; i < this.Capacity; i++)
            {
                foreach (var item in collection)
                {
                    this.Array[i] = item;
                }
            }

            this.Capacity = this.Array.Length;
            this.Length = this.Capacity;
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            protected set
            {
                this.capacity = value;
            }
        }

        public int Length { get; private set; }

        protected T[] Array
        {
            get
            {
                return this.array;
            }

            set
            {
                this.array = value;
            }
        }

        public T this[int i]
        {
            get
            {
                if (i >= this.Length || i <= ~this.Length)
                {
                    throw new ArgumentOutOfRangeException(i.ToString(), "Index is out of range. Index cannot be over the size of the Dynamic Array.");
                }

                if (i < 0 && i > ~this.Length)
                {
                    return this.Array[this.Length + i];
                }
                else
                {
                    return this.Array[i];
                }
            }

            set
            {
                if (i >= this.Length || i <= ~this.Length)
                {
                    throw new ArgumentOutOfRangeException(i.ToString(), "Index is out of range. Index cannot be over the size of the Dynamic Array.");
                }

                if (i < 0 && i > ~this.Length)
                {
                    this.Array[this.Length + i] = value;
                }
                else
                {
                    this.Array[i] = value;
                }
            }
        }

        public void Add(T item)
        {
            if (this.Length == this.Capacity)
            {
                T[] newArray = new T[this.Capacity * 2];

                System.Array.Copy(this.Array, newArray, this.Length);

                newArray[this.Length] = item;

                this.Array = newArray;
                this.Capacity = this.Array.Length;
            }
            else
            {
                this.Array[this.Length] = item;
            }

            this.Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (this.Capacity < this.Length + collection.Count())
            {
                int newLength = this.Capacity + collection.Count();

                T[] newArray = new T[newLength];

                System.Array.Copy(this.Array, newArray, this.Length);

                foreach (var item in collection)
                {
                    newArray[this.Length++] = item;
                }

                this.Array = newArray;
                this.Capacity = this.Array.Length;
            }
            else
            {
                foreach (var item in collection)
                {
                    this.Array[this.Length++] = item;
                }
            }
        }

        public void ChangeCapacity(int newCapacity)
        {
            if (newCapacity < 0 || newCapacity > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(newCapacity), "Capacity size is invalid. Must be non-negative.");
            }

            T[] newArray = new T[newCapacity];

            if (newArray.Length > this.Capacity)
            {
                for (int i = 0; i < this.Array.Length; i++)
                {
                    newArray[i] = this.Array[i];
                }
            }
            else if (newArray.Length < this.Capacity)
            {
                for (int i = 0; i < newArray.Length; i++)
                {
                    newArray[i] = this.Array[i];
                }

                if (newArray.Length < this.Length)
                {
                    this.Length = newArray.Length;
                }
            }
            else
            {
                return;
            }

            this.Array = newArray;
            this.Capacity = this.Array.Length;
        }

        public object Clone()
        {
            return new DynamicArray<T>(this.Array);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.Array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Insert(int index, T item)
        {
            if (index < 0 || index > this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range. Index cannot be over the size of the Dynamic Array.");
            }

            if (this.Length == this.Array.Length)
            {
                T[] newArray = new T[this.Capacity * 2];

                for (int i = 0; i < index; i++)
                {
                    newArray[i] = this.Array[i];
                }

                newArray[index] = item;

                for (int i = index; i < this.Length; i++)
                {
                    newArray[i + 1] = this.Array[i];
                }

                this.Array = newArray;
                this.Capacity = this.Array.Length;
            }
            else
            {
                for (int i = this.Length; i > index; i--)
                {
                    this.Array[i] = this.Array[i - 1];
                }

                this.Array[index] = item;
            }

            this.Length++;

            return true;
        }

        public bool Remove(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                return false;
            }
            else
            {
                for (int i = index; i < this.Length - 1; i++)
                {
                    this.Array[i] = this.Array[i + 1];
                }

                this.Array[--this.Length] = default(T);

                return true;
            }
        }

        public T[] ToArray()
        {
            T[] newArray = new T[this.Length];

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = this.Array[i];
            }

            return newArray;
        }

        public string ShowInfo()
        {
            string info = $@"{nameof(this.Length)}: {this.Length}{Environment.NewLine}" +
                             $"{nameof(this.Capacity)}: {this.Capacity}{Environment.NewLine}" +
                             $"{nameof(this.Array)}[0] = {this[0]}{Environment.NewLine}" +
                             $"{nameof(this.Array)}[-1] = {this[-1]}{Environment.NewLine}";

            return info;
        }
    }
}
