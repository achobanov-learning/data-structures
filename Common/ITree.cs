namespace Common;

public interface ITree<T>
    where T : IComparable<T>
{
    INode<T> Root { get; }
    void Insert(T value);
    void Delete(T value);
    void DeleteMin();
    void DeleteMax();
    int Count();
    ITree<T> Search(T value);
}
