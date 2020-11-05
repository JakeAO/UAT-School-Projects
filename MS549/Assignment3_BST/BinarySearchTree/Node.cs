using System;

namespace BinarySearchTree
{
    public class Node<T> : INode<T> where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Readonly value of the BST node.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Reference to the left (lesser) child node.
        /// </summary>
        public INode<T> LeftChild { get; set; }

        /// <summary>
        /// Reference to the right (greater) child node.
        /// </summary>
        public INode<T> RightChild { get; set; }

        /// <summary>
        /// Construct a new node with the provided value.
        /// </summary>
        /// <param name="value">Value the node will contain.</param>
        public Node(T value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }
    }
}