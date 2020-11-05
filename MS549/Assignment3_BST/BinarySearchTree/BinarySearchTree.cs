using System;

namespace BinarySearchTree
{
    /// <summary>
    /// BST implementation which provides simple operations on a sorted tree.
    /// </summary>
    /// <typeparam name="T">Generic type of the values contained in the tree.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>, IEquatable<T>
    {
        private Node<T> _root = null;
        private int _count = 0;
        private int _depth = 0;

        /// <summary>
        /// Return the current root node of the BST.
        /// </summary>
        public INode<T> Root => _root;

        /// <summary>
        /// Count of nodes contained in the BST.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Maximum depth of the BST.
        /// </summary>
        public int Depth => _depth;
        
        /// <summary>
        /// Construct a new empty BST.
        /// </summary>
        public BinarySearchTree()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Construct a new BST with predefined values.
        /// </summary>
        /// <param name="initialValues">Values to add to the BST upon construction.</param>
        public BinarySearchTree(params T[] initialValues)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Balance the BST in order to achieve optimal performance.
        /// </summary>
        public void Balance()
        {
            
        }
        
        /// <summary>
        /// Add a new node to the BST with the given value.
        /// </summary>
        /// <param name="value">Value to insert into the BST.</param>
        /// <returns>The newly created node, or null if the provided value already exists in the BST.</returns>
        public INode<T> Add(T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find and remove the node that corresponds to the provided value.
        /// </summary>
        /// <param name="value">Value to remove from the BST.</param>
        public void Remove(T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the provided node from the BST.
        /// </summary>
        /// <param name="node">Node to remove from the BST.</param>
        public void Remove(INode<T> node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the node corresponding to the minimum value in the BST.
        /// </summary>
        /// <returns>Node with the current minimum value.</returns>
        public INode<T> Maximum()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the node corresponding to the maximum value in the BST.
        /// </summary>
        /// <returns>Node with the current maximum value.</returns>
        public INode<T> Minimum()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Traverse the BST in order and for each node invoke the provided action.
        /// </summary>
        /// <param name="action">Action to invoke on each BST node.</param>
        public void Traverse(Action<T> action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the node corresponding to the provided value from the BST.
        /// </summary>
        /// <param name="value">Value to find in the BST.</param>
        /// <returns>Node corresponding to the provided value.</returns>
        public INode<T> Find(T value)
        {
            throw new NotImplementedException();
        }
    }
}