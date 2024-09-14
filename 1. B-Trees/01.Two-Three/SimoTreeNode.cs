namespace _01.Two_Three
{
    using System;

    public class SimoTreeNode<T> 
        where T : IComparable<T>
    {
        public T LeftKey;
        public T RightKey;

        public SimoTreeNode<T> LeftChild;
        public SimoTreeNode<T> MiddleChild;
        public SimoTreeNode<T> RightChild;

        public SimoTreeNode(T key)
        {
            this.LeftKey = key;
        }

        public bool IsThreeNode()
        {
            return RightKey != null;
        }

        public bool IsTwoNode()
        {
            return RightKey == null;
        }

        public bool IsLeaf()
        {
            return this.LeftChild == null && this.MiddleChild == null && this.RightChild == null;
        }
    }
}
