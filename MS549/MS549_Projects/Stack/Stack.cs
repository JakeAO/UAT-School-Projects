namespace SadPumpkin.Stack
{
    public class Stack<T> : IStack<T>
    {
        private T[] _array;
        private int _topIdx;
        private int _count;

        public Stack(int capacity = 0)
        {
            _array = new T[capacity];
            _topIdx = 0;
            _count = 0;
        }

        public int Count => _count;

        public int Capacity => _array.Length;

        public void Push(T value)
        {
            throw new System.NotImplementedException();
        }

        public T Pop()
        {
            throw new System.NotImplementedException();
        }

        public T Peek()
        {
            throw new System.NotImplementedException();
        }
    }
}