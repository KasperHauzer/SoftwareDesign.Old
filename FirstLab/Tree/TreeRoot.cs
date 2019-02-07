using System;

namespace FirstLab.Tree
{
    /// <summary>
    /// Фактический корень бинарного дерева поиска <see cref="Tree"/>.
    /// </summary>
    [Serializable]
    public class TreeRoot<T>
    {
        /// <summary>
        /// Логический корень дерева.
        /// </summary>
        /// <value>Логический корень дерева.</value>
        public TreeItem<T> LogicRoot {
            get;
            set;
        }

        public TreeRoot() { }

        /// <summary>
        /// Создает кэземпляр класса.
        /// </summary>
        /// <param name="logicRoot">Логический корень бинарного дерева поиска.</param>
        public TreeRoot(TreeItem<T> logicRoot)
        {
            LogicRoot = logicRoot;
        }
    }
}
