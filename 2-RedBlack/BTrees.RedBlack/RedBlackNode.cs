using Common;
using System;

namespace BTrees.RedBlack;

public class RedBlackNode<T> : Node<T>
    where T : IComparable<T>
{
    public const bool RED = true;
    public const bool BLACK = false;

    public RedBlackNode(T value) : base(value)
    {
        Color = RED;
    }

    public bool Color;

    public override string GetPrintValue()
    {
        var value = base.GetPrintValue();
        if (Color == RED)
        {
            value = $"red:{value}";
        }
        return value;
    }
}
