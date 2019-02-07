using System;

namespace FirstLab.Tree
{
    /// <summary>
    /// Элемент бинарного дерева поиска <see cref="Tree"/>.
    /// </summary>
    [Serializable]
    public class TreeItem<T>
    {
        TreeItem<T> _left, _rigth;

        /// <summary>
        /// Значение элемента.
        /// </summary>
        /// <value>Значение элемента.</value>
        public T Value {
            get;
            set;
        }

        /// <summary>
        /// Корень элемента.
        /// </summary>
        /// <value>Корень элемента.</value>
        public TreeItem<T> Root {
            get;
            set;
        }

        /// <summary>
        /// Левое поддерево.
        /// </summary>
        /// <value>Левое поддерево.</value>
        public TreeItem<T> Left {
            get => _left;
            set {
                if (value != null) value.Root = this;
                _left = value;
            }
        }

        /// <summary>
        /// Правое поддерево.
        /// </summary>
        /// <value>Правое поддерево.</value>
        public TreeItem<T> Rigth {
            get => _rigth;
            set {
                if (value != null) value.Root = this;
                _rigth = value;
            }
        }

        /// <summary>
        /// Указывает, является ли данный элемент листовым.
        /// </summary>
        /// <value><c>true</c> если листовой; в противном случае, <c>false</c>.</value>
        public Boolean IsSheet {
            get => Left is null & Rigth is null;
        }

        public TreeItem()
        {
            Value = default(T);
        }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="value">Значение элемента.</param>
        public TreeItem(T value)
        {
            Value = value;
        }

        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override String ToString()
        {
            return Value.ToString();
        }

        public override Boolean Equals(Object obj)
        {
            if (obj is null) return false;
            if (obj is TreeItem<T>)
                return (obj as TreeItem<T>) == this;

            return false;
        }

        #region Операторы

        public static Boolean operator >(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return false;
            return x.GetHashCode() > y.GetHashCode();
        }

        public static Boolean operator <(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return false;
            return x.GetHashCode() < y.GetHashCode();
        }

        public static Boolean operator ==(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return x is null && y is null;
            return x.GetHashCode() == y.GetHashCode();
        }

        public static Boolean operator !=(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return !(x is null && y is null);
            return x.GetHashCode() != y.GetHashCode();
        }

        public static Boolean operator >=(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return x is null && y is null;
            return x.GetHashCode() >= y.GetHashCode();
        }

        public static Boolean operator <=(TreeItem<T> x, TreeItem<T> y)
        {
            if (x is null || y is null) return x is null && y is null;
            return x.GetHashCode() <= y.GetHashCode();
        }

        #endregion
    }
}