namespace SadPumpkin.Graph.Components
{
    /// <summary>
    /// Interface for a generic weighted Graph vertex/node.
    /// </summary>
    /// <typeparam name="TValue">Type contained in this vertex/node.</typeparam>
    public interface INode<TValue>
    {
        /// <summary>
        /// Value of this vertex/node.
        /// </summary>
        TValue Value { get; }
    }
}