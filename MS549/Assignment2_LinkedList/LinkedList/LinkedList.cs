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
        public INode<T> Last { get; private set; }

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
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode.Value.Equals(value))
                {
                    return testNode;
                }

                testNode = testNode.Next;
            }

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
            if (Last == null)
            {
                // List is empty
                First = Last = new Node<T>(value);
            }
            else
            {
                // Insert after current last
                Last.Next = new Node<T>(value);
                Last.Next.Previous = Last;
                Last = Last.Next;
            }

            Count++;
            return Last;
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
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode.Value.Equals(value))
                {
                    if (testNode.Previous == null)
                    {
                        // Update first node
                        First = testNode.Next;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Previous.Next = testNode.Next;
                    }

                    if (testNode.Next == null)
                    {
                        // Update last node
                        Last = testNode.Previous;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Next.Previous = testNode.Previous;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
            }
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
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode == node)
                {
                    if (testNode.Previous == null)
                    {
                        // Update first node
                        First = testNode.Next;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Previous.Next = testNode.Next;
                    }

                    if (testNode.Next == null)
                    {
                        // Update last node
                        Last = testNode.Previous;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Next.Previous = testNode.Previous;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
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
            StringBuilder sb = new StringBuilder();

            INode<T> testNode = First;
            while (testNode != null)
            {
                sb.Append(testNode.Previous == null ? "|-- " : "--> ")
                    .Append(testNode)
                    .Append(testNode.Next == null ? " --|" : " -->")
                    .AppendLine();

                testNode = testNode.Next;
            }

            return sb.ToString();
        }
    }
}