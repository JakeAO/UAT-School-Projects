using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    /// <summary>
    /// Graph content loading object which sources its data from a matrix of edges.
    /// </summary>
    /// <typeparam name="TValue">Type contained in each vertex/node.</typeparam>
    /// <typeparam name="TWeight">Type of weight assigned to each edge.</typeparam>
    public class MatrixGraphLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight> where TWeight : IEquatable<TWeight>, IComparable<TWeight>
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
        /// Create a GraphLoader object from the provided nodes and edges.
        /// </summary>
        /// <param name="nodeValues">List of nodes to build the Graph from.</param>
        /// <param name="edgeValues">Matrix of weights corresponding to Graph edges.</param>
        public MatrixGraphLoader(IReadOnlyList<TValue> nodeValues, TWeight[,] edgeValues)
        {
            var nodes = new List<INode<TValue>>(nodeValues.Select(x => new Node<TValue>(x)));
            var edges = new List<IEdge<TValue, TWeight>>(nodeValues.Count);

            for (int i = 0; i < edgeValues.GetLength(0); i++)
            {
                for (int j = 0; j < edgeValues.GetLength(1); j++)
                {
                    TWeight edgeWeight = edgeValues[i, j];
                    if (edgeWeight.Equals(default))
                    {
                        continue;
                    }

                    INode<TValue> fromNode = nodes[i];
                    INode<TValue> toNode = nodes[j];

                    IEdge<TValue, TWeight> newEdge = new Edge<TValue, TWeight>(fromNode, toNode, edgeWeight);
                    edges.Add(newEdge);
                }
            }

            GetNodes = nodes;
            GetEdges = edges;
        }
    }
}