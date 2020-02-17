using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms_DataStruct_Lib
{
    public class ArrayQueue<T> : IEnumerable<T>
    {
        private T[] queue;
        private int head;
        private int tail;

        public ArrayQueue()
        {
            const int defaultCapacity = 4;
            queue = new T[defaultCapacity];
        }

        public ArrayQueue(int capacity)
        {
            queue = new T[capacity];
        }

        public void Enqueue(T item)
        {
            if(queue.Length == tail)
            {
                T[] largerArray = new T[Count * 2];
                Array.Copy(queue, largerArray, Count);
                queue = largerArray;
            }

            queue[tail++] = item;
        }

        public void Dequeue()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            queue[head++] = default(T);

            if(IsEmpty) {
                head = tail = 0;
            }
        }

        public T Peek()
        {
            if (IsEmpty) {
                throw new InvalidOperationException();
            }

            return queue[head];
        }

        public int Count => tail - head;
        public bool IsEmpty => Count == 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = head; i < tail; i++) {
                yield return queue[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
