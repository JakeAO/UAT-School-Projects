using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SadPumpkin.Graph.Components;
using SadPumpkin.Graph.GraphLoaders;

namespace SadPumpkin.Graph
{
    /// <summary>
    /// Implementation of a generic, weighted, and directional Graph.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public class Graph<TValue, TWeight> : IGraph<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        private static readonly IReadOnlyCollection<Edge<TValue, TWeight>> EMPTY_EDGES = new Edge<TValue, TWeight>[0];
        private static Func<TWeight, TWeight, TWeight> _weightAdder = null;

        private static TWeight AddWeight(TWeight a, TWeight b)
        {
            if (_weightAdder == null)
            {
                ParameterExpression paramA = Expression.Parameter(typeof(TWeight), "a");
                ParameterExpression paramB = Expression.Parameter(typeof(TWeight), "b");
                BinaryExpression add = Expression.Add(paramA, paramB);
                _weightAdder = Expression.Lambda<Func<TWeight, TWeight, TWeight>>(add, paramA, paramB).Compile();
            }

            return _weightAdder(a, b);
        }

        /// <summary>
        /// Collection of all nodes contained in the Graph.
        /// </summary>
        public IReadOnlyCollection<INode<TValue>> Nodes => _nodes;

        /// <summary>
        /// Collection of all edges contained in the Graph.
        /// </summary>
        public IReadOnlyCollection<IEdge<TValue, TWeight>> Edges => _edges;

        private readonly Dictionary<INode<TValue>, HashSet<IEdge<TValue, TWeight>>> _edgesByNode = new Dictionary<INode<TValue>, HashSet<IEdge<TValue, TWeight>>>(10);
        private readonly HashSet<INode<TValue>> _nodes = new HashSet<INode<TValue>>(10);
        private readonly HashSet<IEdge<TValue, TWeight>> _edges = new HashSet<IEdge<TValue, TWeight>>(10);

        /// <summary>
        /// Create a new, empty Graph.
        /// </summary>
        public Graph()
        {

        }

        /// <summary>
        /// Create a graph using the provided IGraphLoader.
        /// </summary>
        /// <param name="graphLoader">Loader from which to pull Graph data.</param>
        public Graph(IGraphLoader<TValue, TWeight> graphLoader)
        {
            _nodes.UnionWith(graphLoader.GetNodes);
            _edges.UnionWith(graphLoader.GetEdges);

            foreach (var node in _nodes)
            {
                if (node == null)
                    continue;

                _edgesByNode[node] = new HashSet<IEdge<TValue, TWeight>>(10);
                foreach (var edge in _edges)
                {
                    if (edge == null)
                        continue;

                    if (edge.From == node)
                        _edgesByNode[node].Add(edge);
                }
            }
        }

        /// <summary>
        /// Add a node to the Graph with the provided value.
        /// </summary>
        /// <param name="value">Value contained in the node.</param>
        /// <returns>Newly created node, or null if invalid.</returns>
        public INode<TValue> AddNode(TValue value) => AddNode(new Node<TValue>(value));

        /// <summary>
        /// Add the provided node to the Graph.
        /// </summary>
        /// <param name="node">Node to add.</param>
        /// <returns>Newly added node, or null if invalid.</returns>
        public INode<TValue> AddNode(INode<TValue> node)
        {
            if (node == null)
                return null;

            if (!_nodes.Add(node))
                return null;

            _edgesByNode[node] = new HashSet<IEdge<TValue, TWeight>>();
            return node;
        }

        /// <summary>
        /// Remove the provided node from the Graph.
        /// </summary>
        /// <param name="node">Node to remove.</param>
        /// <returns><c>True</c> if removed, otherwise <c>false</c>.</returns>
        public bool RemoveNode(INode<TValue> node) => _nodes.Remove(node);

        /// <summary>
        /// Add an edge to the Graph with the provided values.
        /// </summary>
        /// <param name="from">Value of the originating node.</param>
        /// <param name="to">Value of the terminating node.</param>
        /// <param name="weight">Weight of the edge.</param>
        /// <returns>Newly created edge, or null if invalid.</returns>
        public IEdge<TValue, TWeight> AddEdge(TValue from, TValue to, TWeight weight)
        {
            INode<TValue> fromNode = _nodes.FirstOrDefault(x => x.Value.Equals(from));
            INode<TValue> toNode = _nodes.FirstOrDefault(x => x.Value.Equals(to));
            if (fromNode == null || toNode == null)
                return null;

            return AddEdge(fromNode, toNode, weight);
        }

        /// <summary>
        /// Add an edge to the Graph with the provided values.
        /// </summary>
        /// <param name="from">Node which originates the edge.</param>
        /// <param name="to">Node which terminates the edge.</param>
        /// <param name="weight">Weight of the edge.</param>
        /// <returns>Newly created edge, or null if invalid.</returns>
        public IEdge<TValue, TWeight> AddEdge(INode<TValue> from, INode<TValue> to, TWeight weight) => AddEdge(new Edge<TValue, TWeight>(from, to, weight));

        /// <summary>
        /// Add the provided edge to the Graph.
        /// </summary>
        /// <param name="edge">Edge to add.</param>
        /// <returns>Newly added edge, or null if invalid.</returns>
        public IEdge<TValue, TWeight> AddEdge(IEdge<TValue, TWeight> edge)
        {
            if (edge == null)
                return null;

            if (!_edges.Add(edge))
                return null;

            if (!_edgesByNode.TryGetValue(edge.From, out var edgesFromNode))
                _edgesByNode[edge.From] = edgesFromNode = new HashSet<IEdge<TValue, TWeight>>();

            edgesFromNode.Add(edge);
            return edge;
        }

        /// <summary>
        /// Remove the provided edge from the Graph.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        /// <returns><c>True</c> if removed, otherwise <c>false</c>.</returns>
        public bool RemoveEdge(IEdge<TValue, TWeight> edge) => _edges.Remove(edge);

        /// <summary>
        /// Get all edges originating at the provided node.
        /// </summary>
        /// <param name="node">Originating node.</param>
        /// <returns>Collection of all edges originating at the provided node, empty if invalid.</returns>
        public IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdgesFromNode(INode<TValue> node)
        {
            if (node == null)
                return EMPTY_EDGES;

            if (_edgesByNode.TryGetValue(node, out var edgesFromNode))
                return edgesFromNode;

            return EMPTY_EDGES;
        }

        /// <summary>
        /// Traverse the Graph (depth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        public IEnumerable<INode<TValue>> DepthFirstTraverse(TValue originValue)
        {
            INode<TValue> originNode = _nodes.FirstOrDefault(x => x.Value.Equals(originValue));
            if (originNode == null)
                yield break;

            foreach (var traversedNode in DepthFirstTraverse(originNode))
            {
                yield return traversedNode;
            }
        }

        /// <summary>
        /// Traverse the Graph (depth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        public IEnumerable<INode<TValue>> DepthFirstTraverse(INode<TValue> origin)
        {
            if (origin == null)
                yield break;

            if (!_nodes.Contains(origin))
                throw new ArgumentException("Provided origin node does not exist in the graph.");

            HashSet<INode<TValue>> visitedNodes = new HashSet<INode<TValue>>(_nodes.Count);
            Stack<INode<TValue>> visitStack = new Stack<INode<TValue>>(_nodes.Count);

            visitStack.Push(origin);
            while (visitStack.Count > 0)
            {
                INode<TValue> traversalNode = visitStack.Pop();

                if (visitedNodes.Contains(traversalNode))
                    continue;

                visitedNodes.Add(traversalNode);

                yield return traversalNode;

                foreach (var edgeFromNode in GetEdgesFromNode(traversalNode))
                {
                    if (edgeFromNode.To == null)
                        continue;
                    if (visitedNodes.Contains(edgeFromNode.To))
                        continue;
                    visitStack.Push(edgeFromNode.To);
                }
            }
        }

        public IEnumerable<INode<TValue>> DepthFirstSearch(TValue originValue, TValue targetValue)
        {
            INode<TValue> originNode = _nodes.FirstOrDefault(x => x.Value.Equals(originValue));
            if (originNode == null)
                yield break;
            INode<TValue> targetNode = _nodes.FirstOrDefault(x => x.Value.Equals(targetValue));
            if (targetNode == null)
                yield break;

            foreach (var traversedNode in DepthFirstSearch(originNode, targetNode))
            {
                yield return traversedNode;
            }
        }

        public IEnumerable<INode<TValue>> DepthFirstSearch(INode<TValue> origin, INode<TValue> target)
        {
            foreach (var traversedNode in DepthFirstTraverse(origin))
            {
                yield return traversedNode;
                if (traversedNode.Equals(target))
                    yield break;
            }
        }

        /// <summary>
        /// Traverse the Graph (breadth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        public IEnumerable<INode<TValue>> BreadthFirstTraverse(TValue originValue)
        {
            INode<TValue> originNode = _nodes.FirstOrDefault(x => x.Value.Equals(originValue));
            if (originNode == null)
                yield break;

            foreach (var traversedNode in BreadthFirstTraverse(originNode))
            {
                yield return traversedNode;
            }
        }

        /// <summary>
        /// Traverse the Graph (breadth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        public IEnumerable<INode<TValue>> BreadthFirstTraverse(INode<TValue> origin)
        {
            if (origin == null)
                yield break;

            if (!_nodes.Contains(origin))
                throw new ArgumentException("Provided origin node does not exist in the graph.");

            HashSet<INode<TValue>> visitedNodes = new HashSet<INode<TValue>>(_nodes.Count);
            Queue<INode<TValue>> visitQueue = new Queue<INode<TValue>>(_nodes.Count);

            visitQueue.Enqueue(origin);
            while (visitQueue.Count > 0)
            {
                INode<TValue> traversalNode = visitQueue.Dequeue();

                if (visitedNodes.Contains(traversalNode))
                    continue;

                visitedNodes.Add(traversalNode);

                yield return traversalNode;

                foreach (var edgeFromNode in GetEdgesFromNode(traversalNode))
                {
                    if (edgeFromNode.To == null)
                        continue;
                    if (visitedNodes.Contains(edgeFromNode.To))
                        continue;
                    visitQueue.Enqueue(edgeFromNode.To);
                }
            }
        }

        public IEnumerable<INode<TValue>> BreadthFirstSearch(TValue originValue, TValue targetValue)
        {
            INode<TValue> originNode = _nodes.FirstOrDefault(x => x.Value.Equals(originValue));
            if (originNode == null)
                yield break;
            INode<TValue> targetNode = _nodes.FirstOrDefault(x => x.Value.Equals(targetValue));
            if (targetNode == null)
                yield break;

            foreach (var traversedNode in BreadthFirstSearch(originNode, targetNode))
            {
                yield return traversedNode;
            }
        }

        public IEnumerable<INode<TValue>> BreadthFirstSearch(INode<TValue> origin, INode<TValue> target)
        {
            foreach (var traversedNode in BreadthFirstTraverse(origin))
            {
                yield return traversedNode;
                if (traversedNode.Equals(target))
                    yield break;
            }
        }

        /// <summary>
        /// Find an optimal path through the Graph based on the provided start and end points.
        /// </summary>
        /// <param name="fromNode">Node from which the path originates.</param>
        /// <param name="toNode">Node at which the path terminates.</param>
        /// <param name="pathWeight">Total weight of the resulting path.</param>
        /// <remarks>Implements Dijkstra's Algorithm to find optimal path.</remarks>
        /// <returns>Ordered list containing each Node along the path, or null if invalid.</returns>
        public IReadOnlyList<INode<TValue>> GetPath(INode<TValue> fromNode, INode<TValue> toNode, out TWeight pathWeight)
        {
            pathWeight = default;

            if (fromNode == null || toNode == null)
                return null;

            if (!_nodes.Contains(fromNode))
                throw new ArgumentException("Provided fromNode does not exist in the graph.");

            if (!_nodes.Contains(toNode))
                throw new ArgumentException("Provided toNode does not exist in the graph.");

            HashSet<INode<TValue>> pendingNodes = new HashSet<INode<TValue>>(_nodes.Count);
            Dictionary<INode<TValue>, (TWeight, INode<TValue>)> lowestWeightToNode = new Dictionary<INode<TValue>, (TWeight, INode<TValue>)>(_nodes.Count);

            pendingNodes.Add(fromNode);
            lowestWeightToNode[fromNode] = (default, null);

            while (pendingNodes.Count > 0)
            {
                INode<TValue> node = pendingNodes.First();
                pendingNodes.Remove(node);

                TWeight baseWeight = lowestWeightToNode[node].Item1;
                foreach (IEdge<TValue, TWeight> edge in GetEdgesFromNode(node))
                {
                    TWeight newWeight = AddWeight(baseWeight, edge.Weight);
                    if (!lowestWeightToNode.TryGetValue(edge.To, out var currentLowest) ||
                        newWeight.CompareTo(currentLowest.Item1) < 0)
                    {
                        lowestWeightToNode[edge.To] = (newWeight, node);
                        pendingNodes.Add(edge.To);
                    }
                }
            }

            List<INode<TValue>> bestPath = new List<INode<TValue>>(_nodes.Count);

            pathWeight = lowestWeightToNode[toNode].Item1;
            INode<TValue> earliestNode = toNode;
            do
            {
                bestPath.Insert(0, earliestNode);
                earliestNode = lowestWeightToNode[earliestNode].Item2;
            } while (earliestNode != null);

            return bestPath;
        }
    }
}