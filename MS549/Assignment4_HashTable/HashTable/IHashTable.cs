using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable
{
    public interface IHashTable<TKey, TValue>
    {
        int Count { get; }
        int Capacity { get; }
        float LoadFactor { get; }

        IHashCodeGenerator<TKey> HashGenerator { get; }

        void Insert(TKey key, TValue value);

        TValue Retrieve(TKey key);
        bool TryRetrieve(TKey key, out TValue value);

        bool Remove(TKey key);
        void Clear();
    }
}