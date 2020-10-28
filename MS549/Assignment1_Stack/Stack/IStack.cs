namespace SadPumpkin.Stack
{
    /// <summary>
    /// A generic last-in-first-out (LIFO) collection.
    /// </summary>
    /// <typeparam name="T">Type of element stored in the Stack.</typeparam>
    public interface IStack<T>
    {
        /// <summary>
        /// Returns the number of elements in the Stack.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Returns the current maximum capacity of the Stack.
        /// </summary>
        int Capacity { get; }
        
        /// <summary>
        /// Insert a new element onto the top of the Stack.
        /// </summary>
        /// <param name="value">Value to insert into the Stack.</param>
        void Push(T value);
        
        /// <summary>
        /// Removes and returns the top element of the Stack.
        /// </summary>
        /// <returns>Top element of the stack.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the Stack is empty.</exception>
        T Pop();
        
        /// <summary>
        /// Look at the top element of the Stack without modifying the Stack.
        /// </summary>
        /// <returns>Top element of the stack, or default if the Stack is empty.</returns>
        T Peek();
    }
}