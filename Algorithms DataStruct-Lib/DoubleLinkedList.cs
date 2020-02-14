using System;

namespace Algorithms_DataStruct_Lib
{
    public class DoubleLinkedList<T>
    {
        public DoubleLinkedNode<T> Head { get; private set; }

        public DoubleLinkedNode<T> Tail { get; private set; }

        public void AddFirst(T value) =>
            AddFirst(new DoubleLinkedNode<T>(value));

        private void AddFirst(DoubleLinkedNode<T> node)
        {
            // save off the head
            DoubleLinkedNode<T> temp = Head;
            // point head to node
            Head = node;

            // insert the rest of the list behind 
            Head.Next = temp;

            if (IsEmpty)
            {
                Tail = Head;
            }
            else
            {
                // before: 1(head) <----------> 5 <-> 7 -> null
                // after: 3(head) <-------> 1 <-> 5 <-> 7

                // update "previous" ref of former head
                temp.Previous = Head;
            }

            Count++;
        }

        public void AddLast(T value) =>
            AddLast(new DoubleLinkedNode<T>(value));

        private void AddLast(DoubleLinkedNode<T> node)
        {
            if(IsEmpty) {
                Head = node;
            }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;

            Count++;
        }

        public void RemoveFirst()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            Head = Head.Next;

            Count--;

            if(IsEmpty) {
                Tail = null;
            }
            else {
                Head.Previous = null;
            }
        }

        public void RemoveLast()
        {
            if(IsEmpty) {
                throw new InvalidOperationException();
            }

            if(Count == 1)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Tail.Previous.Next = null; // null the last node
                Tail = Tail.Previous; // shift the Tail (now it is the former penultimate node)
            }

            Count--;
        }

        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;
    }
}
