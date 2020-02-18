using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_DataStruct_Lib
{
    public class BinarySearchST<TKey, TValue>
    {
        private TKey[] keys;
        private TValue[] values;

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        private readonly IComparer<TKey> comparer;

        public int Capacity => keys.Length;
        private const int DefaultCapacity = 4;

        public BinarySearchST(int capacity, IComparer<TKey> comparer)
        {
            keys = new TKey[capacity];
            values = new TValue[capacity];
            this.comparer = comparer ?? throw new ArgumentNullException(paramName: "Comparer can't be null.");
        }

        public BinarySearchST(int capacity)
            :this(capacity, Comparer<TKey>.Default)
        {

        }

        public BinarySearchST():this(DefaultCapacity) { }

        public int Rank(TKey key)
        {
            int low = 0;
            int high = Count - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                int cmp = comparer.Compare(x: key, keys[mid]);

                if (cmp < 0) {
                    high = mid - 1;
                }
                else if(cmp > 0) {
                    low = mid + 1;
                }
                else {
                    return mid;
                }

            }

            return low;
        }

        public TValue GetValueOrDefault(TKey key)
        {
            if(IsEmpty) {
                return default(TValue);
            }

            int rank = Rank(key);
            if(rank < Count && comparer.Compare(keys[rank], key) == 0) {
                return values[rank];
            }

            return default(TValue);
        }

        public void Add(TKey key, TValue value)
        {
            if(key == null) {
                throw new ArgumentNullException("Key can't be null");
            }

            int rank = Rank(key);
            if (rank < Count && comparer.Compare(keys[rank], key) == 0)
            {
                values[rank] = value;
                return;
            }

            if(Count == Capacity) {
                Resize(Capacity * 2);
            }

            for (int j = Count; j > rank; j--)
            {
                keys[j] = keys[j - 1];
                values[j] = values[j - 1];
            }

            keys[rank] = key;
            values[rank] = value;

            Count++;
        }

        public void Remove(TKey key)
        {
            if (key == null) {
                throw new ArgumentNullException("Key can't be null");
            }

            if(IsEmpty) {
                return;
            }

            int r = Rank(key);
            if(r == Count || comparer.Compare(keys[r], key) != 0) {
                return;
            }

            for (int j = r; j < Count -1; j++)
            {
                keys[j] = keys[j + 1];
                values[j] = values[j + 1];
            }

            Count--;
            keys[Count] = default(TKey);
            values[Count] = default(TValue);
        }

        public bool Contains(TKey key)
        {
            int rank = Rank(key);
            if(rank < Count && comparer.Compare(keys[rank], key) == 0) {
                return true;
            }

            return false;
        }

        public IEnumerable<TKey> Keys()
        {
            foreach (var key in keys) {
                yield return key;
            }
        }

        private void Resize(int capacity)
        {
            TKey[] keysTmp = new TKey[capacity];
            TValue[] valuesTmp = new TValue[capacity];

            for (int i = 0; i < Count; i++)
            {
                keysTmp[i] = keys[i];
                valuesTmp[i] = values[i];
            }

            values = valuesTmp;
            keys = keysTmp;
        }

        public TKey Min()
        {
            if(IsEmpty) {
                throw new InvalidOperationException("Table is empty.");
            }

            return keys[0];
        }

        public TKey Max()
        {
            if (IsEmpty) {
                throw new InvalidOperationException("Table is empty.");
            }

            return keys[Count - 1];
        }

        public void RemoveMin()
        {
            if (IsEmpty) {
                throw new InvalidOperationException("Table is empty.");
            }

            Remove(Min());
        }

        public void RemoveMax()
        {
            if (IsEmpty) {
                throw new InvalidOperationException("Table is empty.");
            }

            Remove(Max());
        }

        public TKey Select(int index)
        {
            if(index < 0 || index > Count - 1) {
                throw new ArgumentException("Can't select since index is out of range");
            }

            return keys[index];
        }

        public TKey Ceiling(TKey key)
        {
            if (key == null) {
                throw new ArgumentException("Arguement to ceiling() is null");
            }

            int rank = Rank(key);
            if (rank == Count) {
                return default(TKey);
            }
            else {
                return keys[rank];
            }
        }

        public TKey Floor(TKey key) 
        {
            if (key == null) {
                throw new ArgumentException("Arguement to ceiling() is null");
            }

            int rank = Rank(key);
            if(rank < Count && comparer.Compare(keys[rank], key) == 0) {
                return keys[rank];
            }

            if(rank == 0) {
                return default(TKey);
            }
            else {
                return keys[rank - 1];
            }

        }

        public IEnumerable<TKey> Range(TKey left, TKey right)
        {
            var q = new LinkedQueue<TKey>();
            int low = Rank(left);
            int high = Rank(right);

            for (int i = low; i < high; i++)
            {
                q.Enqueue(keys[i]);
            }
            
            if(Contains(right)) {
                q.Enqueue(keys[Rank(right)]);
            }

            return q;
        }
    }
}
