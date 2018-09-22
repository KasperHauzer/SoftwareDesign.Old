using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Типы поддерживаемых идентификаторов.
    /// </summary>
    public enum IdentifierType
    {
        /// <summary>
        /// Класс.
        /// </summary>
        Class = 1,

        /// <summary>
        /// Константа.
        /// </summary>
        Constant,

        /// <summary>
        /// Метод.
        /// </summary>
        Method,

        /// <summary>
        /// Переменная.
        /// </summary>
        Variable
    }
}
