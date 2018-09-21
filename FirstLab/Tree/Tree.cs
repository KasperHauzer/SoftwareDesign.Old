using System;
using System.Collections;

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
            T[] array = new T[Count];
            ToArray(RealRoot.LogicRoot, ref array);

            foreach (T i in array)
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
                if (point.IsSheet && sheet < point) {
                    point.Left = sheet;
                    break;
                }
                else if (point.IsSheet && sheet >= point) {
                    point.Rigth = sheet;
                    break;
                }
                else if (sheet < point) point = point.Left;
                else point = point.Rigth;

            return true;
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
        static void ToArray(TreeItem<T> rootItem, ref T[] array, Int32 beginingIndex = 0)
        {
            if (rootItem is null ||
                array is null ||
                beginingIndex < 0) return;

            if (rootItem != null) {
                if (rootItem.Left != null) ToArray(rootItem.Left, ref array, beginingIndex + 1);
                if (rootItem.Rigth != null) ToArray(rootItem.Rigth, ref array, beginingIndex + 1);
                array[beginingIndex++] = rootItem.Value;
            }

            return;
        }
    }
}
