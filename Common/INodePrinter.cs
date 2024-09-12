namespace Common;

public interface INodePrinter
{
    void Print<T>(INode<T> node)
        where T : IComparable<T>;
}
