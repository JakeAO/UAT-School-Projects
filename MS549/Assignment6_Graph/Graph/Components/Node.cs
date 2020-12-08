namespace SadPumpkin.Graph.Components
{
    public class Node<T> : INode<T>
    {
        public T Value { get; }

        public Node(T value)
        {
            Value = value;
        }
    }
}