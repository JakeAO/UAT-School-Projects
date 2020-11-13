using System.Collections.Generic;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    /// <summary>
    /// Implementation of IHashTable which uses Chaining to resolve collisions.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    /// <typeparam name="TValue">Type of value to be used.</typeparam>
    public class ChainHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        /// <summary>
        /// Current number of key/value pairs in the HashTable.
        /// </summary>
        public int Count { get; private set; }
        
        /// <summary>
        /// Current maximum capacity of the HashTable.
        /// </summary>
        public int Capacity => _table.Length;
        
        /// <summary>
        /// Defined maximum load factor of the HashTable.
        /// </summary>
        public float LoadFactor { get; }
        
        /// <summary>
        /// Current load of the HashTable.
        /// </summary>
        public int Load { get; private set; }

        /// <summary>
        /// Generator used to determine hashes of key/value pairs.
        /// </summary>
        public IHashCodeGenerator<TKey> HashGenerator { get; }

        /// <summary>
        /// Array which stores the chained key/value pairs in the HashTable.
        /// </summary>
        private LinkedList<(TKey key, TValue value)>[] _table;

        /// <summary>
        /// Create a new, empty HashTable with default values.
        /// </summary>
        public ChainHashTable()
            : this(0, 0.6f)
        {
        }

        /// <summary>
        /// Create a new HashTable with an initial size and load factor, but default generators.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the HashTable.</param>
        /// <param name="loadFactor">Fixed load factor of the HashTable.</param>
        public ChainHashTable(int initialCapacity, float loadFactor)
            : this(initialCapacity, loadFactor,
                new StandardHashGenerator<TKey>())
        {
        }

        /// <summary>
        /// Create a new HashTable with the provided initial properties.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the HashTable.</param>
        /// <param name="loadFactor">Fixed load factor of the HashTable.</param>
        /// <param name="hashGenerator">Generator used to calculate key hashes.</param>
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

        /// <summary>
        /// Insert a new element into the HashTable. Duplicates will be ignored.
        /// </summary>
        /// <param name="key">Key defining the value being added.</param>
        /// <param name="value">Value to add to the table.</param>
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

        /// <summary>
        /// Retrieve an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to retrieve.</param>
        /// <returns>Value defined by the provided key, otherwise default.</returns>
        public TValue Retrieve(TKey key)
        {
            return TryRetrieve(key, out TValue value) ? value : default;
        }

        /// <summary>
        /// Retrieve an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to retrieve.</param>
        /// <param name="value">Value defined by the provided key.</param>
        /// <returns>True if corresponding key/value pair is found, otherwise false.</returns>
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

        /// <summary>
        /// Remove an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to remove.</param>
        /// <returns>True if corresponding key/value pair was removed, otherwise false.</returns>
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

        /// <summary>
        /// Remove all elements from the HashTable.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i]?.Clear();
            }
        }

        /// <summary>
        /// Increase the table array size to the next highest prime number.
        /// </summary>
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