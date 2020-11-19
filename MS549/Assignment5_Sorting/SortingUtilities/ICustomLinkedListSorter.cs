using System;
using SadPumpkin.LinkedList;

namespace SadPumpkin.SortingUtilities
{
    /// <summary>
    /// Interface which defines a sorting algorithm which can sort any custom SadPumpkin.ILinkedList<T> type.
    /// </summary>
    public interface ICustomLinkedListSorter
    {
        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(ILinkedList<T> linkedList) where T : IComparable<T>;
    }
}