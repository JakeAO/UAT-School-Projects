using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace SadPumpkin.LinkedList.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        private static readonly Random RANDOM = new Random();
        private const int TEST_COUNT = 1000;

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_insert_of_custom(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                ILinkedList<int> newList = new LinkedList<int>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newList.Insert(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_remove_of_custom(int removeCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                ILinkedList<int> newList = FillCustomLinkedListWithRandom(removeCount);

                stopwatch.Restart();
                for (int j = 0; j < removeCount; j++)
                {
                    newList.Remove(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{removeCount} removes: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_insert_of_default(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                System.Collections.Generic.LinkedList<int> newList = new System.Collections.Generic.LinkedList<int>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newList.AddLast(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_remove_of_default(int removeCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                System.Collections.Generic.LinkedList<int> newList = FillDefaultLinkedListWithRandom(removeCount);

                stopwatch.Restart();
                for (int j = 0; j < removeCount; j++)
                {
                    newList.Remove(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{removeCount} removes: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_insert_of_list(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                System.Collections.Generic.List<int> newList = new System.Collections.Generic.List<int>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newList.Add(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        public void test_average_remove_of_list(int removeCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                System.Collections.Generic.List<int> newList = FillDefaultListWithRandom(removeCount);

                stopwatch.Restart();
                for (int j = 0; j < removeCount; j++)
                {
                    newList.Remove(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{removeCount} removes: {avg} ticks");
        }

        private static ILinkedList<int> FillCustomLinkedListWithRandom(int elementCount)
        {
            ILinkedList<int> newList = new LinkedList<int>();
            for (int i = 0; i < elementCount; i++)
            {
                newList.Insert(RANDOM.Next());
            }

            return newList;
        }

        private static System.Collections.Generic.LinkedList<int> FillDefaultLinkedListWithRandom(int elementCount)
        {
            System.Collections.Generic.LinkedList<int> newList = new System.Collections.Generic.LinkedList<int>();
            for (int i = 0; i < elementCount; i++)
            {
                newList.AddLast(RANDOM.Next());
            }

            return newList;
        }

        private static System.Collections.Generic.List<int> FillDefaultListWithRandom(int elementCount)
        {
            System.Collections.Generic.List<int> newList = new System.Collections.Generic.List<int>();
            for (int i = 0; i < elementCount; i++)
            {
                newList.Add(RANDOM.Next());
            }

            return newList;
        }
    }
}