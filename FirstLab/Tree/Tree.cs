using System;
using System.Collections;
using System.Collections.Generic;

namespace FirstLab.Tree
{
    /// <summary>
    /// Представляет бинарное дерево поиска.
    /// </summary>
    public class Tree<T> : IEnumerable
    {
        /// <summary>
        /// Число элементов в дереве.
        /// </summary>
        /// <value>Число элементов в дереве.</value>
        public Int32 Count {
            get;
            set;
        }

        /// <summary>
        /// Фактический корень дерева.
        /// </summary>
        /// <value>Фактический корень дерева.</value>
        TreeRoot<T> RealRoot {
            get;
            set;
        }

        /// <summary>
        /// Корень дерева.
        /// </summary>
        /// <value>Корень дерева.</value>
        public T Root {
            get => RealRoot.LogicRoot.Value;
        }

        public Tree()
        {
            Count = 0;
            RealRoot = new TreeRoot<T>();
        }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="rootItem">Корневой элемент дерева.</param>
        public Tree(TreeItem<T> rootItem)
        {
            Count = 1;
            RealRoot = new TreeRoot<T>(rootItem);
        }

        /// <summary>
        /// Добавляет элемент в дерево.
        /// </summary>
        /// <returns>Успешность добавления.</returns>
        /// <param name="item">Добавляесый элемент.</param>
        public Boolean Add(T item)
        {
            var sheet = new TreeItem<T>(item);

            //Если корневой элемент не был определене,
            //то определяем его добавляемым элементом.
            //Соответственно счетчик элементов увеличиваем на один 
            //и возвращае true.
            if (RealRoot.LogicRoot is null) {
                RealRoot.LogicRoot = sheet;
                ++Count;
                return true;
            }

            //Если предыдушее условие не прошло, то пытаемся добавить
            //текущий элемент как листок для элемента, на который указывает
            //ссылка логического корня дерева.
            if (Concat(RealRoot.LogicRoot, sheet)) {
                ++Count;
                return true;
            }

            //Если вдруг по каким-то причинам два предыдущих условия не прошли
            //возвращаем false.
            return false;
        }

        public override String ToString()
        {
            return $"Count {Count}";
        }

        public IEnumerator GetEnumerator()
        {
            List<T> list = new List<T>();
            ToList(RealRoot.LogicRoot, ref list);

            foreach (T i in list)
                yield return i;
        }

        /// <summary>
        /// Соеденяет два элемента дерева связью корень - листок.
        /// </summary>
        /// <returns>Успешность соеденения.</returns>
        /// <param name="root">Корневое дерево.</param>
        /// <param name="sheet">Листовое дерево.</param>
        static Boolean Concat(TreeItem<T> root, TreeItem<T> sheet)
        {
            //Если в корневом дереве имеется добавляемый элемент, то 
            //по правилам бинарного дерева поиска повторное добавление
            //такого элемента невозможно.
            if (IsExists(root, sheet)) return false;

            //Переменная для движения по корневому дереву.
            var point = root;

            //Движемся до корневого элемента для добавляемого.
            while (point != null)
                if (point.Left is null && sheet < point) {
                    point.Left = sheet;
                    return true;
                }
                else if (point.Rigth is null && sheet >= point) {
                    point.Rigth = sheet;
                    return true;
                }
                else if (sheet < point) point = point.Left;
                else point = point.Rigth;

            return false;
        }

        /// <summary>
        /// Сравнивает элементы дерева с листом посредством рекурсивного обхода всех элементов дерева.
        /// </summary>
        /// <returns><c>true</c>, если равный элемент был найден, <c>false</c> в противном случае.</returns>
        /// <param name="tree">Дерево.</param>
        /// <param name="item">Сравниваемый элемент.</param>
        static Boolean IsExists(TreeItem<T> tree, TreeItem<T> item)
        {
            if (tree != null) {
                if (tree.Left != null) return IsExists(tree.Left, item);
                if (tree.Rigth != null) return IsExists(tree.Rigth, item);
                return tree == item;
            }

            return false;
        }

        /// <summary>
        /// Преобразует дерево в массив рекурсивным обходом всех элементов.
        /// </summary>
        /// <param name="rootItem">Корневой элемент дерева.</param>
        /// <param name="array">Массив, в который должны заноситься элементы из дерева.</param>
        /// <param name="beginingIndex">Начальный индекс элемента дерева (не может превышать общее число элементов в дереве).</param>
        static void ToList(TreeItem<T> rootItem, ref List<T> list)
        {
            if (rootItem is null) return;

            if (rootItem != null) {
                list.Add(rootItem.Value);
                if (rootItem.Left != null) ToList(rootItem.Left, ref list);
                if (rootItem.Rigth != null) ToList(rootItem.Rigth, ref list);
            }

            return;
        }
    }
}
