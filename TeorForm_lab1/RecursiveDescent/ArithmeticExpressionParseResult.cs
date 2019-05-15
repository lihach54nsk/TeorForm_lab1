using System.Collections.Generic;

namespace TeorForm_lab1.RecursiveDescent
{
    class ArithmeticExpressionParseResult
    {
        public ArithmeticExpressionParseResult(string resultString, IReadOnlyCollection<IWarning> states)
        {
            ResultString = resultString;
            States = states;
        }

        public string ResultString { get; }
        public IReadOnlyCollection<IWarning> States { get; }
    }
}
