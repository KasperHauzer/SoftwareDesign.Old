using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Идентификатор переменной.
    /// </summary>
    [Serializable]
    public class Variable : Identifier
    {
        /// <summary>
        /// Значение переменной.
        /// </summary>
        /// <value>Значение переменной.</value>
        public String Value {
            get;
            set;
        }

        /// <summary>
        /// Тип переменной.
        /// </summary>
        /// <value>Тип переменной.</value>
        public VariableType VarType {
            get;
            set;
        }

        /// <summary>
        /// Префикс переменной.
        /// </summary>
        /// <value>Префикс переменной.</value>
        public VariablePrefix VarPrefix {
            get;
            set;
        }

        /// <summary>
        /// Создает новый экземпляр класса.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="value">Значение переменной.</param>
        /// <param name="varType">Тип переменной.</param>
        /// <param name="varPrefix">Префикс переменной.</param>
        public Variable(String name, String value, VariableType varType, VariablePrefix varPrefix) : base (name, IdentifierType.Variable)
        {
            Value = value;
            VarType = varType;
            VarPrefix = varPrefix;
        }

        public Variable() : this("default", "NULL", VariableType.String, VariablePrefix.None) { }
    }
}
