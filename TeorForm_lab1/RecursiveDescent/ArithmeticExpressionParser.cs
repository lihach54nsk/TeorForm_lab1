using System.Collections.Generic;
using System.Text;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1.RecursiveDescent
{
    class ArithmeticExpressionParser
    {
        private int count;
        private bool emptyflag;
        private readonly StringBuilder _resultString;
        private readonly LinkedList<Warning> _states;
        private readonly TextData _source;

        ArithmeticExpressionParser(TextData source)
        {
            _resultString = new StringBuilder();
            _states = new LinkedList<Warning>();
            _source = source;
        }

        public static ArithmeticExpressionParseResult Parse(string source)
        {
            var parser = new ArithmeticExpressionParser(new TextData(source));
            parser.ParseMassive();
            return new ArithmeticExpressionParseResult(parser._resultString.ToString(), parser._states);
        }

        void ParseMassive()
        {
            if (_source.PeekChar() == '\0')
            {
                _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                return;
            }
            count = 0;
            //var c1 = 0;
            string str = "DIMENSION";
            StringBuilder temp = new StringBuilder();

            ParseType();

            SkipSpace();
            SaveChar(' ');

            while (true)
            {
                switch (_source.PeekChar())
                {
                    case '\0':
                        _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                        return;
                    case ' ':
                    case '\n':
                    case '\r':
                        goto endloop;
                    default:
                        temp.Append(_source.PeekChar());
                        _source.AdvanceChar();
                        break;
                }
            }

            endloop:

            if(string.Compare(str, temp.ToString(), true) != 0)
            {
                _states.AddFirst(new Warning("Ожидалось выражение DIMENSION", _source.PeekChar(), _source.Position, WarningType.Error));
            }

            SaveChar(str);

            ParseMassiv4ik();
        }

        void SkipSpace()
        {
            while (true)
            {
                switch (_source.PeekChar())
                {
                    case ' ':
                    case '\n':
                    case '\r':
                        _source.AdvanceChar();
                        break;
                    default:
                        return;
                }
            }
        }

        void ParseType()
        {
            //var count = 0;
            string Istr = "integer";
            string Rstr = "real";
            string Check = "";

            SkipSpace();

            while (true)
            {
                switch (_source.PeekChar())
                {
                    case 'I':
                    case 'i':
                        for (int t = 0; t < Istr.Length; t++)
                        {
                            if (_source.PeekChar() == '\0')
                            {
                                _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                                return;
                            }
                            Check += _source.PeekChar().ToString();
                            _source.AdvanceChar();
                            //    if (_source.PeekChar() == Istr[t] || _source.PeekChar() == Istr[t] - 32)
                            //    {
                            //        SaveChar();
                            //        _source.AdvanceChar();
                            //    }
                            //    else
                            //    {
                            //        if (_source.PeekChar() == '\0')
                            //        {
                            //            _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                            //            return;
                            //        }

                            //        if (count == 0)
                            //        {
                            //            _states.AddFirst(new Warning("Ожидался тип integer", _source.PeekChar(), _source.Position, WarningType.Error));
                            //            count++;
                            //        }
                            //        _source.AdvanceChar();
                            //        t--;
                            //    }
                        }
                        if (Check.ToLower() != Istr)
                        {
                            SaveChar(Istr);
                            _states.AddFirst(new Warning("Ожидался тип integer", _source.PeekChar(), _source.Position, WarningType.Error));
                        }
                        else
                        {
                            SaveChar(Check);
                        }
                        return;

                    case 'R':
                    case 'r':
                        for (int i = 0; i < Rstr.Length; i++)
                        {
                            if (_source.PeekChar() == '\0')
                            {
                                _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                                return;
                            }
                            Check += _source.PeekChar().ToString();
                            _source.AdvanceChar();
                            //if (_source.PeekChar() == Rstr[i] || _source.PeekChar() == Rstr[i] - 32)
                            //{
                            //    SaveChar();
                            //    _source.AdvanceChar();
                            //}
                            //else
                            //{
                            //    if (_source.PeekChar() == '\0')
                            //    {
                            //        _states.AddFirst(new Warning("Конец строки!", _source.PeekChar(), _source.Position, WarningType.Error));
                            //        return;
                            //    }
                            //    if (count == 0)
                            //    {
                            //        _states.AddFirst(new Warning("Ожидался тип real", _source.PeekChar(), _source.Position, WarningType.Error));
                            //        count++;
                            //    }                                
                            //    _source.AdvanceChar();
                            //    i--;
                            //}
                        }
                        if (Check.ToLower() != Rstr)
                        {
                            SaveChar(Rstr);
                            _states.AddFirst(new Warning("Ожидался тип real", _source.PeekChar(), _source.Position, WarningType.Error));
                        }
                        else
                        {
                            SaveChar(Check);
                        }
                        return;
                    case '\0':
                        return;
                    default:
                        _states.AddFirst(new Warning("Ожидался тип integer или real", _source.PeekChar(), _source.Position, WarningType.Error));
                        _source.AdvanceChar();
                        break;
                }
            }
        }

        void ParseMassiv4ik()
        {
            SkipSpace();
            SaveChar(' ');

            ParseName();

            if (_source.PeekChar() == '(')
            {
                SaveChar();
                _source.AdvanceChar();
                ParseBracket();
            }
            else
            {
                _states.AddFirst(new Warning("Не найдена открывающая скобка", _source.PeekChar(), _source.Position, WarningType.Error));
                _source.AdvanceChar();
            }

        }

        void ParseBracketClose()
        {
            if (_source.PeekChar() == ')')
            {
                SaveChar();
                _source.AdvanceChar();
            }
            else
            {
                _states.AddFirst(new Warning("Не найдена закрывающая скобка", _source.PeekChar(), _source.Position, WarningType.Error));
                _source.AdvanceChar();
            }
            if (_source.PeekChar() == '\0')
            {
                return;
            }
        }

        void ParseBracket()
        {
            ParseIndex();

            while (true)
            {
                if (_source.PeekChar() == '\0')
                {
                    return;
                }

                if (_source.PeekChar() == ',')
                {
                    SaveChar();
                    _source.AdvanceChar();
                    ParseMassiv4ik();
                    return;
                }
                else
                {
                    _source.AdvanceChar();
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
            SkipSpace();
            if (_source.PeekChar() == '+' || _source.PeekChar() == '-')
            {
                SaveChar();
                _source.AdvanceChar();
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
                        case ':':
                            SaveChar();
                            _source.AdvanceChar();
                            SkipSpace();
                            ParseNWS_Addition();
                            return;
                        case ',':
                        case ')':
                            ParseNWS_Addition();
                            return;
                        case '\0':
                            return;
                        default:
                            _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            _source.AdvanceChar();
                            break;
                    }
                }
            }
            else
            {
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
                        case ':':
                            SaveChar();
                            _source.AdvanceChar();
                            ParseNWS_Addition();
                            return;
                        case ',':
                        case ')':
                            ParseNWS_Addition();
                            return;
                        case '\0':
                            return;
                        default:
                            _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            _source.AdvanceChar();
                            break;
                    }
                }
            }
        }

        void ParseNWS_Addition()
        {
            int num = 0;
            SkipSpace();
            if (_source.PeekChar() == '+' || _source.PeekChar() == '-')
            {
                SaveChar();
                _source.AdvanceChar();
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
                            num++;
                            break;
                        case ',':
                            if (count < 2)
                            {
                                SaveChar();
                                _source.AdvanceChar();
                                count++;
                                SaveChar(' ');
                                ParseIndex();
                                return;
                            }
                            else
                            {
                                SaveChar(')');
                                _states.AddFirst(new Warning("Количество измерений больше 3-х", _source.PeekChar(), _source.Position, WarningType.Error));
                                _source.AdvanceChar();
                                return;
                            }
                        case ')':
                            if (num == 0) _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            ParseBracketClose();
                            return;
                        default:
                            _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            _source.AdvanceChar();
                            break;
                    }
                }
            }
            else
            {
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
                            num++;
                            break;
                        case ',':
                            if (count < 2)
                            {
                                SaveChar();
                                _source.AdvanceChar();
                                count++;
                                SaveChar(' ');
                                ParseIndex();
                                return;
                            }
                            else
                            {
                                SaveChar(')');
                                _states.AddFirst(new Warning("Количество измерений больше 3-х", _source.PeekChar(), _source.Position, WarningType.Error));
                                _source.AdvanceChar();
                                return;
                            }
                        case ')':
                            if (num == 0) _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            ParseBracketClose();
                            return;
                        default:
                            _states.AddFirst(new Warning("Ожидалось число", _source.PeekChar(), _source.Position, WarningType.Error));
                            _source.AdvanceChar();
                            break;
                    }
                }
            }
        }

        void ParseName()
        {
            emptyflag = false;
            ParseFL();
            if (emptyflag == false)
            {
                ParseNameWFL();
            }
        }

        void ParseNameWFL()
        {
            ParseSL();
        }

        void ParseFL()
        {
            switch (_source.PeekChar())
            {
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
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
                case '\0':
                    return;
                default:
                    emptyflag = true;
                    _states.AddFirst(new Warning("Ожидалось имя массива", _source.PeekChar(), _source.Position, WarningType.Error));
                    return;
            }
        }

        void ParseSL()
        {
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
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
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
                    case '\0':
                        return;
                    default:
                        return;
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

        void SaveChar() => SaveChar(_source.PeekChar());
    }
}