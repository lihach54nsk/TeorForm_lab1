using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1.RecursiveDescent
{
    //Здесь будем буквально следовать грамматике
    class ArithmeticExpressionParser
    {
        private readonly StringBuilder _resultString;
        private readonly LinkedList<string> _states;
        private readonly TextData _source;

        ArithmeticExpressionParser(TextData source)
        {
            _resultString = new StringBuilder();
            _states = new LinkedList<string>();
            _source = source;
        }

        public static ArithmeticExpressionParseResult Parse(string source)
        {
            var parser = new ArithmeticExpressionParser(new TextData(source));
            parser.ParseE();
            return new ArithmeticExpressionParseResult(parser._resultString.ToString(), parser._states);
        }

        void ParseE()
        {
            _states.AddLast("E");

            ParseT();
            ParseA();
        }

        void ParseA()
        {
            _states.AddLast("A");

            switch (_source.PeekChar())
            {
                case '+':
                case '-':
                    _states.AddLast(_source.PeekChar().ToString());
                    SaveChar();
                    _source.AdvanceChar();
                    ParseT();
                    ParseA();
                    break;
                default:
                    _states.AddLast("e");
                    return;
            }
        }

        void ParseT()
        {
            _states.AddLast("T");

            ParseO();
            ParseB();
        }

        void ParseB()
        {
            _states.AddLast("B");

            switch (_source.PeekChar())
            {
                case '*':
                case '/':
                    _states.AddLast(_source.PeekChar().ToString());
                    SaveChar();
                    _source.AdvanceChar();
                    ParseO();
                    ParseB();
                    break;
                default:
                    _states.AddLast("e");
                    return;
            }
        }

        void ParseO()
        {
            _states.AddLast("О");

            switch (_source.PeekChar())
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    SaveChar();
                    _source.AdvanceChar();
                    ParseIdentifier();
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    SaveChar();
                    _source.AdvanceChar();
                    ParseNumber();
                    break;
                case '(':
                    SaveChar();
                    _states.AddLast("(");
                    _source.AdvanceChar();
                    ParseBracket();
                    break;
                default:
                    _states.AddLast("Ошибка!");
                    return;
            }
        }

        void ParseNumber()
        {
            _states.AddLast("Number");

            while (true)
            {
                switch (_source.PeekChar())
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        SaveChar();
                        _source.AdvanceChar();
                        break;
                    default:
                        return;
                }
            }
        }

        void ParseIdentifier()
        {
            _states.AddLast("Identifier");

            while (true)
            {
                switch (_source.PeekChar())
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                        SaveChar();
                        _source.AdvanceChar();
                        break;
                    default:
                        return;
                }
            }
        }

        void ParseBracket()
        {
            ParseE();
            if (_source.PeekChar() == ')')
            {
                _states.AddLast(")");
                SaveChar();
                _source.AdvanceChar();
            }
        }

        void SaveChar(char value)
        {
            _resultString.Append(value);
        }

        void SaveChar() => SaveChar(_source.PeekChar());
    }
}