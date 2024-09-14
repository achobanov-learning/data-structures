using Common;
using System;
using System.Linq;

namespace _01.Two_Three.New;

public class TwoThreeNode<T> : Node<T>
    where T : IComparable<T>
{
    public TwoThreeNode(T value) : base(value)
    {
    }

    public T LeftKey
    {
        get => Value;
        set => Value = value;
    }
    public T RightKey { get; set; }

    public TwoThreeNode<T> Middle { get; set; }

    public override string ToString()
    {
        return $"{LeftKey} {RightKey}";
    }

    public override INode<T>[] GetChildren()
    {
        INode<T>[] children = [Left, Middle, Right];
        return children.Where(x => x != null).ToArray();
    }

    public override string GetPrintValue()
    {
        var leftKey = LeftKey.ToString();
        var result = SanitizeValue(leftKey);
        if (RightKey != null)
        {
            result += " " + SanitizeValue(RightKey.ToString());
        }
        return result;
    }

    public override bool IsLess(INode<T> other)
    {
        if (other is not TwoThreeNode<T> twoThreeNode)
        {
            throw new InvalidOperationException($"Cannot compare different nodes. '{other}' is not of type '{nameof(TwoThreeNode<T>)}'");
        }
        return twoThreeNode.RightKey != null
            ? IsLess(twoThreeNode.RightKey)
            : IsLess(twoThreeNode.LeftKey);
    }

    public override bool IsLess(T other)
    {
        return other.CompareTo(LeftKey) < 0;
    }

    public override bool IsMore(T other)
    {
        return other.CompareTo(RightKey) > 0;
    }

    public bool IsDouble()
    {
        return RightKey == null;
    }
}
