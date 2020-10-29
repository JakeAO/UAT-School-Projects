namespace SadPumpkin.LinkedList
{
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