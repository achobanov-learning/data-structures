using Common;
using System;

namespace _01.Two_Three.MySolution.Printers;

public class TopDownPrinter : INodePrinter<string>
{
    private RenderMatrix _matrix;

    public void Print(INode<string> node)
    {
        _matrix = new RenderMatrix();
        var depth = DepthTraverser.Traverse(node, 0);

        PrintNode(node, depth);

        Console.WriteLine(_matrix.Render());
    }

    private void PrintNode(INode<string> node, int thisRow = 1, int thisCol = 150, int depth = 0)
    {
        if (_matrix == null)
        {
            throw new Exception("Render matrix cannot be null");
        }
        if (node is MyTwoThreeTree<string> tree)
        {
            node = (INode<string>)tree.Root;
        }
        if (node is not TwoThreeNode<string> twoTreeNode)
        {
            throw new NotImplementedException();
        }

        var (row, col) = _matrix.PrintNodeKeys(twoTreeNode, thisRow, thisCol);

        var hasMiddle = twoTreeNode.Middle != null;
        if (hasMiddle)
        {
            var (linkRow, linkCol) = _matrix.CreateMiddleChildLink(row, col);
            PrintNode(twoTreeNode.Middle, ++linkRow, linkCol--);
        }
        if (twoTreeNode.Left != null)
        {
            var (linkRow, linkCol) = _matrix.CreateLeftChildLink(row, col, hasMiddle, twoTreeNode.Left.IsLeaf(), depth - 1);
            PrintNode(twoTreeNode.Left, ++linkRow, linkCol - 3);
        }
        if (twoTreeNode.Right != null)
        {
            var (linkRow, linkCol) = _matrix.CreateRightChildLink(row, col, hasMiddle, twoTreeNode.Right.IsLeaf(), depth - 1);
            PrintNode(twoTreeNode.Right, ++linkRow, linkCol + 1);
        }
    }
}
