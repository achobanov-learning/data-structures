using _01.Two_Three.MySolution;
using BTrees.RedBlack;
using Common;
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
            var printer = new BottomsUpPrinter();

            for (int i = 0; i < 50; i++)
            {
                var rbt = new RedBlackTree<int>();
                
                var random = new Random();
                for (var x = 0; x < 20; x++)
                {
                    var value = random.Next(1000);
                    Console.Write(value + " ");
                    try
                    {
                        rbt.Insert(value);
                    }
                    catch (StackOverflowException ex)
                    {
                        ;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Insert completed!");

                printer.Print(rbt);

                Console.WriteLine($"Completed attempt {i}");
            }
        }

        static void Prompt()
        {
            var printer = new BottomsUpPrinter();
            var rbt = new RedBlackTree<int>();
            var ttt = new TwoThreeTree<IntWrapper>();

            while (true)
            {
                Console.Write("Insert int value: ");
                var input = int.Parse(Console.ReadLine());
                var wrapper = new IntWrapper(input);
                
                ttt.Insert(wrapper);
                printer.Print(ttt);
                
                rbt.Insert(input);
                printer.Print(rbt);
            }
        }
    }
}
