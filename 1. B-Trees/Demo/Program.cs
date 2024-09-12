using _01.Two_Three;
using _01.Two_Three.MySolution;
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
            var tree = new MyTwoThreeTree();
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

            var render = tree.Render();
            Console.WriteLine(render);
        }

        static void Prompt()
        {
            var tree = new MyTwoThreeTree();

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
            tree.Insert("p");
            tree.Insert("q");
            tree.Insert("o");
            tree.Insert("u");
            tree.Insert("t");

            Console.WriteLine(tree.Render());

            while (true)
            {
                Console.Write("Insert value: ");
                var input = Console.ReadLine();
                tree.Insert(input);
                Console.WriteLine(tree.Render());
            }
        }
    }
}
