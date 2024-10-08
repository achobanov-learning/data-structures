﻿using Common;
using System;

namespace _01.Two_Three.MySolution
{
    using System;
    using System.Collections.Generic;

    public class OldTwoThreeTree : ITree<string>
    {
        public OldTwoThreeTree()
        {
        }

        public OldTwoThreeNode<string> Root;

        public string Value => Root.Value;
        public INode<string> Left => Root.Left;
        public INode<string> Right => Root.Right;

        INode<string> ITree<string>.Root => Root;

        public void Insert(string element)
        {
            var inputNode = OldTwoThreeNode.Create(element);
            var currentNode = Root;

            if (currentNode == null)
            {
                Root = inputNode;
                return;
            }

            Stack<OldTwoThreeNode<string>> parentsStack = [];

            while (!currentNode.IsLeaf())
            {
                if (currentNode.IsOnTheLeft(inputNode) && currentNode.Left != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = (OldTwoThreeNode<string>)currentNode.Left;
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
                    currentNode = (OldTwoThreeNode<string>)currentNode.Right;
                    continue;
                }
            }

            var rebalanced = currentNode.Add(inputNode);
            while (rebalanced)
            {
                if (!parentsStack.TryPop(out OldTwoThreeNode<string> parent))
                {
                    break;
                }
                rebalanced = parent.Promote(currentNode);
                currentNode = parent;
            }
        }

        public void Delete(string value)
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

        public ITree<string> Search(string value)
        {
            throw new NotImplementedException();
        }

        public INode<string>[] GetChildren()
        {
            return Root.GetChildren();
        }

        public bool IsLessThan(INode<string> other)
        {
            return Root.IsLess(other);
        }

        public bool IsLeaf()
        {
            return Root.IsLeaf();
        }

        public void EachInOrder(Action<string> action)
        {
            throw new NotImplementedException();
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
