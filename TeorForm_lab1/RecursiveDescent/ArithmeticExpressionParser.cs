using System.Collections.Generic;
using System.Text;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1.RecursiveDescent
{
    class ArithmeticExpressionParser
    {
        private int count;
        private int indexPos;
        private readonly StringBuilder _resultString;
        private readonly LinkedList<IWarning> _states;
        private readonly List<ISyntaxToken> _source;

        ArithmeticExpressionParser(List<ISyntaxToken> source)
        {
            _resultString = new StringBuilder();
            _states = new LinkedList<IWarning>();
            _source = source;
            indexPos = 0;
            count = 0;
        }

        public static ArithmeticExpressionParseResult Parse(List<ISyntaxToken> source)
        {
            var parser = new ArithmeticExpressionParser(source);
            parser.ParseMassive();
            return new ArithmeticExpressionParseResult(parser._resultString.ToString(), parser._states);
        }

        void Unknown()
        {
            if (_source.Count == indexPos)
            {
                _states.AddFirst(new UnkWarning("Ожидались латинские символы", "Конец строки!", WarningType.Error));
                return;
            }

            while (_source[indexPos].SyntaxKind == SyntaxKind.Unknown)
            {
                _states.AddFirst(new Warning("Неизвестный токен", _source[indexPos].SyntaxKind.ToString(), _source[indexPos].SourceTextPosition, WarningType.Error));
                indexPos++;
                break;
            }
        }

        void ParseMassive()
        {
            ParseType();

            SaveChar(' ');

            if (_source.Count == indexPos)
            {
                SaveChar("DIMENSION <ИМЯ>(<ИНТЕРВАЛ>)");
                _states.AddFirst(new UnkWarning("Ожидалось выражение", "Конец строки!", WarningType.Error));
                return;
            }

            if (_source[indexPos] is SyntaxTriviaToken token && token.SyntaxKind == SyntaxKind.DimensionKeyword)
            {
                indexPos++;
                SaveChar("DIMENSION");
            }
            else
            {
                SaveChar("DIMENSION");
                _states.AddFirst(new Warning("Ожидалось выражение DIMENSION", _source[indexPos].SyntaxKind.ToString(), _source[indexPos].SourceTextPosition, WarningType.Error));
            }

            ParseMassiv4ik();
        }

        void ParseType()
        {
            if (_source.Count == indexPos)
            {
                SaveChar("<ТИП>");
                _states.AddFirst(new UnkWarning("Ожидалось выражение", "Конец строки!", WarningType.Error));
                return;
            }

            switch (_source[indexPos])
            {
                case SyntaxTriviaToken tokenI:
                    if (tokenI.SyntaxKind == SyntaxKind.IntegerKeyword)
                    {
                        indexPos++;
                        SaveChar("integer");
                    }
                    else if (tokenI.SyntaxKind == SyntaxKind.RealKeyword)
                    {
                        indexPos++;
                        SaveChar("real");
                    }
                    break;

                default:
                    SaveChar("<ТИП>");
                    _states.AddFirst(new Warning("Ожидался тип integer или real", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                    break;
            }
        }

        void ParseMassiv4ik()
        {
            Unknown();

            SaveChar(' ');

            ParseName();

            Unknown();

            while (true)
            {
                if (_source.Count == indexPos)
                {
                    SaveChar("(<ИНТЕРВАЛ>)");
                    _states.AddFirst(new UnkWarning("Ожидалась открывающая скобка", "Конец строки!", WarningType.Error));
                    return;
                }

                switch (_source[indexPos].SyntaxKind)
                {
                    case SyntaxKind.PlusToken:
                    case SyntaxKind.MinusToken:
                    case SyntaxKind.IntLiteralToken:
                        SaveChar('(');
                        _states.AddFirst(new Warning("Не найдена открывающая скобка", _source[indexPos].SyntaxKind.ToString(), _source[indexPos].SourceTextPosition, WarningType.Error));
                        ParseBracket();
                        return;
                    case SyntaxKind.OpeningBracket:
                        SaveChar('(');
                        indexPos++;
                        ParseBracket();
                        return;
                    default:
                        _states.AddFirst(new Warning("Не найдена открывающая скобка", _source[indexPos].SyntaxKind.ToString(), _source[indexPos].SourceTextPosition, WarningType.Error));
                        indexPos++;                       
                        break;
                }
            }                    
        }

        void ParseBracket()
        {
            ParseIndex();

            if (_source.Count == indexPos)
            {
                return;
            }

            if (_source[indexPos] is SyntaxTriviaToken token)
            {
                if (_source[indexPos].SyntaxKind == SyntaxKind.CommaToken)
                {
                    indexPos++;
                    SaveChar(",");
                    ParseMassiv4ik();
                }
            }
        }

        void ParseIndex() => ParseInterval();

        void ParseInterval() => ParseNWS();

        void ParseNWS()
        {
            if (_source.Count == indexPos)
            {
                SaveChar("<ЦСЗ>)");
                _states.AddFirst(new UnkWarning("Ожидалось число со знаком", "Конец строки!", WarningType.Error));
                return;
            }

            if (_source[indexPos] is SyntaxTriviaToken token)
            {
                switch (token.SyntaxKind)
                {
                    case SyntaxKind.MinusToken:
                        indexPos++;
                        SaveChar("-");
                        break;
                    case SyntaxKind.PlusToken:
                        indexPos++;
                        SaveChar("+");
                        break;
                    case SyntaxKind.ClosingBracket:                        
                        SaveChar("<ЦСЗ>)");
                        _states.AddFirst(new Warning("Ожидалось число со знаком", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                        indexPos++;
                        return;
                    default:
                        _states.AddFirst(new UnkWarning("Ожидался знак или число", "Конец строки!", WarningType.Error));
                        indexPos++;
                        break;
                }
            }

            if (_source.Count == indexPos)
            {
                SaveChar("<ЦБЗ>)");
                _states.AddFirst(new UnkWarning("Ожидалось число", "Конец строки!", WarningType.Error));
                return;
            }

            if (_source[indexPos] is SyntaxValueToken<int> tokenNWS)
            {
                if (tokenNWS.SyntaxKind == SyntaxKind.IntLiteralToken)
                {
                    SaveChar(tokenNWS.Value.ToString());
                    indexPos++;
                }
            }

            if (_source.Count == indexPos)
            {
                SaveChar(')');
                _states.AddFirst(new UnkWarning("Ожидалась закрывающаяся скобка", "Конец строки!", WarningType.Error));
                return;
            }

            if (_source[indexPos] is SyntaxTriviaToken tokenSign)
            {
                switch (tokenSign.SyntaxKind)
                {
                    case SyntaxKind.ColonToken:
                        indexPos++;
                        SaveChar(":");                       
                        ParseNWS();
                        break;
                    case SyntaxKind.CommaToken:
                        indexPos++;
                        count++;
                        SaveChar(",");
                        if (count < 2)
                        {
                            ParseIndex();
                        }
                        break;
                    case SyntaxKind.ClosingBracket:
                        indexPos++;
                        SaveChar(")");
                        return;
                    default:
                        _states.AddFirst(new Warning("Ожидался знак или число", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                        indexPos++;
                        break;
                }
            }
        }

        void ParseName()
        {
            while (true)
            {
                if (_source.Count == indexPos)
                {
                    SaveChar("<ИМЯ>");
                    _states.AddFirst(new UnkWarning("Ожидалось имя массива", "Конец строки!", WarningType.Error));
                    return;
                }

                if (_source[indexPos] is SyntaxIdentifierToken token && token.SyntaxKind == SyntaxKind.IdentifierToken)
                {
                    SaveChar(token.IdentifierName);
                    indexPos++;
                    break;
                }
                else
                {
                    _states.AddFirst(new Warning("Ожидалось имя массива", _source[indexPos].SyntaxKind.ToString(), _source[indexPos].SourceTextPosition, WarningType.Error));
                    indexPos++;
                }
            }
        }

        void SaveChar(char value)
        {
            _resultString.Append(value);
        }

        void SaveChar(string value)
        {
            _resultString.Append(value);
        }
    }
}