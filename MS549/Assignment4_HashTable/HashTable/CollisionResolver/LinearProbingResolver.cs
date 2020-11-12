namespace SadPumpkin.HashTable.CollisionResolver
{
    public class LinearProbingResolver : ICollisionResolver
    {
        public int ResolveHash(int originalHash, int misses = 1)
        {
            return originalHash + misses;
        }
    }
}