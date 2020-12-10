using NUnit.Framework;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.Tests
{
    [TestFixture]
    public class EdgeTests
    {
        [Test]
        public void can_create()
        {
            const uint WEIGHT = 100;

            INode<string> nodeA = new Node<string>("A");
            INode<string> nodeB = new Node<string>("B");

            IEdge<string, uint> edge = new Edge<string, uint>(nodeA, nodeB, WEIGHT);

            Assert.IsNotNull(edge);
        }

        [Test]
        public void weight_is_set()
        {
            const uint WEIGHT = 100;

            INode<string> nodeA = new Node<string>("A");
            INode<string> nodeB = new Node<string>("B");

            IEdge<string, uint> edge = new Edge<string, uint>(nodeA, nodeB, WEIGHT);

            Assert.AreEqual(WEIGHT, edge.Weight);
        }

        [Test]
        public void from_is_set()
        {
            const uint WEIGHT = 100;

            INode<string> nodeA = new Node<string>("A");
            INode<string> nodeB = new Node<string>("B");

            IEdge<string, uint> edge = new Edge<string, uint>(nodeA, nodeB, WEIGHT);

            Assert.AreEqual(nodeA, edge.From);
        }

        [Test]
        public void to_is_set()
        {
            const uint WEIGHT = 100;

            INode<string> nodeA = new Node<string>("A");
            INode<string> nodeB = new Node<string>("B");

            IEdge<string, uint> edge = new Edge<string, uint>(nodeA, nodeB, WEIGHT);

            Assert.AreEqual(nodeB, edge.To);
        }
    }
}