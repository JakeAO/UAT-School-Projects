namespace SadPumpkin.LinkedList
{
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
        /// <param name="value">Value to locate</param>
        /// <returns>First node which matches value</returns>
        INode<T> Find(T value);

        /// <summary>
        /// Add a new node to the end of the list.
        /// </summary>
        /// <param name="value">Value to add</param>
        /// <returns>New node at the end of the list</returns>
        INode<T> Insert(T value);

        /// <summary>
        /// Remove the first node that matches the parameter from the list.
        /// </summary>
        /// <param name="value">Value to remove</param>
        void Remove(T value);

        /// <summary>
        /// Remove the provided node from the list.
        /// </summary>
        /// <param name="node">Node to remove</param>
        void Remove(INode<T> node);

        /// <summary>
        /// Return a string which contains all current nodes of the list.
        /// </summary>
        /// <returns>Debug string with list contents</returns>
        string Print();
    }
}