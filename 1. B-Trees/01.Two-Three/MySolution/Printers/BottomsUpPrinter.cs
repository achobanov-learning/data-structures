using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01.Two_Three.MySolution.Printers;

public class BottomsUpPrinter<T> : INodePrinter<T>
    where T : IComparable<T>
{
    BottomUpLeftRightMatrix _matrix = new();

    public void Print(INode<T> node)
    {
        if (node == null || node.Value == null)
        {
            return;
        }
            
        RecursiveImprint(node);
        _matrix.PrintAndFlush();
    }

    int RecursiveImprint(INode<T> node)
    {
        var children = node.GetChildren();
        if (children.Length > 0)
        {
            if (_matrix.CanMoveDown())
            {
                _matrix.MoveDown();
            }

            var linkCols = children.Select(RecursiveImprint).ToList();
            PrintLinks(linkCols);

            return PrintNodeWithArms(linkCols.First(), linkCols.Last(), node);
        }
        else
        {
            return Print(PrepareNode(node));
        }
    }

    void PrintLinks(List<int> childrenCols)
    {
        var startCol = childrenCols.First();
        var endCol = childrenCols.Last();
        _matrix.MoveUp(startCol);

        for (int col = startCol; col <= endCol; col++)
        {
            if (childrenCols.Contains(col))
            {
                _matrix.Imprint("|");
            }
            else
            {
                _matrix.Imprint(" ");
            }
        }
    }

    int PrintNodeWithArms(int startCol, int endCol, INode<T> node)
    {
        var nodeValue = PrepareNode(node);
        _matrix.MoveUp(startCol);

        var center = (startCol + endCol) / 2;
        var nodeOffset = nodeValue.Length / 2;
        var nodeSpaceOffset = 1;
        var linkArm = new string('-', Math.Max(center - nodeOffset - nodeSpaceOffset - startCol, 1));
        return Print($"{linkArm} {nodeValue} {linkArm}");
    }

    int Print(string nodeHead)
    {
        var currentCol = _matrix.GetCurrentColumn();
        var nodeCenter = currentCol + nodeHead.Length / 2;
        _matrix.Imprint(nodeHead);
        _matrix.Imprint("      ");
        return nodeCenter;
    }

    string PrepareNode(INode<T> node)
    {
        if (node is TwoThreeNode<T> twoThreeNode)
        {
            return SanitizeTwoTreeValue(twoThreeNode);
        }
        else if (node is MyTwoThreeTree<T> tree)
        {
            return SanitizeTwoTreeValue(tree.Root);
        }
        else
        {
            return SanitizeValue(node.Value.ToString());
        }

        string SanitizeTwoTreeValue(TwoThreeNode<T> node)
        {
            var leftKey = node.LeftKey.ToString();
            var result = SanitizeValue(leftKey);
            if (node.RightKey != null)
            {
                result += " " + SanitizeValue(node.RightKey.ToString());
            }
            return result;
        }

        string SanitizeValue(string value)
        {
            if (value.Length > 3)
            {
                return $"{value[..1]}+";
            }
            return value;
        }
    }
}

public class BottomUpLeftRightMatrix
{
    List<List<char>> _matrix = [ [] ];
    int _currentHeight = 0;

    public int GetCurrentColumn()
    {
        return _matrix[_currentHeight].Count;
    }

    public void MoveUp(int startCol)
    {
        _currentHeight++;
        if (_matrix.Count == _currentHeight)
        {
            var row = new List<char>();
            for (var i = 0; i < startCol; i++)
            {
                row.Add(' ');
            }
            _matrix.Add(row);
        }
        else
        {
            for (var i = _matrix[_currentHeight].Count; i < startCol; i++)
            {
                _matrix[_currentHeight].Add(' ');
            }
        }
    }

    public bool CanMoveDown()
    {
        return _currentHeight > 0;
    }

    public void MoveDown()
    {
        if (_currentHeight <= 1)
        {
            throw new InvalidOperationException("Cannot move matrix further down than it's starting row");
        }
        _currentHeight -= 2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>the next free col (x) to imprint on the current height</returns>
    public int Imprint(string text)
    {
        foreach (char c in text)
        {
            _matrix[_currentHeight].Add(c);
        }
        return _matrix[_currentHeight].Count;
    }

    public void PrintAndFlush()
    {
        _matrix.Reverse();
        var sb = new StringBuilder();
        sb.AppendLine();
        foreach (var row in _matrix)
        {
            sb.Append("  ");
            foreach (var c in row)
            {
                sb.Append(c);
            }
            sb.AppendLine();
        }
        sb.AppendLine();
        Console.Write(sb.ToString());
        _matrix = [[]];
    }
}
