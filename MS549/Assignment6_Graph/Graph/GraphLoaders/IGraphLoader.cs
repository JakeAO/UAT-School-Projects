using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    public interface IGraphLoader<TValue, TWeight>
    {
        IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        IReadOnlyCollection<IEdge<TValue, TWeight>> GetEdges { get; }
    }
}