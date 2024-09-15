namespace Common;

public class BottomsUpPrinter : INodePrinter
{
    BottomUpLeftRightMatrix _matrix = new();

    public void Print<T>(ITree<T> tree)
        where T : IComparable<T>
    {
        Print(tree.Root);
    }

    public void Print<T>(INode<T> node)
        where T : IComparable<T>

    {
        if (node == null || node.Value == null)
        {
            return;
        }
        RecursiveImprint(node);
        _matrix.PrintAndFlush();
    }

    int RecursiveImprint<T>(INode<T> node)
        where T : IComparable<T>
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
            return ImprintNode(node.GetPrintable());
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
                _matrix.Imprint('|');
            }
            else
            {
                _matrix.Imprint(' ');
            }
        }
    }

    int PrintNodeWithArms<T>(int startCol, int endCol, INode<T> node)
        where T : IComparable<T>
    {
        var nodeValue = node.GetPrintable();
        _matrix.MoveUp(startCol);

        var center = (startCol + endCol) / 2;
        var nodeOffset = nodeValue.Length / 2;
        var nodeSpaceOffset = 1;
        var linkArmLength = Math.Max(center - nodeOffset - nodeSpaceOffset - startCol, 1);
        var linkArm = Printable.Create(new string('-', linkArmLength));
        var padding = Printable.Create(' ');
        var nodeWithLinks = Printable.Combine(linkArm, padding, nodeValue, padding, linkArm);
        return ImprintNode(nodeWithLinks);
    }

    int ImprintNode(Printable[] nodeHead)
    {
        var startCol = _matrix.GetCurrentColumn();
        _matrix.Imprint(nodeHead);

        _matrix.ImprintRightPadding(6);
        
        var nodeCenter = startCol + nodeHead.Length / 2;
        return nodeCenter;
    }
}

public class BottomUpLeftRightMatrix
{
    List<List<Printable>> _matrix = [ [] ];
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
            var row = new List<Printable>();
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
            var row = Enumerable.Repeat(new Printable(' '), col).ToList();
            _matrix.Add(row);
            _matrix.Reverse();
            _currentHeight = _currentHeight + 2;
        }
        _currentHeight -= 2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="printables"></param>
    /// <returns>the next free col (x) to imprint on the current height</returns>
    public int Imprint(params Printable[] printables)
    {
        foreach (var printable in printables)
        {
            _matrix[_currentHeight].Add(printable);
        }
        return _matrix[_currentHeight].Count;
    }

    public void ImprintRightPadding(int size)
    {
        var padding = Printable.Create(new string(' ', size));
        var tempHeight = _currentHeight;
        while (tempHeight >= 0)
        {
            foreach (var p in padding)
            {
                _matrix[tempHeight].Add(p);
            }
            tempHeight--;
        }
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
                row[i].Print();
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
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
