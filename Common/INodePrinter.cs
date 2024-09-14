namespace Common;

public interface INodePrinter<T>
    where T : IComparable<T>
{
    void Print(INode<T> node);
    void Print(ITree<T> tree);
}
