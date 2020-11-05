using NUnit.Framework;

namespace BinarySearchTree.Tests
{
    [TestFixture]
    public class TreeTests
    {
        [Test]
        public void can_create_new()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            Assert.IsNotNull(newList);
        }

        [Test]
        public void can_create_new_with_values()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>(5, 6, 11, 9);

            Assert.IsNotNull(newList);
            Assert.AreEqual(5, newList.Root.Value);
            Assert.AreEqual(4, newList.Count);
        }

        [Test]
        public void is_empty_by_default()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            Assert.AreEqual(0, newList.Count);
            Assert.IsNull(newList.Root);
        }

        [TestCase(new int[0], ExpectedResult = 0)]
        [TestCase(new[] {1}, ExpectedResult = 1)]
        [TestCase(new[] {1, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 4}, ExpectedResult = 4)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 5)]
        public int count_returns_accurate_count(params int[] values)
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            foreach (int value in values)
            {
                newList.Add(value);
            }

            return newList.Count;
        }

        [TestCase(new int[0])]
        [TestCase(new[] {1})]
        [TestCase(new[] {1, 2})]
        [TestCase(new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4})]
        [TestCase(new[] {1, 2, 3, 4, 5})]
        public void count_is_accurate_as_elements_are_added_and_removed(params int[] values)
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            Assert.AreEqual(0, newList.Count);

            for (int i = 0; i < values.Length; i++)
            {
                newList.Add(values[i]);
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
        public void first_added_becomes_root()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            INode<int> newNode = newList.Add(0);

            Assert.AreEqual(newNode, newList.Root);
        }

        [Test]
        public void remove_value_reduces_count()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(0);
            newList.Add(1);
            newList.Add(2);

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
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            INode<int> node0 = newList.Add(0);
            INode<int> node1 = newList.Add(1);
            INode<int> node2 = newList.Add(2);

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
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            INode<int> node0 = newList.Add(0);
            INode<int> node1 = newList.Add(1);
            INode<int> node2 = newList.Add(2);

            Assert.AreEqual(node2, newList.Find(2));
            Assert.AreEqual(node0, newList.Find(0));
            Assert.AreEqual(node1, newList.Find(1));
        }

        [Test]
        public void traverse_never_invokes_when_empty()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            // yuck closure
            int callCount = 0;
            void OnNodeTraversed(int nodeValue)
            {
                callCount++;
            }

            newList.Traverse(OnNodeTraversed);

            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void traverse_invokes_once_per_node()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(5);
            newList.Add(7);
            newList.Add(2);

            // yuck closure
            int callCount = 0;
            void OnNodeTraversed(int nodeValue)
            {
                callCount++;
            }

            newList.Traverse(OnNodeTraversed);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void traverse_invokes_in_order()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(15);
            newList.Add(1);
            newList.Add(42);
            newList.Add(7);
            newList.Add(99);
            newList.Add(6);
            
            // yuck closure
            bool error = false;
            int? prevValue = null;
            void OnNodeTraversed(int nodeValue)
            {
                if (prevValue.HasValue &&
                    prevValue >= nodeValue)
                {
                    error = true;
                }

                prevValue = nodeValue;
            }

            newList.Traverse(OnNodeTraversed);

            Assert.IsFalse(error);
        }

        [Test]
        public void balance_maintains_count()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(1);
            newList.Add(2);
            newList.Add(3);
            newList.Add(4);
            newList.Add(5);
            newList.Add(6);

            Assert.AreEqual(6, newList.Count);

            newList.Balance();

            Assert.AreEqual(6, newList.Count);
        }

        [Test]
        public void balance_maintains_order()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(1);
            newList.Add(6);
            newList.Add(7);
            newList.Add(15);
            newList.Add(42);
            newList.Add(99);

            // yuck closure
            bool error = false;
            int? prevValue = null;
            void OnNodeTraversed(int nodeValue)
            {
                if (prevValue.HasValue &&
                    prevValue >= nodeValue)
                {
                    error = true;
                }

                prevValue = nodeValue;
            }

            newList.Balance();
            newList.Traverse(OnNodeTraversed);

            Assert.IsFalse(error);
        }

        [Test]
        public void balance_reduces_depth_of_linear_tree()
        {
            BinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(1);
            newList.Add(2);
            newList.Add(3);
            newList.Add(4);
            newList.Add(5);
            newList.Add(6);

            int originalDepth = newList.Depth;
            Assert.AreEqual(6, originalDepth);

            newList.Balance();

            int newDepth = newList.Depth;
            Assert.AreEqual(3, newDepth);
        }

        [Test]
        public void minimum_returns_lowest_value()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(3);
            INode<int> minNode = newList.Add(1);
            newList.Add(5);
            newList.Add(2);
            newList.Add(6);
            newList.Add(4);

            INode<int> returnedNode = newList.Minimum();

            Assert.AreEqual(minNode, returnedNode);
        }

        [Test]
        public void maximum_returns_highest_value()
        {
            IBinarySearchTree<int> newList = new BinarySearchTree<int>();

            newList.Add(3);
            newList.Add(1);
            newList.Add(5);
            newList.Add(2);
            INode<int> maxNode = newList.Add(6);
            newList.Add(4);

            INode<int> returnedNode = newList.Maximum();

            Assert.AreEqual(maxNode, returnedNode);
        }
    }
}