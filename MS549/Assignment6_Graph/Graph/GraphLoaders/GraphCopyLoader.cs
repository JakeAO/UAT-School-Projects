using System;
using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    /// <summary>
    /// Graph content loading object which sources its data from another Graph.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public class GraphCopyLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Collection of vertices/nodes to fill out the Graph.
        /// </summary>
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        
        /// <summary>
        /// Collection of edges to fill out the Graph.
        /// </summary>
        public IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; }

        /// <summary>
        /// Create a GraphLoader object from the provided Graph.
        /// </summary>
        /// <param name="copyGraph">Source Graph to copy from.</param>
        public GraphCopyLoader(IGraph<TValue, TWeight> copyGraph)
        {
            GetNodes = new List<INode<TValue>>(copyGraph.Nodes);
            GetEdges = new List<IEdge<TValue, TWeight>>(copyGraph.Edges);
        }
    }
}