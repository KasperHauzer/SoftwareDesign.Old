using FirstLab.Identifiers;
using FirstLab.Tree;
using FirstLab.Parser;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FirstLab
{
    class Program
    {
        static void Main()
        {
            String text = File.ReadAllText("input.txt");
            Tree<Identifier> tree = new Tree<Identifier>();
            CSCodeParser parser = new CSCodeParser(text);

            int n = 0;
            foreach (Identifier i in parser) {
                Boolean flag = tree.Add(i);

                Console.WriteLine($"{++n}. Добавляется: {i} - {flag};");
            }

            n = 0;
            Console.WriteLine("\nВывод элементов дерева.");
            foreach (Identifier i in tree) {
                Console.WriteLine($"{++n}. {i}");
            }
        }
    }
}
