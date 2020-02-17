using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class CircularQueue<T> : IEnumerable<T>
    {
        private T[] queue;

        private int head;
        private int tail;

        public int Count => head <= tail ? tail - head : tail - head + queue.Length;

        public int Capacity => queue.Length;

        public bool IsEmpty => Count == 0;

        public CircularQueue()
        {
            const int defaultCapacity = 4;
            queue = new T[defaultCapacity];
        }

        public CircularQueue(int capacity)
        {
            queue = new T[capacity];
        }

        public void Enqueue(T item)
        {
            if(Count == queue.Length - 1)
            {
                int countPriorResize = Count;
                T[] newArray = new T[queue.Length * 2];

                Array.Copy(queue, head, newArray, 0, queue.Length - head);
                Array.Copy(queue, 0, newArray, queue.Length - head, tail);

                queue = newArray;

                head = 0;
                tail = countPriorResize;
            }

            queue[tail] = item;
            if(tail < queue.Length - 1) {
                tail++;
            }
            else {
                tail = 0;
            }
        }

        public void Dequeue()
        {
            if (IsEmpty) {
                throw new InvalidOperationException();
            }

            queue[head++] = default(T);

            if(IsEmpty) {
                head = tail = 0;
            }
            else if(head == queue.Length) {
                head = 0;
            }
        }


        public T Peek()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            return queue[head];
        }


        public IEnumerator<T> GetEnumerator()
        {
            if(head <= tail)
            {
                for (int i = head; i < tail; i++) {
                    yield return queue[i];
                }
            }
            else
            {
                for (int i = head; i < queue.Length; i++) {
                    yield return queue[i];
                }

                for (int i = 0; i < tail; i++) {
                    yield return queue[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
