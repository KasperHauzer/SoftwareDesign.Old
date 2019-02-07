using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Идентификатор константы.
    /// </summary>
    [Serializable]
    public class Constant : Identifier
    {
        /// <summary>
        /// Значение константы.
        /// </summary>
        /// <value>Значение константы.</value>
        public String Value {
            get;
            set;
        }

        /// <summary>
        /// Тип константы.
        /// </summary>
        /// <value>Тип константы.</value>
        public VariableType VarType {
            get;
            set;
        }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="name">Имя константы.</param>
        /// <param name="value">Значение константы.</param>
        /// <param name="varType">Тип константы.</param>
        public Constant(String name, String value, VariableType varType) : base(name, IdentifierType.Constant)
        {
            Value = value;
            VarType = varType;
        }

        public Constant() : this("", "", VariableType.Bool) { }
    }
}
