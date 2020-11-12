namespace SadPumpkin.HashTable.HashGenerators
{
    public class StandardHashGenerator<TKey> : IHashCodeGenerator<TKey>
    {
        public int GetHashCode(TKey key)
        {
            return key?.GetHashCode() ?? 0;
        }
    }
}