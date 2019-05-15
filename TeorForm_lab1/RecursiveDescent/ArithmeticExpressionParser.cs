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
        private readonly LinkedList<Warning> _states;
        private readonly List<ISyntaxToken> _source;

        ArithmeticExpressionParser(List<ISyntaxToken> source)
        {
            _resultString = new StringBuilder();
            _states = new LinkedList<Warning>();
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

        void ParseMassive()
        {
            if (_source.Count == indexPos)
            {
                _states.AddFirst(new Warning("Конец строки!", "Backslash zero!", indexPos, WarningType.Error));
                return;
            }

            ParseType();

            SaveChar(' ');

            if (_source[indexPos] is SyntaxTriviaToken token && token.SyntaxKind == SyntaxKind.DimensionKeyword)
            {
                indexPos++;
                SaveChar("DIMENSION");
            }
            else
            {
                SaveChar("DIMENSION");
                _states.AddFirst(new Warning("Ожидалось выражение DIMENSION", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
            }

            ParseMassiv4ik();
        }

        void ParseType()
        {
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
            SaveChar(' ');

            ParseName();

            if (_source[indexPos] is SyntaxTriviaToken token && token.SyntaxKind == SyntaxKind.OpeningBracket)
            {
                indexPos++;
                SaveChar("(");
                ParseBracket();
            }
            else
            {
                indexPos++;
                SaveChar("(");
                _states.AddFirst(new Warning("Не найдена открывающая скобка", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
            }      
        }

        void ParseBracket()
        {
            ParseIndex();

            if (_source.Count == indexPos)
            {
                _states.AddFirst(new Warning("Конец строки!", "Backslash zero!", indexPos, WarningType.Error));
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

        void ParseIndex()
        {
            ParseInterval();
        }

        void ParseInterval()
        {
            ParseNWS();
        }

        void ParseNWS()
        {
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
                    default:
                        indexPos++;
                        _states.AddFirst(new Warning("Ожидался знак или число", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                        break;
                }
                /*if (token.SyntaxKind == SyntaxKind.MinusToken)
                {
                    indexPos++;
                    SaveChar("-");
                }
                else if (token.SyntaxKind == SyntaxKind.PlusToken)
                {
                    indexPos++;
                    SaveChar("+");
                }
                else
                {
                    indexPos++;
                    _states.AddFirst(new Warning("Ожидался знак или число", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                }*/
            }

            if (_source[indexPos] is SyntaxValueToken<int> tokenNWS)
            {
                if (tokenNWS.SyntaxKind == SyntaxKind.IntLiteralToken)
                {
                    indexPos++;
                    SaveChar(tokenNWS.Value.ToString());
                }
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
                        while (count < 2)
                        {
                            ParseIndex();
                        }
                        break;
                    case SyntaxKind.ClosingBracket:
                        indexPos++;
                        SaveChar(")");
                        return;
                    default:
                        indexPos++;
                        _states.AddFirst(new Warning("Ожидался знак или число", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                        break;
                }
                /*if (tokenSign.SyntaxKind == SyntaxKind.ColonToken)
                {
                    indexPos++;
                    SaveChar(":");
                    ParseNWS();
                }
                else if (tokenSign.SyntaxKind == SyntaxKind.CommaToken)
                {
                    indexPos++;
                    count++;
                    SaveChar(",");
                    while (count < 2)
                    {
                        ParseIndex();
                    }                    
                }
                else if (tokenSign.SyntaxKind == SyntaxKind.ClosingBracket)
                {
                    indexPos++;
                    SaveChar(")");
                    return;
                }
                else
                {
                    indexPos++;
                    _states.AddFirst(new Warning("Ожидался знак или число", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
                }*/
            }
        }
        
        void ParseName()
        {
            while (true)
            {
                if (_source[indexPos] is SyntaxIdentifierToken token && token.SyntaxKind == SyntaxKind.IdentifierToken)
                {
                    indexPos++;
                    SaveChar(token.IdentifierName);
                    break;
                }
                else
                {
                    indexPos++;
                    _states.AddFirst(new Warning("Ожидалось имя массива", _source[indexPos].SyntaxKind.ToString(), indexPos, WarningType.Error));
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