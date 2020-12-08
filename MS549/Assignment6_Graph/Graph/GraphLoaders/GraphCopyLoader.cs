using System.Collections.Generic;
using SadPumpkin.Graph.Components;

namespace SadPumpkin.Graph.GraphLoaders
{
    public class GraphCopyLoader<TValue, IWeight> : IGraphLoader<TValue, IWeight>
    {
        public IReadOnlyCollection<INode<TValue>> GetNodes { get; }
        public IReadOnlyCollection<IEdge<TValue, IWeight>> GetEdges { get; }

        public GraphCopyLoader(IGraph<TValue, IWeight> copyGraph)
        {
            GetNodes = new List<INode<TValue>>(copyGraph.Nodes);
            GetEdges = new List<IEdge<TValue, IWeight>>(copyGraph.Edges);
        }
    }
}