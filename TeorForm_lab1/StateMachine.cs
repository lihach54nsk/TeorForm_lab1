using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1
{
    class StateMachine
    {
        Mode mode;
        List<Errors> errors;
        TextData data;
        StringBuilder resultString;

        public bool Parser(TextData textData, out List<Errors> errorsCollection, out string result)
        {
            mode = Mode.Decimal;
            errors = new List<Errors>();
            data = textData;
            resultString = new StringBuilder();

            while (true)
            {
                switch (mode)
                {
                    case Mode.Decimal: // старт - знак или цифра 1
                        ParseDecimal();
                        break;
                    case Mode.UnsignedDecimal: // цифры и экспонента, точка 2
                        ParseUnsignedDecimal();
                        break;
                    case Mode.DecimalWithExponent: // нашли экспоненту 4
                        ParseDecimalWithExponent();
                        break;
                    /* case Mode.UnsignedDecimalWithExponentDigit: // обязательна одна цифра после Е 5
                         ParseUnsignedDecimalWithExponentDigit();
                         break;*/
                    case Mode.UnsignedDecimalWithExponent: // считывание дальнейшего значения экспоненты и типа после неё 6
                        ParseUnsignedDecimalWithExponent();
                        break;
                    case Mode.UnsignedDecimalWithDot: // после точки не может быть второй точки 3
                        ParseUnsignedDecimalWithDot();
                        break;
                    case Mode.End:
                        errorsCollection = errors;
                        result = resultString.ToString();
                        return errors.All(x => x.errorType != ErrorType.Error);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        void ParseUnsignedDecimalWithDot()
        {
            while (true)
            {
                switch (data.PeekChar())
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
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                    case 'E':
                    case 'e':
                        mode = Mode.DecimalWithExponent; // нашли экспоненту
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case 'F': // нашли тип 
                    case 'f':
                    case 'L':
                    case 'l':
                        mode = Mode.End;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    default:
                        MakeWarning("Unknown character! There can only be digit from 0 to 9 or type character or E/e",
                            data.PeekChar(),
                            data.Position,
                            ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseUnsignedDecimalWithExponent()
        {
            while (true)
            {
                switch (data.PeekChar())
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
                        mode = Mode.UnsignedDecimalWithExponent;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                    case 'F': // нашли тип 
                    case 'f':
                    case 'L':
                    case 'l':
                        mode = Mode.End;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                    case ' ':
                    case '\t':
                    case '\n':
                        mode = Mode.End;
                        return;
                    default:
                        MakeWarning("Unknown character! There can only be digit from 0 to 9 or type character",
                            data.PeekChar(),
                            data.Position,
                            ErrorType.Error);
                        data.AdvanceChar();
                        return;
                }
            }
        }

        /* private void ParseUnsignedDecimalWithExponentDigit()
         {
             while (true)
             {
                 switch (data.PeekChar())
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
                         mode = Mode.UnsignedDecimalWithExponent;
                         resultString.Append(data.PeekChar());
                         data.AdvanceChar();
                         break;
                     case 'F':
                     case 'f':
                     case 'L':
                     case 'l':
                         mode = Mode.End;
                         resultString.Append(data.PeekChar());
                         data.AdvanceChar();
                         return;
                     case '\0':
                     case ' ':
                     case '\t':
                     case '\n':
                         mode = Mode.End;
                         return;
                     default:
                         MakeWarning("Unknown character! There can only be digit from 0 to 9",
                     data.PeekChar(),
                     data.Position,
                     ErrorType.Error);
                         data.AdvanceChar();
                         break;
                 }
             }
         }*/

        void ParseDecimalWithExponent()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '0': // обязательна одна цифра после Е/е
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        mode = Mode.UnsignedDecimalWithExponent;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '-':
                    case '+':
                        mode = Mode.UnsignedDecimalWithExponent;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    default:
                        MakeWarning("Unknown character! There can only be digit from 0 to 9 or '.' character",
                    data.PeekChar(),
                    data.Position,
                    ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseUnsignedDecimal()
        {
            while (true)
            {
                switch (data.PeekChar())
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
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                    case '.': // нашли точку, и она должна быть только одна
                        mode = Mode.UnsignedDecimalWithDot;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case ',':
                        mode = Mode.UnsignedDecimalWithDot;
                        resultString.Append('.');
                        MakeWarning("There can only be digit from 0 to 9 or '.' character",
                            data.PeekChar(),
                            data.Position,
                            ErrorType.Warning);
                        data.AdvanceChar();
                        return;
                    case 'E':
                    case 'e':
                        mode = Mode.DecimalWithExponent; // нашли экспоненту
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case 'F': // нашли тип 
                    case 'f':
                    case 'L':
                    case 'l':
                        mode = Mode.End;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                    case ' ':
                    case '\t':
                    case '\n':
                        mode = Mode.End;
                        return;
                    default:
                        MakeWarning("Unknown character! There can only be digit from 0 to 9 or '.' character",
                    data.PeekChar(),
                    data.Position,
                    ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseDecimal() // проверка первого символа
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case ' ':
                    case '\t':
                    case '\n':
                        //SKIP
                        data.AdvanceChar();
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
                        mode = Mode.UnsignedDecimal;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '+':
                    case '-':
                        mode = Mode.UnsignedDecimal;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        MakeWarning(
                            "Value cannot be empty",
                            data.PeekChar(),
                            data.Position,
                            ErrorType.Error);
                        mode = Mode.End;
                        return;
                    default:
                        MakeWarning(
                            "Unknown character! There can only be digit or sign.",
                            data.PeekChar(),
                            data.Position,
                            ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void MakeWarning(string text, char character, int position, ErrorType error)
        {
            errors.Add(new Errors(text, character, position, error));
        }
    }

    class Errors
    {
        public string Text { get; }
        public char Character { get; }
        public int Position { get; }
        public ErrorType errorType { get; }

        public Errors(string text, char character, int position, ErrorType error)
        {
            Text = text;
            Character = character;
            Position = position;
            errorType = error;
        }

        public string AddError() => $"{errorType}: Character: '{Character}' at position {Position}; {Text};";
    }

    enum Mode : byte // состояния КА
    {
        Decimal,
        UnsignedDecimal,
        UnsignedDecimalWithDot,
        DecimalWithExponent,
        UnsignedDecimalWithExponent,
        // UnsignedDecimalWithExponentDigit,
        End,
    }

    enum ErrorType : byte // варианты ошибок
    {
        Error,
        Warning,
    }
}