using System;

namespace BinarySearchTree
{
    public interface INode<T> where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Readonly value of the BST node.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Reference to the left (lesser) child node.
        /// </summary>
        INode<T> LeftChild { get; set; }

        /// <summary>
        /// Reference to the right (greater) child node.
        /// </summary>
        INode<T> RightChild { get; set; }
    }
}