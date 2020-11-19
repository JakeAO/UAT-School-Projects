using System;
using System.Collections.Generic;
using NUnit.Framework;
using SadPumpkin.LinkedList;
using SadPumpkin.SortingUtilities.Sorters;

using CustomLinkedList = SadPumpkin.LinkedList.LinkedList<int>;
using DefaultLinkedList = System.Collections.Generic.LinkedList<int>;
using DefaultList = System.Collections.Generic.List<int>;

namespace SadPumpkin.SortingUtilities.Tests
{
    [TestFixture]
    public class SelectionSorterTests
    {
        private static readonly Random RANDOM = new Random();

        [Test]
        public void sort_maintains_length_of_custom_linked_list()
        {
            ICustomLinkedListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 10; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                CustomLinkedList linkedList = new CustomLinkedList();
                for (int j = 0; j < listLength; j++)
                {
                    linkedList.Insert(RANDOM.Next());
                }

                int oldLength = linkedList.Count;
                Assert.AreEqual(listLength, oldLength);

                sorter.Sort(linkedList);

                int newLength = linkedList.Count;
                Assert.AreEqual(listLength, newLength);
            }
        }

        [Test]
        public void sort_orders_custom_linked_list()
        {
            ICustomLinkedListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 100; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                CustomLinkedList linkedList = new CustomLinkedList();
                for (int j = 0; j < listLength; j++)
                {
                    linkedList.Insert(RANDOM.Next());
                }

                sorter.Sort(linkedList);

                INode<int> node = linkedList.First;
                do
                {
                    Assert.LessOrEqual(node.Value, node.Next.Value);
                    node = node.Next;
                } while (node != linkedList.Last);
            }
        }

        [Test]
        public void sort_maintains_length_of_default_linked_list()
        {
            ILinkedListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 10; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                DefaultLinkedList linkedList = new DefaultLinkedList();
                for (int j = 0; j < listLength; j++)
                {
                    linkedList.AddLast(RANDOM.Next());
                }

                int oldLength = linkedList.Count;
                Assert.AreEqual(listLength, oldLength);

                sorter.Sort(linkedList);

                int newLength = linkedList.Count;
                Assert.AreEqual(listLength, newLength);
            }
        }

        [Test]
        public void sort_orders_default_linked_list()
        {
            ILinkedListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 100; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                DefaultLinkedList linkedList = new DefaultLinkedList();
                for (int j = 0; j < listLength; j++)
                {
                    linkedList.AddLast(RANDOM.Next());
                }

                sorter.Sort(linkedList);

                LinkedListNode<int> node = linkedList.First;
                do
                {
                    Assert.LessOrEqual(node.Value, node.Next.Value);
                    node = node.Next;
                } while (node != linkedList.Last);
            }
        }

        [Test]
        public void sort_maintains_length_of_default_list()
        {
            IListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 10; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                DefaultList list = new DefaultList();
                for (int j = 0; j < listLength; j++)
                {
                    list.Add(RANDOM.Next());
                }

                int oldLength = list.Count;
                Assert.AreEqual(listLength, oldLength);

                sorter.Sort(list);

                int newLength = list.Count;
                Assert.AreEqual(listLength, newLength);
            }
        }

        [Test]
        public void sort_orders_default_list()
        {
            IListSorter sorter = new SelectionSorter();

            for (int i = 0; i < 100; i++)
            {
                int listLength = RANDOM.Next() % 50 + 5;

                DefaultList list = new DefaultList();
                for (int j = 0; j < listLength; j++)
                {
                    list.Add(RANDOM.Next());
                }

                sorter.Sort(list);

                for (int j = 0; j < list.Count - 1; j++)
                {
                    Assert.LessOrEqual(list[j], list[j + 1]);
                }
            }
        }
    }
}