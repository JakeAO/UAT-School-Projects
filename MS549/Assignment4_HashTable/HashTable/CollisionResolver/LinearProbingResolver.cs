namespace SadPumpkin.HashTable.CollisionResolver
{
    /// <summary>
    /// Implementation of ICollisionResolver which uses Linear Probing resolution.
    /// </summary>
    public class LinearProbingResolver : ICollisionResolver
    {
        /// <summary>
        /// Returns the initial hash incremented by the number of misses.
        /// </summary>
        /// <param name="originalHash">Initial HashCode of the colliding key.</param>
        /// <param name="misses">Number of collisions since the initial HashCode.</param>
        /// <returns>Resolved HashCode</returns>
        public int ResolveHash(int originalHash, int misses = 1)
        {
            return originalHash + misses;
        }
    }
}