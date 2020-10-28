using System;
using NUnit.Framework;

namespace SadPumpkin.Stack.Tests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void can_create_new()
        {
            IStack<int> newStack = new Stack<int>();

            Assert.IsNotNull(newStack);
        }

        [Test]
        public void is_empty_by_default()
        {
            IStack<int> newStack = new Stack<int>();

            Assert.AreEqual(0, newStack.Count);
            Assert.AreEqual(0, newStack.Capacity);
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(10, ExpectedResult = 10)]
        [TestCase(50, ExpectedResult = 50)]
        [TestCase(99, ExpectedResult = 99)]
        public int capacity_matches_parameter(int capacity)
        {
            IStack<int> newStack = new Stack<int>(capacity);

            return newStack.Capacity;
        }

        [Test]
        public void pop_throws_when_empty()
        {
            IStack<int> newStack = new Stack<int>();

            Assert.Throws<InvalidOperationException>(() => newStack.Pop());
        }

        [Test]
        public void peek_returns_default_when_empty()
        {
            IStack<int> newStack = new Stack<int>();

            Assert.AreEqual(0, newStack.Peek());
        }

        [TestCase(new int[0], ExpectedResult = 0)]
        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 1}, ExpectedResult = 2)]
        [TestCase(new[] {1, 1, 1}, ExpectedResult = 3)]
        [TestCase(new[] {1, 1, 1, 1}, ExpectedResult = 4)]
        [TestCase(new[] {1, 1, 1, 1, 1}, ExpectedResult = 5)]
        public int count_returns_accurate_count(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }

            return newStack.Count;
        }

        [TestCase(new int[0])]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 1})]
        [TestCase(new[] {1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1, 1})]
        public void count_is_accurate_as_elements_are_added_and_removed(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);

            Assert.AreEqual(0, newStack.Count);

            for (int i = 0; i < values.Length; i++)
            {
                newStack.Push(values[i]);
                Assert.AreEqual(i + 1, newStack.Count);
            }

            Assert.AreEqual(values.Length, newStack.Count);

            for (int i = values.Length; i > 0; i--)
            {
                newStack.Pop();
                Assert.AreEqual(i - 1, newStack.Count);
            }

            Assert.AreEqual(0, newStack.Count);
        }

        [TestCase(new int[0])]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 1})]
        [TestCase(new[] {1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1, 1})]
        public void capacity_remains_same_when_elements_fit(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            Assert.AreEqual(values.Length, newStack.Capacity);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }
            
            Assert.AreEqual(values.Length, newStack.Capacity);
        }

        [TestCase(0, new[] {1}, ExpectedResult = 1)]
        [TestCase(1, new[] {1, 1}, ExpectedResult = 2)]
        [TestCase(2, new[] {1, 1, 1}, ExpectedResult = 4)]
        [TestCase(3, new[] {1, 1, 1, 1}, ExpectedResult = 6)]
        [TestCase(1, new[] {1, 1, 1, 1, 1}, ExpectedResult = 8)]
        public int capacity_doubles_when_elements_do_not_fit(int capacity, params int[] values)
        {
            IStack<int> newStack = new Stack<int>(capacity);

            Assert.AreEqual(capacity, newStack.Capacity);

            foreach (int value in values)
            {
                newStack.Push(value);
            }

            Assert.AreEqual(values.Length, newStack.Count);

            return newStack.Capacity;
        }

        [TestCase(0, new[] {1}, ExpectedResult = 1)]
        [TestCase(1, new[] {1, 1}, ExpectedResult = 2)]
        [TestCase(2, new[] {1, 1, 1}, ExpectedResult = 4)]
        [TestCase(3, new[] {1, 1, 1, 1}, ExpectedResult = 6)]
        [TestCase(1, new[] {1, 1, 1, 1, 1}, ExpectedResult = 8)]
        public int capacity_remains_fixed_when_elements_are_removed(int capacity, params int[] values)
        {
            IStack<int> newStack = new Stack<int>(capacity);

            Assert.AreEqual(capacity, newStack.Capacity);

            foreach (int value in values)
            {
                newStack.Push(value);
            }

            Assert.AreEqual(values.Length, newStack.Count);

            for (int i = 0; i < values.Length; i++)
            {
                newStack.Pop();
            }
            
            return newStack.Capacity;
        }

        [TestCase(new int[0], ExpectedResult = 0)]
        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 4)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 5)]
        public int peek_returns_top_value(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }

            return newStack.Peek();
        }

        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 4)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 5)]
        public int push_inserts_new_value(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }

            return newStack.Peek();
        }

        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 4)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 5)]
        public int pop_returns_top_value(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }

            return newStack.Pop();
        }

        [TestCase(new[] {1}, ExpectedResult = 0)]
        [TestCase(new[] {1, 2}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 4)]
        public int pop_removes_top_value(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);
            
            foreach (int value in values)
            {
                newStack.Push(value);
            }

            newStack.Pop();
            return newStack.Peek();
        }

        [TestCase(new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4})]
        [TestCase(new[] {1, 2, 3, 4, 5})]
        [TestCase(new[] {1, 2, 3, 4, 5, 6})]
        public void order_is_maintained(params int[] values)
        {
            IStack<int> newStack = new Stack<int>(values.Length);

            foreach (int value in values)
            {
                newStack.Push(value);
            }

            for (int i = values.Length - 1; i >= 0; i--)
            {
                int val = newStack.Pop();

                Assert.AreEqual(values[i], val);
            }
        }
    }
}