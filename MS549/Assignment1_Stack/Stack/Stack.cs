using System;

namespace SadPumpkin.Stack
{
    public class Stack<T> : IStack<T>
    {
        private T[] _array;
        private int _topIdx;

        public Stack(int capacity = 0)
        {
            _array = new T[capacity];
            _topIdx = -1;
        }

        public int Count => _topIdx + 1;

        public int Capacity => _array.Length;

        public void Push(T value)
        {
            if (_array.Length == 0)
                Array.Resize(ref _array, 1);
            while (_topIdx >= _array.Length - 1)
                Array.Resize(ref _array, _array.Length * 2);

            _array[++_topIdx] = value;
        }

        public T Pop()
        {
            if (_topIdx == -1)
                throw new InvalidOperationException("Attempted Pop() when Stack had no elements.");

            return _array[_topIdx--];
        }

        public T Peek()
        {
            if (_topIdx == -1)
                return default;

            return _array[_topIdx];
        }
    }
}