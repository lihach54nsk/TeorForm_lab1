using System.Collections.Generic;
using System.Text;
using TeorForm_lab1.UniversalStateMachine;

namespace TeorForm_lab1
{
    class EmailSearchStateMaсhineWithUSM
    {
        private const string separators = "^ ;,|$";
        private const string emailAllowChars = "-_";

        private readonly UniversalStateMachine<char> stateMachine;
        private readonly List<string> resultStrings;
        private StringBuilder sb;

        EmailSearchStateMaсhineWithUSM()
        {
            var nodes = new StateMachineNode<char>[8];
            sb = new StringBuilder();
            resultStrings = new List<string>();

            nodes[0] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[1] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c), c =>
                {
                    sb.Clear();
                    sb.Append(c);
                }, 2),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[2] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '@', c => sb.Append(c), 3),
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c) || IsEmailAllowSymbol(c) || c == '.', c => sb.Append(c), 2),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[3] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '.', c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c) || IsEmailAllowSymbol(c), c=> sb.Append(c), 3),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[4] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c), c => sb.Append(c), 5),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[5] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c), c => sb.Append(c), 6),
                new StateMachineTransaction<char>(c => IsSeparator(c), c =>
                {
                    var result = sb.ToString();
                    resultStrings.Add(result);
                }, 1),
            }, 0);

            nodes[6] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '.', c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => IsSeparator(c), c =>
                {
                    var result = sb.ToString();
                    resultStrings.Add(result);
                }, 1),
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c), c => sb.Append(c), 6),
                new StateMachineTransaction<char>(c => IsEmailAllowSymbol(c), c => sb.Append(c), 7),
            }, 0);

            nodes[7] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '.', c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c) || IsEmailAllowSymbol(c), c => sb.Append(c), 7),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1)
            }, 0);

            stateMachine = new UniversalStateMachine<char>(nodes);
        }

        /// <summary>
        /// Производит поиск всех Email в исходной строке
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns></returns>
        public static List<string> FindEmails(string str, out List<int> traceData)
        {
            var obj = new EmailSearchStateMaсhineWithUSM();
            var result = FindEmails(str, obj);
            traceData = obj.stateMachine.TraceData as List<int>;

            return obj.resultStrings;
        }

#if DEBUG
        public static List<string> FindEmails(string str, out IReadOnlyList<int> traceData)
        {
            var obj = new EmailSearchStateMaсhineWithUSM();
            var result = FindEmails(str, obj);

            traceData = obj.stateMachine.TraceData;

            return obj.resultStrings;
        }
#endif

        static List<string> FindEmails(string str, EmailSearchStateMaсhineWithUSM stateMaсhine)
        {
            str = '^' + str + '$';

            foreach (var item in str.ToCharArray()) stateMaсhine.PutChar(item);

            return stateMaсhine.resultStrings;
        }

        private static bool CheckCharacter(char character, string allowChars)
        {
            foreach (var item in allowChars)
            {
                if (item == character) return true;
            }
            return false;
        }

        private void PutChar(char ch) => stateMachine.PutSymbol(ch);

        private static bool IsEmailAllowSymbol(char character) => CheckCharacter(character, emailAllowChars);

        private static bool IsSeparator(char character) => CheckCharacter(character, separators);
    }
}
