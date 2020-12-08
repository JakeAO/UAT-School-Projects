namespace SadPumpkin.Graph.Components
{
    public class Edge<TValue, TWeight> : IEdge<TValue, TWeight>
    {
        public TWeight Weight { get; }
        
        public INode<TValue> From { get; }
        public INode<TValue> To { get; }

        public Edge(INode<TValue> from, INode<TValue> to, TWeight weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}