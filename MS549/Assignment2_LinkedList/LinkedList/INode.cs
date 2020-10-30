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
    public interface INode<T>
    {
        /// <summary>
        /// Readonly value of the node.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Reference to the previous node in the list.
        /// </summary>
        INode<T> Previous { get; set; }

        /// <summary>
        /// Reference to the next node in the list.
        /// </summary>
        INode<T> Next { get; set; }
    }
}