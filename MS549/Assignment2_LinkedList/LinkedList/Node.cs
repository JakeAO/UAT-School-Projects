namespace SadPumpkin.LinkedList
{
    public class Node<T> : INode<T>
    {
        public T Value { get; }
        public INode<T> Previous { get; set; }
        public INode<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"({Value})";
        }
    }
}