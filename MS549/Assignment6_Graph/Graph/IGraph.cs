using System;
using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph
{
    /// <summary>
    /// Interface for a generic, weighted, and directional Graph.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public interface IGraph<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Collection of all nodes contained in the Graph.
        /// </summary>
        IReadOnlyCollection<INode<TValue>> Nodes { get; }

        /// <summary>
        /// Collection of all edges contained in the Graph.
        /// </summary>
        IReadOnlyCollection<IEdge<TValue, TWeight>> Edges { get; }

        /// <summary>
        /// Add a node to the Graph with the provided value.
        /// </summary>
        /// <param name="value">Value contained in the node.</param>
        /// <returns>Newly created node, or null if invalid.</returns>
        INode<TValue> AddNode(TValue value);

        /// <summary>
        /// Add the provided node to the Graph.
        /// </summary>
        /// <param name="node">Node to add.</param>
        /// <returns>Newly added node, or null if invalid.</returns>
        INode<TValue> AddNode(INode<TValue> node);

        /// <summary>
        /// Remove the provided node from the Graph.
        /// </summary>
        /// <param name="node">Node to remove.</param>
        /// <returns><c>True</c> if removed, otherwise <c>false</c>.</returns>
        bool RemoveNode(INode<TValue> node);

        /// <summary>
        /// Add an edge to the Graph with the provided values.
        /// </summary>
        /// <param name="from">Value of the originating node.</param>
        /// <param name="to">Value of the terminating node.</param>
        /// <param name="weight">Weight of the edge.</param>
        /// <returns>Newly created edge, or null if invalid.</returns>
        IEdge<TValue, TWeight> AddEdge(TValue from, TValue to, TWeight weight);

        /// <summary>
        /// Add an edge to the Graph with the provided values.
        /// </summary>
        /// <param name="from">Node which originates the edge.</param>
        /// <param name="to">Node which terminates the edge.</param>
        /// <param name="weight">Weight of the edge.</param>
        /// <returns>Newly created edge, or null if invalid.</returns>
        IEdge<TValue, TWeight> AddEdge(INode<TValue> from, INode<TValue> to, TWeight weight);

        /// <summary>
        /// Add the provided edge to the Graph.
        /// </summary>
        /// <param name="edge">Edge to add.</param>
        /// <returns>Newly added edge, or null if invalid.</returns>
        IEdge<TValue, TWeight> AddEdge(IEdge<TValue, TWeight> edge);

        /// <summary>
        /// Remove the provided edge from the Graph.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        /// <returns><c>True</c> if removed, otherwise <c>false</c>.</returns>
        bool RemoveEdge(IEdge<TValue, TWeight> edge);

        /// <summary>
        /// Get all edges originating at the provided node.
        /// </summary>
        /// <param name="node">Originating node.</param>
        /// <returns>Collection of all edges originating at the provided node, empty if invalid.</returns>
        IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdgesFromNode(INode<TValue> node);

        /// <summary>
        /// Traverse the Graph (depth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> DepthFirstTraverse(TValue originValue);

        /// <summary>
        /// Traverse the Graph (depth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> DepthFirstTraverse(INode<TValue> origin);

        /// <summary>
        /// Search the Graph (depth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <param name="targetValue">Value of target node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> DepthFirstSearch(TValue originValue, TValue targetValue);

        /// <summary>
        /// Search the Graph (depth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <param name="target">Target node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> DepthFirstSearch(INode<TValue> origin, INode<TValue> target);

        /// <summary>
        /// Traverse the Graph (breadth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> BreadthFirstTraverse(TValue originValue);

        /// <summary>
        /// Traverse the Graph (breadth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> BreadthFirstTraverse(INode<TValue> origin);

        /// <summary>
        /// Search the Graph (breadth-first) beginning at the node corresponding to the provided value.
        /// </summary>
        /// <param name="originValue">Value of origin node.</param>
        /// <param name="targetValue">Value of target node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> BreadthFirstSearch(TValue originValue, TValue targetValue);

        /// <summary>
        /// Search the Graph (breadth-first) beginning at the node.
        /// </summary>
        /// <param name="origin">Origin node.</param>
        /// <param name="target">Target node.</param>
        /// <returns>Enumerable of each node in depth-first order.</returns>
        IEnumerable<INode<TValue>> BreadthFirstSearch(INode<TValue> origin, INode<TValue> target);

        /// <summary>
        /// Find an optimal path through the Graph based on the provided start and end points.
        /// </summary>
        /// <param name="fromNode">Node from which the path originates.</param>
        /// <param name="toNode">Node at which the path terminates.</param>
        /// <param name="pathWeight">Total weight of the resulting path.</param>
        /// <returns>Ordered list containing each Node along the path, or null if invalid.</returns>
        IReadOnlyList<INode<TValue>> GetPath(INode<TValue> fromNode, INode<TValue> toNode, out TWeight pathWeight);
    }
}