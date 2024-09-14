namespace Common;

public static class TreeOperations
{
    public static void EachInOrder<T>(INode<T> node, Action<T> action)
        where T : IComparable<T>
    {
        if (node == null)
        {
            return;
        }
        EachInOrder(node.Left, action);
        action(node.Value);
        EachInOrder(node.Right, action);
    }
}
