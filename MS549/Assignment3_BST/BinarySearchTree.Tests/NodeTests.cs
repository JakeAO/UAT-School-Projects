using NUnit.Framework;

namespace SadPumpkin.BST.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void can_create_new()
        {
            INode<int> newNode = new Node<int>(0);

            Assert.IsNotNull(newNode);
        }

        [Test]
        public void value_matches_param()
        {
            INode<int> newNode = new Node<int>(123);

            Assert.AreEqual(123, newNode.Value);
        }

        [Test]
        public void left_begins_null()
        {
            INode<int> newNode = new Node<int>(123);

            Assert.IsNull(newNode.LeftChild);
        }

        [Test]
        public void right_begins_null()
        {
            INode<int> newNode = new Node<int>(123);

            Assert.IsNull(newNode.RightChild);
        }

        [Test]
        public void left_can_be_set()
        {
            Node<int> newNode = new Node<int>(1);
            INode<int> newNext = new Node<int>(2);

            newNode.LeftChild = newNext;

            Assert.AreEqual(newNext, newNode.LeftChild);
        }

        [Test]
        public void right_can_be_set()
        {
            Node<int> newNode = new Node<int>(1);
            INode<int> newPrev = new Node<int>(0);

            newNode.RightChild = newPrev;

            Assert.AreEqual(newPrev, newNode.RightChild);
        }
    }
}