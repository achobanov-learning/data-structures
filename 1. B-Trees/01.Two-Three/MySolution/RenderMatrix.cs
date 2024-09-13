using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01.Two_Three.MySolution;

internal class RenderMatrix
{
    private const int _width_size = 300;
    private const int _height_size = 50;
    private const int _arm_length = 10;
    private const int _space = 2;
    private const int _secondKeyOffset = 3;
    private const int _padding = _arm_length + _space;
    private int _printRow;
    private int _printCol;
    private char[][] _matrix;

    public RenderMatrix()
    {
        GeneratePrintMatrix();
        _printRow = 1;
        _printCol = _width_size / 2;
    }

    public string Render()
    {
        var sb = new StringBuilder();
        var filledRows = _matrix.Where(cols => cols.Any(x => x != ' '));
        var maxLength = filledRows
            .Select(x => x.Length)
            .Max();
        var sanitizedRows = filledRows.Select(x => string.Join("", x));
        foreach (var row in sanitizedRows)
        {
            sb.AppendLine(row.ToUpper());
        }
        return sb.ToString();
    }

    public (int row, int col) PrintNodeKeys(TwoThreeNode<string> node, int row, int col)
    {
        _printRow = row;
        _printCol = col;
        _matrix[_printRow][_printCol] = node.LeftKey[0];

        _printCol += 2;

        if (node.RightKey != null)
        {
            _matrix[_printRow][_printCol] = node.RightKey[0];
        }
        //PrintRightArm();

        return (_printRow, _printCol);
    }

    public (int row, int col) CreateLeftChildLink(int parentRow, int parentCol, bool hasMiddle, bool isLeaf, int height)
    {
        var middleOffset = hasMiddle ? -8 : 0;
        var nodeKeysOffset = -3;
        var siblingOffset = isLeaf ? 0 : -4 - 8 * Math.Abs(Math.Max(height, 1));
        var offset = middleOffset + nodeKeysOffset + siblingOffset;

        for (var i = parentCol + nodeKeysOffset - 1; i > parentCol + offset; i--)
        {
            _matrix[parentRow][i] = '-';
        }

        _printRow = parentRow + 1;
        _printCol = parentCol + offset;
        _matrix[_printRow][_printCol] = '/';

        return (_printRow, _printCol);
    }

    public (int row, int col) CreateMiddleChildLink(int parentRow, int parentCol)
    {
        _printRow = parentRow + 1;
        _printCol = parentCol - 1; //-1 to center in between both keys
        _matrix[_printRow][_printCol] = '|';

        return (_printRow, _printCol);
    }

    public (int row, int col) CreateRightChildLink(int parentRow, int parentCol, bool hasMiddle, bool isLeaf, int height)
    {
        var middleOffset = hasMiddle ? 8 : 0;
        var spaceOffset = 1;
        var siblingOffset = isLeaf ? 0 : 4 + 8 * Math.Abs(Math.Max(height, 1));
        var offset = middleOffset + spaceOffset + siblingOffset;

        for (var i = parentCol + spaceOffset + 1; i < parentCol + offset; i++)
        {
            _matrix[parentRow][i] = '-';
        }

        _printRow = parentRow + 1;
        _printCol = parentCol + offset;
        _matrix[_printRow][_printCol] = '\\';

        return (_printRow, _printCol);
    }

    public void MoveToNextChild(string position)
    {
        if (position == "root")
        {
            _printRow = 1;
            _printCol = _width_size / 2;
            return;
        }
        var prevLinkRow = _printRow - 1;
        for (var col = _padding; col < _width_size - _padding; col++)
        {
            var isEmpty = _matrix[_printRow][col] == ' ';
            if (position == "left" && _matrix[_printRow][col - _padding] == ' ' && _matrix[prevLinkRow][col] == '/')
            {
                _printCol = col - 1;
                break;
            }
            else if (position == "middle" && _matrix[_printRow][col] == ' ' && _matrix[prevLinkRow][col] == '|')
            {
                _printCol = col;
                break;
            }
            else if (position == "right" && _matrix[_printRow][col + _padding] == ' ' && _matrix[prevLinkRow][col] == '\\')
            {
                _printCol = col += 2;
                break;
            }
        }
    }

    private void GeneratePrintMatrix()
    {
        var rows = new List<List<char>>();
        for (var row = 0; row < _height_size; row++)
        {
            var cols = new List<char>();
            for (var col = 0; col < _width_size; col++)
            {
                cols.Add(' ');
            }

            rows.Add(cols);
        }

        _matrix = rows
            .Select(x => x.ToArray())
            .ToArray();
    }
}
