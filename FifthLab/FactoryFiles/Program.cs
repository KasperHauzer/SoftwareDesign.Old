using FirstLab.Identifiers;
using FirstLab.Parser;
using FirstLab.Tree;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace FifthLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //Генерируем тестовый экземпляр бинарного дерева.
            string text = File.ReadAllText("input.txt");
            Tree<Identifier> tree = new Tree<Identifier>();
            CSCodeParser parser = new CSCodeParser(text);
            
            foreach (Identifier i in parser) {
                tree.Add(i);
            }

            //Бинарная сериализация в буфер и обратно.
            byte[] buffer = new byte[0];
            Tree<Identifier> treeClone;

            using (var stream = new MemoryStream()) {
                new BinaryFormatter().Serialize(stream, tree);
                buffer = stream.GetBuffer();
            }

            using (var stream = new MemoryStream(buffer)) {
                treeClone = (Tree<Identifier>)new BinaryFormatter().Deserialize(stream);
            }

            //Xml сериализация в файл и обратно.
            var types = new Type[] {
                typeof(Identifier),
                typeof(Variable),
                typeof(Constant),
                typeof(Method),
                typeof(Class)
            };

            using (var stream = new FileStream("output.txt", FileMode.OpenOrCreate)) {
                new XmlSerializer(typeof(Tree<Identifier>), types).Serialize(stream, tree);
            }


        }
    }
}
