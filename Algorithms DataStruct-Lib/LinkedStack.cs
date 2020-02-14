using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class LinkedStack<T> : IEnumerable<T>
    {
        private readonly SingleLinkedList<T> list = new SingleLinkedList<T>();


        public T Peek()
        {
            if (IsEmpty) {
                throw new InvalidOperationException();
            }

            return list.Head.Value;

        }

        public void Pop()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            list.RemoveFirst();
        }

        public void Push(T item)
        {
            list.AddFirst(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty => Count == 0;
        public int Count => list.Count;
    }
}
