namespace _01.RedBlackTree
{
    using Common;
    using System;
    using System.Linq;

    public class SimoRedBlackTree<T> where T : IComparable<T>
    {
        public const bool RED = true;
        public const bool BLACK = false;

        public class Node : INode<T>
        {
            public Node(T value)
            {
                this.Value = value;
                Color = RED;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
            INode<T> INode<T>.Left
            {
                get => Left;
                set => Left = (Node)value;
            }
            INode<T> INode<T>.Right 
            { 
                get => Right;
                set => Right = (Node)value;
            }

            public INode<T>[] GetChildren()
            {
                INode<T>[] children = [Left, Right];
                return children.Where(x => x != null).ToArray();
            }

            public Printable[] GetPrintable()
            {
                var value = Value.ToString();
                if (Color == RED)
                {
                    return Printable.Create(value, ConsoleColor.Red);
                }
                return Printable.Create(value);
            }

            public bool IsLeaf()
            {
                return GetChildren().Length == 0;
            }

            public bool IsLess(T value)
            {
                return value.CompareTo(Value) > 0;
            }

            public bool IsLess(INode<T> other)
            {
                return other.Value.CompareTo(Value) < 0;
            }

            public bool IsMore(T other)
            {
                return other.CompareTo(Value) > 0;
            }
        }

        public Node root;

        private SimoRedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public SimoRedBlackTree()
        {

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public SimoRedBlackTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new SimoRedBlackTree<T>(current);
        }

        public void Insert(T value)
        {
            root = Add(root, value);
            root.Color = BLACK;
        } 

        public void Delete(T key)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = Delete(root, key);

            if (this.root != null)
            {
                this.root.Color = BLACK;
            }
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = DeleteMin(root);
            if (this.root != null)
            {
                this.root.Color = BLACK;
            }
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = DeleteMax(root);
            if (this.root != null)
            {
                this.root.Color = BLACK;
            }
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
                node = RotateRight(node);

            if (node.Right == null)
                return null;

            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                node = MoveRedRight(node);

            node.Right = DeleteMax(node.Right);

            return FixUp(node);
        }

        public int Count()
        {
            return Count(root);
        }

        private T FindMinimalValue(Node x)
        {
            if (x.Left == null) return x.Value;
            else return FindMinimalValue(x.Left);
        }

        private Node Delete(Node node, T value)
        {
            if (IsLesser(value, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                    node = MoveRedLeft(node);
                node.Left = Delete(node.Left, value);
            }
            else
            {
                if (IsRed(node.Left))
                    node = RotateRight(node);
                if (AreEqual(value, node.Value) && (node.Right == null))
                    return null;
                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                    node = MoveRedRight(node);
                if (AreEqual(value, node.Value))
                {
                    //node.Value = FindValue(node.Right, FindMinimalValue(node.Right));
                    node.Value = FindMinimalValue(node.Right);
                    node.Right = DeleteMin(node.Right);
                }
                else node.Right = Delete(node.Right, value);
            }

            return FixUp(node);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
                return null;

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                node = MoveRedLeft(node);

            node.Left = DeleteMin(node.Left);

            return FixUp(node);
        }

        private Node Add(Node node, T value)
        {
            if (node == null)
                return new Node(value);

            if (AreEqual(value, node.Value))
                node.Value = value;
            else if (IsLesser(value, node.Value))
                node.Left = Add(node.Left, value);
            else
                node.Right = Add(node.Right, value);

            if (IsRed(node.Right))
                node = RotateLeft(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);

            if (IsRed(node.Left) && IsRed(node.Right))
                ColorFlip(node);

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return Count(node.Left) + Count(node.Right) + 1;
        }

        // Red-Black Helpers

        private bool IsRed(Node node)
        {
            if (node == null) return false;
            return (node.Color == RED);
        }

        private void ColorFlip(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        private Node RotateLeft(Node node)
        {  // Make a right-leaning 3-node lean to the left.
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = temp.Left.Color;
            temp.Left.Color = RED;
            return temp;
        }

        private Node RotateRight(Node node)
        {  // Make a left-leaning 3-node lean to the right.
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = temp.Right.Color;
            temp.Right.Color = RED;
            return temp;
        }

        private Node MoveRedLeft(Node node)
        {  // Assuming that h is red and both h.left and h.left.left
           // are black, make h.left or one of its children red.
            ColorFlip(node);
            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                ColorFlip(node);
            }
            return node;
        }

        private Node MoveRedRight(Node h)
        {  // Assuming that h is red and both h.right and h.right.left
           // are black, make h.right or one of its children red.
            ColorFlip(h);
            if (IsRed(h.Left.Left))
            {
                h = RotateRight(h);
                ColorFlip(h);
            }
            return h;
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
                node = RotateLeft(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);

            if (IsRed(node.Left) && IsRed(node.Right))
                ColorFlip(node);

            return node;
        }

        // Default BST Helpers

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        private bool AreEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }


        //private T FindValue(Node x, T key)
        //{
        //    if (x == null) throw new InvalidOperationException(); //return default;
        //    if (AreEqual(key, x.Value)) return x.Value;
        //    if (IsLesser(key, x.Value)) return FindValue(x.Left, key);
        //    else return FindValue(x.Right, key);
        //}
    }
}