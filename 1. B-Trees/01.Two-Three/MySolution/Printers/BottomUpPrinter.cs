using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace _01.Two_Three.MySolution.Printers;

public class BottomUpPrinter<T> : INodePrinter<T>
    where T : IComparable<T>
{
    BottomUpLeftRightMatrix _matrix = new();

    public void Print(INode<T> node)
    {
        RecursiveImprint(node);
        _matrix.PrintAndFlush();
    }

    void RecursiveImprint(INode<T> node)
    {
        var children = node.GetChildren();
        if (children.Length > 0)
        {
            if (_matrix.CanMoveDown())
            {
                _matrix.MoveDown();
            }
            foreach (var child in children)
            {
                RecursiveImprint(child);
            }
            _matrix.MoveUp();
        }

        PrintNode(node);
    }

    void PrintNode(INode<T> node)
    {
        string result = null;
        if (node is TwoThreeNode<T> twoThreeNode)
        {
            result = SanitizeTwoTreeValue(twoThreeNode);
        }
        else if (node is MyTwoThreeTree<T> tree)
        {
            result = SanitizeTwoTreeValue(tree.Root);
        }
        else
        {
            result = SanitizeValue(node.Value.ToString());
        }

        _matrix.Imprint(result);
        _matrix.Imprint("   ");

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

    public void MoveUp()
    {
        _currentHeight++;
        if (_matrix.Count == _currentHeight)
        {
            _matrix.Add([]);
        }
    }

    public bool CanMoveDown()
    {
        return _currentHeight > 0;
    }

    public void MoveDown()
    {
        if (_currentHeight <= 0)
        {
            throw new InvalidOperationException("Cannot move matrix further down than it's starting row");
        }
        _currentHeight--;
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
        foreach (var row in _matrix)
        {
            foreach (var c in row)
            {
                sb.Append(c);
            }
            sb.AppendLine();
        }
        Console.Write(sb.ToString());
        _matrix.Clear();
    }
}
