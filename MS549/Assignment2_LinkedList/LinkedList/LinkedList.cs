using System;
using System.Text;

namespace SadPumpkin.LinkedList
{
    /// <summary>
    /// Bare-bones implementation of ILinkedList and the
    /// linked-list data structure.
    /// </summary>
    /// <example>
    /// <code>
    /// ILinkedList<int> newList = new LinkedList<int>();
    /// newList.Insert(5);
    /// newList.Insert(7);
    /// </code>
    /// </example>
    /// <typeparam name="T">Value type which list nodes will contain.</typeparam>
    public class LinkedList<T> : ILinkedList<T>
    {
        /// <summary>
        /// The number of nodes in the list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// The first node in the list.
        /// </summary>
        public INode<T> First { get; private set; }

        /// <summary>
        /// The last node in the list.
        /// </summary>
        public INode<T> Last => First?.Previous;

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
        public INode<T> Find(T value)
        {
            if (First == null)
                return null;

            INode<T> testNode = First;
            do
            {
                if (testNode.Value.Equals(value))
                {
                    return testNode;
                }

                testNode = testNode.Next;
            } while (testNode != First);

            return null;
        }

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
        public INode<T> Insert(T value)
        {
            INode<T> newNode = new Node<T>(value);

            if (First == null)
            {
                // List is empty
                First = newNode;
                First.Next = newNode;
                First.Previous = newNode;
                
                Count = 1;
            }
            else
            {
                // Insert after current last
                Last.Next = newNode;
                newNode.Previous = Last;

                First.Previous = newNode;
                newNode.Next = First;
                
                Count++;
            }

            return newNode;
        }

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
        public void Remove(T value)
        {
            if (First == null)
                return;

            INode<T> testNode = First;
            do
            {
                if (testNode.Value.Equals(value))
                {
                    // Stitch together the nodes on either side of the removed node
                    testNode.Previous.Next = testNode.Next;
                    testNode.Next.Previous = testNode.Previous;

                    if (testNode == First)
                    {
                        First = testNode.Next;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
            } while (testNode != First);
        }

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
        public void Remove(INode<T> node)
        {
            if (First == null)
                return;

            INode<T> testNode = First;
            do
            {
                if (testNode == node)
                {
                    // Stitch together the nodes on either side of the removed node
                    testNode.Previous.Next = testNode.Next;
                    testNode.Next.Previous = testNode.Previous;

                    if (testNode == First)
                    {
                        First = testNode.Next;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
            } while (testNode != First);
        }

        /// <summary>
        /// Remove all nodes from the list.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            First = null;
        }
        
        /// <summary>
        /// Swap the positions of two nodes in the list.
        /// </summary>
        /// <param name="nodeA">First node to swap</param>
        /// <param name="nodeB">Second node to swap</param>
        public void Swap(INode<T> nodeA, INode<T> nodeB)
        {
            if (nodeA == null || nodeB == null)
                throw new InvalidOperationException("Cannot swap null nodes.");

            if (nodeA.Previous == nodeB)
            {
                if (nodeB.Previous != nodeA)
                {
                    Swap(nodeB, nodeA);
                }

                return;
            }

            // if (First == null)
            //     throw new InvalidOperationException("Cannot swap nodes not contained in the list.");

            // bool foundA = false, foundB = false;
            // INode<T> testNode = First;
            // do
            // {
            //     if (testNode == nodeA)
            //         foundA = true;
            //
            //     if (testNode == nodeB)
            //         foundB = true;
            //
            //     if (foundA && foundB)
            //         break;
            //
            //     testNode = testNode.Next;
            // } while (testNode != First);
            //
            // if (!foundA || !foundB)
            //     throw new InvalidOperationException("Cannot swap nodes not contained in the list.");

            INode<T> pa = nodeA.Previous, sb = nodeB.Next;

            // remove a from list
            pa.Next = nodeA.Next;
            pa.Next.Previous = pa;

            // remove b from list
            sb.Previous = nodeB.Previous;
            sb.Previous.Next = sb;

            // add a before sb
            nodeA.Previous = sb.Previous;
            nodeA.Next = sb;
            nodeA.Previous.Next = nodeA;
            nodeA.Next.Previous = nodeA;

            // add b after pa
            nodeB.Next = pa.Next;
            nodeB.Previous = pa;
            nodeB.Previous.Next = nodeB;
            nodeB.Next.Previous = nodeB;

            // update first
            if (nodeA == First)
            {
                First = nodeB;
            }
            else if (nodeB == First)
            {
                First = nodeA;
            }
        }

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
        public string Print()
        {
            if (First == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            INode<T> testNode = First;
            do
            {
                sb.Append('[').Append(testNode.Value).Append(']');
                if (testNode.Next != First)
                    sb.Append('-');

                testNode = testNode.Next;
            } while (testNode != First);

            return sb.ToString();
        }
    }
}