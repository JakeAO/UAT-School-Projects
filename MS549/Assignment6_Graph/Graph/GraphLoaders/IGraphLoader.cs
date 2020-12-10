using System;
using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    /// <summary>
    /// Interface for generic Graph content loading object.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public interface IGraphLoader<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Collection of vertices/nodes to fill out the Graph.
        /// </summary>
        IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        
        /// <summary>
        /// Collection of edges to fill out the Graph.
        /// </summary>
        IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; }
    }
}