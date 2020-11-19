using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SadPumpkin.SortingUtilities.Sorters;

using CustomLinkedList = SadPumpkin.LinkedList.LinkedList<int>;
using DefaultLinkedList = System.Collections.Generic.LinkedList<int>;
using DefaultList = System.Collections.Generic.List<int>;

namespace SadPumpkin.SortingUtilities.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        private static readonly Random RANDOM = new Random();

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_bubble_sort_on_custom(int length, int averageAcross)
        {
            ICustomLinkedListSorter sorter = new BubbleSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                CustomLinkedList list = GenerateCustomLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_bubble_sort_on_default(int length, int averageAcross)
        {
            ILinkedListSorter sorter = new BubbleSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultLinkedList list = GenerateStandardLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_bubble_sort_on_list(int length, int averageAcross)
        {
            IListSorter sorter = new BubbleSorter();

            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultList list = GenerateStandardList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_insertion_sort_on_custom(int length, int averageAcross)
        {
            ICustomLinkedListSorter sorter = new InsertionSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                CustomLinkedList list = GenerateCustomLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_insertion_sort_on_default(int length, int averageAcross)
        {
            ILinkedListSorter sorter = new InsertionSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultLinkedList list = GenerateStandardLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_insertion_sort_on_list(int length, int averageAcross)
        {
            IListSorter sorter = new InsertionSorter();

            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultList list = GenerateStandardList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_selection_sort_on_custom(int length, int averageAcross)
        {
            ICustomLinkedListSorter sorter = new SelectionSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                CustomLinkedList list = GenerateCustomLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_selection_sort_on_default(int length, int averageAcross)
        {
            ILinkedListSorter sorter = new SelectionSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultLinkedList list = GenerateStandardLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 100)]
        [TestCase(1000, 20)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_selection_sort_on_list(int length, int averageAcross)
        {
            IListSorter sorter = new SelectionSorter();

            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultList list = GenerateStandardList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 1)]
        [TestCase(1000, 1)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_standard_sort_on_custom(int length, int averageAcross)
        {
            ICustomLinkedListSorter sorter = new StandardSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                CustomLinkedList list = GenerateCustomLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 1)]
        [TestCase(1000, 1)]
        [TestCase(10000, 1)]
        [TestCase(100000, 1)]
        public void evaluate_standard_sort_on_default(int length, int averageAcross)
        {
            ILinkedListSorter sorter = new StandardSorter();
            
            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultLinkedList list = GenerateStandardLinkedList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        [TestCase(100, 10)]
        [TestCase(1000, 10)]
        [TestCase(10000, 10)]
        [TestCase(100000, 10)]
        public void evaluate_standard_sort_on_list(int length, int averageAcross)
        {
            IListSorter sorter = new StandardSorter();

            long[] results = new long[averageAcross];
            Parallel.For(0, averageAcross, i =>
            {
                DefaultList list = GenerateStandardList(length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                sorter.Sort(list);
                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            });

            double avg = results.Average();
            Assert.Pass($"{length} sort: {avg} ticks");
        }

        private static CustomLinkedList GenerateCustomLinkedList(int length)
        {
            CustomLinkedList list = new CustomLinkedList();
            for (int i = 0; i < length; i++)
            {
                list.Insert(RANDOM.Next());
            }

            return list;
        }

        private static DefaultLinkedList GenerateStandardLinkedList(int length)
        {
            DefaultLinkedList list = new DefaultLinkedList();
            for (int i = 0; i < length; i++)
            {
                list.AddLast(RANDOM.Next());
            }

            return list;
        }

        private static DefaultList GenerateStandardList(int length)
        {
            DefaultList list = new DefaultList(length);
            for (int i = 0; i < length; i++)
            {
                list.Add(RANDOM.Next());
            }

            return list;
        }
    }
}