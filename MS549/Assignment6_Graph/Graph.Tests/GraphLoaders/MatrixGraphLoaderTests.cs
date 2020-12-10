using System.Linq;
using NUnit.Framework;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph.Tests.GraphLoaders
{
    [TestFixture]
    public class MatrixGraphLoaderTests
    {
        [Test]
        public void can_create()
        {
            IGraphLoader<char, uint> graphLoader = new MatrixGraphLoader<char, uint>(new char[0], new uint[0, 0]);

            Assert.IsNotNull(graphLoader);
        }

        [Test]
        public void nodes_match_input()
        {
            IGraphLoader<char, uint> graphLoader = new MatrixGraphLoader<char, uint>(
                new[]
                {
                    'A',
                    'B',
                    'C'
                },
                new uint[,]
                {
                    {0, 100, 0},
                    {0, 0, 100},
                    {100, 0, 0},
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
            IGraphLoader<char, uint> graphLoader = new MatrixGraphLoader<char, uint>(
                new[]
                {
                    'A',
                    'B',
                    'C'
                },
                new uint[,]
                {
                    {default, 100, default},
                    {default, default, 100},
                    {100, default, default},
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