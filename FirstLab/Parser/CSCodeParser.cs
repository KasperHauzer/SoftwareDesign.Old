using FirstLab.Identifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Collections;

namespace FirstLab.Parser
{
    /// <summary>
    /// Парсер мета-идентификаторов csharp.
    /// </summary>
    public class CSCodeParser : IEnumerable
    {
        String _text;

        #region Регулярные выражения

        /// <summary>
        /// Основные шаблоны
        /// </summary>
        static readonly Regex Space = new Regex("[ ]*");
        static readonly Regex End = new Regex("\\b" + Space);
        static readonly Regex SomeText = new Regex("[\\s\\S]");

        /// <summary>
        /// Шаблоны ключевых слов, типов, имени
        /// </summary>
        static readonly Regex Class = new Regex("class" + End, RegexOptions.IgnoreCase);
        static readonly Regex Const = new Regex("const" + End, RegexOptions.IgnoreCase);
        static readonly Regex VarTypes = new Regex("(bool|char|double|float|int|string|void)" + End, RegexOptions.IgnoreCase);
        static readonly Regex KeyWords = new Regex($"\\b({Class}|{Const}|{VarTypes}|(true|false)|(out|ref))" + End, RegexOptions.IgnoreCase);
        static readonly Regex BaseTypes = new Regex($"({Class}|{Const}|{VarTypes})" + End, RegexOptions.IgnoreCase);
        static readonly Regex Name = new Regex($"(?<={BaseTypes})[\\d\\w]+" + End);

        /// <summary>
        /// Шаблоны для составления переменной
        /// </summary>
        static readonly Regex Prefixes = new Regex($"(out|ref|{Space})" + End);
        static readonly Regex BoolForce = new Regex("(true|false)");
        static readonly Regex CharForce = new Regex("'[\\s\\S]'");
        static readonly Regex DigitForce = new Regex("(\\d+(.|,)\\d+|//d+)");
        static readonly Regex StringForce = new Regex("\"[\\s\\S]*\"");
        static readonly Regex AllForces = new Regex($"({BoolForce}|{CharForce}|{DigitForce}|{StringForce})");
        static readonly Regex AnyForce = new Regex($"(?<=={Space}){AllForces}");
        static readonly Regex AnyVar = new Regex($"{Prefixes}{VarTypes}{Name}(={Space}{AllForces}|{End})");
        static readonly Regex AnyMethod = new Regex($"{VarTypes}{Name}[(]{SomeText}*[)]");
        static readonly Regex Options = new Regex($"(?<=[(]{Space}){SomeText}*(?={Space}[)])" + End);

        #endregion

        #region Свойства

        public String Text {
            get => _text;
            set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(value.GetType().Name, "The text can't be null, empty or only white space.");
                if (CheckText(value))
                    throw new ArgumentException("The text inludes prohibitted symbols.", value.GetType().Name);

                _text = value
                    .Replace("\n", " ")
                    .Replace("\t", " ")
                    .Trim();

                Statements = _text
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                Index = 0;
                CurrentId = null;
            }
        }

        public String[] Statements {
            get;
            private set;
        }

        /// <summary>
        /// Список идентификаторов.
        /// </summary>
        /// <value>Список идентификаторов.</value>
        public List<Identifier> IdList {
            get;
            private set;
        }

        Int32 Index {
            get;
            set;
        }

        String Row {
            get => Statements[Index];
        }

        Identifier CurrentId {
            get;
            set;
        }

        #endregion

        public CSCodeParser(String text)
        {
            Text = text;
            IdList = new List<Identifier>();
        }

        public Boolean SiftId()
        {
            if (String.IsNullOrWhiteSpace(Row))
                return false;

            if (Class.IsMatch(Row)) {
                CurrentId = new Class(GetName(Row));
                return true;
            }

            if (Const.IsMatch(Row)) {
                CurrentId = new Constant(
                    GetName(Row), 
                    GetValue(Row), 
                    GetVarType(Row)
                );
                return true;
            }

            if (AnyMethod.IsMatch(Row)) {
                CurrentId = new Method(
                    GetName(Row), 
                    GetVarType(Row),
                    GetOptions(Row)
                );
                return true;
            }

            if (AnyVar.IsMatch(Row)) {
                CurrentId = new Variable(
                    GetName(Row),
                    GetValue(Row),
                    GetVarType(Row),
                    GetPrefix(Row)
                );
                return true;
            }

            return false;
        }

        public Boolean Select()
        {
            if (CurrentId == null)
                return false;

            IdList.Add(CurrentId);
            CurrentId = null;
            return true;
        }

        public Boolean Next()
        {
            if (Index + 1 >= Statements.Length)
                return false;

            Select();
            Index++;
            if (String.IsNullOrWhiteSpace(Row)) return Next();
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            Index = 0;

            do {
                if (SiftId()) yield return CurrentId;
            } while (Next());

            Index = 0;
        }

        public static Boolean CheckText(String text)
        {
            Regex allowedSymbols = new Regex($"[^\\d|\\w|{Space}|,|;|\"]");
            return !allowedSymbols.IsMatch(text);
        }

        static String GetName(String row)
        {
            if (String.IsNullOrWhiteSpace(row))
                return null;

            foreach (Match i in Name.Matches(row))
                if (!KeyWords.IsMatch(i.Value))
                    return i.Value.Trim();

            return "no name";
        }

        static String GetValue(String row)
        {
            if (String.IsNullOrWhiteSpace(row))
                return null;

            return  AnyForce.Match(row).Value.Trim()
                            .Replace("\"", "")
                            .Replace("'", "");
        }

        static VariableType GetVarType(String row)
        {
            if (String.IsNullOrWhiteSpace(row))
                return default(VariableType);

            String tmp = VarTypes.Match(row).Value
                                 .Trim().ToLower();

            if (tmp == "bool") return VariableType.Bool;
            if (tmp == "char") return VariableType.Char;
            if (tmp == "double") return VariableType.Double;
            if (tmp == "float") return VariableType.Float;
            if (tmp == "int") return VariableType.Int;
            if (tmp == "string") return VariableType.String;
            if (tmp == "void") return VariableType.Void;
            return default(VariableType);
        }

        static VariablePrefix GetPrefix(String row)
        {
            if (String.IsNullOrWhiteSpace(row))
                return default(VariablePrefix);

            String tmp = Prefixes.Match(row).Value
                                 .Trim().ToLower();

            if (tmp == "") return VariablePrefix.None;
            if (tmp == "out") return VariablePrefix.Outputted;
            if (tmp == "ref") return VariablePrefix.Reflected;
            return default(VariablePrefix);
        }

        static List<Identifier> GetOptions(String row)
        {
            if (String.IsNullOrWhiteSpace(row))
                return new List<Identifier>();

            List<Identifier> list = new List<Identifier>();
            row = Options.Match(row).Value.Trim();

            foreach (Match i in AnyVar.Matches(row)) {
                list.Add(new Variable(
                    GetName(i.Value),
                    GetValue(i.Value),
                    GetVarType(i.Value),
                    GetPrefix(i.Value)
                ));
            }

            return list;
        }
    }
}
