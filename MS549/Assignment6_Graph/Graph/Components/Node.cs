namespace SadPumpkin.Graph.Components
{
    /// <summary>
    /// Implementation of a generic weighted Graph vertex/node.
    /// </summary>
    /// <typeparam name="TValue">Type contained in this vertex/node.</typeparam>
    public class Node<T> : INode<T>
    {
        /// <summary>
        /// Value of this vertex/node.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Create an immutable Vertex/Node from the provided data.
        /// </summary>
        /// <param name="value">Value of this Vertex/Node.</param>
        public Node(T value)
        {
            Value = value;
        }
    }
}