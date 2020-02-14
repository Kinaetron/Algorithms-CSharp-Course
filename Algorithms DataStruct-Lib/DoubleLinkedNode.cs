using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class DoubleLinkedNode<T>
    {
        public DoubleLinkedNode<T> Next { get; internal set; }
        public DoubleLinkedNode<T> Previous { get; internal set; }

        public T Value { get; set; }

        public DoubleLinkedNode(T value)
        {
            Value = value;
        }
    }
}
