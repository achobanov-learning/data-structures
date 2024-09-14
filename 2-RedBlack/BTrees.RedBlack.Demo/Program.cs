using BTrees.RedBlack;
using Common;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var printer = new BottomsUpPrinter<int>();
            var rbt = new RedBlackTree<int>(5, new RedBlackNode<int>(10));

            printer.Print(rbt);
        }
    }
}
