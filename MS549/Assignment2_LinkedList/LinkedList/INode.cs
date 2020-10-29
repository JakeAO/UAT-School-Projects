namespace SadPumpkin.LinkedList
{
    public interface INode<T>
    {
        T Value { get; }
        INode<T> Previous { get; }
        INode<T> Next { get; }
    }
}