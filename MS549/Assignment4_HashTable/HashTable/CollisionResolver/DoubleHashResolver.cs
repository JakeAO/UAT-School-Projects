using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.CollisionResolver
{
    public class DoubleHashResolver : ICollisionResolver
    {
        public IHashCodeGenerator<int> SecondaryGenerator;

        public DoubleHashResolver(IHashCodeGenerator<int> secondaryGenerator)
        {
            SecondaryGenerator = secondaryGenerator;
        }

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