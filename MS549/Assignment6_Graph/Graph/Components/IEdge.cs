using System;

namespace SadPumpkin.Graph.Components
{
    /// <summary>
    /// Interface for a generic weighted Graph edge.
    /// </summary>
    /// <typeparam name="TValue">Type contained in the vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to the edge.</typeparam>
    public interface IEdge<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Weight value assigned to this edge.
        /// </summary>
        TWeight Weight { get; }
        
        /// <summary>
        /// Vertex/node which this edge originates from.
        /// </summary>
        INode<TValue> From { get; }
        
        /// <summary>
        /// Vertex/node which this edge terminates at.
        /// </summary>
        INode<TValue> To { get; }
    }
}