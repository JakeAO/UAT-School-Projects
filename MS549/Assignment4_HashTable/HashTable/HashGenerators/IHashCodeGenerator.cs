namespace SadPumpkin.HashTable.HashGenerators
{
    /// <summary>
    /// Interface which defines the method by which a HashTable's key is converted to an integer HashCode.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    public interface IHashCodeGenerator<TKey>
    {
        /// <summary>
        /// Calculates the integer HashCode of a given generic key object.
        /// </summary>
        /// <param name="key">Key to calculate the HashCode of.</param>
        /// <returns>Calculated HashCode of parameter.</returns>
        int GetHashCode(TKey key);
    }
}