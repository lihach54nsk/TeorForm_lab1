using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeorForm_lab1.Lexer;
using TeorForm_lab1.StateMachineModes;

namespace TeorForm_lab1
{
    class StateMachine : IStateMachine
    {
        StateMachineEnum mode;
        List<Errors> errors;
        TextData data;
        StringBuilder resultString;

        public bool Parser(TextData textData, out List<Errors> errorsCollection, out string result)
        {
            mode = StateMachineEnum.Start;
            errors = new List<Errors>();
            data = textData;
            resultString = new StringBuilder();

            while (true)
            {
                switch (mode)
                {
                    case StateMachineEnum.Start:
                        ParseStart();
                        break;
                    case StateMachineEnum.FirstSlash:
                        ParseFirstSlash();
                        break;
                    case StateMachineEnum.SecondSlash:
                        ParseSecondSlash();
                        break;
                    case StateMachineEnum.FirstStar:
                        ParseFirstStar();
                        break;
                    case StateMachineEnum.SecondStar:
                        ParseSecondStar();
                        break;
                    case StateMachineEnum.LastSlash:
                        ParseLastSlash();
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

        void ParseStart()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case ' ':
                    case '\t':
                    case '\n':
                        // SKIP
                        data.AdvanceChar();
                        break;
                    case '/':
                        mode = StateMachineEnum.FirstSlash;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        MakeWarning("Unknown character! Expected slash", data.PeekChar(), data.Position, ErrorType.Error);
                        return;
                    default:
                        MakeWarning("Unknown character! Expected slash", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseFirstSlash()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '/':
                        mode = StateMachineEnum.SecondSlash;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '*':
                        mode = StateMachineEnum.FirstStar;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        MakeWarning("Unknown character! Expected slash or star", data.PeekChar(), data.Position, ErrorType.Error);
                        return;
                    default:
                        MakeWarning("Unknown character! Expected slash or star", data.PeekChar(), data.Position, ErrorType.Error);
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseSecondSlash()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '\n':
                        mode = StateMachineEnum.Start;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseFirstStar()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '*':
                        mode = StateMachineEnum.SecondStar;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        break;
                }
            }
        }

        void ParseSecondStar()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '/':
                        mode = StateMachineEnum.Start;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        mode = StateMachineEnum.FirstStar;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                }
            }
        }

        void ParseLastSlash()
        {
            while (true)
            {
                switch (data.PeekChar())
                {
                    case '/':
                        mode = StateMachineEnum.FirstSlash;
                        resultString.Append(data.PeekChar());
                        data.AdvanceChar();
                        return;
                    case '\0':
                        mode = StateMachineEnum.End;
                        return;
                    default:
                        MakeWarning("Unknown character! Expected slash", data.PeekChar(), data.Position, ErrorType.Error);
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