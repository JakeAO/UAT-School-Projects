namespace SadPumpkin.HashTable.CollisionResolver
{
    public interface ICollisionResolver
    {
        int ResolveHash(int originalHash, int misses = 1);
    }
}