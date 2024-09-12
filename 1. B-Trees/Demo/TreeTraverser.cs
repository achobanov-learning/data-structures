using _01.Two_Three.MySolution;
using System;
using System.Collections.Generic;

namespace Demo;

public class TreeTraverser
{
    private readonly Dictionary<int, List<TreeNode<string>>> _nodesByDepth = [];

    public TreeTraverser(MyTwoThreeTree tree)
    {
        Traverse(tree.Root, 0);
    }

    private void Traverse(TreeNode<string> node, int depth)
    {
        AddNode(depth, node);

        if (node.LeftChild != null)
        {
            Traverse(node.LeftChild, depth + 1);
        }
        if (node.MiddleChild != null)
        {
            Traverse(node.MiddleChild, depth + 1);
        }
        if (node.RightChild != null)
        {
            Traverse(node.RightChild, depth + 1);
        }
    }

    private void AddNode(int depth, TreeNode<string> node)
    {
        if (_nodesByDepth.ContainsKey(depth))
        {
            _nodesByDepth[depth].Add(node);
        }
        else
        {
            _nodesByDepth.Add(depth, [ node ]);
        }
    }
}
