namespace SadPumpkin.LinkedList
{
    /// <summary>
    /// Interface for a LinkedList node which contains
    /// a generic Value and links to adjacent Nodes.
    /// </summary>
    /// <example>
    /// <code>
    /// INode<int> newNode1 = new Node<int>(6);
    /// INode<int> newNode2 = new Node<int>(7);
    /// newNode1.Next = newNode2;
    /// newNode2.Previous = newNode1;
    /// </code>
    /// </example>
    /// <typeparam name="T">Value type which node will contain.</typeparam>
    public class Node<T> : INode<T>
    {
        /// <summary>
        /// Readonly value of the node.
        /// </summary>
        public T Value { get; }
        
        /// <summary>
        /// Reference to the previous node in the list.
        /// </summary>
        public INode<T> Previous { get; set; }
        
        /// <summary>
        /// Reference to the next node in the list.
        /// </summary>
        public INode<T> Next { get; set; }

        /// <summary>
        /// Construct a new node with the provided value and no links.
        /// </summary>
        public Node(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns a debug string containing the node's contents.
        /// </summary>
        /// <example>
        /// <code>
        /// INode<int> newNode = new Node<int>(6);
        /// Console.WriteLine(newNode.ToString());
        /// </code>
        /// </example>
        /// <returns>Debug string of the node's contents.</returns>
        public override string ToString()
        {
            return $"({Value})";
        }
    }
}