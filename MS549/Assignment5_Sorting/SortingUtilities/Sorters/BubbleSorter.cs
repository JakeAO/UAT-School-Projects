using System;
using System.Collections.Generic;
using SadPumpkin.LinkedList;

namespace SadPumpkin.SortingUtilities.Sorters
{
    /*
     * [Bubble Sort]
     * For each element in the collection, compare it against
     * every other element and swap if unordered. Repeat this
     * process for each element in the collection.
     */

    /// <summary>
    /// Sorter implementation which uses the BubbleSort algorithm to sort collections.
    /// </summary>
    public class BubbleSorter : ICustomLinkedListSorter, ILinkedListSorter, IListSorter
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

            // Repeat this process for each element...
            for (int i = 0; i < linkedList.Count - 1; i++)
            {
                // For each element in the collection...
                INode<T> nodeA = linkedList.First;
                while (nodeA != null && nodeA != linkedList.Last)
                {
                    // ...compare it against...and swap if unordered.
                    if (nodeA.Value.CompareTo(nodeA.Next.Value) > 0)
                    {
                        // Custom LinkedList uses immutable nodes,
                        // so we need to swap the actual node positions.
                        linkedList.Swap(nodeA, nodeA.Next);
                        nodeA = nodeA.Previous;
                    }

                    nodeA = nodeA.Next;
                }
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

            // Repeat this process for each element...
            for (int i = 0; i < linkedList.Count - 1; i++)
            {
                // For each element in the collection...
                LinkedListNode<T> nodeA = linkedList.First;
                while (nodeA != null && nodeA != linkedList.Last)
                {
                    // ...compare it against...and swap if unordered.
                    if (nodeA.Value.CompareTo(nodeA.Next.Value) > 0)
                    {
                        // .NET Standard LinkedList uses mutable nodes, so
                        // we can just swap the Values instead of the whole nodes.
                        T tmp = nodeA.Next.Value;
                        nodeA.Next.Value = nodeA.Value;
                        nodeA.Value = tmp;
                    }

                    nodeA = nodeA.Next;
                }
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

            // Repeat this process for each element...
            for (int i = 0; i < list.Count - 1; i++)
            {
                // For each element in the collection...
                for (int a = 0; a < list.Count - 1; a++)
                {
                    // ...compare it against...and swap if unordered.
                    if (list[a].CompareTo(list[a + 1]) > 0)
                    {
                        // .NET Standard List interface has array-style access,
                        // so we can just swap the values at the two indices.
                        T tmp = list[a + 1];
                        list[a + 1] = list[a];
                        list[a] = tmp;
                    }
                }
            }
        }
    }
}