using Common;
using System;
using System.Xml.Linq;

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
        public RedBlackTree()
        {
        }

        public void EachInOrder(Action<T> action)
        {
            TreeOperations.EachInOrder(Root, action);
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
            Root = RedBlackOperations.Insert(value, Root);
            Root.MarkBlack();
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

        private static class RedBlackOperations
        {
            public static RedBlackNode<T> Insert(T value, RedBlackNode<T> node)
            {
                if (node == null)
                {
                    return new RedBlackNode<T>(value);
                }

                if (node.IsLeaf())
                {
                    var next = new RedBlackNode<T>(value);
                    return Merge(node, next);
                }

                if (node.IsLess(value))
                {
                    return Merge(node, Insert(value, node.Left.AsRedBlack()));
                }
                else
                {
                    return Merge(node, Insert(value, node.Right.AsRedBlack()));
                }
            }

            public static RedBlackNode<T> Merge(RedBlackNode<T> current, RedBlackNode<T> next)
            {
                if (current.IsLess(next))
                {
                    current.Left = next;
                }
                else
                {
                    current.Right = next;
                    if (current.Right.IsRed())
                    {
                        current = RotateLeft(current);
                    }
                }
                if (current.Left.IsRed() && current.Left.Left.IsRed())
                {
                    current = RotateRight(current);
                    if (current.Left.IsRed() && current.Right.IsRed())
                    {
                        current = FlipColors(current);
                    }
                }
                return current;
            }

            public static RedBlackNode<T> RotateLeft(RedBlackNode<T> node)
            {
                var newNode = (RedBlackNode<T>)node.Right;
                node.Right = newNode.Left;
                newNode.Left = node;
                newNode.Color = node.Color;
                node.MarkRed();
                return newNode;
            }

            public static RedBlackNode<T> RotateRight(RedBlackNode<T> node)
            {
                var newNode = (RedBlackNode<T>)node.Left;
                node.Left = newNode.Right;
                newNode.Right = node;
                newNode.Color = node.Color;
                node.MarkRed();
                return newNode;
            }

            public static RedBlackNode<T> FlipColors(RedBlackNode<T> node)
            {
                var left = (RedBlackNode<T>)node.Left;
                var right = (RedBlackNode<T>)node.Right;
                node.FlipColor();
                left.FlipColor();
                right.FlipColor();
                return node;
            }
        }
    }
}