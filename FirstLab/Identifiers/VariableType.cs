using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Поддерживаемые типы переменных.
    /// </summary>
    public enum VariableType
    {
        /// <summary>
        /// Булев тип.
        /// </summary>
        Bool,

        /// <summary>
        /// Литерал.
        /// </summary>
        Char,

        /// <summary>
        /// Число с плавающей точкой двойной точности.
        /// </summary>
        Double,

        /// <summary>
        /// Число с плавающей точкой.
        /// </summary>
        Float,

        /// <summary>
        /// Целочисленный тип.
        /// </summary>
        Int,

        /// <summary>
        /// Строка.
        /// </summary>
        String,

        /// <summary>
        /// Пустой тип.
        /// </summary>
        Void
    }
}
