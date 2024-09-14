using Common;
using System;

namespace BTrees.RedBlack
{
    public class RedBlackTree<T> : ITree<T>
        where T : IComparable<T>
    {
        public RedBlackNode<T> Root;
        INode<T> ITree<T>.Root => Root;

        public RedBlackTree(T value, RedBlackNode<T> node)
        {
            Root = new RedBlackNode<T>(value)
            {
                Right = node
            };
            Root.Color = RedBlackNode<T>.BLACK;
        }

        public void EachInOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        ITree<T> ITree<T>.Search(T value)
        {
            return Search(value);
        }
        public RedBlackTree<T> Search(T value)
        {
            throw new NotImplementedException();
        }

        public void Insert(T value)
        {
            throw new NotImplementedException();
        } 

        public void Delete(T key)
        {
            throw new NotImplementedException();
        }

        public void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}