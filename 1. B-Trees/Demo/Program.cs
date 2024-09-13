using _01.Two_Three.MySolution;
using _01.Two_Three.MySolution.Printers;
using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            Static();
        }

        static void Static()
        {
            var tree = new MyTwoThreeTree<string>();

            tree.Insert("d");
            tree.Insert("f");
            tree.Insert("e");
            tree.Insert("a");
            tree.Insert("c");
            tree.Insert("b");
            tree.Insert("h");
            tree.Insert("i");
            tree.Insert("j");
            tree.Insert("k");
            tree.Insert("m");
            tree.Insert("n");
            tree.Insert("g");
            tree.Insert("l");
            tree.Insert("p");
            tree.Insert("o");

            var printer = new TopDownPrinter();
            printer.Print(tree);

            var bottomUpPrinter = new BottomsUpPrinter<string>();
            bottomUpPrinter.Print(tree);
        }

        static void Prompt()
        {
            var printer = new TopDownPrinter();
            var tree = new MyTwoThreeTree<string>();

            printer.Print(tree);

            while (true)
            {
                Console.Write("Insert value: ");
                var input = Console.ReadLine();
                tree.Insert(input);
                printer.Print(tree);
            }
        }
    }
}
