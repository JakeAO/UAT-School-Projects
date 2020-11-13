namespace SadPumpkin.HashTable.CollisionResolver
{
    /// <summary>
    /// Interface which defines the method by which a hash collision is resolved in a HashTable.
    /// </summary>
    public interface ICollisionResolver
    {
        /// <summary>
        /// Converts an original hash and number of misses into a new, resolved HashCode.
        /// </summary>
        /// <param name="originalHash">Initial HashCode of the colliding key.</param>
        /// <param name="misses">Number of collisions since the initial HashCode.</param>
        /// <returns>Resolved HashCode</returns>
        int ResolveHash(int originalHash, int misses = 1);
    }
}