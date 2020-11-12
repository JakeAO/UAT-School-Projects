using NUnit.Framework;
using SadPumpkin.HashTable.CollisionResolver;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.Tests
{
    [TestFixture]
    public class HashTableTests
    {
        [Test]
        public void can_create_new()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.IsNotNull(newTable);
        }

        [Test]
        public void can_create_new_with_capacity()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(50, 0.6f);

            Assert.IsNotNull(newTable);
        }

        [Test]
        public void can_create_new_with_modules()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(
                50, 0.6f,
                new StandardHashGenerator<int>(),
                new LinearProbingResolver());

            Assert.IsNotNull(newTable);
        }

        [Test]
        public void count_defaults_to_zero()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.Zero(newTable.Count);
        }

        [Test]
        public void capacity_defaults_to_zero()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.Zero(newTable.Capacity);
        }

        [Test]
        public void load_factor_defaults_to_sixty_percent()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.AreEqual(0.6f, newTable.LoadFactor);
        }

        [Test]
        public void hash_generator_defaults_to_standard()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.IsInstanceOf<StandardHashGenerator<int>>(newTable.HashGenerator);
        }

        [Test]
        public void collision_resolver_defaults_to_quadratic()
        {
            HashTable<int, object> newTable = new HashTable<int, object>();

            Assert.IsInstanceOf<QuadraticProbingResolver>(newTable.CollisionResolver);
        }

        [TestCase(new int[0], ExpectedResult = 0)]
        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 4)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 5)]
        public int count_returns_accurate_count(params int[] values)
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            foreach (int value in values)
            {
                newTable.Insert(value, value);
            }

            return newTable.Count;
        }

        [TestCase(new int[0])]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 2})]
        [TestCase(new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4})]
        [TestCase(new[] {1, 2, 3, 4, 5})]
        public void count_is_accurate_as_elements_are_added_and_removed(params int[] values)
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            Assert.AreEqual(0, newTable.Count);

            for (int i = 0; i < values.Length; i++)
            {
                newTable.Insert(values[i], values[i]);
                Assert.AreEqual(i + 1, newTable.Count);
            }

            Assert.AreEqual(values.Length, newTable.Count);

            for (int i = values.Length - 1; i >= 0; i--)
            {
                newTable.Remove(values[i]);
                Assert.AreEqual(i, newTable.Count);
            }

            Assert.AreEqual(0, newTable.Count);
        }

        [Test]
        public void remove_value_reduces_count()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            newTable.Insert(0, 123);
            newTable.Insert(1, 123);
            newTable.Insert(2, 123);

            Assert.AreEqual(3, newTable.Count);

            newTable.Remove(1);

            Assert.AreEqual(2, newTable.Count);

            newTable.Remove(2);

            Assert.AreEqual(1, newTable.Count);

            newTable.Remove(0);

            Assert.AreEqual(0, newTable.Count);
        }

        [Test]
        public void remove_returns_true_when_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            newTable.Insert(0, 123);
            newTable.Insert(1, 123);
            newTable.Insert(2, 123);

            bool result = newTable.Remove(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void remove_returns_false_when_empty()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            bool result = newTable.Remove(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void remove_returns_false_when_not_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            newTable.Insert(0, 123);
            newTable.Insert(1, 123);
            newTable.Insert(2, 123);

            bool result = newTable.Remove(3);

            Assert.IsFalse(result);
        }

        [Test]
        public void clear_reduces_count_to_zero()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>();

            newTable.Insert(0, 123);
            newTable.Insert(1, 123);
            newTable.Insert(2, 123);
            newTable.Clear();

            Assert.Zero(newTable.Count);
        }

        [Test]
        public void clear_maintains_capacity()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 123);
            newTable.Insert(2, 123);
            newTable.Clear();

            Assert.AreEqual(20, newTable.Capacity);
        }

        [Test]
        public void retrieve_returns_default_when_not_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 456);
            newTable.Insert(2, 789);

            object result = newTable.Retrieve(3);

            Assert.AreEqual(default, result);
        }

        [Test]
        public void retrieve_returns_value_when_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 456);
            newTable.Insert(2, 789);

            object result = newTable.Retrieve(1);

            Assert.AreEqual(456, result);
        }

        [Test]
        public void try_retrieve_returns_false_when_not_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 456);
            newTable.Insert(2, 789);

            bool result = newTable.TryRetrieve(3, out object _);

            Assert.IsFalse(result);
        }

        [Test]
        public void try_retrieve_returns_true_when_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 456);
            newTable.Insert(2, 789);

            bool result = newTable.TryRetrieve(1, out object _);

            Assert.IsTrue(result);
        }

        [Test]
        public void try_retrieve_returns_value_when_found()
        {
            IHashTable<int, object> newTable = new HashTable<int, object>(20, 0.6f);

            newTable.Insert(0, 123);
            newTable.Insert(1, 456);
            newTable.Insert(2, 789);

            bool result = newTable.TryRetrieve(1, out object resultValue);

            Assert.AreEqual(456, resultValue);
        }
    }
}