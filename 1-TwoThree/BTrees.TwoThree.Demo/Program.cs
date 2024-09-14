using _01.Two_Three.MySolution;
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

        static void Static()
        {
            var tree = new OldTwoThreeTree();
            tree.Insert("q");
            tree.Insert("w");
            tree.Insert("e");
            tree.Insert("r");
            tree.Insert("t");
            tree.Insert("y");
            tree.Insert("u");
            tree.Insert("i");
            tree.Insert("o");
            tree.Insert("p");
            tree.Insert("a");
            tree.Insert("s");
            tree.Insert("d");
            tree.Insert("f");
            tree.Insert("g");
            tree.Insert("h");
            tree.Insert("h");
            tree.Insert("j");
            tree.Insert("k");
            // debug tree.Insert("l") ?

            var bottomUpPrinter = new BottomsUpPrinter();
            bottomUpPrinter.Print(tree);
        }

        static void Prompt()
        {
            var printer = new BottomsUpPrinter();
            var tree = new TwoThreeTree<string>();

            while (true)
            {
                Console.Write("Insert value: ");
                var input = Console.ReadLine();
                tree.Insert(input);
                printer.Print(tree);
            }
        }

        static void PromptInt()
        {
            var printer = new BottomsUpPrinter();
            var tree = new TwoThreeTree<IntWrapper>();

            while (true)
            {
                Console.Write("Insert int value: ");
                var input = new IntWrapper(int.Parse(Console.ReadLine()));
                tree.Insert(input);
                printer.Print(tree);
            }
        }
    }
}

