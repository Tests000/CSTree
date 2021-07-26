using System;
using System.Collections.Generic;

namespace CSTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string formula = Console.ReadLine();
            Binary_tree tree = new Binary_tree(formula);
            tree.print();
            Console.WriteLine(tree.Result());
        }
    }
}
