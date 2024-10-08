﻿namespace Common;

public class Node<T> : INode<T>
    where T : IComparable<T>
{
    public Node(T value)
    {
        Value = value;
    }

    public T Value { get; protected set; }
    public INode<T> Left { get; set; }
    public INode<T> Right { get; set; }

    public virtual INode<T>[] GetChildren()
    {
        INode<T>[] children = [Left, Right];
        return children.Where(x => x != null).ToArray();
    }

    public virtual bool IsLess(T other)
    {
        return other.CompareTo(Value) < 0;
    }

    public virtual bool IsMore(T other)
    {
        return other.CompareTo(Value) > 0;
    }

    public virtual bool IsLess(INode<T> other)
    {
        return other.Value.CompareTo(Value) < 0;
    }

    public virtual bool IsLeaf()
    {
        return GetChildren().Length == 0;
    }

    public virtual Printable[] GetPrintable()
    {
        var value = Value.ToString();
        return Printable.Create(SanitizeValue(value)); // Not sure if necessary any more
    }

    public override string ToString()
    {
        return $"{Value}";
    }

    protected string SanitizeValue(string value)
    {
        if (value.Length > 3)
        {
            return $"{value[..2]}+";
        }
        return value;
    }
}

public interface INode<T>
    where T : IComparable<T>
{
    T Value { get; }
    INode<T> Left { get; set; }
    INode<T> Right { get; set; }
    INode<T>[] GetChildren();
    bool IsMore(T other);
    bool IsLess(T value);
    bool IsLess(INode<T> other);
    bool IsLeaf();
    Printable[] GetPrintable();
}
