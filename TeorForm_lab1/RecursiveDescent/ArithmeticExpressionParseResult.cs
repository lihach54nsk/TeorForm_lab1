using System.Collections.Generic;

namespace TeorForm_lab1.RecursiveDescent
{
    class ArithmeticExpressionParseResult
    {
        public ArithmeticExpressionParseResult(string resultString, IReadOnlyCollection<Warning> states)
        {
            ResultString = resultString;
            States = states;
        }

        public string ResultString { get; }
        public IReadOnlyCollection<Warning> States { get; }
    }
}
