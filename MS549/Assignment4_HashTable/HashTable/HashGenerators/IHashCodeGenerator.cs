namespace SadPumpkin.HashTable.HashGenerators
{
    public interface IHashCodeGenerator<TKey>
    {
        int GetHashCode(TKey key);
    }
}