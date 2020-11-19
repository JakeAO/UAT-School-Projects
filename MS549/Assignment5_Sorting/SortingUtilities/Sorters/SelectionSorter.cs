using System;
using System.Collections.Generic;
using SadPumpkin.LinkedList;

namespace SadPumpkin.SortingUtilities.Sorters
{
    /*
     * [Selection Sort]
     * For each element A in the collection, compare against every subsequent
     * element B in the collection. Swap the position of initial element A and
     * subsequent element B with the lowest value.
     */

    /// <summary>
    /// Sorter implementation which uses the SelectionSort algorithm to sort collections.
    /// </summary>
    public class SelectionSorter : ICustomLinkedListSorter, ILinkedListSorter, IListSorter
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
            INode<T> nodeA = linkedList.First;
            do
            {
                INode<T> minNode = nodeA;

                // ...compare against every subsequent element B.
                INode<T> nodeB = nodeA.Next;
                do
                {
                    if (nodeB.Value.CompareTo(minNode.Value) < 0)
                    {
                        minNode = nodeB;
                    }

                    nodeB = nodeB.Next;
                } while (nodeB != linkedList.First);

                // Custom LinkedList uses immutable nodes,
                // so we need to swap the actual node positions.
                if (nodeA != minNode)
                {
                    linkedList.Swap(nodeA, minNode);
                }

                // Continue sort starting from next node
                nodeA = minNode.Next;
            } while (nodeA != linkedList.Last);
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
            LinkedListNode<T> nodeA = linkedList.First;
            while (nodeA != null && nodeA != linkedList.Last)
            {
                LinkedListNode<T> minNode = nodeA;

                // ...compare against every subsequent element B.
                LinkedListNode<T> nodeB = nodeA.Next;
                while (nodeB != null && nodeB != linkedList.First)
                {
                    if (nodeB.Value.CompareTo(minNode.Value) < 0)
                    {
                        minNode = nodeB;
                    }

                    nodeB = nodeB.Next;
                }

                // .NET Standard LinkedList uses mutable nodes, so
                // we can just swap the Values instead of the whole nodes.
                if (nodeA != minNode)
                {
                    T tmp = nodeA.Value;
                    nodeA.Value = minNode.Value;
                    minNode.Value = tmp;
                }

                // Continue sort starting from next node
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
            for (int a = 0; a < list.Count - 1; a++)
            {
                int minIdx = a;

                // ...compare against every subsequent element B.
                for (int b = a + 1; b < list.Count; b++)
                {
                    if (list[b].CompareTo(list[minIdx]) < 0)
                    {
                        minIdx = b;
                    }
                }

                // .NET Standard List interface has array-style access,
                // so we can just swap the values at the two indices.
                if (a != minIdx)
                {
                    T tmp = list[a];
                    list[a] = list[minIdx];
                    list[minIdx] = tmp;
                }
            }
        }
    }
}