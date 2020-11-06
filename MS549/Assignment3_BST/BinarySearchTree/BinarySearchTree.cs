using System;
using System.Text;

namespace SadPumpkin.BST
{
    /// <summary>
    /// BST implementation which provides simple operations on a sorted tree.
    /// </summary>
    /// <typeparam name="T">Generic type of the values contained in the tree.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>, IEquatable<T>
    {
        private INode<T> _root = null;
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
        }

        /// <summary>
        /// Construct a new BST with predefined values.
        /// </summary>
        /// <param name="initialValues">Values to add to the BST upon construction.</param>
        public BinarySearchTree(params T[] initialValues)
        {
            foreach (T value in initialValues)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Add a new node to the BST with the given value.
        /// </summary>
        /// <param name="value">Value to insert into the BST.</param>
        /// <returns>The newly created node, or null if the provided value already exists in the BST.</returns>
        public INode<T> Add(T value)
        {
            if (_root == null)
            {
                _root = new Node<T>(value);
                _count = 1;
                _depth = 1;
                return _root;
            }

            INode<T> newNode = null;
            INode<T> parent = _root;
            do
            {
                // No duplicates allowed in the BST
                if (value.Equals(parent.Value))
                    break;

                if (value.CompareTo(parent.Value) > 0)
                {
                    // Greater than
                    if (parent.RightChild == null)
                    {
                        newNode = parent.RightChild = new Node<T>(value);
                    }
                    else
                    {
                        parent = parent.RightChild;
                    }
                }
                else
                {
                    // Less than
                    if (parent.LeftChild == null)
                    {
                        newNode = parent.LeftChild = new Node<T>(value);
                    }
                    else
                    {
                        parent = parent.LeftChild;
                    }
                }
            } while (newNode == null);

            if (newNode != null)
            {
                _count++;
                _depth = NodeUtilities.GetDepth(_root);
            }

            return newNode;
        }

        /// <summary>
        /// Find and remove the node that corresponds to the provided value.
        /// </summary>
        /// <param name="value">Value to remove from the BST.</param>
        public void Remove(T value)
        {
            INode<T> InnerRemove(INode<T> node, T innerValue)
            {
                if (node == null)
                    return null;

                if (node.Value.Equals(innerValue))
                {
                    if (node.LeftChild == null)
                        return node.RightChild;
                    if (node.RightChild == null)
                        return node.LeftChild;

                    INode<T> minGreater = NodeUtilities.GetMinimum(node.RightChild);
                    Remove(minGreater);

                    minGreater.LeftChild = node.LeftChild;
                    minGreater.RightChild = node.RightChild;
                    return minGreater;
                }
                else if (node.Value.CompareTo(innerValue) < 0)
                {
                    node.RightChild = InnerRemove(node.RightChild, innerValue);
                }
                else
                {
                    node.LeftChild = InnerRemove(node.LeftChild, innerValue);
                }

                return node;
            }

            _root = InnerRemove(_root, value);
            _count = NodeUtilities.GetCount(_root);
            _depth = NodeUtilities.GetDepth(_root);
        }

        /// <summary>
        /// Remove the provided node from the BST.
        /// </summary>
        /// <param name="value">Node to remove from the BST.</param>
        public void Remove(INode<T> value)
        {
            INode<T> InnerRemove(INode<T> node, INode<T> innerValue)
            {
                if (node == null)
                    return null;

                if (node == innerValue)
                {
                    if (node.LeftChild == null)
                        return node.RightChild;
                    if (node.RightChild == null)
                        return node.LeftChild;

                    INode<T> minGreater = NodeUtilities.GetMinimum(node.RightChild);
                    Remove(minGreater);

                    minGreater.LeftChild = node.LeftChild;
                    minGreater.RightChild = node.RightChild;
                    return minGreater;
                }
                else if (node.Value.CompareTo(innerValue.Value) < 0)
                {
                    node.RightChild = InnerRemove(node.RightChild, innerValue);
                }
                else
                {
                    node.LeftChild = InnerRemove(node.LeftChild, innerValue);
                }

                return node;
            }

            _root = InnerRemove(_root, value);
            _count = NodeUtilities.GetCount(_root);
            _depth = NodeUtilities.GetDepth(_root);
        }

        /// <summary>
        /// Return the node corresponding to the minimum value in the BST.
        /// </summary>
        /// <returns>Node with the current minimum value.</returns>
        public INode<T> Maximum()
        {
            return NodeUtilities.GetMaximum(_root);
        }

        /// <summary>
        /// Return the node corresponding to the maximum value in the BST.
        /// </summary>
        /// <returns>Node with the current maximum value.</returns>
        public INode<T> Minimum()
        {
            return NodeUtilities.GetMinimum(_root);
        }

        /// <summary>
        /// Traverse the BST in order and for each node invoke the provided action.
        /// </summary>
        /// <param name="action">Action to invoke on each BST node.</param>
        public void Traverse(Action<T> action)
        {
            void InnerTraverse(INode<T> node, Action<T> innerAction)
            {
                if (node == null)
                    return;
                // In-order traversal (left->me->right)
                InnerTraverse(node.LeftChild, innerAction);
                innerAction(node.Value);
                InnerTraverse(node.RightChild, innerAction);
            }

            InnerTraverse(_root, action);
        }

        /// <summary>
        /// Return the node corresponding to the provided value from the BST.
        /// </summary>
        /// <param name="value">Value to find in the BST.</param>
        /// <returns>Node corresponding to the provided value.</returns>
        public INode<T> Find(T value)
        {
            INode<T> InnerFind(INode<T> node, T innerValue)
            {
                if (node == null)
                    return null;
                if (node.Value.Equals(innerValue))
                    return node;
                return InnerFind(
                    node.Value.CompareTo(innerValue) > 0
                        ? node.LeftChild
                        : node.RightChild,
                    innerValue);
            }

            return InnerFind(_root, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            // yuck closure
            StringBuilder sb = new StringBuilder();
            void OnNodeTraversed(T value)
            {
                if (sb.Length != 0)
                    sb.Append(" -> ");
                sb.Append('(').Append(value).Append(')');
            }

            Traverse(OnNodeTraversed);
            return sb.ToString();
        }
    }
}