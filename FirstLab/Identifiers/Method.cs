using System;
using System.Collections.Generic;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    [Serializable]
    public class Method : Identifier
    {
        /// <summary>
        /// Возвращаемый тип метода.
        /// </summary>
        /// <value>Возвращаемый тип.</value>
        public VariableType ReturnType {
            get;
            private set;
        }

        /// <summary>
        /// Список параметров метода.
        /// </summary>
        /// <value>Список параметров.</value>
        public List<Identifier> Options {
            get;
            private set;
        }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="name">Имя метода.</param>
        /// <param name="returnType">Возвращаемый тип метода.</param>
        public Method(String name, VariableType returnType) : base(name, IdentifierType.Method)
        {
            ReturnType = returnType;
            Options = new List<Identifier>();
        }

        /// <summary>
        /// Создает экземпляр класса.
        /// </summary>
        /// <param name="name">Имя метода.</param>
        /// <param name="returnType">Возвращаемый тип метода.</param>
        /// <param name="options">Список параметров.</param>
        public Method(String name, VariableType returnType, List<Identifier> options) : base(name, IdentifierType.Method)
        {
            ReturnType = returnType;
            Options = options;
        }
    }
}
