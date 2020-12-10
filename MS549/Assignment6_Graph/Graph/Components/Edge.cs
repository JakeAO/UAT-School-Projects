using System;

namespace SadPumpkin.Graph.Components
{
    /// <summary>
    /// Generic weighted Graph edge implementation.
    /// </summary>
    /// <typeparam name="TValue">Type contained in the vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to the edge.</typeparam>
    public class Edge<TValue, TWeight> : IEdge<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
    {
        /// <summary>
        /// Weight value assigned to this edge.
        /// </summary>
        public TWeight Weight { get; }
        
        /// <summary>
        /// Vertex/node which this edge originates from.
        /// </summary>
        public INode<TValue> From { get; }
        
        /// <summary>
        /// Vertex/node which this edge terminates at.
        /// </summary>
        public INode<TValue> To { get; }

        /// <summary>
        /// Create an immutable Edge from the provided data.
        /// </summary>
        /// <param name="from">Node which originates this edge.</param>
        /// <param name="to">Node which terminates this edge.</param>
        /// <param name="weight">Weight of this edge.</param>
        public Edge(INode<TValue> from, INode<TValue> to, TWeight weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}