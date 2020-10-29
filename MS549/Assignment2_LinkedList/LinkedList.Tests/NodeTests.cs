using NUnit.Framework;

namespace SadPumpkin.LinkedList.Tests
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
        public void next_begins_null()
        {
            INode<int> newNode = new Node<int>(123);

            Assert.IsNull(newNode.Next);
        }

        [Test]
        public void previous_begins_null()
        {
            INode<int> newNode = new Node<int>(123);

            Assert.IsNull(newNode.Previous);
        }

        [Test]
        public void next_can_be_set()
        {
            Node<int> newNode = new Node<int>(1);
            INode<int> newNext = new Node<int>(2);

            newNode.Next = newNext;

            Assert.AreEqual(newNext, newNode.Next);
        }

        [Test]
        public void previous_can_be_set()
        {
            Node<int> newNode = new Node<int>(1);
            INode<int> newPrev = new Node<int>(0);

            newNode.Next = newPrev;

            Assert.AreEqual(newPrev, newNode.Next);
        }
    }
}