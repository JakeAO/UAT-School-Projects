namespace SadPumpkin.Graph.Components
{
    public interface IEdge<TValue, TWeight>
    {
        TWeight Weight { get; }
        
        INode<TValue> From { get; }
        INode<TValue> To { get; }
    }
}