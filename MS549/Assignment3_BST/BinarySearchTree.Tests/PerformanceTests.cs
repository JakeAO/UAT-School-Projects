using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace SadPumpkin.BST.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        private static readonly Random RANDOM = new Random();
        private const int TEST_COUNT = 1000;

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT/2)]
        [TestCase(10000, TEST_COUNT/10)]
        public void test_average_add_of_custom(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                BinarySearchTree<int> newList = new BinarySearchTree<int>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newList.Add(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} add: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT/2)]
        [TestCase(10000, TEST_COUNT/10)]
        public void test_average_remove_of_custom(int removeCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                BinarySearchTree<int> newList = FillCustomTreeWithRandom(removeCount);

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
        [TestCase(1000, TEST_COUNT/2)]
        [TestCase(10000, TEST_COUNT/10)]
        public void test_average_add_of_default(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                SortedSet<int> newList = new SortedSet<int>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newList.Add(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} add: {avg} ticks");
        }

        [TestCase(10, TEST_COUNT)]
        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT/2)]
        [TestCase(10000, TEST_COUNT/10)]
        public void test_average_remove_of_default(int removeCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                SortedSet<int> newList = FillDefaultTreeWithRandom(removeCount);

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
        
        private static BinarySearchTree<int> FillCustomTreeWithRandom(int elementCount)
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();
            for (int i = 0; i < elementCount; i++)
            {
                newList.Add(RANDOM.Next());
            }

            return newList;
        }

        private static SortedSet<int> FillDefaultTreeWithRandom(int elementCount)
        {
            SortedSet<int> newList = new SortedSet<int>();
            for (int i = 0; i < elementCount; i++)
            {
                newList.Add(RANDOM.Next());
            }

            return newList;
        }
    }
}