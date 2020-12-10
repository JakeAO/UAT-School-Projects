using NUnit.Framework;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void can_create()
        {
            const string VALUE = "A";

            INode<string> node = new Node<string>(VALUE);

            Assert.IsNotNull(node);
        }

        [Test]
        public void value_is_set()
        {
            const string VALUE = "A";
            
            INode<string> node = new Node<string>(VALUE);

            Assert.AreEqual(VALUE, node.Value);
        }
    }
}