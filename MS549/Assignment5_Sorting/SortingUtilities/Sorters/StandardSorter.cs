using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.LinkedList;

namespace SadPumpkin.SortingUtilities.Sorters
{
    /*
     * ['Standard' Sort]
     * .NET LinkedLists don't have any built-in sorting implementation since they
     * aren't used all that often. So for LinkedList we're relying on LINQ's OrderBy,
     * and for IList we're using the ArrayList wrapper class to sort the collection.
     * Both of these methods result in unwanted allocations:
     *     - The LinkedList methods because of the use of LINQ's ToArray method.
     *     - The List method because of ArrayList's Adapter method, or List.Sort depending on type.
     */

    /// <summary>
    /// Sorter implementation which uses the .NET standard sorting algorithm(s) to sort collections.
    /// Either QuickSort or IntrospectiveSort depending on collection type.
    /// </summary>
    public class StandardSorter : ICustomLinkedListSorter, ILinkedListSorter, IListSorter
    {
        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <remarks>
        /// Uses QuickSort via LINQ.OrderBy()
        /// </remarks>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(ILinkedList<T> linkedList) where T : IComparable<T>
        {
            // ILinkedList doesn't implement IEnumerable so we have a manual one here.
            IEnumerable<T> ToEnumerable(ILinkedList<T> innerList)
            {
                INode<T> node = innerList.First;
                if (node != null)
                {
                    do
                    {
                        yield return node.Value;
                        node = node.Next;
                    } while (node != innerList.First);
                }
            }

            T[] sortedValues = ToEnumerable(linkedList).OrderBy(x => x).ToArray();
            linkedList.Clear();
            foreach (T value in sortedValues)
            {
                linkedList.Insert(value);
            }
        }

        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <remarks>
        /// Uses QuickSort via LINQ.OrderBy()
        /// </remarks>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(System.Collections.Generic.LinkedList<T> linkedList) where T : IComparable<T>
        {
            T[] sortedValues = linkedList.OrderBy(x => x).ToArray();
            linkedList.Clear();
            foreach (T value in sortedValues)
            {
                linkedList.AddLast(value);
            }
        }

        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <remarks>
        /// Uses IntrospectiveSort via ArrayList.Sort()
        /// </remarks>
        /// <param name="list">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            if (list is List<T> typedList)
            {
                typedList.Sort();
            }
            else
            {
                ArrayList.Adapter((IList) list).Sort();
            }
        }
    }
}