using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Поддерживаемые префиксы переменных.
    /// </summary>
    public enum VariablePrefix
    {
        /// <summary>
        /// Без префикса.
        /// </summary>
        None = 1,

        /// <summary>
        /// Выводимое значение.
        /// </summary>
        Outputted,

        /// <summary>
        /// Отраженное значение.
        /// </summary>
        Reflected
    }
}
