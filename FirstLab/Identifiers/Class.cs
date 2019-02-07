using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Идентификатор класса.
    /// </summary>
    [Serializable]
    public class Class : Identifier
    {
        /// <summary>
        /// Создает кэземпляр класса.
        /// </summary>
        /// <param name="name">Имя класса.</param>
        public Class(String name) : base(name, IdentifierType.Class) { }
    }
}
