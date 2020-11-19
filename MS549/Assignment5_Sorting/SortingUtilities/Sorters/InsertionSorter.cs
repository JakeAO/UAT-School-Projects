using System;
using System.Collections.Generic;
using SadPumpkin.LinkedList;

namespace SadPumpkin.SortingUtilities.Sorters
{
    /*
     * [Insertion Sort]
     * For each element A in the collection, compare against each
     * previous element B in the collection. Insert element A such
     * that it is in sorted order, adjacent to B.
     */

    /// <summary>
    /// Sorter implementation which uses the InsertionSort algorithm to sort collections.
    /// </summary>
    public class InsertionSorter : ICustomLinkedListSorter, ILinkedListSorter, IListSorter
    {
        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(ILinkedList<T> linkedList) where T : IComparable<T>
        {
            // You can't sort nothing.
            if (linkedList == null)
                return;

            // No need to sort an empty or single-element collection.
            if (linkedList.Count < 2)
                return;

            // For each element A...
            INode<T> nodeA = linkedList.First?.Next; // Start at 2nd node
            while (nodeA != null && nodeA != linkedList.First)
            {
                T value = nodeA.Value;

                // ...compare against every previous element B.
                INode<T> nodeB = nodeA.Previous;
                while (nodeB != null && nodeB != linkedList.Last)
                {
                    if (value.CompareTo(nodeB.Value) < 0)
                    {
                        // Custom LinkedList uses immutable nodes,
                        // so we need to swap the actual node positions.
                        if (nodeA != nodeB)
                        {
                            linkedList.Swap(nodeB, nodeB.Next);
                            nodeB = nodeB.Previous;
                        }
                    }
                    else
                    {
                        break;
                    }

                    nodeB = nodeB.Previous;
                }

                nodeA = nodeA.Next;
            }
        }

        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(System.Collections.Generic.LinkedList<T> linkedList) where T : IComparable<T>
        {
            // You can't sort nothing.
            if (linkedList == null)
                return;

            // No need to sort an empty or single-element collection.
            if (linkedList.Count < 2)
                return;

            // For each element A...
            LinkedListNode<T> nodeA = linkedList.First?.Next; // Start at 2nd node
            while (nodeA != null && nodeA != linkedList.First)
            {
                T value = nodeA.Value;

                // ...compare against every previous element B.
                LinkedListNode<T> nodeB = nodeA.Previous;
                while (nodeB != null && nodeB != linkedList.Last)
                {
                    if (value.CompareTo(nodeB.Value) < 0)
                    {
                        // .NET Standard LinkedList uses mutable nodes, so
                        // we can just swap the Values instead of the whole nodes.
                        if (nodeA != nodeB)
                        {
                            nodeB.Next.Value = nodeB.Value;
                            nodeB.Value = value;
                        }
                    }
                    else
                    {
                        break;
                    }

                    nodeB = nodeB.Previous;
                }

                nodeA = nodeA.Next;
            }
        }

        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="list">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            // You can't sort nothing.
            if (list == null)
                return;

            // No need to sort an empty or single-element collection.
            if (list.Count < 2)
                return;

            // For each element A...
            for (int a = 1; a < list.Count; a++)
            {
                T value = list[a];

                // ...compare against every previous element B.
                for (int b = a - 1; b >= 0;)
                {
                    if (value.CompareTo(list[b]) < 0)
                    {
                        // .NET Standard List interface has array-style access,
                        // so we can just swap the values at the two indices.
                        if (a != b)
                        {
                            list[b + 1] = list[b];
                            b--;
                            list[b + 1] = value;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}