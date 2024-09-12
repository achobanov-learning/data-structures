namespace _01.Two_Three.MySolution
{
    using System;

    public static class MyTreeNode
    {
        public static TreeNode<T> Create<T>(T value) where T : IComparable<T>
            => new(value);
    }

    public class TreeNode<T>
        where T : IComparable<T>
    {
        public T LeftKey;
        public T RightKey;

        public TreeNode<T> LeftChild { get; private set; }
        public TreeNode<T> MiddleChild { get; private set; }
        public TreeNode<T> RightChild { get; private set; }

        public TreeNode(T key)
        {
            LeftKey = key;
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
            return LeftChild == null && MiddleChild == null && RightChild == null;
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
                    LeftChild = node.LeftChild;
                    MiddleChild = node.RightChild; // Maybe check IsInTheMiddle?
                }
                else if (IsOnTheRight(node))
                {
                    RightKey = node.LeftKey;
                    MiddleChild = node.LeftChild;
                    RightChild = node.RightChild;
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
            if (LeftChild.LeftKey?.CompareTo(node.LeftKey) == 0)
            {
                LeftChild = null;
            }
            else if (MiddleChild?.LeftKey.CompareTo(node.LeftKey) == 0)
            {
                MiddleChild = null;
            }
            else if (RightChild?.LeftKey.CompareTo(node.LeftKey) == 0)
            {
                RightChild = null;
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
                newRightChild.LeftChild = MiddleChild;
                newRightChild.RightChild = RightChild;

                LeftChild = node;
                RightChild = newRightChild;
            }
            else if (IsInTheMiddle(node))
            {
                var promoteValue = node.LeftKey;
                var newLeftChild = MyTreeNode.Create(LeftKey);
                newLeftChild.LeftChild = LeftChild;
                var newRightChild = MyTreeNode.Create(RightKey!);
                newRightChild.RightChild = RightChild;

                var middleResult = MiddleChild?.LeftKey.CompareTo(promoteValue);
                if (middleResult == 0)
                {
                    throw new InvalidOperationException($"Cannot add value '{promoteValue}', because it already exists");
                }
                else if (middleResult < 0)
                {
                    newLeftChild.RightChild = MiddleChild;
                }
                else
                {
                    newRightChild.LeftChild = MiddleChild;
                }

                LeftChild = newLeftChild;
                RightChild = newRightChild;
                LeftKey = node.LeftKey;
            }
            else
            {
                var newLeftChild = MyTreeNode.Create(LeftKey);
                newLeftChild.LeftChild = LeftChild;
                newLeftChild.RightChild = MiddleChild;

                LeftChild = newLeftChild;
                RightChild = node;
                LeftKey = RightKey!;
            }
            MiddleChild = null;
            RightKey = default;
        }

        private void AddNode(Action<T> keySetter, TreeNode<T> node)
        {
            keySetter(node.LeftKey);
            LeftChild = node.LeftChild;
            MiddleChild = node.MiddleChild;
            RightChild = node.RightChild;
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
