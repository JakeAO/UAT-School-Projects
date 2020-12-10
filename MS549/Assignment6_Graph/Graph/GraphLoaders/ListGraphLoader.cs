using System;
using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    /// <summary>
    /// Graph content loading object which sources its data from a collection of nodes and corresponding edges.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public class ListGraphLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
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
        /// Create a GraphLoader based on the provided collection of nodes/edges.
        /// </summary>
        /// <param name="valuesToEdges">Collection of nodes and edges to build from.</param>
        public ListGraphLoader(IReadOnlyDictionary<TValue, IReadOnlyCollection<(TValue Value, TWeight Weight)>> valuesToEdges)
        {
            var nodes = new List<INode<TValue>>(valuesToEdges.Count);
            var edges = new List<IEdge<TValue, TWeight>>(valuesToEdges.Count);

            foreach (var nodeValue in valuesToEdges.Keys)
            {
                nodes.Add(new Node<TValue>(nodeValue));
            }

            foreach (var kvp in valuesToEdges)
            {
                INode<TValue> fromNode = nodes.Find(x => x.Value.Equals(kvp.Key));
                foreach ((TValue toValue, TWeight weight) in kvp.Value)
                {
                    INode<TValue> toNode = nodes.Find(x => x.Value.Equals(toValue));
                    IEdge<TValue, TWeight> newEdge = new Edge<TValue, TWeight>(fromNode, toNode, weight);
                    edges.Add(newEdge);
                }
            }

            GetNodes = nodes;
            GetEdges = edges;
        }
    }
}