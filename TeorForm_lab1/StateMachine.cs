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

        public bool Parser(TextData textData)
        {
            mode = Mode.Decimal;
            errors = new List<Errors>();
            data = textData;
            resultString = new StringBuilder();

            while (true)
            {
                switch (mode)
                {
                    case Mode.Decimal:
                        ParseDecimal();
                        break;
                    case Mode.UnsignedDecimal:
                        ParseUnsignedDecimal();
                        break;
                    case Mode.DecimalWithExponent:
                        ParseDecimalWithExponent();
                        break;
                    case Mode.UnsignedDecimalWithExponent:
                        ParseUnsignedDecimalWithExponent();
                        break;
                    case Mode.DecimalWithExponentAndType:
                        ParseDecimalWithExponentAndType();
                        break;
                    case Mode.UnsignedDecimalWithExponentAndType:
                        ParseUnsignedDecimalWithExponentAndType();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private void ParseUnsignedDecimalWithExponentAndType()
        {
            throw new NotImplementedException();
        }

        private void ParseDecimalWithExponentAndType()
        {
            throw new NotImplementedException();
        }

        private void ParseUnsignedDecimalWithExponent()
        {
            throw new NotImplementedException();
        }

        private void ParseDecimalWithExponent()
        {
            throw new NotImplementedException();
        }

        private void ParseUnsignedDecimal()
        {
            throw new NotImplementedException();
        }

        private void ParseDecimal()
        {
            throw new NotImplementedException();
        }

        enum Mode : byte // состояния КА
        {
            Decimal,
            UnsignedDecimal,
            DecimalWithExponent,
            UnsignedDecimalWithExponent,
            DecimalWithExponentAndType,
            UnsignedDecimalWithExponentAndType,
        }

        enum Errors : byte // варианты ошибок
        {
            Error,
            Warning,
        }
    }
}