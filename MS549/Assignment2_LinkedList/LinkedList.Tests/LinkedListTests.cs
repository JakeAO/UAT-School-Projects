﻿using System;
using NUnit.Framework;

namespace SadPumpkin.LinkedList.Tests
{
    [TestFixture]
    public class LinkedListTests
    {
        [Test]
        public void can_create_new()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            Assert.IsNotNull(newList);
        }

        [Test]
        public void is_empty_by_default()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            Assert.AreEqual(0, newList.Count);
            Assert.IsNull(newList.First);
            Assert.IsNull(newList.Last);
        }

        [TestCase(new int[0], ExpectedResult = 0)]
        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 1}, ExpectedResult = 2)]
        [TestCase(new[] {1, 1, 1}, ExpectedResult = 3)]
        [TestCase(new[] {1, 1, 1, 1}, ExpectedResult = 4)]
        [TestCase(new[] {1, 1, 1, 1, 1}, ExpectedResult = 5)]
        public int count_returns_accurate_count(params int[] values)
        {
            ILinkedList<int> newList = new LinkedList<int>();

            foreach (int value in values)
            {
                newList.Insert(value);
            }

            return newList.Count;
        }

        [TestCase(new int[0])]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 1})]
        [TestCase(new[] {1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1})]
        [TestCase(new[] {1, 1, 1, 1, 1})]
        public void count_is_accurate_as_elements_are_added_and_removed(params int[] values)
        {
            ILinkedList<int> newList = new LinkedList<int>();

            Assert.AreEqual(0, newList.Count);

            for (int i = 0; i < values.Length; i++)
            {
                newList.Insert(values[i]);
                Assert.AreEqual(i + 1, newList.Count);
            }

            Assert.AreEqual(values.Length, newList.Count);

            for (int i = values.Length - 1; i >= 0; i--)
            {
                newList.Remove(values[i]);
                Assert.AreEqual(i, newList.Count);
            }

            Assert.AreEqual(0, newList.Count);
        }

        [Test]
        public void first_added_becomes_first_and_last()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            INode<int> newNode = newList.Insert(0);

            Assert.AreEqual(newNode, newList.First);
            Assert.AreEqual(newNode, newList.Last);
        }

        [Test]
        public void first_two_added_become_first_and_last()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            INode<int> newNodeA = newList.Insert(0);
            INode<int> newNodeB = newList.Insert(1);

            Assert.AreNotEqual(newNodeA, newNodeB);
            Assert.AreEqual(newNodeA, newList.First);
            Assert.AreEqual(newNodeB, newList.Last);
        }

        [Test]
        public void remove_value_reduces_count()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            newList.Insert(0);
            newList.Insert(1);
            newList.Insert(2);

            Assert.AreEqual(3, newList.Count);

            newList.Remove(1);

            Assert.AreEqual(2, newList.Count);

            newList.Remove(2);

            Assert.AreEqual(1, newList.Count);

            newList.Remove(0);

            Assert.AreEqual(0, newList.Count);
        }

        [Test]
        public void remove_node_reduces_count()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            INode<int> node0 = newList.Insert(0);
            INode<int> node1 = newList.Insert(1);
            INode<int> node2 = newList.Insert(2);

            Assert.AreEqual(3, newList.Count);

            newList.Remove(node1);

            Assert.AreEqual(2, newList.Count);

            newList.Remove(node2);

            Assert.AreEqual(1, newList.Count);

            newList.Remove(node0);

            Assert.AreEqual(0, newList.Count);
        }

        [Test]
        public void find_returns_correct_node()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            INode<int> node0 = newList.Insert(0);
            INode<int> node1 = newList.Insert(1);
            INode<int> node2 = newList.Insert(2);

            Assert.AreEqual(node2, newList.Find(2));
            Assert.AreEqual(node0, newList.Find(0));
            Assert.AreEqual(node1, newList.Find(1));
        }

        [Test]
        public void find_returns_first_node_with_value()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            INode<int> node00 = newList.Insert(0);
            INode<int> node11 = newList.Insert(1);
            INode<int> node12 = newList.Insert(1);
            INode<int> node21 = newList.Insert(2);
            INode<int> node22 = newList.Insert(2);

            Assert.AreEqual(node21, newList.Find(2));
            Assert.AreEqual(node00, newList.Find(0));
            Assert.AreEqual(node11, newList.Find(1));
        }

        [Test]
        public void print_returns_empty_when_list_empty()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            Assert.AreEqual(string.Empty, newList.Print());
        }

        [Test]
        public void print_content_scales_with_count()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            string print0 = newList.Print();

            newList.Insert(1);

            string print1 = newList.Print();
            Assert.Less(print0.Length, print1.Length);

            newList.Insert(2);

            string print2 = newList.Print();
            Assert.Less(print1.Length, print2.Length);

            newList.Insert(3);

            string print3 = newList.Print();
            Assert.Less(print2.Length, print3.Length);

            newList.Remove(1);

            string print4 = newList.Print();
            Assert.Greater(print3.Length, print4.Length);

            newList.Remove(2);

            string print5 = newList.Print();
            Assert.Greater(print4.Length, print5.Length);
        }

        [Test]
        public void insert_order_is_preserved()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            newList.Insert(1);
            newList.Insert(2);
            newList.Insert(3);
            newList.Insert(4);
            newList.Insert(5);
            newList.Insert(6);

            INode<int> testNode = newList.First;
            do
            {
                Assert.Less(testNode.Value, testNode.Next.Value);

                testNode = testNode.Next;
            } while (testNode.Next != newList.First);
        }

        [Test]
        public void alter_root_fails_given_null()
        {
            ILinkedList<int> listA = new LinkedList<int>();
            INode<int> validNode = listA.Insert(5);
            
            Assert.Throws<InvalidOperationException>(() => listA.Swap(null, validNode));
            Assert.Throws<InvalidOperationException>(() => listA.Swap(validNode, null));
            Assert.Throws<InvalidOperationException>(() => listA.Swap(null, null));
        }

        [Test]
        public void swap_fails_given_invalid()
        {
            ILinkedList<int> listA = new LinkedList<int>();
            listA.Insert(1);
            INode<int> validNode = listA.Insert(2);
            listA.Insert(3);

            INode<int> invalidNode = new Node<int>(4);

            Assert.Throws<InvalidOperationException>(() => listA.Swap(validNode, invalidNode));
            Assert.Throws<InvalidOperationException>(() => listA.Swap(invalidNode, validNode));
            Assert.Throws<InvalidOperationException>(() => listA.Swap(invalidNode, invalidNode));
        }

        [Test]
        public void alter_root_succeeds_given_valid()
        {
            ILinkedList<int> listA = new LinkedList<int>();
            INode<int> first = listA.Insert(1);
            listA.Insert(2);
            INode<int> last = listA.Insert(3);

            listA.Swap(first, last);

            Assert.AreEqual(last, listA.First);
            Assert.AreEqual(first, listA.Last);
        }

        [Test]
        public void swap_maintains_count()
        {
            ILinkedList<int> listA = new LinkedList<int>();
            listA.Insert(1);
            INode<int> nodeA = listA.Insert(2);
            listA.Insert(3);
            INode<int> nodeB = listA.Insert(4);
            listA.Insert(5);
            listA.Insert(6);

            Assert.AreEqual(6, listA.Count);

            listA.Swap(nodeA, nodeB);

            Assert.AreEqual(6, listA.Count);
        }

        [Test]
        public void clear_removes_first()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            newList.Insert(1);
            newList.Insert(2);
            newList.Insert(3);
            newList.Insert(4);
            newList.Insert(5);
            newList.Insert(6);
            
            Assert.IsNotNull(newList.First);
            
            newList.Clear();
            
            Assert.IsNull(newList.First);
        }

        [Test]
        public void clear_removes_last()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            newList.Insert(1);
            newList.Insert(2);
            newList.Insert(3);
            newList.Insert(4);
            newList.Insert(5);
            newList.Insert(6);
            
            Assert.IsNotNull(newList.Last);
            
            newList.Clear();
            
            Assert.IsNull(newList.Last);
        }

        [Test]
        public void clear_sets_count_to_zero()
        {
            ILinkedList<int> newList = new LinkedList<int>();

            newList.Insert(1);
            newList.Insert(2);
            newList.Insert(3);
            newList.Insert(4);
            newList.Insert(5);
            newList.Insert(6);
            
            Assert.AreEqual(6, newList.Count);
            
            newList.Clear();
            
            Assert.AreEqual(0, newList.Count);
        }
    }
}