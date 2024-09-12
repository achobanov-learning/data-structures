using _01.Two_Three.MySolution;
using System.Collections.Generic;

namespace _01.Two_Three.MySolution
{
    using System;
    using System.Collections.Generic;

    public class MyTwoThreeTree
    {
        private RenderMatrix _renderMatrix;

        public MyTwoThreeTree()
        {
        }

        public TreeNode<string> Root;


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

        public string Render()
        {
            _renderMatrix = new RenderMatrix();
            var depth = DepthTraverser.Traverse(Root, 0);
            PrintNode(Root, depth: depth);

            return _renderMatrix.Render();
        }

        private void PrintNode(TreeNode<string> node, int thisRow = 1, int thisCol = 150, int depth = 0)
        {
            if (_renderMatrix == null)
            {
                throw new Exception("Render matrix cannot be null");
            }

            //_renderMatrix.SetColumn(startIndex);
            //_renderMatrix.MoveToNextChild(position);
            var (row, col) = _renderMatrix.PrintNodeKeys(node, thisRow, thisCol);

            var hasMiddle = node.MiddleChild != null;

            if (hasMiddle)
            {
                var (linkRow, linkCol) = _renderMatrix.CreateMiddleChildLink(row, col);
                PrintNode(node.MiddleChild, ++linkRow, linkCol--);
            }
            if (node.LeftChild != null)
            {
                var (linkRow, linkCol) = _renderMatrix.CreateLeftChildLink(row, col, hasMiddle, node.LeftChild.IsLeaf(), depth - 1);
                PrintNode(node.LeftChild, ++linkRow, linkCol - 3);
            }

            if (node.RightChild != null)
            {
                var (linkRow, linkCol) = _renderMatrix.CreateRightChildLink(row, col, hasMiddle, node.RightChild.IsLeaf(), depth - 1);
                PrintNode(node.RightChild, ++linkRow, linkCol + 1);
            }
        }
    }
}

public static class DepthTraverser
{
    public static int Traverse(TreeNode<string> node, int depth)
    {
        depth++;
        if (node.LeftChild != null)
        {
            return Traverse(node.LeftChild, depth);
        }
        if (node.MiddleChild != null)
        {
            return Traverse(node.MiddleChild, depth);
        }
        if (node.RightChild != null)
        {
            return Traverse(node.RightChild, depth);
        }
        return depth;
    }
}
