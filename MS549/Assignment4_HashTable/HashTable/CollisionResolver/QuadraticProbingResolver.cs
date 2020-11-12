using System;

namespace SadPumpkin.HashTable.CollisionResolver
{
    public class QuadraticProbingResolver : ICollisionResolver
    {
        public int ResolveHash(int originalHash, int misses = 1)
        {
            int newHash = originalHash;
            if (misses > 0)
                newHash += (int) Math.Pow(misses, 2);
            return newHash;
        }
    }
}