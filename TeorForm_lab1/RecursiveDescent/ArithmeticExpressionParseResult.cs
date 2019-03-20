using System.Collections.Generic;

namespace TeorForm_lab1.RecursiveDescent
{
    class ArithmeticExpressionParseResult
    {
        public ArithmeticExpressionParseResult(string resultString, IReadOnlyCollection<string> states)
        {
            ResultString = resultString;
            States = states;
        }

        public string ResultString { get; }
        public IReadOnlyCollection<string> States { get; }
    }
}
