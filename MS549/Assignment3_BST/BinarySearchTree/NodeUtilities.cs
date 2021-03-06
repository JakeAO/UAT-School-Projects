using System;

namespace SadPumpkin.BST
{
    /// <summary>
    /// Utility methods for acting on BST node objects.
    /// </summary>
    public static class NodeUtilities
    {
        /// <summary>
        /// Return the total number of nodes in the subtree rooted in the provided node.
        /// </summary>
        /// <remarks>Recursive</remarks>
        /// <param name="node">Root node which denotes a subtree.</param>
        /// <returns>Count of nodes in the subtree.</returns>
        public static int GetCount<T>(INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            if (node == null)
                return 0;

            int leftCount = GetCount(node.LeftChild);
            int rightCount = GetCount(node.RightChild);

            return 1 + leftCount + rightCount;
        }

        /// <summary>
        /// Return the maximum depth of the subtree rooted in the provided node.
        /// </summary>
        /// <remarks>Recursive</remarks>
        /// <param name="node">Root node which denotes a subtree.</param>
        /// <returns>Maximum depth of the subtree.</returns>
        public static int GetDepth<T>(INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            if (node == null)
                return 0;

            int leftDepth = GetDepth(node.LeftChild);
            int rightDepth = GetDepth(node.RightChild);

            return 1 + Math.Max(leftDepth, rightDepth);
        }

        /// <summary>
        /// Rotate the given node to the right.
        /// </summary>
        /// <param name="node">Node at the center of the rotation.</param>
        /// <returns>True if the rotation modified the subtree, otherwise false.</returns>
        public static bool RotateRight<T>(ref INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            if (node?.LeftChild == null)
                return false;

            var newRoot = node.LeftChild;
            var abandonedSubtree = newRoot.RightChild;

            newRoot.RightChild = node;
            node.LeftChild = abandonedSubtree;

            node = newRoot;

            return true;
        }

        /// <summary>
        /// Rotate the given node to the left.
        /// </summary>
        /// <param name="node">Node at the center of the rotation.</param>
        /// <returns>True if the rotation modified the subtree, otherwise false.</returns>
        public static bool RotateLeft<T>(ref INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            if (node?.RightChild == null)
                return false;

            var newRoot = node.RightChild;
            var abandonedSubtree = newRoot.LeftChild;

            newRoot.LeftChild = node;
            node.RightChild = abandonedSubtree;

            node = newRoot;

            return true;
        }

        /// <summary>
        /// Return the node corresponding to the minimum value in a BST.
        /// </summary>
        /// <param name="node">Starting node of search.</param>
        /// <returns>Node containing the minimum value.</returns>
        public static INode<T> GetMinimum<T>(INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            INode<T> scanner = node;
            while (scanner.LeftChild != null)
            {
                scanner = scanner.LeftChild;
            }

            return scanner;
        }
        
        /// <summary>
        /// Return the node corresponding to the maximum value in a BST.
        /// </summary>
        /// <param name="node">Starting node of search.</param>
        /// <returns>Node containing the maximum value.</returns>
        public static INode<T> GetMaximum<T>(INode<T> node) where T : IComparable<T>, IEquatable<T>
        {
            INode<T> scanner = node;
            while (scanner.RightChild != null)
            {
                scanner = scanner.RightChild;
            }

            return scanner;
        }
    }
}