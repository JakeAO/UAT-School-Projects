namespace SadPumpkin.HashTable.HashGenerators
{
    /// <summary>
    /// Implementation of IHashCodeGenerator which uses the .NET standard object.GetHashCode() method.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    public class StandardHashGenerator<TKey> : IHashCodeGenerator<TKey>
    {
        /// <summary>
        /// Calculates the integer HashCode of a given generic key object.
        /// </summary>
        /// <param name="key">Key to calculate the HashCode of.</param>
        /// <returns>Calculated HashCode of parameter.</returns>
        public int GetHashCode(TKey key)
        {
            return key?.GetHashCode() ?? 0;
        }
    }
}