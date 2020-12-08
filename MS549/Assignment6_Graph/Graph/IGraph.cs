using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph
{
    public interface IGraph<TValue, TWeight>
    {
        IReadOnlyCollection<INode<TValue>> Nodes { get; }
        IReadOnlyCollection<IEdge<TValue, TWeight>> Edges { get; }

        INode<TValue> AddNode(TValue value);
        INode<TValue> AddNode(INode<TValue> node);
        bool RemoveNode(INode<TValue> node);

        IEdge<TValue, TWeight> AddEdge(INode<TValue> from, INode<TValue> to);
        IEdge<TValue, TWeight> AddEdge(IEdge<TValue, TWeight> edge);
        bool RemoveEdge(IEdge<TValue, TWeight> edge);

        IEnumerable<INode<TValue>> TraverseDFS(INode<TValue> origin);
        IEnumerable<INode<TValue>> TraverseBFS(INode<TValue> origin);
    }
}