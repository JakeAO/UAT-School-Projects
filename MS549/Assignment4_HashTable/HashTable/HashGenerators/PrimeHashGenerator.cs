namespace SadPumpkin.HashTable.HashGenerators
{
    /// <summary>
    /// Implementation of IHashCodeGenerator which uses a simplified Prime substraction strategy.
    /// </summary>
    /// <typeparam name="TKey">Type of key to be used.</typeparam>
    public class PrimeHashGenerator<TKey> : IHashCodeGenerator<TKey>
    {
        /// <summary>
        /// Prime number used as the seed of the calculation.
        /// </summary>
        public int Seed { get; }

        /// <summary>
        /// Construct a new generator with the provided prime seed.
        /// </summary>
        /// <param name="seed">Prime number to factor into the calculation.</param>
        public PrimeHashGenerator(int seed = 17)
        {
            Seed = seed;
        }

        /// <summary>
        /// Calculates the integer HashCode of a given generic key object.
        /// </summary>
        /// <param name="key">Key to calculate the HashCode of.</param>
        /// <returns>Calculated HashCode of parameter.</returns>
        public int GetHashCode(TKey key)
        {
            return Seed - ((key?.GetHashCode() ?? 0) % Seed);
        }
    }
}