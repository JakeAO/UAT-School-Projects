using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    public class ListGraphLoader<TValue, TWeight> : IGraphLoader<TValue, TWeight>
    {
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        public IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; }

        public ListGraphLoader(IReadOnlyCollection<INode<TValue>> nodes, IReadOnlyCollection<IEdge<TValue, TWeight>> edges)
        {
            GetNodes = new List<INode<TValue>>(nodes);
            GetEdges = new List<IEdge<TValue, TWeight>>(edges);
        }

        public ListGraphLoader(IReadOnlyDictionary<INode<TValue>, IReadOnlyCollection<IEdge<TValue, TWeight>>> nodesToEdges)
        {
            GetNodes = new List<INode<TValue>>(nodesToEdges.Keys);
            GetEdges = nodesToEdges.Values.SelectMany(x => x).ToArray();
        }

        public ListGraphLoader(IReadOnlyCollection<(INode<TValue> Node, IReadOnlyCollection<IEdge<TValue, TWeight>> Edges)> nodesToEdges)
        {
            GetNodes = new List<INode<TValue>>(nodesToEdges.Select(x => x.Node));
            GetEdges = nodesToEdges.SelectMany(x => x.Edges).ToArray();
        }

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