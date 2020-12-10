using System.Linq;
using NUnit.Framework;
using QuickGraph;
using QuickGraph.Algorithms;

namespace PerformanceTests
{
    [TestFixture]
    public class QuickGraphTests
    {
        private const char EXAMPLE_START = 's';
        private const char EXAMPLE_END = 't';
        private const string CAPITALS_START = Constants.ME;
        private const string CAPITALS_END = Constants.CA;
        
        private class WeightedEdge<T> : Edge<T>
        {
            public double Weight { get; }

            public WeightedEdge(T source, T target, double weight) : base(source, target)
            {
                Weight = weight;
            }
        }

        [Test]
        public void can_create_empty()
        {
            var graph = new AdjacencyGraph<char, WeightedEdge<char>>();

            Assert.IsNotNull(graph);
        }

        [Test]
        public void can_create_example()
        {
            var graph = GetExampleGraph();

            Assert.IsNotNull(graph);
            Assert.AreEqual(11, graph.VertexCount);
            Assert.AreEqual(21, graph.EdgeCount);
        }

        [Test]
        public void example_graph_depth_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var depthSearch = graph.TreeDepthFirstSearch(EXAMPLE_START);
            Assert.NotNull(depthSearch);

            bool success = depthSearch(EXAMPLE_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<char>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(EXAMPLE_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(EXAMPLE_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        [Test]
        public void example_graph_breadth_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var breadthSearch = graph.TreeBreadthFirstSearch(EXAMPLE_START);
            Assert.NotNull(breadthSearch);

            bool success = breadthSearch(EXAMPLE_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<char>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(EXAMPLE_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(EXAMPLE_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        [Test]
        public void example_graph_dijkstra_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var dijkstraSearch = graph.ShortestPathsDijkstra(edge => edge.Weight, EXAMPLE_START);
            Assert.NotNull(dijkstraSearch);

            bool success = dijkstraSearch(EXAMPLE_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<char>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(EXAMPLE_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(EXAMPLE_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        [Test]
        public void can_create_capitals()
        {
            var graph = GetStateCapitalGraph();

            Assert.IsNotNull(graph);
            Assert.AreEqual(48, graph.VertexCount);
            Assert.AreEqual(206, graph.EdgeCount);
        }

        [Test]
        public void capitals_graph_depth_search()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var depthSearch = graph.TreeDepthFirstSearch(CAPITALS_START);
            Assert.NotNull(depthSearch);

            bool success = depthSearch(CAPITALS_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<string>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(CAPITALS_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(CAPITALS_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        [Test]
        public void capitals_graph_breadth_search()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var breadthSearch = graph.TreeBreadthFirstSearch(CAPITALS_START);
            Assert.NotNull(breadthSearch);

            bool success = breadthSearch(CAPITALS_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<string>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(CAPITALS_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(CAPITALS_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        [Test]
        public void capitals_graph_dijkstra_search()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var dijkstraSearch = graph.ShortestPathsDijkstra(edge => edge.Weight, CAPITALS_START);
            Assert.NotNull(dijkstraSearch);

            bool success = dijkstraSearch(CAPITALS_END, out var result);
            Assert.IsTrue(success);

            WeightedEdge<string>[] arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Source.Equals(CAPITALS_START));
            Assert.IsTrue(arrayResult.Last().Target.Equals(CAPITALS_END));
            Assert.Pass(string.Join("\n", arrayResult.Select(x => x.ToString())));
        }

        private static AdjacencyGraph<char, WeightedEdge<char>> GetExampleGraph()
        {
            var graph = new AdjacencyGraph<char, WeightedEdge<char>>();
            graph.AddVertex('s');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');
            graph.AddVertex('I');
            graph.AddVertex('t');
            graph.AddEdge(new WeightedEdge<char>('s', 'A', 1));
            graph.AddEdge(new WeightedEdge<char>('s', 'D', 4));
            graph.AddEdge(new WeightedEdge<char>('s', 'G', 6));
            graph.AddEdge(new WeightedEdge<char>('A', 'B', 2));
            graph.AddEdge(new WeightedEdge<char>('A', 'E', 2));
            graph.AddEdge(new WeightedEdge<char>('B', 'C', 2));
            graph.AddEdge(new WeightedEdge<char>('C', 't', 4));
            graph.AddEdge(new WeightedEdge<char>('D', 'A', 3));
            graph.AddEdge(new WeightedEdge<char>('D', 'E', 3));
            graph.AddEdge(new WeightedEdge<char>('E', 'C', 2));
            graph.AddEdge(new WeightedEdge<char>('E', 'F', 3));
            graph.AddEdge(new WeightedEdge<char>('E', 'I', 3));
            graph.AddEdge(new WeightedEdge<char>('F', 'C', 1));
            graph.AddEdge(new WeightedEdge<char>('F', 't', 3));
            graph.AddEdge(new WeightedEdge<char>('G', 'D', 2));
            graph.AddEdge(new WeightedEdge<char>('G', 'E', 1));
            graph.AddEdge(new WeightedEdge<char>('G', 'H', 6));
            graph.AddEdge(new WeightedEdge<char>('H', 'E', 2));
            graph.AddEdge(new WeightedEdge<char>('H', 'I', 6));
            graph.AddEdge(new WeightedEdge<char>('I', 'F', 1));
            graph.AddEdge(new WeightedEdge<char>('I', 't', 4));
            return graph;
        }

        private static AdjacencyGraph<string, WeightedEdge<string>> GetStateCapitalGraph()
        {
            var graph = new AdjacencyGraph<string, WeightedEdge<string>>();

            // State Capitals
            graph.AddVertex(Constants.WA);
            graph.AddVertex(Constants.OR);
            graph.AddVertex(Constants.CA);
            graph.AddVertex(Constants.ID);
            graph.AddVertex(Constants.NV);
            graph.AddVertex(Constants.UT);
            graph.AddVertex(Constants.AZ);
            graph.AddVertex(Constants.MT);
            graph.AddVertex(Constants.WY);
            graph.AddVertex(Constants.CO);
            graph.AddVertex(Constants.NM);
            graph.AddVertex(Constants.ND);
            graph.AddVertex(Constants.SD);
            graph.AddVertex(Constants.NE);
            graph.AddVertex(Constants.KS);
            graph.AddVertex(Constants.OK);
            graph.AddVertex(Constants.TX);
            graph.AddVertex(Constants.MN);
            graph.AddVertex(Constants.IA);
            graph.AddVertex(Constants.MO);
            graph.AddVertex(Constants.AR);
            graph.AddVertex(Constants.LA);
            graph.AddVertex(Constants.WI);
            graph.AddVertex(Constants.IL);
            graph.AddVertex(Constants.MS);
            graph.AddVertex(Constants.MI);
            graph.AddVertex(Constants.IN);
            graph.AddVertex(Constants.KY);
            graph.AddVertex(Constants.TN);
            graph.AddVertex(Constants.AL);
            graph.AddVertex(Constants.OH);
            graph.AddVertex(Constants.GA);
            graph.AddVertex(Constants.FL);
            graph.AddVertex(Constants.SC);
            graph.AddVertex(Constants.NC);
            graph.AddVertex(Constants.VA);
            graph.AddVertex(Constants.WV);
            graph.AddVertex(Constants.MD);
            graph.AddVertex(Constants.DE);
            graph.AddVertex(Constants.PA);
            graph.AddVertex(Constants.NJ);
            graph.AddVertex(Constants.NY);
            graph.AddVertex(Constants.CT);
            graph.AddVertex(Constants.RI);
            graph.AddVertex(Constants.MA);
            graph.AddVertex(Constants.VT);
            graph.AddVertex(Constants.NH);
            graph.AddVertex(Constants.ME);

            // Land Routes Between State Capitals not Crossing into another State
            // Notable Exceptions:
            //   - 'Four Corners': you can't gross diagonally w/o entering one of the other adjacent states.
            //   - MO <-> KY: There one river crossing dips into TN, so it doesn't count.
            graph.AddEdge(new WeightedEdge<string>(Constants.WA, Constants.OR, 154));
            graph.AddEdge(new WeightedEdge<string>(Constants.WA, Constants.ID, 627));
            graph.AddEdge(new WeightedEdge<string>(Constants.OR, Constants.WA, 154));
            graph.AddEdge(new WeightedEdge<string>(Constants.OR, Constants.ID, 476));
            graph.AddEdge(new WeightedEdge<string>(Constants.OR, Constants.CA, 536));
            graph.AddEdge(new WeightedEdge<string>(Constants.OR, Constants.NV, 675));
            graph.AddEdge(new WeightedEdge<string>(Constants.CA, Constants.OR, 536));
            graph.AddEdge(new WeightedEdge<string>(Constants.CA, Constants.NV, 130));
            graph.AddEdge(new WeightedEdge<string>(Constants.CA, Constants.AZ, 756));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.WA, 627));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.OR, 476));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.NV, 551));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.MT, 488));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.WY, 794));
            graph.AddEdge(new WeightedEdge<string>(Constants.ID, Constants.UT, 339));
            graph.AddEdge(new WeightedEdge<string>(Constants.NV, Constants.OR, 675));
            graph.AddEdge(new WeightedEdge<string>(Constants.NV, Constants.ID, 551));
            graph.AddEdge(new WeightedEdge<string>(Constants.NV, Constants.UT, 546));
            graph.AddEdge(new WeightedEdge<string>(Constants.NV, Constants.AZ, 732));
            graph.AddEdge(new WeightedEdge<string>(Constants.NV, Constants.CA, 130));
            graph.AddEdge(new WeightedEdge<string>(Constants.UT, Constants.NV, 546));
            graph.AddEdge(new WeightedEdge<string>(Constants.UT, Constants.ID, 339));
            graph.AddEdge(new WeightedEdge<string>(Constants.UT, Constants.WY, 440));
            graph.AddEdge(new WeightedEdge<string>(Constants.UT, Constants.CO, 518));
            graph.AddEdge(new WeightedEdge<string>(Constants.UT, Constants.AZ, 663));
            graph.AddEdge(new WeightedEdge<string>(Constants.AZ, Constants.CA, 756));
            graph.AddEdge(new WeightedEdge<string>(Constants.AZ, Constants.NV, 732));
            graph.AddEdge(new WeightedEdge<string>(Constants.AZ, Constants.UT, 663));
            graph.AddEdge(new WeightedEdge<string>(Constants.AZ, Constants.NM, 481));
            graph.AddEdge(new WeightedEdge<string>(Constants.MT, Constants.ID, 488));
            graph.AddEdge(new WeightedEdge<string>(Constants.MT, Constants.WY, 687));
            graph.AddEdge(new WeightedEdge<string>(Constants.MT, Constants.ND, 613));
            graph.AddEdge(new WeightedEdge<string>(Constants.MT, Constants.SD, 803));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.ID, 794));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.MT, 687));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.SD, 464));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.NE, 442));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.CO, 100));
            graph.AddEdge(new WeightedEdge<string>(Constants.WY, Constants.UT, 440));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.UT, 518));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.WY, 100));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.NE, 484));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.KS, 541));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.OK, 630));
            graph.AddEdge(new WeightedEdge<string>(Constants.CO, Constants.NM, 358));
            graph.AddEdge(new WeightedEdge<string>(Constants.NM, Constants.AZ, 481));
            graph.AddEdge(new WeightedEdge<string>(Constants.NM, Constants.CO, 358));
            graph.AddEdge(new WeightedEdge<string>(Constants.NM, Constants.OK, 586));
            graph.AddEdge(new WeightedEdge<string>(Constants.NM, Constants.TX, 688));
            graph.AddEdge(new WeightedEdge<string>(Constants.ND, Constants.MT, 613));
            graph.AddEdge(new WeightedEdge<string>(Constants.ND, Constants.SD, 210));
            graph.AddEdge(new WeightedEdge<string>(Constants.ND, Constants.MN, 435));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.MT, 803));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.ND, 210));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.MN, 399));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.IA, 502));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.NE, 413));
            graph.AddEdge(new WeightedEdge<string>(Constants.SD, Constants.WY, 464));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.SD, 413));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.IA, 189));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.MO, 356));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.KS, 168));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.CO, 484));
            graph.AddEdge(new WeightedEdge<string>(Constants.NE, Constants.WY, 442));
            graph.AddEdge(new WeightedEdge<string>(Constants.KS, Constants.NE, 168));
            graph.AddEdge(new WeightedEdge<string>(Constants.KS, Constants.MO, 204));
            graph.AddEdge(new WeightedEdge<string>(Constants.KS, Constants.OK, 297));
            graph.AddEdge(new WeightedEdge<string>(Constants.KS, Constants.CO, 541));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.KS, 297));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.MO, 420));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.AR, 366));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.TX, 388));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.NM, 586));
            graph.AddEdge(new WeightedEdge<string>(Constants.OK, Constants.CO, 630));
            graph.AddEdge(new WeightedEdge<string>(Constants.TX, Constants.NM, 688));
            graph.AddEdge(new WeightedEdge<string>(Constants.TX, Constants.OK, 388));
            graph.AddEdge(new WeightedEdge<string>(Constants.TX, Constants.AR, 514));
            graph.AddEdge(new WeightedEdge<string>(Constants.TX, Constants.LA, 429));
            graph.AddEdge(new WeightedEdge<string>(Constants.MN, Constants.ND, 438));
            graph.AddEdge(new WeightedEdge<string>(Constants.MN, Constants.SD, 399));
            graph.AddEdge(new WeightedEdge<string>(Constants.MN, Constants.IA, 300));
            graph.AddEdge(new WeightedEdge<string>(Constants.MN, Constants.WI, 259));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.MN, 300));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.SD, 502));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.NE, 189));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.MO, 255));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.IL, 299));
            graph.AddEdge(new WeightedEdge<string>(Constants.IA, Constants.WI, 293));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.IA, 255));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.NE, 356));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.KS, 204));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.OK, 297));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.AR, 344));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.TN, 509));
            graph.AddEdge(new WeightedEdge<string>(Constants.MO, Constants.IL, 192));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.MO, 344));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.OK, 366));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.TX, 514));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.LA, 361));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.MS, 267));
            graph.AddEdge(new WeightedEdge<string>(Constants.AR, Constants.TN, 349));
            graph.AddEdge(new WeightedEdge<string>(Constants.LA, Constants.TX, 429));
            graph.AddEdge(new WeightedEdge<string>(Constants.LA, Constants.AR, 361));
            graph.AddEdge(new WeightedEdge<string>(Constants.LA, Constants.MS, 160));
            graph.AddEdge(new WeightedEdge<string>(Constants.WI, Constants.MN, 259));
            graph.AddEdge(new WeightedEdge<string>(Constants.WI, Constants.IA, 293));
            graph.AddEdge(new WeightedEdge<string>(Constants.WI, Constants.IL, 265));
            graph.AddEdge(new WeightedEdge<string>(Constants.WI, Constants.MI, 619));
            graph.AddEdge(new WeightedEdge<string>(Constants.IL, Constants.WI, 265));
            graph.AddEdge(new WeightedEdge<string>(Constants.IL, Constants.IA, 299));
            graph.AddEdge(new WeightedEdge<string>(Constants.IL, Constants.MO, 192));
            graph.AddEdge(new WeightedEdge<string>(Constants.IL, Constants.KY, 376));
            graph.AddEdge(new WeightedEdge<string>(Constants.IL, Constants.IN, 209));
            graph.AddEdge(new WeightedEdge<string>(Constants.MS, Constants.LA, 160));
            graph.AddEdge(new WeightedEdge<string>(Constants.MS, Constants.AR, 267));
            graph.AddEdge(new WeightedEdge<string>(Constants.MS, Constants.TN, 415));
            graph.AddEdge(new WeightedEdge<string>(Constants.MS, Constants.AL, 243));
            graph.AddEdge(new WeightedEdge<string>(Constants.MI, Constants.WI, 619));
            graph.AddEdge(new WeightedEdge<string>(Constants.MI, Constants.IN, 255));
            graph.AddEdge(new WeightedEdge<string>(Constants.MI, Constants.OH, 254));
            graph.AddEdge(new WeightedEdge<string>(Constants.IN, Constants.MI, 255));
            graph.AddEdge(new WeightedEdge<string>(Constants.IN, Constants.IL, 209));
            graph.AddEdge(new WeightedEdge<string>(Constants.IN, Constants.KY, 166));
            graph.AddEdge(new WeightedEdge<string>(Constants.IN, Constants.OH, 175));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.IN, 166));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.IL, 376));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.TN, 210));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.VA, 557));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.WV, 197));
            graph.AddEdge(new WeightedEdge<string>(Constants.KY, Constants.OH, 186));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.KY, 210));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.MO, 509));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.AR, 349));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.MS, 415));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.AL, 280));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.GA, 248));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.NC, 539));
            graph.AddEdge(new WeightedEdge<string>(Constants.TN, Constants.VA, 614));
            graph.AddEdge(new WeightedEdge<string>(Constants.AL, Constants.MS, 243));
            graph.AddEdge(new WeightedEdge<string>(Constants.AL, Constants.TN, 280));
            graph.AddEdge(new WeightedEdge<string>(Constants.AL, Constants.GA, 161));
            graph.AddEdge(new WeightedEdge<string>(Constants.AL, Constants.FL, 211));
            graph.AddEdge(new WeightedEdge<string>(Constants.OH, Constants.MI, 254));
            graph.AddEdge(new WeightedEdge<string>(Constants.OH, Constants.IN, 175));
            graph.AddEdge(new WeightedEdge<string>(Constants.OH, Constants.KY, 186));
            graph.AddEdge(new WeightedEdge<string>(Constants.OH, Constants.WV, 162));
            graph.AddEdge(new WeightedEdge<string>(Constants.OH, Constants.PA, 422));
            graph.AddEdge(new WeightedEdge<string>(Constants.GA, Constants.AL, 161));
            graph.AddEdge(new WeightedEdge<string>(Constants.GA, Constants.TN, 248));
            graph.AddEdge(new WeightedEdge<string>(Constants.GA, Constants.SC, 212));
            graph.AddEdge(new WeightedEdge<string>(Constants.GA, Constants.FL, 273));
            graph.AddEdge(new WeightedEdge<string>(Constants.FL, Constants.AL, 211));
            graph.AddEdge(new WeightedEdge<string>(Constants.FL, Constants.GA, 273));
            graph.AddEdge(new WeightedEdge<string>(Constants.SC, Constants.GA, 212));
            graph.AddEdge(new WeightedEdge<string>(Constants.SC, Constants.NC, 227));
            graph.AddEdge(new WeightedEdge<string>(Constants.NC, Constants.SC, 227));
            graph.AddEdge(new WeightedEdge<string>(Constants.NC, Constants.TN, 539));
            graph.AddEdge(new WeightedEdge<string>(Constants.NC, Constants.VA, 170));
            graph.AddEdge(new WeightedEdge<string>(Constants.VA, Constants.NC, 170));
            graph.AddEdge(new WeightedEdge<string>(Constants.VA, Constants.TN, 614));
            graph.AddEdge(new WeightedEdge<string>(Constants.VA, Constants.KY, 557));
            graph.AddEdge(new WeightedEdge<string>(Constants.VA, Constants.WV, 317));
            graph.AddEdge(new WeightedEdge<string>(Constants.VA, Constants.MD, 143));
            graph.AddEdge(new WeightedEdge<string>(Constants.WV, Constants.OH, 162));
            graph.AddEdge(new WeightedEdge<string>(Constants.WV, Constants.PA, 364));
            graph.AddEdge(new WeightedEdge<string>(Constants.WV, Constants.MD, 386));
            graph.AddEdge(new WeightedEdge<string>(Constants.WV, Constants.VA, 317));
            graph.AddEdge(new WeightedEdge<string>(Constants.WV, Constants.KY, 197));
            graph.AddEdge(new WeightedEdge<string>(Constants.MD, Constants.VA, 143));
            graph.AddEdge(new WeightedEdge<string>(Constants.MD, Constants.WV, 386));
            graph.AddEdge(new WeightedEdge<string>(Constants.MD, Constants.PA, 112));
            graph.AddEdge(new WeightedEdge<string>(Constants.MD, Constants.DE, 64));
            graph.AddEdge(new WeightedEdge<string>(Constants.DE, Constants.MD, 64));
            graph.AddEdge(new WeightedEdge<string>(Constants.DE, Constants.PA, 129));
            graph.AddEdge(new WeightedEdge<string>(Constants.DE, Constants.NJ, 117));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.OH, 422));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.WV, 364));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.MD, 112));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.DE, 129));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.NJ, 127));
            graph.AddEdge(new WeightedEdge<string>(Constants.PA, Constants.NY, 298));
            graph.AddEdge(new WeightedEdge<string>(Constants.NJ, Constants.DE, 117));
            graph.AddEdge(new WeightedEdge<string>(Constants.NJ, Constants.PA, 127));
            graph.AddEdge(new WeightedEdge<string>(Constants.NJ, Constants.NY, 205));
            graph.AddEdge(new WeightedEdge<string>(Constants.NY, Constants.PA, 298));
            graph.AddEdge(new WeightedEdge<string>(Constants.NY, Constants.NJ, 205));
            graph.AddEdge(new WeightedEdge<string>(Constants.NY, Constants.CT, 122));
            graph.AddEdge(new WeightedEdge<string>(Constants.NY, Constants.MA, 170));
            graph.AddEdge(new WeightedEdge<string>(Constants.NY, Constants.VT, 158));
            graph.AddEdge(new WeightedEdge<string>(Constants.CT, Constants.NY, 122));
            graph.AddEdge(new WeightedEdge<string>(Constants.CT, Constants.MA, 101));
            graph.AddEdge(new WeightedEdge<string>(Constants.CT, Constants.RI, 86));
            graph.AddEdge(new WeightedEdge<string>(Constants.RI, Constants.CT, 86));
            graph.AddEdge(new WeightedEdge<string>(Constants.RI, Constants.MA, 50));
            graph.AddEdge(new WeightedEdge<string>(Constants.MA, Constants.CT, 101));
            graph.AddEdge(new WeightedEdge<string>(Constants.MA, Constants.RI, 50));
            graph.AddEdge(new WeightedEdge<string>(Constants.MA, Constants.NY, 170));
            graph.AddEdge(new WeightedEdge<string>(Constants.MA, Constants.VT, 225));
            graph.AddEdge(new WeightedEdge<string>(Constants.MA, Constants.NH, 67));
            graph.AddEdge(new WeightedEdge<string>(Constants.VT, Constants.NY, 158));
            graph.AddEdge(new WeightedEdge<string>(Constants.VT, Constants.MA, 225));
            graph.AddEdge(new WeightedEdge<string>(Constants.VT, Constants.NH, 118));
            graph.AddEdge(new WeightedEdge<string>(Constants.NH, Constants.VT, 118));
            graph.AddEdge(new WeightedEdge<string>(Constants.NH, Constants.MA, 67));
            graph.AddEdge(new WeightedEdge<string>(Constants.NH, Constants.ME, 164));
            graph.AddEdge(new WeightedEdge<string>(Constants.ME, Constants.NH, 164));

            return graph;
        }
    }
}