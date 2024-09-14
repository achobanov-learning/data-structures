using Common;
using System;

namespace _01.Two_Three.MySolution;

public class TwoThreeTree<T> : ITree<T>
    where T : IComparable<T>
{
    TwoThreeNode<T> _root;

    public INode<T> Root => _root;

    public TwoThreeTree()
    {
    }

    public TwoThreeTree(T key, TwoThreeNode<T> left, TwoThreeNode<T> right)
    {
        _root = new TwoThreeNode<T>(key)
        {
            Left = left,
            Right = right
        };
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public void Delete(T value)
    {
        throw new NotImplementedException();
    }

    public void DeleteMax()
    {
        throw new NotImplementedException();
    }

    public void DeleteMin()
    {
        throw new NotImplementedException();
    }

    public void Insert(T value)
    {
        var (_, node) = TwoTreeOperations.Insert(value, _root);
        _root = node;
    }

    public ITree<T> Search(T value)
    {
        throw new NotImplementedException();
    }

    public void EachInOrder(Action<T> action)
    {
        throw new NotImplementedException();
    }

    private static class TwoTreeOperations
    {
        public static (bool rebalanced, TwoThreeNode<T> node) Insert(T value, TwoThreeNode<T> node)
        {
            if (node == null)
            {
                return (false, new TwoThreeNode<T>(value));
            }

            if (node.IsLeaf())
            {
                var valueNode = new TwoThreeNode<T>(value);
                return Merge(node, valueNode);
            }

            (bool, TwoThreeNode<T>) result;
            if (node.IsLess(value))
            {
                result = Insert(value, (TwoThreeNode<T>)node.Left);
            }
            else if (node.IsMore(value))
            {
                result = Insert(value, (TwoThreeNode<T>)node.Right);
            }
            else
            {
                result = Insert(value, node.Middle);
            }
            var (rebalanced, newNode) = result;
            if (rebalanced)
            {
                return Merge(node, newNode);
            }
            return (false, node);
        }

        public static (bool rebalance, TwoThreeNode<T> node) Merge(TwoThreeNode<T> current, TwoThreeNode<T> next)
        {
            if (current.IsDouble())
            {
                if (current.IsLess(next))
                {
                    current.RightKey = current.LeftKey;
                    current.LeftKey = next.Value;
                    current.Left = next.Left;
                    current.Middle = (TwoThreeNode<T>)next.Right;
                }
                else
                {
                    current.RightKey = next.Value;
                    current.Right = next.Right;
                    current.Middle = (TwoThreeNode<T>)next.Left;
                }
                return (false, current);
            }
            else
            {
                if (current.IsLess(next))
                {
                    current.Left = next;
                    current.Right = new TwoThreeNode<T>(current.RightKey) // TODO: try to reduce allocations and benchmark against.
                    {
                        Left = current.Middle,
                        Right = current.Right,
                    };
                }
                else if (next.IsLess(current))
                {
                    current.Left = new TwoThreeNode<T>(current.LeftKey)
                    {
                        Left = current.Left,
                        Right = current.Middle,
                    };
                    current.Right = next;
                    current.LeftKey = current.RightKey;
                }
                else
                {
                    current.Left = new TwoThreeNode<T>(current.LeftKey)
                    {
                        Left = current.Left,
                        Right = current.Middle?.Right
                    };
                    current.Right = new TwoThreeNode<T>(current.RightKey)
                    {
                        Right = current.Right,
                        Left = current.Middle?.Left
                    };
                    current.LeftKey = next.LeftKey;
                }
                current.Middle = null;
                current.RightKey = default;
            }
            return (true, current);
        }
    }
}
