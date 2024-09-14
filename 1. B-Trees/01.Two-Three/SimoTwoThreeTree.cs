namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class SimoTwoThreeTree<T> where T : IComparable<T>
    {
        private SimoTreeNode<T> _root;

        public void Insert(T element)
        {
            this._root = this.Insert(this._root, element);
        }

        private SimoTreeNode<T> Insert(SimoTreeNode<T> node, T element)
        {
            if (node == null)
            {
                return new SimoTreeNode<T>(element);
            }

            if (node.IsLeaf())
            {
                return this.MergeNodes(node, new SimoTreeNode<T>(element));
            }

            if (IsLesser(element, node.LeftKey))
            {
                var newNode = this.Insert(node.LeftChild, element);

                return newNode == node.LeftChild ? node : this.MergeNodes(node, newNode);
            }
            else if (node.IsTwoNode() || IsLesser(element, node.RightKey))
            {
                var newNode = this.Insert(node.MiddleChild, element);

                return newNode == node.MiddleChild ? node : this.MergeNodes(node, newNode);
            }
            else
            {
                var newNode = this.Insert(node.RightChild, element);

                return newNode == node.RightChild ? node : this.MergeNodes(node, newNode);
            }
        }

        private bool IsLesser(T element, T key)
        {
            return element.CompareTo(key) < 0;
        }

        private SimoTreeNode<T> MergeNodes(SimoTreeNode<T> current, SimoTreeNode<T> node)
        {
            if (current.IsTwoNode())
            {
                if (IsLesser(current.LeftKey, node.LeftKey))
                {
                    current.RightKey = node.LeftKey;
                    current.MiddleChild = node.LeftChild;
                    current.RightChild = node.MiddleChild;
                }
                else
                {
                    current.RightKey = current.LeftKey;
                    current.RightChild = current.MiddleChild;
                    current.MiddleChild = node.MiddleChild;
                    current.LeftChild = node.LeftChild;
                    current.LeftKey = node.LeftKey;
                }

                return current;
            }
            else if (IsLesser(node.LeftKey, current.LeftKey))
            {
                var newNode = new SimoTreeNode<T>(current.LeftKey)
                {
                    LeftChild = node,
                    MiddleChild = current
                };

                current.LeftChild = current.MiddleChild;
                current.MiddleChild = current.RightChild;
                current.LeftKey = current.RightKey;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }
            else if (IsLesser(node.LeftKey, current.RightKey))
            {
                node.MiddleChild = new SimoTreeNode<T>(current.RightKey)
                {
                    LeftChild = node.MiddleChild,
                    MiddleChild = current.RightChild
                };

                node.LeftChild = current;
                current.RightKey = default;
                current.RightChild = null;

                return node;
            }
            else
            {
                var newNode = new SimoTreeNode<T>(current.RightKey)
                {
                    LeftChild = current,
                    MiddleChild = node
                };

                node.LeftChild = current.RightChild;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this._root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(SimoTreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
