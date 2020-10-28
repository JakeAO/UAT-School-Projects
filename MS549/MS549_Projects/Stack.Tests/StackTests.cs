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
    }
}