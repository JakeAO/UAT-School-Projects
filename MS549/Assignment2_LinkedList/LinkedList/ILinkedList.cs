namespace SadPumpkin.LinkedList
{
    public interface ILinkedList<T>
    {
        int Count { get; }
        INode<T> First { get; }
        INode<T> Last { get; }

        INode<T> Find(T value);
        INode<T> Insert(T value);
        void Remove(T value);
        void Remove(INode<T> node);
        string Print();
    }
}