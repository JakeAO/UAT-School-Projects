using System;
using SadPumpkin.HashTable.CollisionResolver;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    public class HashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        public int Count { get; private set; }
        public int Capacity => _table.Length;
        public float LoadFactor { get; }

        public IHashCodeGenerator<TKey> HashGenerator { get; }
        public ICollisionResolver CollisionResolver { get; }

        private (TKey key, TValue value)[] _table;

        public HashTable()
            : this(0, 0.6f)
        {
        }

        public HashTable(int initialCapacity, float loadFactor)
            : this(initialCapacity, loadFactor,
                new StandardHashGenerator<TKey>(),
                new QuadraticProbingResolver())
        {
        }

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

        public TValue Retrieve(TKey key)
        {
            return TryRetrieve(key, out TValue value) ? value : default;
        }

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

        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = default;
            }
        }

        private int GetTableIndex(int hashCode)
        {
            return Math.Abs(hashCode) % _table.Length;
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