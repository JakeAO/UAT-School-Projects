using System.Linq;
using NUnit.Framework;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph.Tests.GraphLoaders
{
    [TestFixture]
    public class GraphCopyLoaderTests
    {
        [Test]
        public void can_create()
        {
            IGraph<char, uint> graph = new Graph<char, uint>();

            IGraphLoader<char, uint> graphLoader = new GraphCopyLoader<char, uint>(graph);

            Assert.IsNotNull(graphLoader);
        }

        [Test]
        public void nodes_match_source()
        {
            IGraph<char, uint> graph = new Graph<char, uint>();
            graph.AddNode('A');
            graph.AddNode('B');
            graph.AddNode('C');

            IGraphLoader<char, uint> graphLoader = new GraphCopyLoader<char, uint>(graph);

            Assert.IsNotNull(graphLoader.GetNodes);
            Assert.AreEqual(graph.Nodes.Count, graphLoader.GetNodes.Count);
            foreach (var originalNode in graph.Nodes)
            {
                var correspondingNode = graphLoader.GetNodes.First(x => x.Value.Equals(originalNode.Value));
                Assert.IsNotNull(correspondingNode);
            }
        }

        [Test]
        public void edges_match_source()
        {
            IGraph<char, uint> graph = new Graph<char, uint>();
            var nodeA = graph.AddNode('A');
            var nodeB = graph.AddNode('B');
            var nodeC = graph.AddNode('C');
            graph.AddEdge(nodeA, nodeB, 100);
            graph.AddEdge(nodeB, nodeC, 100);
            graph.AddEdge(nodeC, nodeA, 100);

            IGraphLoader<char, uint> graphLoader = new GraphCopyLoader<char, uint>(graph);

            Assert.IsNotNull(graphLoader.GetEdges);
            Assert.AreEqual(graph.Edges.Count, graphLoader.GetEdges.Count);
            foreach (var originalEdge in graph.Edges)
            {
                var correspondingEdge = graphLoader.GetEdges.First(
                    x =>
                        x.From.Equals(originalEdge.From) &&
                        x.To.Equals(originalEdge.To) &&
                        x.Weight.Equals(originalEdge.Weight));
                Assert.IsNotNull(correspondingEdge);
            }
        }
    }
}