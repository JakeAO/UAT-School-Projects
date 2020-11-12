using System.Collections.Generic;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    public class ChainHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        public int Count { get; private set; }
        public int Capacity => _table.Length;
        public float LoadFactor { get; }
        public int Load { get; private set; }

        public IHashCodeGenerator<TKey> HashGenerator { get; }

        private LinkedList<(TKey key, TValue value)>[] _table;

        public ChainHashTable()
            : this(0, 0.6f)
        {
        }

        public ChainHashTable(int initialCapacity, float loadFactor)
            : this(initialCapacity, loadFactor,
                new StandardHashGenerator<TKey>())
        {
        }

        public ChainHashTable(
            int initialCapacity,
            float loadFactor,
            IHashCodeGenerator<TKey> hashGenerator)
        {
            Count = 0;
            LoadFactor = loadFactor;
            HashGenerator = hashGenerator;

            _table = new LinkedList<(TKey key, TValue value)>[initialCapacity];
        }

        public void Insert(TKey key, TValue value)
        {
            // Initialize the table if necessary
            if (_table.Length == 0)
            {
                IncreaseTableSize();
            }

            int hashCode = HashGenerator.GetHashCode(key);
            int tableIndex = hashCode % _table.Length;

            // Check load and increase array size if necessary
            if (_table[tableIndex] == null || _table[tableIndex].Count == 0)
            {
                Load++;

                int maxLoad = (int) (Capacity * LoadFactor);
                if (Load > maxLoad)
                {
                    IncreaseTableSize();
                    tableIndex = hashCode % _table.Length;
                }

                // Initialize LinkedList if necessary
                _table[tableIndex] ??= new LinkedList<(TKey key, TValue value)>();
            }

            // Check table for key that already exists
            foreach ((TKey testKey, TValue _) in _table[tableIndex])
            {
                // HashTable already contains the key
                if (Equals(key, testKey))
                    return;
            }

            // Add new key/value pair to table
            Count++;
            _table[tableIndex].AddLast((key, value));
        }

        public TValue Retrieve(TKey key)
        {
            return TryRetrieve(key, out TValue value) ? value : default;
        }

        public bool TryRetrieve(TKey key, out TValue value)
        {
            value = default;

            if (Count == 0)
                return false;

            int hashCode = HashGenerator.GetHashCode(key);
            int tableIndex = hashCode % _table.Length;
            if (_table[tableIndex] != null)
            {
                foreach ((TKey testKey, TValue testValue) in _table[tableIndex])
                {
                    if (Equals(key, testKey))
                    {
                        value = testValue;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            if (Count == 0)
                return false;

            int hashCode = HashGenerator.GetHashCode(key);
            int tableIndex = hashCode % _table.Length;
            if (_table[tableIndex] != null)
            {
                foreach (var entry in _table[tableIndex])
                {
                    if (Equals(key, entry.key))
                    {
                        Count--;
                        _table[tableIndex].Remove(entry);
                        if (_table[tableIndex].Count == 0)
                            Load--;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i]?.Clear();
            }
        }

        private void IncreaseTableSize()
        {
            int newCapacity = Capacity;
            for (int i = 0; i < Constants.PRIME_NUMBERS.Length; i++)
            {
                if (Constants.PRIME_NUMBERS[i] > newCapacity)
                {
                    newCapacity = Constants.PRIME_NUMBERS[i];
                    break;
                }
            }

            var oldTable = _table;

            Count = 0;
            _table = new LinkedList<(TKey, TValue)>[newCapacity];
            for (int i = 0; i < oldTable.Length; i++)
            {
                if (oldTable[i]?.Count > 0)
                {
                    foreach ((TKey key, TValue value) in oldTable[i])
                    {
                        Insert(key, value);
                    }
                }
            }
        }
    }
}