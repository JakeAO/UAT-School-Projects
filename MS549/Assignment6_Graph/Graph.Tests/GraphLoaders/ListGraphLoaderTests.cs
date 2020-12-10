using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph.Tests.GraphLoaders
{
    [TestFixture]
    public class ListGraphLoaderTests
    {
        [Test]
        public void can_create()
        {
            IGraphLoader<char, uint> graphLoader = new ListGraphLoader<char, uint>(new Dictionary<char, IReadOnlyCollection<(char Value, uint Weight)>>());

            Assert.IsNotNull(graphLoader);
        }

        [Test]
        public void nodes_match_input()
        {
            IGraphLoader<char, uint> graphLoader = new ListGraphLoader<char, uint>(new Dictionary<char, IReadOnlyCollection<(char Value, uint Weight)>>()
            {
                {'A', new (char Value, uint Weight)[] {('B', 100)}},
                {'B', new (char Value, uint Weight)[] {('C', 100)}},
                {'C', new (char Value, uint Weight)[] {('A', 100)}},
            });

            Assert.IsNotNull(graphLoader.GetNodes);
            Assert.AreEqual(3, graphLoader.GetNodes.Count);
            Assert.IsNotNull(graphLoader.GetNodes.First(x => x.Value.Equals('A')));
            Assert.IsNotNull(graphLoader.GetNodes.First(x => x.Value.Equals('B')));
            Assert.IsNotNull(graphLoader.GetNodes.First(x => x.Value.Equals('C')));
        }

        [Test]
        public void edges_match_input()
        {
            IGraphLoader<char, uint> graphLoader = new ListGraphLoader<char, uint>(new Dictionary<char, IReadOnlyCollection<(char Value, uint Weight)>>()
            {
                {'A', new (char Value, uint Weight)[] {('B', 100)}},
                {'B', new (char Value, uint Weight)[] {('C', 100)}},
                {'C', new (char Value, uint Weight)[] {('A', 100)}},
            });

            Assert.IsNotNull(graphLoader.GetEdges);
            Assert.AreEqual(3, graphLoader.GetEdges.Count);
            Assert.IsNotNull(graphLoader.GetEdges.First(x =>
                x.From.Value.Equals('A') &&
                x.To.Value.Equals('B') &&
                x.Weight.Equals(100)));
            Assert.IsNotNull(graphLoader.GetEdges.First(x =>
                x.From.Value.Equals('B') &&
                x.To.Value.Equals('C') &&
                x.Weight.Equals(100)));
            Assert.IsNotNull(graphLoader.GetEdges.First(x =>
                x.From.Value.Equals('C') &&
                x.To.Value.Equals('A') &&
                x.Weight.Equals(100)));
        }
    }
}