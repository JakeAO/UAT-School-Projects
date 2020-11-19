using System;
using System.Collections.Generic;

namespace SadPumpkin.SortingUtilities
{
    /// <summary>
    /// Interface which defines a sorting algorithm which can sort any IList<T> type.
    /// </summary>
    public interface IListSorter
    {
        /// <summary>
        /// Sorts (ascending) the provided collection based on their IComparable implementation.
        /// </summary>
        /// <param name="list">Collection to be sorted</param>
        /// <typeparam name="T">Type of element in the collection</typeparam>
        public void Sort<T>(IList<T> list) where T : IComparable<T>;
    }
}