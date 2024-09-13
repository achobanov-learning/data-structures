﻿using Common;
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

            var linksCols = new List<int>();
            foreach (var child in children)
            {
                var childLinkCol = RecursiveImprint(child);
                linksCols.Add(childLinkCol);
            }
            _matrix.MoveUp(linksCols.First());
            for (int col = linksCols.First(); col <= linksCols.Last(); col++)
            {
                if (linksCols.Contains(col))
                {
                    _matrix.Imprint("|");
                }
                else
                {
                    _matrix.Imprint(" ");
                }
            }

            _matrix.MoveUp((linksCols.First() + linksCols.Last()) / 2);
        }

        return PrintNode(node);
    }

    int PrintNode(INode<T> node)
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

        var currentCol = _matrix.GetCurrentColumn();
        var nodeCenter = currentCol + result.Length / 2;
        _matrix.Imprint(result);
        _matrix.Imprint("   ");
        return nodeCenter;

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

    public void MoveUp(int col)
    {
        _currentHeight++;
        if (_matrix.Count == _currentHeight)
        {
            var row = new List<char>();
            for (var i = 0; i < col; i++)
            {
                row.Add(' ');
            }
            _matrix.Add(row);
        }
        else
        {
            for (var i = _matrix[_currentHeight].Count; i < col; i++)
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
