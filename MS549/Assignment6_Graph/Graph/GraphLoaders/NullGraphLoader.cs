using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    public class NullGraphLoader<TValue, IWeight> : IGraphLoader<TValue, IWeight>
    {
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; } = new INode<TValue>[0];
        public IReadOnlyCollection<IEdge<TValue, IWeight>> GetEdges { get; } = new IEdge<TValue, IWeight>[0];
    }
}