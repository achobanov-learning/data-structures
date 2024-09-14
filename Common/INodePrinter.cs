namespace Common;

public interface INodePrinter
{
    void Print<T>(INode<T> node)
        where T : IComparable<T>;
    void Print<T>(ITree<T> tree)
        where T : IComparable<T>;
}
