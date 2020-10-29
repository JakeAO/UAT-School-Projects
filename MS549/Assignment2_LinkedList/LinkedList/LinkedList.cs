using System.Text;

namespace SadPumpkin.LinkedList
{
    public class LinkedList<T> : ILinkedList<T>
    {
        public int Count { get; private set; }
        public INode<T> First { get; private set; }
        public INode<T> Last { get; private set; }

        public INode<T> Find(T value)
        {
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode.Value.Equals(value))
                {
                    return testNode;
                }

                testNode = testNode.Next;
            }

            return null;
        }

        public INode<T> Insert(T value)
        {
            if (Last == null)
            {
                // List is empty
                First = Last = new Node<T>(value);
            }
            else
            {
                // Insert after current last
                Last.Next = new Node<T>(value);
                Last.Next.Previous = Last;
                Last = Last.Next;
            }

            Count++;
            return Last;
        }

        public void Remove(T value)
        {
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode.Value.Equals(value))
                {
                    if (testNode.Previous == null)
                    {
                        // Update first node
                        First = testNode.Next;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Previous.Next = testNode.Next;
                    }

                    if (testNode.Next == null)
                    {
                        // Update last node
                        Last = testNode.Previous;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Next.Previous = testNode.Previous;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
            }
        }

        public void Remove(INode<T> node)
        {
            INode<T> testNode = First;
            while (testNode != null)
            {
                if (testNode == node)
                {
                    if (testNode.Previous == null)
                    {
                        // Update first node
                        First = testNode.Next;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Previous.Next = testNode.Next;
                    }

                    if (testNode.Next == null)
                    {
                        // Update last node
                        Last = testNode.Previous;
                    }
                    else
                    {
                        // Stitch together the nodes on either side of the removed node
                        testNode.Next.Previous = testNode.Previous;
                    }

                    Count--;
                    return;
                }

                testNode = testNode.Next;
            }
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            INode<T> testNode = First;
            while (testNode != null)
            {
                sb.Append(testNode.Previous == null ? "|-- " : "--> ")
                    .Append(testNode)
                    .Append(testNode.Next == null ? " --|" : " -->")
                    .AppendLine();

                testNode = testNode.Next;
            }

            return sb.ToString();
        }
    }
}