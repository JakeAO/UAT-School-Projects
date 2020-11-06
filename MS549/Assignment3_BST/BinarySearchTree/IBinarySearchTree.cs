using System;

namespace SadPumpkin.BST
{
    /// <summary>
    /// BST interface which provides simple operations on a sorted tree.
    /// </summary>
    /// <typeparam name="T">Generic type of the values contained in the tree.</typeparam>
    public interface IBinarySearchTree<T> where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Add a new node to the BST with the given value.
        /// </summary>
        /// <param name="value">Value to insert into the BST.</param>
        /// <returns>The newly created node, or null if the provided value already exists in the BST.</returns>
        INode<T> Add(T value);
        
        /// <summary>
        /// Find and remove the node that corresponds to the provided value.
        /// </summary>
        /// <param name="value">Value to remove from the BST.</param>
        void Remove(T value);
        
        /// <summary>
        /// Remove the provided node from the BST.
        /// </summary>
        /// <param name="value">Node to remove from the BST.</param>
        void Remove(INode<T> value);
        
        /// <summary>
        /// Return the node corresponding to the minimum value in the BST.
        /// </summary>
        /// <returns>Node with the current minimum value.</returns>
        INode<T> Maximum();
        
        /// <summary>
        /// Return the node corresponding to the maximum value in the BST.
        /// </summary>
        /// <returns>Node with the current maximum value.</returns>
        INode<T> Minimum();
        
        /// <summary>
        /// Traverse the BST in order and for each node invoke the provided action.
        /// </summary>
        /// <param name="action">Action to invoke on each BST node.</param>
        void Traverse(Action<T> action);
        
        /// <summary>
        /// Return the node corresponding to the provided value from the BST.
        /// </summary>
        /// <param name="value">Value to find in the BST.</param>
        /// <returns>Node corresponding to the provided value.</returns>
        INode<T> Find(T value);
    }
}