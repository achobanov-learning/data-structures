namespace _01.Two_Three.MySolution
{
    using Common;
    using System;
    using System.Linq;

    public static class MyTreeNode
    {
        public static TreeNode<T> Create<T>(T value) where T : IComparable<T>
            => new(value);
    }

    public class TreeNode<T> : Node<T>
        where T : IComparable<T>
    {
        public T LeftKey
        {
            get => Value;
            set => Value = value;
        }
        public T RightKey;

        public TreeNode<T> Middle { get; private set; }

        public TreeNode(T key) : base(key)
        {
        }

        public override INode<T>[] GetChildren()
        {
            INode<T>[] children = [Left, Middle, Right];
            return children.Where(x => x != null).ToArray();
        }

        public override string ToString()
        {
            var value = LeftKey.ToString();
            if (RightKey != null)
            {
                value += $" {RightKey}";
            }
            return value;
        }

        public bool IsTriple()
        {
            return RightKey != null;
        }

        public bool IsDouble()
        {
            return !IsTriple();
        }

        public bool IsLeaf()
        {
            return Left == null && Middle == null && Right == null;
        }

        public bool Add(TreeNode<T> node)
        {
            if (node.IsTriple())
            {
                throw new InvalidOperationException("Cannot add node triple node. Only double nodes are supposed to be promoted");
            }
            var isThisTripleNode = IsTriple();
            var isOnTheLeft = IsOnTheLeft(node);
            if (isThisTripleNode)
            {
                Rebalance(node);
                return true;
            }
            else
            {
                if (isOnTheLeft)
                {
                    RightKey = LeftKey; // TODO: reference equality?
                    LeftKey = node.LeftKey;
                    Left = node.Left;
                    Middle = (TreeNode<T>)node.Right; // Maybe check IsInTheMiddle?
                }
                else if (IsOnTheRight(node))
                {
                    RightKey = node.LeftKey;
                    Middle = (TreeNode<T>)node.Left;
                    Right = node.Right;
                }
                return false;
            }
        }

        public bool Promote(TreeNode<T> node)
        {
            if (node.IsTriple())
            {
                throw new InvalidOperationException("Cannot promote triple node. Only double nodes are supposed to be promoted");
            }
            if (Left.Value?.CompareTo(node.LeftKey) == 0)
            {
                Left = null;
            }
            else if (Middle?.LeftKey.CompareTo(node.LeftKey) == 0)
            {
                Middle = null;
            }
            else if (Right?.Value.CompareTo(node.LeftKey) == 0)
            {
                Right = null;
            }
            else
            {
                throw new Exception($"Cannot promote node '{node}' as it's not direct child of '{this}'");
            }

            return Add(node);
        }

        private void Rebalance(TreeNode<T> node)
        {
            if (IsOnTheLeft(node))
            {
                var newRightChild = MyTreeNode.Create(RightKey!);
                newRightChild.Left = Middle;
                newRightChild.Right = Right;

                Left = node;
                Right = newRightChild;
            }
            else if (IsInTheMiddle(node))
            {
                var promoteValue = node.LeftKey;
                var newLeftChild = MyTreeNode.Create(LeftKey);
                newLeftChild.Left = Left;
                var newRightChild = MyTreeNode.Create(RightKey!);
                newRightChild.Right = Right;

                var middleResult = Middle?.LeftKey.CompareTo(promoteValue);
                if (middleResult == 0)
                {
                    throw new InvalidOperationException($"Cannot add value '{promoteValue}', because it already exists");
                }
                else if (middleResult < 0)
                {
                    newLeftChild.Right = Middle;
                }
                else
                {
                    newRightChild.Left = Middle;
                }

                Left = newLeftChild;
                Right = newRightChild;
                LeftKey = node.LeftKey;
            }
            else
            {
                var newLeftChild = MyTreeNode.Create(LeftKey);
                newLeftChild.Left = Left;
                newLeftChild.Right = Middle;

                Left = newLeftChild;
                Right = node;
                LeftKey = RightKey!;
            }
            Middle = null;
            RightKey = default;
        }

        private void AddNode(Action<T> keySetter, TreeNode<T> node)
        {
            keySetter(node.LeftKey);
            Left = node.Left;
            Middle = node.Middle;
            Right = node.Right;
        }

        public bool IsOnTheLeft(TreeNode<T> node)
        {
            var result = Compare(node.LeftKey, LeftKey);
            return result < 0;
        }

        public bool IsInTheMiddle(TreeNode<T> node)
        {
            if (RightKey == null)
            {
                return false;
            }
            var value = node.LeftKey;
            return Compare(value, LeftKey) > 0 && Compare(value, RightKey!) < 0;
        }

        public bool IsOnTheRight(TreeNode<T> node)
        {
            var result = RightKey != null
                ? Compare(node.LeftKey, RightKey)
                : Compare(node.LeftKey, LeftKey);
            return result > 0;
        }

        private int Compare(T value, T key)
        {
            var result = value.CompareTo(key);
            if (result == 0)
            {
                throw new InvalidOperationException($"Tree already contains value '{value}'");
            }
            return result;
        }
    }
}
