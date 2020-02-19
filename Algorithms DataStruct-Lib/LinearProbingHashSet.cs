using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class LinearProbingHashSet<TKey, TValue>
    {
        private const int defaultCapacity = 4;
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        private TKey[] keys;
        private TValue[] values;

        public LinearProbingHashSet():this(defaultCapacity)
        {

        }

        public LinearProbingHashSet(int capacity)
        {
            Capacity = capacity;
            keys = new TKey[capacity];
            values = new TValue[capacity];
        }

        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % Capacity;
        }

        public bool Contains(TKey key)
        {
            if(key == null) {
                throw new ArgumentNullException("Key is not allowed to be null");
            }

            for (int i = Hash(key); keys[i] != null; i = (i+1)%Capacity)
            {
                if(keys[i].Equals(key)) {
                    return true;
                }
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            if(key == null) {
                throw new ArgumentNullException("Key isn's allowed to be null");
            }

            for (int i = Hash(key); keys[i] != null; i = (i + 1) % Capacity)
            {
                if(keys.Equals(key)) {
                    return values[i];
                }
            }

            throw new ArgumentException("Key was not found");
        }

        public bool TryGet(TKey key, out int index)
        {
            if (key == null) {
                throw new ArgumentNullException("Key isn's allowed to be null");
            }

            for (int i = Hash(key); keys[i] != null; i = (i + 1) % Capacity)
            {
                if (keys.Equals(key)) {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        public void Remove(TKey key)
        {
            if (key == null) {
                throw new ArgumentNullException("Key isn's allowed to be null");
            }

            if(!TryGet(key, out int index)) {
                return;
            }

            keys[index] = default(TKey);
            values[index] = default(TValue);

            index = (index + 1) % Capacity;

            while (keys[index] != null)
            {
                TKey keyToRehash = keys[index];
                TValue valToRehash = values[index];

                keys[index] = default(TKey);
                values[index] = default(TValue);

                Count--;

                Add(keyToRehash, valToRehash);

                index = (index + 1) % Capacity;
            }

            Count--;

            if(Count > 0 && Count <= Capacity / 8) {
                Resize(Capacity / 2);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) {
                throw new ArgumentNullException("Key isn's allowed to be null");
            }

            if(value == null)
            {
                Remove(key);
                return;
            }

            if(Count >= Capacity / 2) {
                Resize(2 * Capacity);
            }

            int i;
            for (i = Hash(key); keys[i] != null; i = (i + 1) % Capacity)
            {
                if(keys.Equals(key))
                {
                    values[i] = value;
                    return;
                }
            }

            keys[i] = key;
            values[i] = value;
        }

        private void Resize(int capacity)
        {
            var temp = new LinearProbingHashSet<TKey, TValue>(capacity);

            for (int i = 0; i < Capacity; i++)
            {
                if(keys[i] != null) {
                    temp.Add(keys[i], values[i]);
                }
            }

            keys = temp.keys;
            values = temp.values;

            Capacity = temp.Capacity;
        }

        public IEnumerable<TKey> Keys()
        {
            var q = new Queue<TKey>();
            for (int i = 0; i < Capacity; i++)
            {
                if(keys[i] != null) {
                    q.Enqueue(keys[i]);
                }
            }

            return q;
        }
    }
}
