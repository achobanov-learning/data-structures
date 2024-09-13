namespace Common;

public interface INodePrinter<T>
    where T : IComparable<T>
{
    void Print(INode<T> node);
}
