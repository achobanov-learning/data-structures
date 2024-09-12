using System.Reflection;

namespace Common;

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

    public bool IsLessThan(INode<T> other)
    {
        return Value.CompareTo(other.Value) < 0;
    }

    public virtual bool IsLeaf()
    {
        return GetChildren().Length == 0;
    }
}

public interface INode<T>
    where T : IComparable<T>
{
    T Value { get; }
    INode<T> Left { get; }
    INode<T> Right { get; }
    INode<T>[] GetChildren();
    bool IsLessThan(INode<T> other);
    bool IsLeaf();
}
