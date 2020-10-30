namespace SadPumpkin.LinkedList
{
    /// <summary>
    /// Interface for a bare-bones LinkedList implementation which
    /// supports Insert, Remove, and Find operations as well as a
    /// debug Print method.
    /// </summary>
    /// <example>
    /// <code>
    /// ILinkedList<int> newList = new LinkedList<int>();
    /// newList.Insert(5);
    /// newList.Insert(7);
    /// </code>
    /// </example>
    /// <typeparam name="T">Value type which list nodes will contain.</typeparam>
    public interface ILinkedList<T>
    {
        /// <summary>
        /// The number of nodes in the list.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// The first node in the list.
        /// </summary>
        INode<T> First { get; }

        /// <summary>
        /// The last node in the list.
        /// </summary>
        INode<T> Last { get; }

        /// <summary>
        /// Return the first node which contains the provided value.
        /// </summary>
        /// <example>
        /// <code>
        /// ILinkedList<int> newList = new LinkedList<int>();
        /// newList.Insert(5);
        /// newList.Insert(7);
        /// INode<int> foundNode = newList.Find(7);
        /// </code>
        /// </example>
        /// <param name="value">Value to locate</param>
        /// <returns>First node which matches value</returns>
        INode<T> Find(T value);

        /// <summary>
        /// Add a new node to the end of the list.
        /// </summary>
        /// <example>
        /// <code>
        /// ILinkedList<int> newList = new LinkedList<int>();
        /// newList.Insert(5);
        /// newList.Insert(7);
        /// </code>
        /// </example>
        /// <param name="value">Value to add</param>
        /// <returns>New node at the end of the list</returns>
        INode<T> Insert(T value);

        /// <summary>
        /// Remove the first node that matches the parameter from the list.
        /// </summary>
        /// <example>
        /// <code>
        /// ILinkedList<int> newList = new LinkedList<int>();
        /// newList.Insert(5);
        /// newList.Insert(7);
        /// newList.Remove(7);
        /// </code>
        /// </example>
        /// <param name="value">Value to remove</param>
        void Remove(T value);

        /// <summary>
        /// Remove the provided node from the list.
        /// </summary>
        /// <example>
        /// <code>
        /// ILinkedList<int> newList = new LinkedList<int>();
        /// INode<int> firstNode = newList.Insert(5);
        /// INode<int> secondNode = newList.Insert(7);
        /// newList.Remove(secondNode);
        /// </code>
        /// </example>
        /// <param name="node">Node to remove</param>
        void Remove(INode<T> node);

        /// <summary>
        /// Return a string which contains all current nodes of the list.
        /// </summary>
        /// <example>
        /// <code>
        /// ILinkedList<int> newList = new LinkedList<int>();
        /// newList.Insert(5);
        /// newList.Insert(7);
        /// Console.WriteLine(newList.Print());
        /// </code>
        /// </example>
        /// <returns>Debug string with list contents</returns>
        string Print();
    }
}