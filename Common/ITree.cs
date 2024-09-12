namespace Common;

public interface ITree<T> : INode<T>
    where T : IComparable<T>
{
    void Insert(T value);
    void Delete(T value);
    void DeleteMin();
    void DeleteMax();
    int Count();
    T Search(T value);
}
