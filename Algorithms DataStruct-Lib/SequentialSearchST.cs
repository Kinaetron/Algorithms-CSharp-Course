using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class SequentialSearchST<Tkey, TValue>
    {
        private class Node
        {
            public Tkey Key { get; }

            public TValue Value { get; set; }

            public Node Next { get; set; }

            public Node(Tkey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        private Node first;
        private readonly IEqualityComparer<Tkey> comparer;

        public int Count { get; private set; }

        public SequentialSearchST()
        {
            comparer = EqualityComparer<Tkey>.Default;
        }


        public SequentialSearchST(IEqualityComparer<Tkey> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException();
        }

        public bool TryGet(Tkey key, out TValue val)
        {
            for (Node x = first; x != null; x = x.Next)
            {
                if(comparer.Equals(x:key, y:x.Key))
                {
                    val = x.Value;
                    return true;
                }
            }
            val = default(TValue);
            return false;
        }

        public void Add(Tkey key, TValue val)
        {
            if(key == null) {
                throw new ArgumentNullException(paramName: "Key can't be null");
            }

            for (Node x = first; x != null; x = x.Next)
            {
                if (comparer.Equals(x: key, y: x.Key))
                {
                    x.Value = val;
                    return;
                }
            }

            first = new Node(key, val, first);
            Count++;
        }

        public bool Contains(Tkey key)
        {
            for (Node x = first; x != null; x = x.Next)
            {
                if (comparer.Equals(x: key, y: x.Key)) {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<Tkey> Keys()
        {
            for (Node x = first; x != null; x = x.Next) {
                yield return x.Key;
            }
        }

        public bool Remove(Tkey key)
        {
            if(key == null) {
                throw new ArgumentNullException(paramName: "Key can't be null");
            }

            if(Count == 1 && comparer.Equals(x: first.Key, y: key))
            {
                first = null;
                Count--;
                return true;
            }

            Node prev = null;
            Node current = first;

            while (current != null)
            {
                prev = current;
                current = current.Next;

                if (comparer.Equals(x: current.Key, y: key))
                {
                    if (prev == null)
                    {
                        first = current.Next;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }

                    Count--;
                    return true;
                }

                prev = current;
                current = current.Next;
            }

            return false;
        }
    }
}
