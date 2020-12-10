using System.Linq;
using NUnit.Framework;
using SadPumpkin.Graph;

namespace PerformanceTests
{
    [TestFixture]
    public class CustomGraphTests
    {
        private const char EXAMPLE_START = 's';
        private const char EXAMPLE_END = 't';
        private const string CAPITALS_START = Constants.ME;
        private const string CAPITALS_END = Constants.CA;

        [Test]
        public void can_create_empty()
        {
            var graph = new Graph<string, uint>();

            Assert.IsNotNull(graph);
        }

        [Test]
        public void can_create_example()
        {
            var graph = GetExampleGraph();

            Assert.IsNotNull(graph);
            Assert.AreEqual(11, graph.Nodes.Count);
            Assert.AreEqual(21, graph.Edges.Count);
        }

        [Test]
        public void example_graph_depth_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var result = graph.DepthFirstSearch(EXAMPLE_START, EXAMPLE_END);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.AreEqual(EXAMPLE_START, arrayResult.First().Value);
            Assert.AreEqual(EXAMPLE_END, arrayResult.Last().Value);
            Assert.Pass(string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        [Test]
        public void example_graph_breadth_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var result = graph.BreadthFirstSearch(EXAMPLE_START, EXAMPLE_END);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.AreEqual(EXAMPLE_START, arrayResult.First().Value);
            Assert.AreEqual(EXAMPLE_END, arrayResult.Last().Value);
            Assert.Pass(string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        [Test]
        public void example_graph_dijkstra_search()
        {
            var graph = GetExampleGraph();
            Assert.IsNotNull(graph);

            var result = graph.GetPath(
                graph.Nodes.First(x => x.Value.Equals(EXAMPLE_START)),
                graph.Nodes.First(x => x.Value.Equals(EXAMPLE_END)),
                out uint pathWeight);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Value.Equals(EXAMPLE_START));
            Assert.IsTrue(arrayResult.Last().Value.Equals(EXAMPLE_END));
            Assert.Pass($"Weight: {pathWeight}\n" + string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        [Test]
        public void can_create_capitals()
        {
            var graph = GetStateCapitalGraph();

            Assert.IsNotNull(graph);
            Assert.AreEqual(48, graph.Nodes.Count);
            Assert.AreEqual(206, graph.Edges.Count);
        }

        [Test]
        public void capitals_graph_depth_search()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var result = graph.DepthFirstSearch(CAPITALS_START, CAPITALS_END);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.AreEqual(CAPITALS_START, arrayResult.First().Value);
            Assert.AreEqual(CAPITALS_END, arrayResult.Last().Value);
            Assert.Pass(string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        [Test]
        public void capitals_graph_breadth_traverse()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var result = graph.BreadthFirstSearch(CAPITALS_START, CAPITALS_END);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.AreEqual(CAPITALS_START, arrayResult.First().Value);
            Assert.AreEqual(CAPITALS_END, arrayResult.Last().Value);
            Assert.Pass(string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        [Test]
        public void capitals_graph_dijkstra_search()
        {
            var graph = GetStateCapitalGraph();
            Assert.IsNotNull(graph);

            var result = graph.GetPath(
                graph.Nodes.First(x => x.Value.Equals(CAPITALS_START)),
                graph.Nodes.First(x => x.Value.Equals(CAPITALS_END)),
                out uint pathWeight);
            Assert.NotNull(result);

            var arrayResult = result.ToArray();
            Assert.IsNotNull(arrayResult);
            Assert.IsTrue(arrayResult.First().Value.Equals(CAPITALS_START));
            Assert.IsTrue(arrayResult.Last().Value.Equals(CAPITALS_END));
            Assert.Pass($"Weight: {pathWeight}\n" + string.Join("->\n", arrayResult.Select(x => x.Value)));
        }

        private static Graph<char, uint> GetExampleGraph()
        {
            Graph<char, uint> graph = new Graph<char, uint>();
            graph.AddNode('s');
            graph.AddNode('A');
            graph.AddNode('B');
            graph.AddNode('C');
            graph.AddNode('D');
            graph.AddNode('E');
            graph.AddNode('F');
            graph.AddNode('G');
            graph.AddNode('H');
            graph.AddNode('I');
            graph.AddNode('t');
            graph.AddEdge('s', 'A', 1);
            graph.AddEdge('s', 'D', 4);
            graph.AddEdge('s', 'G', 6);
            graph.AddEdge('A', 'B', 2);
            graph.AddEdge('A', 'E', 2);
            graph.AddEdge('B', 'C', 2);
            graph.AddEdge('C', 't', 4);
            graph.AddEdge('D', 'A', 3);
            graph.AddEdge('D', 'E', 3);
            graph.AddEdge('E', 'C', 2);
            graph.AddEdge('E', 'F', 3);
            graph.AddEdge('E', 'I', 3);
            graph.AddEdge('F', 'C', 1);
            graph.AddEdge('F', 't', 3);
            graph.AddEdge('G', 'D', 2);
            graph.AddEdge('G', 'E', 1);
            graph.AddEdge('G', 'H', 6);
            graph.AddEdge('H', 'E', 2);
            graph.AddEdge('H', 'I', 6);
            graph.AddEdge('I', 'F', 1);
            graph.AddEdge('I', 't', 4);
            return graph;
        }

        private static Graph<string, uint> GetStateCapitalGraph()
        {
            Graph<string, uint> graph = new Graph<string, uint>();

            // State Capitals
            graph.AddNode(Constants.WA);
            graph.AddNode(Constants.OR);
            graph.AddNode(Constants.CA);
            graph.AddNode(Constants.ID);
            graph.AddNode(Constants.NV);
            graph.AddNode(Constants.UT);
            graph.AddNode(Constants.AZ);
            graph.AddNode(Constants.MT);
            graph.AddNode(Constants.WY);
            graph.AddNode(Constants.CO);
            graph.AddNode(Constants.NM);
            graph.AddNode(Constants.ND);
            graph.AddNode(Constants.SD);
            graph.AddNode(Constants.NE);
            graph.AddNode(Constants.KS);
            graph.AddNode(Constants.OK);
            graph.AddNode(Constants.TX);
            graph.AddNode(Constants.MN);
            graph.AddNode(Constants.IA);
            graph.AddNode(Constants.MO);
            graph.AddNode(Constants.AR);
            graph.AddNode(Constants.LA);
            graph.AddNode(Constants.WI);
            graph.AddNode(Constants.IL);
            graph.AddNode(Constants.MS);
            graph.AddNode(Constants.MI);
            graph.AddNode(Constants.IN);
            graph.AddNode(Constants.KY);
            graph.AddNode(Constants.TN);
            graph.AddNode(Constants.AL);
            graph.AddNode(Constants.OH);
            graph.AddNode(Constants.GA);
            graph.AddNode(Constants.FL);
            graph.AddNode(Constants.SC);
            graph.AddNode(Constants.NC);
            graph.AddNode(Constants.VA);
            graph.AddNode(Constants.WV);
            graph.AddNode(Constants.MD);
            graph.AddNode(Constants.DE);
            graph.AddNode(Constants.PA);
            graph.AddNode(Constants.NJ);
            graph.AddNode(Constants.NY);
            graph.AddNode(Constants.CT);
            graph.AddNode(Constants.RI);
            graph.AddNode(Constants.MA);
            graph.AddNode(Constants.VT);
            graph.AddNode(Constants.NH);
            graph.AddNode(Constants.ME);

            // Land Routes Between State Capitals not Crossing into another State
            // Notable Exceptions:
            //   - 'Four Corners': you can't gross diagonally w/o entering one of the other adjacent states.
            //   - MO <-> KY: There one river crossing dips into TN, so it doesn't count.
            graph.AddEdge(Constants.WA, Constants.OR, 154);
            graph.AddEdge(Constants.WA, Constants.ID, 627);
            graph.AddEdge(Constants.OR, Constants.WA, 154);
            graph.AddEdge(Constants.OR, Constants.ID, 476);
            graph.AddEdge(Constants.OR, Constants.CA, 536);
            graph.AddEdge(Constants.OR, Constants.NV, 675);
            graph.AddEdge(Constants.CA, Constants.OR, 536);
            graph.AddEdge(Constants.CA, Constants.NV, 130);
            graph.AddEdge(Constants.CA, Constants.AZ, 756);
            graph.AddEdge(Constants.ID, Constants.WA, 627);
            graph.AddEdge(Constants.ID, Constants.OR, 476);
            graph.AddEdge(Constants.ID, Constants.NV, 551);
            graph.AddEdge(Constants.ID, Constants.MT, 488);
            graph.AddEdge(Constants.ID, Constants.WY, 794);
            graph.AddEdge(Constants.ID, Constants.UT, 339);
            graph.AddEdge(Constants.NV, Constants.OR, 675);
            graph.AddEdge(Constants.NV, Constants.ID, 551);
            graph.AddEdge(Constants.NV, Constants.UT, 546);
            graph.AddEdge(Constants.NV, Constants.AZ, 732);
            graph.AddEdge(Constants.NV, Constants.CA, 130);
            graph.AddEdge(Constants.UT, Constants.NV, 546);
            graph.AddEdge(Constants.UT, Constants.ID, 339);
            graph.AddEdge(Constants.UT, Constants.WY, 440);
            graph.AddEdge(Constants.UT, Constants.CO, 518);
            graph.AddEdge(Constants.UT, Constants.AZ, 663);
            graph.AddEdge(Constants.AZ, Constants.CA, 756);
            graph.AddEdge(Constants.AZ, Constants.NV, 732);
            graph.AddEdge(Constants.AZ, Constants.UT, 663);
            graph.AddEdge(Constants.AZ, Constants.NM, 481);
            graph.AddEdge(Constants.MT, Constants.ID, 488);
            graph.AddEdge(Constants.MT, Constants.WY, 687);
            graph.AddEdge(Constants.MT, Constants.ND, 613);
            graph.AddEdge(Constants.MT, Constants.SD, 803);
            graph.AddEdge(Constants.WY, Constants.ID, 794);
            graph.AddEdge(Constants.WY, Constants.MT, 687);
            graph.AddEdge(Constants.WY, Constants.SD, 464);
            graph.AddEdge(Constants.WY, Constants.NE, 442);
            graph.AddEdge(Constants.WY, Constants.CO, 100);
            graph.AddEdge(Constants.WY, Constants.UT, 440);
            graph.AddEdge(Constants.CO, Constants.UT, 518);
            graph.AddEdge(Constants.CO, Constants.WY, 100);
            graph.AddEdge(Constants.CO, Constants.NE, 484);
            graph.AddEdge(Constants.CO, Constants.KS, 541);
            graph.AddEdge(Constants.CO, Constants.OK, 630);
            graph.AddEdge(Constants.CO, Constants.NM, 358);
            graph.AddEdge(Constants.NM, Constants.AZ, 481);
            graph.AddEdge(Constants.NM, Constants.CO, 358);
            graph.AddEdge(Constants.NM, Constants.OK, 586);
            graph.AddEdge(Constants.NM, Constants.TX, 688);
            graph.AddEdge(Constants.ND, Constants.MT, 613);
            graph.AddEdge(Constants.ND, Constants.SD, 210);
            graph.AddEdge(Constants.ND, Constants.MN, 435);
            graph.AddEdge(Constants.SD, Constants.MT, 803);
            graph.AddEdge(Constants.SD, Constants.ND, 210);
            graph.AddEdge(Constants.SD, Constants.MN, 399);
            graph.AddEdge(Constants.SD, Constants.IA, 502);
            graph.AddEdge(Constants.SD, Constants.NE, 413);
            graph.AddEdge(Constants.SD, Constants.WY, 464);
            graph.AddEdge(Constants.NE, Constants.SD, 413);
            graph.AddEdge(Constants.NE, Constants.IA, 189);
            graph.AddEdge(Constants.NE, Constants.MO, 356);
            graph.AddEdge(Constants.NE, Constants.KS, 168);
            graph.AddEdge(Constants.NE, Constants.CO, 484);
            graph.AddEdge(Constants.NE, Constants.WY, 442);
            graph.AddEdge(Constants.KS, Constants.NE, 168);
            graph.AddEdge(Constants.KS, Constants.MO, 204);
            graph.AddEdge(Constants.KS, Constants.OK, 297);
            graph.AddEdge(Constants.KS, Constants.CO, 541);
            graph.AddEdge(Constants.OK, Constants.KS, 297);
            graph.AddEdge(Constants.OK, Constants.MO, 420);
            graph.AddEdge(Constants.OK, Constants.AR, 366);
            graph.AddEdge(Constants.OK, Constants.TX, 388);
            graph.AddEdge(Constants.OK, Constants.NM, 586);
            graph.AddEdge(Constants.OK, Constants.CO, 630);
            graph.AddEdge(Constants.TX, Constants.NM, 688);
            graph.AddEdge(Constants.TX, Constants.OK, 388);
            graph.AddEdge(Constants.TX, Constants.AR, 514);
            graph.AddEdge(Constants.TX, Constants.LA, 429);
            graph.AddEdge(Constants.MN, Constants.ND, 438);
            graph.AddEdge(Constants.MN, Constants.SD, 399);
            graph.AddEdge(Constants.MN, Constants.IA, 300);
            graph.AddEdge(Constants.MN, Constants.WI, 259);
            graph.AddEdge(Constants.IA, Constants.MN, 300);
            graph.AddEdge(Constants.IA, Constants.SD, 502);
            graph.AddEdge(Constants.IA, Constants.NE, 189);
            graph.AddEdge(Constants.IA, Constants.MO, 255);
            graph.AddEdge(Constants.IA, Constants.IL, 299);
            graph.AddEdge(Constants.IA, Constants.WI, 293);
            graph.AddEdge(Constants.MO, Constants.IA, 255);
            graph.AddEdge(Constants.MO, Constants.NE, 356);
            graph.AddEdge(Constants.MO, Constants.KS, 204);
            graph.AddEdge(Constants.MO, Constants.OK, 297);
            graph.AddEdge(Constants.MO, Constants.AR, 344);
            graph.AddEdge(Constants.MO, Constants.TN, 509);
            graph.AddEdge(Constants.MO, Constants.IL, 192);
            graph.AddEdge(Constants.AR, Constants.MO, 344);
            graph.AddEdge(Constants.AR, Constants.OK, 366);
            graph.AddEdge(Constants.AR, Constants.TX, 514);
            graph.AddEdge(Constants.AR, Constants.LA, 361);
            graph.AddEdge(Constants.AR, Constants.MS, 267);
            graph.AddEdge(Constants.AR, Constants.TN, 349);
            graph.AddEdge(Constants.LA, Constants.TX, 429);
            graph.AddEdge(Constants.LA, Constants.AR, 361);
            graph.AddEdge(Constants.LA, Constants.MS, 160);
            graph.AddEdge(Constants.WI, Constants.MN, 259);
            graph.AddEdge(Constants.WI, Constants.IA, 293);
            graph.AddEdge(Constants.WI, Constants.IL, 265);
            graph.AddEdge(Constants.WI, Constants.MI, 619);
            graph.AddEdge(Constants.IL, Constants.WI, 265);
            graph.AddEdge(Constants.IL, Constants.IA, 299);
            graph.AddEdge(Constants.IL, Constants.MO, 192);
            graph.AddEdge(Constants.IL, Constants.KY, 376);
            graph.AddEdge(Constants.IL, Constants.IN, 209);
            graph.AddEdge(Constants.MS, Constants.LA, 160);
            graph.AddEdge(Constants.MS, Constants.AR, 267);
            graph.AddEdge(Constants.MS, Constants.TN, 415);
            graph.AddEdge(Constants.MS, Constants.AL, 243);
            graph.AddEdge(Constants.MI, Constants.WI, 619);
            graph.AddEdge(Constants.MI, Constants.IN, 255);
            graph.AddEdge(Constants.MI, Constants.OH, 254);
            graph.AddEdge(Constants.IN, Constants.MI, 255);
            graph.AddEdge(Constants.IN, Constants.IL, 209);
            graph.AddEdge(Constants.IN, Constants.KY, 166);
            graph.AddEdge(Constants.IN, Constants.OH, 175);
            graph.AddEdge(Constants.KY, Constants.IN, 166);
            graph.AddEdge(Constants.KY, Constants.IL, 376);
            graph.AddEdge(Constants.KY, Constants.TN, 210);
            graph.AddEdge(Constants.KY, Constants.VA, 557);
            graph.AddEdge(Constants.KY, Constants.WV, 197);
            graph.AddEdge(Constants.KY, Constants.OH, 186);
            graph.AddEdge(Constants.TN, Constants.KY, 210);
            graph.AddEdge(Constants.TN, Constants.MO, 509);
            graph.AddEdge(Constants.TN, Constants.AR, 349);
            graph.AddEdge(Constants.TN, Constants.MS, 415);
            graph.AddEdge(Constants.TN, Constants.AL, 280);
            graph.AddEdge(Constants.TN, Constants.GA, 248);
            graph.AddEdge(Constants.TN, Constants.NC, 539);
            graph.AddEdge(Constants.TN, Constants.VA, 614);
            graph.AddEdge(Constants.AL, Constants.MS, 243);
            graph.AddEdge(Constants.AL, Constants.TN, 280);
            graph.AddEdge(Constants.AL, Constants.GA, 161);
            graph.AddEdge(Constants.AL, Constants.FL, 211);
            graph.AddEdge(Constants.OH, Constants.MI, 254);
            graph.AddEdge(Constants.OH, Constants.IN, 175);
            graph.AddEdge(Constants.OH, Constants.KY, 186);
            graph.AddEdge(Constants.OH, Constants.WV, 162);
            graph.AddEdge(Constants.OH, Constants.PA, 422);
            graph.AddEdge(Constants.GA, Constants.AL, 161);
            graph.AddEdge(Constants.GA, Constants.TN, 248);
            graph.AddEdge(Constants.GA, Constants.SC, 212);
            graph.AddEdge(Constants.GA, Constants.FL, 273);
            graph.AddEdge(Constants.FL, Constants.AL, 211);
            graph.AddEdge(Constants.FL, Constants.GA, 273);
            graph.AddEdge(Constants.SC, Constants.GA, 212);
            graph.AddEdge(Constants.SC, Constants.NC, 227);
            graph.AddEdge(Constants.NC, Constants.SC, 227);
            graph.AddEdge(Constants.NC, Constants.TN, 539);
            graph.AddEdge(Constants.NC, Constants.VA, 170);
            graph.AddEdge(Constants.VA, Constants.NC, 170);
            graph.AddEdge(Constants.VA, Constants.TN, 614);
            graph.AddEdge(Constants.VA, Constants.KY, 557);
            graph.AddEdge(Constants.VA, Constants.WV, 317);
            graph.AddEdge(Constants.VA, Constants.MD, 143);
            graph.AddEdge(Constants.WV, Constants.OH, 162);
            graph.AddEdge(Constants.WV, Constants.PA, 364);
            graph.AddEdge(Constants.WV, Constants.MD, 386);
            graph.AddEdge(Constants.WV, Constants.VA, 317);
            graph.AddEdge(Constants.WV, Constants.KY, 197);
            graph.AddEdge(Constants.MD, Constants.VA, 143);
            graph.AddEdge(Constants.MD, Constants.WV, 386);
            graph.AddEdge(Constants.MD, Constants.PA, 112);
            graph.AddEdge(Constants.MD, Constants.DE, 64);
            graph.AddEdge(Constants.DE, Constants.MD, 64);
            graph.AddEdge(Constants.DE, Constants.PA, 129);
            graph.AddEdge(Constants.DE, Constants.NJ, 117);
            graph.AddEdge(Constants.PA, Constants.OH, 422);
            graph.AddEdge(Constants.PA, Constants.WV, 364);
            graph.AddEdge(Constants.PA, Constants.MD, 112);
            graph.AddEdge(Constants.PA, Constants.DE, 129);
            graph.AddEdge(Constants.PA, Constants.NJ, 127);
            graph.AddEdge(Constants.PA, Constants.NY, 298);
            graph.AddEdge(Constants.NJ, Constants.DE, 117);
            graph.AddEdge(Constants.NJ, Constants.PA, 127);
            graph.AddEdge(Constants.NJ, Constants.NY, 205);
            graph.AddEdge(Constants.NY, Constants.PA, 298);
            graph.AddEdge(Constants.NY, Constants.NJ, 205);
            graph.AddEdge(Constants.NY, Constants.CT, 122);
            graph.AddEdge(Constants.NY, Constants.MA, 170);
            graph.AddEdge(Constants.NY, Constants.VT, 158);
            graph.AddEdge(Constants.CT, Constants.NY, 122);
            graph.AddEdge(Constants.CT, Constants.MA, 101);
            graph.AddEdge(Constants.CT, Constants.RI, 86);
            graph.AddEdge(Constants.RI, Constants.CT, 86);
            graph.AddEdge(Constants.RI, Constants.MA, 50);
            graph.AddEdge(Constants.MA, Constants.CT, 101);
            graph.AddEdge(Constants.MA, Constants.RI, 50);
            graph.AddEdge(Constants.MA, Constants.NY, 170);
            graph.AddEdge(Constants.MA, Constants.VT, 225);
            graph.AddEdge(Constants.MA, Constants.NH, 67);
            graph.AddEdge(Constants.VT, Constants.NY, 158);
            graph.AddEdge(Constants.VT, Constants.MA, 225);
            graph.AddEdge(Constants.VT, Constants.NH, 118);
            graph.AddEdge(Constants.NH, Constants.VT, 118);
            graph.AddEdge(Constants.NH, Constants.MA, 67);
            graph.AddEdge(Constants.NH, Constants.ME, 164);
            graph.AddEdge(Constants.ME, Constants.NH, 164);

            return graph;
        }
    }
}