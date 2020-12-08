using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    public class MatrixGraphLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight>
    {
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        public IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; }

        public MatrixGraphLoader(IReadOnlyList<INode<TValue>> graphNodes, TWeight[,] edgeValues)
        {
            var nodes = new List<INode<TValue>>(graphNodes);
            var edges = new List<IEdge<TValue, TWeight>>(graphNodes.Count);

            for (int i = 0; i < edgeValues.GetLength(0); i++)
            {
                for (int j = 1; j < edgeValues.GetLength(1); j++)
                {
                    TWeight edgeWeight = edgeValues[i, j];
                    if (edgeWeight.Equals(default))
                    {
                        continue;
                    }

                    INode<TValue> fromNode = graphNodes[i];
                    INode<TValue> toNode = graphNodes[j];

                    IEdge<TValue, TWeight> newEdge = new Edge<TValue, TWeight>(fromNode, toNode, edgeWeight);
                    edges.Add(newEdge);
                }
            }

            GetNodes = nodes;
            GetEdges = edges;
        }
        
        public MatrixGraphLoader(IReadOnlyList<TValue> nodeValues, TWeight[,] edgeValues)
        {
            var nodes = new List<INode<TValue>>(nodeValues.Select(x => new Node<TValue>(x)));
            var edges = new List<IEdge<TValue, TWeight>>(nodeValues.Count);

            for (int i = 0; i < edgeValues.GetLength(0); i++)
            {
                for (int j = 1; j < edgeValues.GetLength(1); j++)
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