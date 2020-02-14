using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms_DataStruct_Lib
{
    public class ArrayStack<T> : IEnumerable<T>
    {
        private T[] items;

        public ArrayStack()
        {
            const int defaultCapacity = 4;
            items = new T[defaultCapacity];
        }

        public ArrayStack(int capacity)
        {
            items = new T[capacity];
        }

        public T Peek() 
        {
            if (IsEmpty) {
                throw new InvalidOperationException();
            }

            return items[Count - 1];
        }

        public void Pop()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            items[--Count] = default(T);
        }

        public void Push(T item)
        {
            if(items.Length == Count)
            {
                T[] largerArray = new T[Count * 2];
                Array.Copy(items, largerArray, Count);

                items = largerArray;
            }

            items[Count++] = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty => Count == 0;
        public int Count { get; private set; }

    }
}
