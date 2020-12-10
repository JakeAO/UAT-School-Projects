using NUnit.Framework;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph.Tests.GraphLoaders
{
    [TestFixture]
    public class NullGraphLoaderTests
    {
        [Test]
        public void can_create()
        {
            IGraphLoader<char, uint> graphLoader = new NullGraphLoader<char, uint>();
            
            Assert.NotNull(graphLoader);
        }

        [Test]
        public void nodes_empty()
        {
            IGraphLoader<char, uint> graphLoader = new NullGraphLoader<char, uint>();

            Assert.NotNull(graphLoader.GetNodes);
            Assert.IsEmpty(graphLoader.GetNodes);
        }

        [Test]
        public void edges_empty()
        {
            IGraphLoader<char, uint> graphLoader = new NullGraphLoader<char, uint>();

            Assert.NotNull(graphLoader.GetEdges);
            Assert.IsEmpty(graphLoader.GetEdges);
        }
    }
}