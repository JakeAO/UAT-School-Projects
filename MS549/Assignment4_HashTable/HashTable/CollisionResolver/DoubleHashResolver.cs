using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.CollisionResolver
{
    /// <summary>
    /// Implementation of ICollisionResolver which uses Double Hashing resolution.
    /// </summary>
    public class DoubleHashResolver : ICollisionResolver
    {
        /// <summary>
        /// Generator which is used to calculate the secondary hash.
        /// </summary>
        public IHashCodeGenerator<int> SecondaryGenerator;

        /// <summary>
        /// Constructs a new DoubleHashResolver with the provided hash generator.
        /// </summary>
        /// <param name="secondaryGenerator">Secondary hash generator to use when resolving.</param>
        public DoubleHashResolver(IHashCodeGenerator<int> secondaryGenerator)
        {
            SecondaryGenerator = secondaryGenerator;
        }

        /// <summary>
        /// Returns a new HashCode double-hashed a number of times based on the number of misses.
        /// </summary>
        /// <param name="originalHash">Initial HashCode of the colliding key.</param>
        /// <param name="misses">Number of collisions since the initial HashCode.</param>
        /// <returns>Resolved HashCode</returns>
        public int ResolveHash(int originalHash, int misses = 1)
        {
            int newHash = originalHash;
            for (int i = 0; i < misses; i++)
            {
                newHash += SecondaryGenerator.GetHashCode(newHash);
            }
            return newHash;
        }
    }
}