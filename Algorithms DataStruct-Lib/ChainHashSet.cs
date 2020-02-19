using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class ChainHashSet<TKey, TValue>
    {
        private SequentialSearchST<TKey, TValue> []chains;

        private const int defaultCapacity = 4;
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public ChainHashSet():this(Prime.MinPrime)
        {

        }

        public ChainHashSet(int capacity)
        {
            Capacity = capacity;
            chains = new SequentialSearchST<TKey, TValue>[capacity];
            for (int i = 0; i < Capacity; i++)
            {
                chains[i] = new SequentialSearchST<TKey, TValue>();
            }
        }

        private int Hash(TKey key) {
            return (key.GetHashCode() & 0x7fffffff) % Capacity;
        }

        public TValue Get(TKey key)
        {
            if(key == null) {
                throw new ArgumentNullException("Key is not allowed to be null");
            }

            int index = Hash(key);
            if(chains[index].TryGet(key, out TValue val)) {
                return val;
            }

            throw new ArgumentException("Key was not found.");
        }

        public bool Cotains(TKey key)
        {
            if(key == null) {
                throw new ArgumentNullException("key is not allowed to be null");
            }

            int index = Hash(key);
            return chains[index].TryGet(key, out TValue _);
        }

        public bool Remove(TKey key)
        {
            if (key == null) {
                throw new ArgumentNullException("key is not allowed to be null");
            }

            int index = Hash(key);
            if(chains[index].Contains(key))
            {
                Count--;
                chains[index].Remove(key);

                if(Capacity > defaultCapacity && Count <= 2*Capacity) {
                    Resize(Prime.ReducePrime(Capacity));
                }

                return true;
            }

            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) {
                throw new ArgumentNullException("key is not allowed to be null");
            }

            if(value == null)
            {
                Remove(key);
                return;
            }

            if (Count >= 10 * Capacity) {
                Resize(Prime.ExpandPrime(Capacity));
            }

            int i = Hash(key);
            if(!chains[i].Contains(key)) {
                Count++;
            }

            chains[i].Add(key, value);
        }

        private void Resize(int chains)
        {
            var temp = new ChainHashSet<TKey, TValue>(chains);
            for (int i = 0; i < Capacity; i++)
            {
                foreach (TKey key in this.chains[i].Keys())
                {
                    if(this.chains[i].TryGet(key, out TValue val))
                    {
                        temp.Add(key, val);
                    }
                }
            }

            Capacity = temp.Capacity;
            Count = temp.Count;
            this.chains = temp.chains;
        }

        public IEnumerable<TKey> Keys()
        {
            var queue = new LinkedQueue<TKey>();

            for (int i = 0; i < Capacity; i++)
            {
                foreach (TKey key in chains[i].Keys()){
                    queue.Enqueue(key);
                }
            }

            return queue;
        }
    }
}
