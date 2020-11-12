using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace SadPumpkin.HashTable.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        private static readonly Random RANDOM = new Random();
        private const int TEST_COUNT = 1000;

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void insert_hash_table(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                IHashTable<int, object> newTable = new HashTable<int, object>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    int randValue = RANDOM.Next();
                    newTable.Insert(randValue, randValue);
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void insert_chain_table(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                IHashTable<int, object> newTable = new ChainHashTable<int, object>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    int randValue = RANDOM.Next();
                    newTable.Insert(randValue, randValue);
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void insert_default_hashtable(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                Hashtable newTable = new Hashtable();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    int randValue = RANDOM.Next();
                    try
                    {
                        newTable.Add(randValue, randValue);
                    }
                    catch
                    {
                        // Ignored
                        // .NET Hashtable does not contain a 'safe add' method.
                        // Adding an existing key/value will result in an exception.
                    }
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void insert_default_dictionary(int insertCount, int averageAcross)
        {
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                Dictionary<int, object> newTable = new Dictionary<int, object>();

                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    int randValue = RANDOM.Next();
                    newTable[randValue] = randValue;
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void retrieve_hash_table(int retrieveCount, int averageAcross)
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();
            for (int i = 0; i < 1000; i++)
            {
                int randValue = RANDOM.Next();
                newTable.Insert(randValue, randValue);
            }
            
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                stopwatch.Restart();
                for (int j = 0; j < retrieveCount; j++)
                {
                    newTable.Retrieve(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{retrieveCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void retrieve_chain_table(int insertCount, int averageAcross)
        {
            IHashTable<int, object> newTable = new ChainHashTable<int, object>();
            for (int i = 0; i < 1000; i++)
            {
                int randValue = RANDOM.Next();
                newTable.Insert(randValue, randValue);
            }

            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newTable.Retrieve(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void retrieve_default_hashtable(int insertCount, int averageAcross)
        {
            Hashtable newTable = new Hashtable();
            for (int i = 0; i < 1000; i++)
            {
                int randValue = RANDOM.Next();
                newTable.Add(randValue, randValue);
            }
            
            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newTable.Contains(RANDOM.Next());
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }

        [TestCase(100, TEST_COUNT)]
        [TestCase(1000, TEST_COUNT)]
        [TestCase(10000, TEST_COUNT/2)]
        [TestCase(100000, TEST_COUNT/10)]
        public void retrieve_default_dictionary(int insertCount, int averageAcross)
        {
            Dictionary<int, object> newTable = new Dictionary<int, object>();
            for (int i = 0; i < 1000; i++)
            {
                int randValue = RANDOM.Next();
                newTable.Add(randValue, randValue);
            }

            long[] results = new long[averageAcross];
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < averageAcross; i++)
            {
                stopwatch.Restart();
                for (int j = 0; j < insertCount; j++)
                {
                    newTable.TryGetValue(RANDOM.Next(), out object _);
                }

                stopwatch.Stop();

                results[i] = stopwatch.ElapsedTicks;
            }

            double avg = results.Average();
            Assert.Pass($"{insertCount} inserts: {avg} ticks");
        }
    }
}