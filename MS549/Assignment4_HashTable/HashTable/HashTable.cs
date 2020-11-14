using System;
using SadPumpkin.HashTable.CollisionResolver;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    /// <summary>
    /// Implementation of IHashTable which uses Open Addressing to resolve collisions.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    /// <typeparam name="TValue">Type of value to be used.</typeparam>
    public class HashTable<TKey, TValue> : IHashTable<TKey, TValue>
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
        /// Generator used to determine hashes of key/value pairs.
        /// </summary>
        public IHashCodeGenerator<TKey> HashGenerator { get; }
        
        /// <summary>
        /// Generator used to resolve hash conflicts in the table.
        /// </summary>
        public ICollisionResolver CollisionResolver { get; }

        /// <summary>
        /// Array which stores the key/value pairs in the HashTable.
        /// </summary>
        private (TKey key, TValue value)[] _table;

        /// <summary>
        /// Create a new, empty HashTable with default values.
        /// </summary>
        public HashTable()
            : this(0, 0.6f)
        {
        }

        /// <summary>
        /// Create a new HashTable with an initial size and load factor, but default generators.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the HashTable.</param>
        /// <param name="loadFactor">Fixed load factor of the HashTable.</param>
        public HashTable(int initialCapacity, float loadFactor)
            : this(initialCapacity, loadFactor,
                new StandardHashGenerator<TKey>(),
                new QuadraticProbingResolver())
        {
        }

        /// <summary>
        /// Create a new HashTable with the provided initial properties.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the HashTable.</param>
        /// <param name="loadFactor">Fixed load factor of the HashTable.</param>
        /// <param name="hashGenerator">Generator used to calculate key hashes.</param>
        /// <param name="collisionResolver">Generator used to resolve collisions in key hashes.</param>
        public HashTable(
            int initialCapacity,
            float loadFactor,
            IHashCodeGenerator<TKey> hashGenerator,
            ICollisionResolver collisionResolver)
        {
            Count = 0;
            LoadFactor = loadFactor;
            HashGenerator = hashGenerator;
            CollisionResolver = collisionResolver;

            _table = new (TKey, TValue)[initialCapacity];
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

            // Check load and increase array size if necessary
            if (Count > (int) (LoadFactor * _table.Length))
            {
                IncreaseTableSize();
            }

            int originalHash = HashGenerator.GetHashCode(key);

            int misses = 0;
            int hashCode = originalHash;
            int tableIndex = GetTableIndex(hashCode);
            while (true)
            {
                // We've found a matching entry
                if (Equals(_table[tableIndex].key, key) &&
                    !Equals(_table[tableIndex].value, default(TValue)))
                {
                    return;
                }

                // We've hit an empty spot
                if (Equals(_table[tableIndex].key, default(TKey)) &&
                    Equals(_table[tableIndex].value, default(TValue)))
                {
                    Count++;
                    _table[tableIndex] = (key, value);
                    return;
                }

                // Increment misses and get new hashCode
                misses++;
                hashCode = CollisionResolver.ResolveHash(originalHash, misses);
                tableIndex = GetTableIndex(hashCode);
            }
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

            int originalHash = HashGenerator.GetHashCode(key);

            int misses = 0;
            int hashCode = originalHash;
            int tableIndex = GetTableIndex(hashCode);
            while (true)
            {
                // We've found our entry
                if (Equals(_table[tableIndex].key, key))
                {
                    // But it's empty
                    if (Equals(_table[tableIndex].value, default(TValue)))
                    {
                        return false;
                    }

                    value = _table[tableIndex].value;
                    return true;
                }

                // We've hit an empty spot
                if (Equals(_table[tableIndex].key, default(TKey)) &&
                    Equals(_table[tableIndex].value, default(TValue)))
                {
                    return false;
                }

                // Increment misses and get new hashCode
                misses++;
                hashCode = CollisionResolver.ResolveHash(originalHash, misses);
                tableIndex = GetTableIndex(hashCode);
            }
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

            int originalHash = HashGenerator.GetHashCode(key);

            int misses = 0;
            int hashCode = originalHash;
            int tableIndex = GetTableIndex(hashCode);
            while (true)
            {
                // We've found our entry
                if (Equals(_table[tableIndex].key, key) &&
                    !Equals(_table[tableIndex].value, default(TValue)))
                {
                    // Remove current entry
                    Count--;
                    _table[tableIndex] = default;

                    // Pull forward descended key/value pairs
                    // Start with the next index as if we had missed
                    misses++;
                    hashCode = CollisionResolver.ResolveHash(originalHash, misses);
                    int futureIndex = GetTableIndex(hashCode);
                    while (true)
                    {
                        // We've hit an empty spot, stop pull forward
                        if (Equals(_table[futureIndex].key, default(TKey)) &&
                            Equals(_table[futureIndex].value, default(TValue)))
                        {
                            break;
                        }

                        // We've hit a different key, stop pull forward
                        if (!Equals(_table[futureIndex].key, key))
                        {
                            break;
                        }
                        
                        // Replace current with future
                        _table[tableIndex] = _table[futureIndex];
                        _table[futureIndex] = default;
                        tableIndex = futureIndex;

                        // Increment misses and get new hashCode
                        misses++;
                        hashCode = CollisionResolver.ResolveHash(originalHash, misses);
                        futureIndex = GetTableIndex(hashCode);
                    }

                    return true;
                }

                // We've hit an empty spot
                if (Equals(_table[tableIndex].key, default(TKey)) &&
                    Equals(_table[tableIndex].value, default(TValue)))
                {
                    return false;
                }

                // Increment misses and get new hashCode
                misses++;
                hashCode = CollisionResolver.ResolveHash(originalHash, misses);
                tableIndex = GetTableIndex(hashCode);
            }
        }

        /// <summary>
        /// Remove all elements from the HashTable.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = default;
            }
        }

        /// <summary>
        /// Convert HashCode into bounded array index.
        /// </summary>
        /// <param name="hashCode">HashCode of the key to be inserted.</param>
        /// <returns>Bounded index into table array.</returns>
        private int GetTableIndex(int hashCode)
        {
            return Math.Abs(hashCode % _table.Length);
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
            _table = new (TKey, TValue)[newCapacity];
            for (int i = 0; i < oldTable.Length; i++)
            {
                if (!Equals(oldTable[i].key, default(TKey)) ||
                    !Equals(oldTable[i].value, default(TValue)))
                {
                    Insert(oldTable[i].key, oldTable[i].value);
                }
            }
        }
    }
}