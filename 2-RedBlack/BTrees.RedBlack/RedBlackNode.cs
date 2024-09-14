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

    public bool IsRed()
    {
        return Color == RED;
    }

    public void MarkRed()
    {
        Color = RED;
    }

    public void MarkBlack()
    {
        Color = BLACK;
    }

    public void FlipColor()
    {
        Color = !Color;
    }
}

public static class RedBlackNodeExtensions
{
    public static RedBlackNode<T> AsRedBlack<T>(this INode<T> node)
        where T : IComparable<T>
    {
        if (node == null)
        {
            return null;
        }
        if (node is not RedBlackNode<T> redBlack)
        {
            throw new InvalidOperationException($"Node '{node}' is not a red-black node");
        }
        return redBlack;
    }
 
    public static bool IsRed<T>(this INode<T> node)
        where T : IComparable<T>
    {
        if (node == null)
        {
            return false;
        }
        if (node is not RedBlackNode<T> redBlack)
        {
            throw new InvalidOperationException($"Node '{node}' is not a red-black node");
        }
        return redBlack.IsRed();
    }
}
