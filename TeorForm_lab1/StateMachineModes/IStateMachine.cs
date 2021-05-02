using System.Collections.Generic;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1.StateMachineModes
{
    public interface IStateMachine
    {
        bool Parser(TextData textData, out List<Errors> errorsCollection, out string result);
        void MakeWarning(string text, char character, int position, ErrorType error);
    }
}
