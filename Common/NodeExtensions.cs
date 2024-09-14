namespace Common;

public static class NodeExtensions
{
    public static TNode As<T, TNode>(this INode<T> node)
        where T : IComparable<T>
        where TNode : INode<T>
    {
        return (TNode)node;
    }
}
