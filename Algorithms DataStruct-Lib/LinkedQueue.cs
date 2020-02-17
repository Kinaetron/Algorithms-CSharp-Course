using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        public int Count => list.Count;

        public bool IsEmpty => Count == 0;

        private readonly SingleLinkedList<T> list = new SingleLinkedList<T>();

        public void Enqueue(T item) {
            list.AddLast(item);
        }

        public void Dequeue() 
        {
            list.RemoveFirst();
        }

        public T Peek()
        {
            if (IsEmpty) {
                throw new InvalidOperationException();
            }
            return list.Head.Value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
