using Common;
using System;

namespace _01.Two_Three.MySolution
{
    using System;
    using System.Collections.Generic;

    public class MyTwoThreeTree : ITree<string>
    {
        public MyTwoThreeTree()
        {
        }

        public TreeNode<string> Root;

        public string Value => Root.Value;
        public INode<string> Left => Root.Left;
        public INode<string> Right => Root.Right;

        public void Insert(string element)
        {
            var inputNode = MyTreeNode.Create(element);
            var currentNode = Root;

            if (currentNode == null)
            {
                Root = inputNode;
                return;
            }

            Stack<TreeNode<string>> parentsStack = [];

            while (!currentNode.IsLeaf())
            {
                if (currentNode.IsOnTheLeft(inputNode) && currentNode.LeftChild != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                    continue;
                }
                else if (currentNode.IsInTheMiddle(inputNode) && currentNode.MiddleChild != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = currentNode.MiddleChild;
                    continue;
                }
                else if (currentNode.IsOnTheRight(inputNode) && currentNode.RightChild != null)
                {
                    parentsStack.Push(currentNode);
                    currentNode = currentNode.RightChild;
                    continue;
                }
            }

            var rebalanced = currentNode.Add(inputNode);
            while (rebalanced)
            {
                if (!parentsStack.TryPop(out TreeNode<string> parent))
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

        public string Search(string value)
        {
            throw new NotImplementedException();
        }

        public INode<string>[] GetChildren()
        {
            return Root.GetChildren();
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
