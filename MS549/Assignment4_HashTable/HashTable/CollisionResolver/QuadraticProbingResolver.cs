using System;

namespace SadPumpkin.HashTable.CollisionResolver
{
    /// <summary>
    /// Implementation of ICollisionResolver which uses Quadratic Probing resolution.
    /// </summary>
    public class QuadraticProbingResolver : ICollisionResolver
    {
        /// <summary>
        /// Returns the initial hash incremented quadratically by the number of misses.
        /// </summary>
        /// <param name="originalHash">Initial HashCode of the colliding key.</param>
        /// <param name="misses">Number of collisions since the initial HashCode.</param>
        /// <returns>Resolved HashCode</returns>
        public int ResolveHash(int originalHash, int misses = 1)
        {
            int newHash = originalHash;
            if (misses > 0)
                newHash += (int) Math.Pow(misses, 2);
            return newHash;
        }
    }
}