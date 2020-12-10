using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SadPumpkin.Graph.Components;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph.Tests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void can_create()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            Assert.IsNotNull(graph);
        }

        [Test]
        public void can_create_from_loader()
        {
            ListGraphLoader<string, uint> graphLoader = new ListGraphLoader<string, uint>(
                new Dictionary<string, IReadOnlyCollection<(string Value, uint Weight)>>()
                {
                    {"s", new[] {("A", 1u), ("C", 4u), ("C", 6u)}},
                    {"A", new[] {("A", 1u), ("B", 4u), ("C", 6u)}},
                    {"B", new[] {("A", 1u), ("A", 4u), ("s", 6u)}},
                    {"C", new[] {("s", 1u), ("B", 4u), ("A", 6u)}},
                });
            Assert.IsNotNull(graphLoader);

            IGraph<string, uint> graph = new Graph<string, uint>(graphLoader);
            Assert.IsNotNull(graph);

            Assert.AreEqual(4, graph.Nodes.Count);
            Assert.AreEqual(12, graph.Edges.Count);
        }

        [Test]
        public void starts_empty()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            Assert.AreEqual(0, graph.Nodes.Count);
            Assert.AreEqual(0, graph.Edges.Count);
        }

        [Test]
        public void can_add_node()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("TEST");

            INode<string> retNode = graph.AddNode(node);

            Assert.AreEqual(1, graph.Nodes.Count);
            Assert.IsNotNull(graph.Nodes.First(x => x.Equals(node)));
            Assert.AreSame(node, retNode);
        }

        [Test]
        public void can_add_node_from_value()
        {
            const string NODE_VALUE = "TEST";

            IGraph<string, uint> graph = new Graph<string, uint>();

            INode<string> retNode = graph.AddNode(NODE_VALUE);

            Assert.AreEqual(1, graph.Nodes.Count);
            Assert.IsNotNull(graph.Nodes.First(x => x.Value.Equals(NODE_VALUE)));
            Assert.IsNotNull(retNode);
        }

        [Test]
        public void cannot_add_node_twice()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("TEST");

            INode<string> retNodeA = graph.AddNode(node);
            INode<string> retNodeB = graph.AddNode(node);

            Assert.AreEqual(1, graph.Nodes.Count);
            Assert.IsNotNull(retNodeA);
            Assert.IsNull(retNodeB);
        }

        [Test]
        public void can_add_duplicate_node_values()
        {
            const string VALUE = "TEST";

            IGraph<string, uint> graph = new Graph<string, uint>();

            INode<string> retNodeA = graph.AddNode(VALUE);
            INode<string> retNodeB = graph.AddNode(VALUE);

            Assert.AreEqual(2, graph.Nodes.Count);
            Assert.AreNotSame(retNodeA, retNodeB);
            Assert.IsNotNull(retNodeA);
            Assert.IsNotNull(retNodeB);
        }

        [Test]
        public void cannot_add_invalid_node()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            INode<string> retNode = graph.AddNode((INode<string>) null);

            Assert.AreEqual(0, graph.Nodes.Count);
            Assert.IsNull(retNode);
        }

        [Test]
        public void can_remove_node()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("TEST");

            graph.AddNode(node);

            Assert.AreEqual(1, graph.Nodes.Count);

            bool result = graph.RemoveNode(node);

            Assert.AreEqual(0, graph.Nodes.Count);
            Assert.IsTrue(result);
        }

        [Test]
        public void cannot_remove_invalid_node()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("TEST");

            bool result = graph.RemoveNode(node);

            Assert.AreEqual(0, graph.Nodes.Count);
            Assert.IsFalse(result);
        }

        [Test]
        public void can_add_edge()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> from = new Node<string>("A");
            INode<string> to = new Node<string>("Z");
            IEdge<string, uint> edge = new Edge<string, uint>(from, to, 100);

            IEdge<string, uint> retEdge = graph.AddEdge(edge);

            Assert.AreEqual(1, graph.Edges.Count);
            Assert.IsNotNull(graph.Edges.First(x => x.Equals(edge)));
            Assert.AreSame(edge, retEdge);
        }

        [Test]
        public void can_add_edge_twice()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> from = new Node<string>("A");
            INode<string> to = new Node<string>("Z");
            IEdge<string, uint> edge = new Edge<string, uint>(from, to, 100);

            IEdge<string, uint> retEdgeA = graph.AddEdge(edge);
            IEdge<string, uint> retEdgeB = graph.AddEdge(edge);

            Assert.AreEqual(1, graph.Edges.Count);
            Assert.IsNotNull(graph.Edges.First(x => x.Equals(edge)));
            Assert.AreNotSame(retEdgeA, retEdgeB);
            Assert.IsNotNull(retEdgeA);
            Assert.IsNull(retEdgeB);
        }

        [Test]
        public void can_add_edge_from_value()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> from = new Node<string>("A");
            INode<string> to = new Node<string>("Z");

            IEdge<string, uint> retEdge = graph.AddEdge(from, to, 100);

            Assert.AreEqual(1, graph.Edges.Count);
            Assert.IsNotNull(graph.Edges.First(x => x.Weight.Equals(100)));
            Assert.IsNotNull(retEdge);
            Assert.AreEqual(from, retEdge.From);
            Assert.AreEqual(to, retEdge.To);
            Assert.AreEqual(100, retEdge.Weight);
        }

        [Test]
        public void cannot_add_null_edge()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            IEdge<string, uint> retEdge = graph.AddEdge(null);

            Assert.AreEqual(0, graph.Edges.Count);
            Assert.IsNull(retEdge);
        }

        [Test]
        public void cannot_add_invalid_edge()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            IEdge<string, uint> retEdge = graph.AddEdge("A", "B", 100);

            Assert.AreEqual(0, graph.Edges.Count);
            Assert.IsNull(retEdge);
        }

        [Test]
        public void can_remove_edge()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> from = new Node<string>("A");
            INode<string> to = new Node<string>("Z");
            IEdge<string, uint> edge = new Edge<string, uint>(from, to, 100);

            graph.AddEdge(edge);

            Assert.AreEqual(1, graph.Edges.Count);

            bool result = graph.RemoveEdge(edge);

            Assert.AreEqual(0, graph.Edges.Count);
            Assert.IsTrue(result);
        }

        [Test]
        public void cannot_remove_invalid_edge()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> from = new Node<string>("A");
            INode<string> to = new Node<string>("Z");
            IEdge<string, uint> edge = new Edge<string, uint>(from, to, 100);

            bool result = graph.RemoveEdge(edge);

            Assert.AreEqual(0, graph.Edges.Count);
            Assert.IsFalse(result);
        }

        [Test]
        public void get_edges_returns_empty_when_invalid()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("A");

            var edges = graph.GetEdgesFromNode(node);

            Assert.IsNotNull(edges);
            Assert.IsEmpty(edges);
        }

        [Test]
        public void get_edges_returns_empty_when_null()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();

            var edges = graph.GetEdgesFromNode(null);

            Assert.IsNotNull(edges);
            Assert.IsEmpty(edges);
        }

        [Test]
        public void get_edges_returns_empty_when_no_edges()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> node = new Node<string>("A");

            graph.AddNode(node);

            var edges = graph.GetEdgesFromNode(node);

            Assert.IsNotNull(edges);
            Assert.IsEmpty(edges);
        }

        [Test]
        public void get_edges_returns_edges_when_valid()
        {
            IGraph<string, uint> graph = new Graph<string, uint>();
            INode<string> nodeA = new Node<string>("A");
            INode<string> nodeB = new Node<string>("B");
            IEdge<string, uint> edgeAB = new Edge<string, uint>(nodeA, nodeB, 100);
            IEdge<string, uint> edgeBA = new Edge<string, uint>(nodeB, nodeA, 50);
            IEdge<string, uint> edgeBB = new Edge<string, uint>(nodeB, nodeB, 10);

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);

            graph.AddEdge(edgeAB);
            graph.AddEdge(edgeBA);
            graph.AddEdge(edgeBB);

            Assert.AreEqual(2, graph.Nodes.Count);
            Assert.AreEqual(3, graph.Edges.Count);

            var edgesFromA = graph.GetEdgesFromNode(nodeA);
            var edgesFromB = graph.GetEdgesFromNode(nodeB);

            Assert.IsNotNull(edgesFromA);
            Assert.AreEqual(1, edgesFromA.Count);
            Assert.IsNotNull(edgesFromA.First(x => x.Equals(edgeAB)));

            Assert.IsNotNull(edgesFromB);
            Assert.AreEqual(2, edgesFromB.Count);
            Assert.IsNotNull(edgesFromB.First(x => x.Equals(edgeBA)));
            Assert.IsNotNull(edgesFromB.First(x => x.Equals(edgeBB)));
        }

        [Test]
        public void depth_first_traverse_from_null_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.DepthFirstTraverse(null);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(0, travArray.Length);
        }

        [Test]
        public void depth_first_traverse_from_invalid_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> zNode = new Node<char>('z');
            Assert.IsNotNull(zNode);

            var traversal = graph.BreadthFirstTraverse(zNode);
            Assert.IsNotNull(traversal);

            Assert.Throws<ArgumentException>(() => traversal.ToArray());
        }

        [Test]
        public void depth_first_traverse_from_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> sNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('s'));
            Assert.IsNotNull(sNode);

            var traversal = graph.DepthFirstTraverse(sNode);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(graph.Nodes.Count, travArray.Length);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
        }

        [Test]
        public void depth_first_search_from_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> sNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('s'));
            Assert.IsNotNull(sNode);

            INode<char> tNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('t'));
            Assert.IsNotNull(tNode);

            var traversal = graph.DepthFirstSearch(sNode, tNode);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
            
            Assert.IsTrue(travArray.First().Value.Equals('s'));
            Assert.IsTrue(travArray.Last().Value.Equals('t'));
        }

        [Test]
        public void depth_first_traverse_from_value()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.DepthFirstTraverse('s');
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(graph.Nodes.Count, travArray.Length);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
        }

        [Test]
        public void depth_first_search_from_value()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.DepthFirstSearch('s', 't');
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);

            Assert.IsTrue(travArray.First().Value.Equals('s'));
            Assert.IsTrue(travArray.Last().Value.Equals('t'));
        }

        [Test]
        public void breadth_first_traverse_from_null_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.BreadthFirstTraverse(null);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(0, travArray.Length);
        }

        [Test]
        public void breadth_first_traverse_from_invalid_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> zNode = new Node<char>('z');
            Assert.IsNotNull(zNode);

            var traversal = graph.BreadthFirstTraverse(zNode);
            Assert.IsNotNull(traversal);

            Assert.Throws<ArgumentException>(() => traversal.ToArray());
        }

        [Test]
        public void breadth_first_traverse_from_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> sNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('s'));
            Assert.IsNotNull(sNode);

            var traversal = graph.BreadthFirstTraverse(sNode);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(graph.Nodes.Count, travArray.Length);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
        }

        [Test]
        public void breadth_first_search_from_node()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> sNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('s'));
            Assert.IsNotNull(sNode);

            INode<char> tNode = graph.Nodes.FirstOrDefault(x => x.Value.Equals('t'));
            Assert.IsNotNull(tNode);

            var traversal = graph.BreadthFirstSearch(sNode, tNode);
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
            
            Assert.IsTrue(travArray.First().Value.Equals('s'));
            Assert.IsTrue(travArray.Last().Value.Equals('t'));
        }

        [Test]
        public void breadth_first_traverse_from_value()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.BreadthFirstTraverse('s');
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);
            Assert.AreEqual(graph.Nodes.Count, travArray.Length);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);
        }

        [Test]
        public void breadth_first_search_from_value()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            var traversal = graph.BreadthFirstSearch('s', 't');
            Assert.IsNotNull(traversal);

            var travArray = traversal.ToArray();
            Assert.IsNotNull(travArray);

            var uniqueTravArray = traversal.Distinct().ToArray();
            Assert.AreEqual(travArray.Length, uniqueTravArray.Length);

            Assert.IsTrue(travArray.First().Value.Equals('s'));
            Assert.IsTrue(travArray.Last().Value.Equals('t'));
        }

        [Test]
        public void get_path_returns_shortest_path()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> start = graph.Nodes.First(x => x.Value.Equals('s'));
            Assert.IsNotNull(start);

            INode<char> end = graph.Nodes.First(x => x.Value.Equals('t'));
            Assert.IsNotNull(end);

            IReadOnlyList<INode<char>> bestPath = graph.GetPath(start, end, out uint pathWeight);
            Assert.IsNotNull(bestPath);
            Assert.AreEqual(5, bestPath.Count);
            Assert.AreSame(start, bestPath[0]);
            Assert.AreSame(end, bestPath[4]);
            Assert.AreEqual(9, pathWeight);
        }

        [Test]
        public void get_path_returns_null_when_given_null()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            IReadOnlyList<INode<char>> bestPath = graph.GetPath(null, null, out uint pathWeight);
            Assert.IsNull(bestPath);
            Assert.AreEqual(0, pathWeight);
        }

        [Test]
        public void get_path_throws_when_given_invalid()
        {
            IGraph<char, uint> graph = GenerateExampleGraph();
            Assert.IsNotNull(graph);

            INode<char> invalidNode = new Node<char>('\0');

            Assert.Throws<ArgumentException>(() => graph.GetPath(invalidNode, invalidNode, out uint _));
        }

        private static IGraph<char, uint> GenerateExampleGraph()
        {
            IGraph<char, uint> graph = new Graph<char, uint>();
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
    }
}