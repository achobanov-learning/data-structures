using BTrees.RedBlack;
using Common;
using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            Prompt();
        }

        static void Prompt()
        {
            var printer = new BottomsUpPrinter<int>();
            var tree = new RedBlackTree<int>();

            while (true)
            {
                Console.Write("Insert int value: ");
                var input = int.Parse(Console.ReadLine());
                tree.Insert(input);
                printer.Print(tree);
            }
        }
    }
}
