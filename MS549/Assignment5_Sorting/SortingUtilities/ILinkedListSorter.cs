using System;
using System.Collections.Generic;

namespace SadPumpkin.SortingUtilities
{
    /// <summary>
    /// Interface which defines a sorting algorithm which can sort the standard .NET LinkedList<T> type.
    /// </summary>
    public interface ILinkedListSorter
    {
        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="linkedList">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(LinkedList<T> linkedList) where T : IComparable<T>;
    }
}