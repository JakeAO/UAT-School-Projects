using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    /// <summary>
    /// Interface which defines the structure of a generic HashTable.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    /// <typeparam name="TValue">Type of value to be used.</typeparam>
    public interface IHashTable<TKey, TValue>
    {
        /// <summary>
        /// Current number of key/value pairs in the HashTable.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Current maximum capacity of the HashTable.
        /// </summary>
        int Capacity { get; }
        
        /// <summary>
        /// Defined maximum load factor of the HashTable.
        /// </summary>
        float LoadFactor { get; }

        /// <summary>
        /// Generator used to determine hashes of key/value pairs.
        /// </summary>
        IHashCodeGenerator<TKey> HashGenerator { get; }

        /// <summary>
        /// Insert a new element into the HashTable. Duplicates will be ignored.
        /// </summary>
        /// <param name="key">Key defining the value being added.</param>
        /// <param name="value">Value to add to the table.</param>
        void Insert(TKey key, TValue value);

        /// <summary>
        /// Retrieve an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to retrieve.</param>
        /// <returns>Value defined by the provided key, otherwise default.</returns>
        TValue Retrieve(TKey key);
        
        /// <summary>
        /// Retrieve an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to retrieve.</param>
        /// <param name="value">Value defined by the provided key.</param>
        /// <returns>True if corresponding key/value pair is found, otherwise false.</returns>
        bool TryRetrieve(TKey key, out TValue value);

        /// <summary>
        /// Remove an existing element from the HashTable.
        /// </summary>
        /// <param name="key">Key defining the value to remove.</param>
        /// <returns>True if corresponding key/value pair was removed, otherwise false.</returns>
        bool Remove(TKey key);
        
        /// <summary>
        /// Remove all elements from the HashTable.
        /// </summary>
        void Clear();
    }
}