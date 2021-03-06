﻿using System;

namespace FirstLab.Identifiers
{
    /// <summary>
    /// Базовый класс для всех идентификаторов.
    /// </summary>
    [Serializable]
    public class Identifier
    {
        /// <summary>
        /// Имя.
        /// </summary>
        /// <value>The name.</value>
        public String Name {
            get;
            set;
        }

        /// <summary>
        /// Тип.
        /// </summary>
        /// <value>Тип идентификатора.</value>
        public IdentifierType IdType {
            get;
            set;
        }

        /// <summary>
        /// Создает кэземпляр класса.
        /// </summary>
        /// <param name="name">Имя идентификатора.</param>
        /// <param name="idType">Тип идентификатора.</param>
        public Identifier(String name, IdentifierType idType)
        {
            Name = name;
            IdType = idType;
        }

        public Identifier() : this("", IdentifierType.Variable) { }

        public override String ToString()
        {
            return $"{IdType} {Name}";
        }
    }
}
