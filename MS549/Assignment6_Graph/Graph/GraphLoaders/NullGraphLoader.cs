using System;
using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    /// <summary>
    /// Null-Object Pattern implementation of IGraphLoader, supplies empty collections.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public class NullGraphLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Collection of vertices/nodes to fill out the Graph.
        /// </summary>
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; } = new INode<TValue>[0];
        
        /// <summary>
        /// Collection of edges to fill out the Graph.
        /// </summary>
        public IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; } = new IEdge<TValue, TWeight>[0];
    }
}