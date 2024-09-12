namespace Common;

public interface INode<T>
    where T : IComparable<T>
{
    T Value { get; }
    INode<T>? Left { get; }
    INode<T>? Right { get; }
    INode<T>[] GetChildren();
}
