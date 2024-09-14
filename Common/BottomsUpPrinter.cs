using Common;
using System.Text;

namespace Common;

public class BottomsUpPrinter<T> : INodePrinter<T>
    where T : IComparable<T>
{
    BottomUpLeftRightMatrix _matrix = new();

    public void Print(ITree<T> tree)
    {
        Print(tree.Root);
    }

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
            if (!_matrix.IsEmpty())
            {
                _matrix.MoveDown();
            }

            var linkCols = children.Select(RecursiveImprint).ToList();
            PrintLinks(linkCols);

            return PrintNodeWithArms(linkCols.First(), linkCols.Last(), node);
        }
        else
        {
            return Print(node.GetPrintValue());
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
        var nodeValue = node.GetPrintValue();
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
}

public class BottomUpLeftRightMatrix
{
    List<List<char>> _matrix = [ [] ];
    int _currentHeight = 0;

    public int GetCurrentColumn(int? height = null)
    {
        return height == null
            ? _matrix[_currentHeight].Count
            : _matrix[height.Value].Count;
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

    public void MoveDown()
    {
        if (_currentHeight <= 1)
        {
            _matrix.Reverse();
            _matrix.Add([]);
            var col = GetCurrentColumn(_matrix.Count - 2);
            var row = Enumerable.Repeat(' ', col).ToList();
            _matrix.Add(row);
            _matrix.Reverse();
            _currentHeight = _currentHeight + 2;
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
        Console.WriteLine();
        foreach (var row in _matrix)
        {
            Console.Write("  ");
            for (var i = 0; i < row.Count; i++)
            {
                if (row[i] == 'r' && row[i + 1] == 'e' && row[i + 2] == 'd' && row[i + 3] == ':')
                {
                    i += 4;
                    var node = $"{row[i]}{row[i + 1]}{row[i + 2]}".Trim();
                    i += node.Length;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"  {node}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(row[i]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Flush();
    }

    public bool IsEmpty()
    {
        return _matrix.Count == 1 && _matrix[0].Count == 0;
    }

    private void Flush()
    {
        _matrix = [[]];
        _currentHeight = 0;
    }
}
