using Common;
using System;

namespace _01.Two_Three.MySolution
{
    using System;
    using System.Collections.Generic;

    public class MyTwoThreeTree<T> : ITree<T>
        where T : IComparable<T>
    {
        public MyTwoThreeTree()
        {
        }

        public TwoThreeNode<T> Root;

        public T Value => Root == null ? default : Root.Value;
        public INode<T> Left => Root?.Left;
        public INode<T> Right => Root?.Right;

        public void Insert(T element)
        {
            var inputNode = TwoThreeNode.Create(element);
            var currentNode = Root;

            if (currentNode == null)
            {
                Root = inputNode;
                return;
            }

            Stack<TwoThreeNode<T>> parentsStack = [];

            while (!currentNode.IsLeaf())
            {
                if (currentNode.IsOnTheLeft(inputNode) && currentNode.Left != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = (TwoThreeNode<T>)currentNode.Left;
                    continue;
                }
                else if (currentNode.IsInTheMiddle(inputNode) && currentNode.Middle != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = currentNode.Middle;
                    continue;
                }
                else if (currentNode.IsOnTheRight(inputNode) && currentNode.Right != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = (TwoThreeNode<T>)currentNode.Right;
                    continue;
                }
            }

            var rebalanced = currentNode.Add(inputNode);
            while (rebalanced)
            {
                if (!parentsStack.TryPop(out TwoThreeNode<T> parent))
                {
                    break;
                }
                rebalanced = parent.Promote(currentNode);
                currentNode = parent;
            }
        }

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public T Search(T value)
        {
            throw new NotImplementedException();
        }

        public INode<T>[] GetChildren()
        {
            return Root.GetChildren();
        }

        public bool IsLessThan(INode<T> other)
        {
            return Root.IsLessThan(other);
        }

        public bool IsLeaf()
        {
            return Root.IsLeaf();
        }
    }
}

public static class DepthTraverser
{
    public static int Traverse<T>(INode<T> node, int depth)
        where T : IComparable<T>
    {
        if (node == null)
        {
            return depth;
        }

        depth++;
        foreach (var child in node.GetChildren())
        {
            depth = Traverse(child, depth);
        }
        return depth;
    }
}
