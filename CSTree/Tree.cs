using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSTree
{

    struct Operation
    {
        public int priority { get; set; }
        public int pos { get; set; }

    }

    class Tree
    {
        public string info = "";
        public Tree left, right;
    }

    class Binary_tree
    {
        public Tree root { get; set; }
        public Binary_tree(string formula)
        {
            root = Build_tree(formula);
        }
        private Tree Build_tree(string formula)
        {
            var op = ListOfPriority(formula);
            int pos;
            Tree tree = new Tree();
            if (op.Count > 0)
            {
                pos = op.First().pos;
                tree.info = formula[pos].ToString();
                tree.left = Build_tree(formula.Substring(0, pos));
                tree.right = Build_tree(formula.Substring(pos + 1, formula.Length - pos - 1));
            }
            else
            {
                if (formula.Length > 0)
                {
                    while (formula[0] == '(')
                        formula = formula.Substring(1, formula.Length - 1);
                    while (formula[formula.Length - 1] == ')')
                        formula = formula.Substring(0, formula.Length - 1);
                }
                tree.info = formula;
            }
            return tree;
        }
        private List<Operation> ListOfPriority(string formula)
        {
            int priority = 0;
            List<Operation> listOperation = new List<Operation>();
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '(')
                    priority += 5;
                else if (formula[i] == ')')
                    priority -= 5;
                else if (formula[i] == '+')
                {
                    listOperation.Add(new Operation { priority = priority + 1, pos = i });
                }
                else if (formula[i] == '-')
                {
                    listOperation.Add(new Operation { priority = priority + 2, pos = i });
                }
                else if (formula[i] == '*')
                {
                    listOperation.Add(new Operation { priority = priority + 3, pos = i });
                }
                else if (formula[i] == '/')
                {
                    listOperation.Add(new Operation { priority = priority + 4, pos = i });
                }
                else if (formula[i] == '^')
                {
                    listOperation.Add(new Operation { priority = priority + 5, pos = i });
                }
            }
            listOperation.Sort((Operation a, Operation b) =>
            {
                return (b.priority % 6 == 5 && a.priority % 6 == 5) ? Math.Sign(a.pos - b.pos) : Math.Sign(b.pos - a.pos) + 2 * (a.priority - b.priority);
            });
            return listOperation;
        }
        public void print()
        {
            Print_tree(root, 0);
        }
        private void Print_tree(Tree t, int level)
        {
            if (t != null)
            {
                Print_tree(t.right, level + 1);
                for (int i = 1; i <= level; i++)
                    Console.Write("   ");
                Console.WriteLine(t.info);
                Print_tree(t.left, level + 1);
            }
        }
        public double Result()
        {
            return Count(root);
        }
        private double Count(Tree tree)
        {
            if (tree.info == "*")
                return Count(tree.left) * Count(tree.right);
            else if (tree.info == "/")
                return Count(tree.left) / Count(tree.right);
            else if (tree.info == "+")
                return Count(tree.left) + Count(tree.right);
            else if (tree.info == "-")
                return Count(tree.left) - Count(tree.right);
            else if (tree.info == "^")
                return Math.Pow(Count(tree.left), Count(tree.right));
            else
            {
                if (tree.info != "")
                    return Int32.Parse(tree.info);
                else
                    return 0;
            }
        }
    }
}
