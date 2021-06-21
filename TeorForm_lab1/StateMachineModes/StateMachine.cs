using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeorForm_lab1.Lexer;
using TeorForm_lab1.StateMachineModes;

namespace TeorForm_lab1
{
    public class StateMachine : IStateMachine
    {
        StateMachineEnum mode;
        List<Errors> errors;
        TextData data;
        StringBuilder resultString;

        public bool Parser(TextData textData, out List<Errors> errorsCollection, out string result)
        {
            mode = StateMachineEnum.FirstSymbol;
            errors = new List<Errors>();
            data = textData;
            resultString = new StringBuilder();

            while (true)
            {
                switch (mode)
                {
                    case StateMachineEnum.FirstSymbol:
                        ParseFirstSymbol();
                        break;
                    case StateMachineEnum.SecondSymbol:
                        ParseSecondSymbol();
                        break;
                    case StateMachineEnum.ThirdSymbol:
                        ParseThirdSymbol();
                        break;
                    case StateMachineEnum.FourthSymbol:
                        ParseFourthSymbol();
                        break;
                    case StateMachineEnum.SecondC:
                        ParseSecondC();
                        break;
                    case StateMachineEnum.End:
                        errorsCollection = errors;
                        result = resultString.ToString();
                        return errors.All(x => x.errorType != ErrorType.Error);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        void ParseFirstSymbol()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case ' ':
                    case '\t':
                    case '\n':
                        data.AdvanceChar();
                        break;
                    case 'a':
                        mode = StateMachineEnum.SecondSymbol;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case 'c':
                        mode = StateMachineEnum.SecondC;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected a or c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseSecondSymbol()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case 'b':
                        mode = StateMachineEnum.ThirdSymbol;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case 'c':
                        mode = StateMachineEnum.SecondC;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected b or c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseThirdSymbol()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case 'c':
                        mode = StateMachineEnum.FourthSymbol;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseFourthSymbol()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case 'a':
                        mode = StateMachineEnum.SecondSymbol;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case 'c':
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected a or c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        private void ParseSecondC()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case 'a':
                        mode = StateMachineEnum.SecondSymbol;
                        resultString.Remove(resultString.Length - 1, 1);
                        resultString.Append(data.PeekChar());
                        MakeWarning("Unknown character! Expected c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        return;
                    case 'c':
                        mode = StateMachineEnum.FourthSymbol;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected c", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        public void MakeWarning(string text, char character, int position, ErrorType error)
        {
            errors.Add(new Errors(text, character, position, error));
        }
    }
}