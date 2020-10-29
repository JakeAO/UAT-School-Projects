namespace SadPumpkin.LinkedList
{
    public class LinkedList<T> : ILinkedList<T>
    {
        private Node<T> _first;
        private Node<T> _last;
        private int _count;

        public int Count => _count;
        public INode<T> First => _first;
        public INode<T> Last => _last;
        
        public INode<T> Find(T value)
        {
            throw new System.NotImplementedException();
        }

        public INode<T> Insert(T value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(INode<T> node)
        {
            throw new System.NotImplementedException();
        }

        public string Print()
        {
            throw new System.NotImplementedException();
        }
    }
}