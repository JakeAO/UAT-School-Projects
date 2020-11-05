using NUnit.Framework;

namespace BinarySearchTree.Tests
{
    [TestFixture]
    public class NodeUtilityTests
    {
        [Test]
        public void count_is_accurate()
        {
            INode<int> root = new Node<int>(5);
            INode<int> left = new Node<int>(3);
            INode<int> leftLeft = new Node<int>(2);
            INode<int> leftRight = new Node<int>(4);
            INode<int> right = new Node<int>(7);
            INode<int> rightLeft = new Node<int>(6);
            INode<int> rightRight = new Node<int>(8);

            Assert.AreEqual(1, NodeUtilities.GetCount(root));

            root.LeftChild = left;
            root.RightChild = right;

            Assert.AreEqual(3, NodeUtilities.GetCount(root));

            left.LeftChild = leftLeft;
            left.RightChild = leftRight;

            Assert.AreEqual(5, NodeUtilities.GetCount(root));

            right.LeftChild = rightLeft;
            right.RightChild = rightRight;

            Assert.AreEqual(7, NodeUtilities.GetCount(root));
        }

        [Test]
        public void depth_is_accurate()
        {
            INode<int> root = new Node<int>(5);
            INode<int> left = new Node<int>(3);
            INode<int> leftLeft = new Node<int>(2);
            INode<int> leftRight = new Node<int>(4);
            INode<int> right = new Node<int>(7);
            INode<int> rightLeft = new Node<int>(6);
            INode<int> rightRight = new Node<int>(8);

            Assert.AreEqual(1, NodeUtilities.GetDepth(root));

            root.LeftChild = left;
            root.RightChild = right;

            Assert.AreEqual(2, NodeUtilities.GetDepth(root));

            left.LeftChild = leftLeft;
            left.RightChild = leftRight;

            Assert.AreEqual(3, NodeUtilities.GetDepth(root));

            right.LeftChild = rightLeft;
            right.RightChild = rightRight;

            Assert.AreEqual(3, NodeUtilities.GetDepth(root));
        }

        [Test]
        public void rotate_right_fails_gracefully()
        {
            INode<int> root = new Node<int>(5);
            INode<int> right = new Node<int>(7);

            root.RightChild = right;

            INode<int> originalRoot = root;
            bool result = NodeUtilities.RotateRight(ref root);

            Assert.IsFalse(result);
            Assert.AreEqual(originalRoot, root);
        }

        [Test]
        public void rotate_left_fails_gracefully()
        {
            INode<int> root = new Node<int>(5);
            INode<int> left = new Node<int>(3);

            root.LeftChild = left;

            INode<int> originalRoot = root;
            bool result = NodeUtilities.RotateLeft(ref root);

            Assert.IsFalse(result);
            Assert.AreEqual(originalRoot, root);
        }

        [Test]
        public void rotate_right_modifies_node()
        {
            INode<int> root = new Node<int>(5);
            INode<int> left = new Node<int>(3);
            INode<int> right = new Node<int>(7);

            root.LeftChild = left;
            root.RightChild = right;

            INode<int> originalRoot = root;
            bool result = NodeUtilities.RotateRight(ref root);

            Assert.IsTrue(result);
            Assert.AreEqual(left, root);
            Assert.AreEqual(originalRoot, root.RightChild);
            Assert.AreEqual(right, root.RightChild.RightChild);
            Assert.IsNull(root.LeftChild);
            Assert.IsNull(root.RightChild.LeftChild);
        }

        [Test]
        public void rotate_left_modifies_node()
        {
            INode<int> root = new Node<int>(5);
            INode<int> left = new Node<int>(3);
            INode<int> right = new Node<int>(7);

            root.LeftChild = left;
            root.RightChild = right;

            INode<int> originalRoot = root;
            bool result = NodeUtilities.RotateLeft(ref root);

            Assert.IsTrue(result);
            Assert.AreEqual(right, root);
            Assert.AreEqual(originalRoot, root.LeftChild);
            Assert.AreEqual(left, root.LeftChild.LeftChild);
            Assert.IsNull(root.RightChild);
            Assert.IsNull(root.LeftChild.RightChild);
        }
    }
}