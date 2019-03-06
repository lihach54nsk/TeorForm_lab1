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
        private readonly List<FindedDataInfo> resultStrings;
        private StringBuilder sb;
        private int position;
        private int startPosition;

        EmailSearchStateMaсhineWithUSM()
        {
            var nodes = new StateMachineNode<char>[9];
            sb = new StringBuilder();
            resultStrings = new List<FindedDataInfo>();

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
                    startPosition = position;
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
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c), c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[4] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '.', c => sb.Append(c), 5),
                new StateMachineTransaction<char>(c => char.IsLetterOrDigit(c) || IsEmailAllowSymbol(c), c=> sb.Append(c), 4),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[5] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => char.IsLetter(c), c => sb.Append(c), 6),
                new StateMachineTransaction<char>(c => char.IsNumber(c) || IsEmailAllowSymbol(c), c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[6] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => char.IsLetter(c), c => sb.Append(c), 7),
                new StateMachineTransaction<char>(c => char.IsNumber(c) || IsEmailAllowSymbol(c), c => sb.Append(c), 4),
                new StateMachineTransaction<char>(c => IsSeparator(c), null, 1),
            }, 0);

            nodes[7] = new StateMachineNode<char>(new StateMachineTransaction<char>[]
            {
                new StateMachineTransaction<char>(c => c == '.', c => sb.Append(c), 5),
                new StateMachineTransaction<char>(c => IsSeparator(c), c =>
                {
                    ApplySearchResult();
                }, 1),
                new StateMachineTransaction<char>(c => char.IsLetter(c), c => sb.Append(c), 7),
                new StateMachineTransaction<char>(c => char.IsNumber(c) || IsEmailAllowSymbol(c), c => sb.Append(c), 4),
            }, 0);

            stateMachine = new UniversalStateMachine<char>(nodes);
        }

        /// <summary>
        /// Производит поиск всех Email в исходной строке
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns></returns>
        public static List<FindedDataInfo> FindEmails(string str)
        {
            var obj = new EmailSearchStateMaсhineWithUSM();
            var result = FindEmails(str, obj);

            return result;
        }

#if DEBUG
        public static List<FindedDataInfo> FindEmails(string str, out IReadOnlyList<int> traceData)
        {
            var obj = new EmailSearchStateMaсhineWithUSM();
            var result = FindEmails(str, obj);

            traceData = obj.stateMachine.TraceData;

            return result;
        }
#endif

        static List<FindedDataInfo> FindEmails(string str, EmailSearchStateMaсhineWithUSM stateMaсhine)
        {
            str = '^' + str + '$';

            char[] array = str.ToCharArray();
            for (stateMaсhine.position = 0; stateMaсhine.position < array.Length; stateMaсhine.position++)
            {
                char item = array[stateMaсhine.position];
                stateMaсhine.PutChar(item);
            };

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


        private void ApplySearchResult()
        {
            var result = sb.ToString();
            resultStrings.Add(new FindedDataInfo
            {
                data = result,
                length = position - startPosition,
                position = startPosition - 1,
            });
        }

        private static bool IsEmailAllowSymbol(char character) => CheckCharacter(character, emailAllowChars);

        private static bool IsSeparator(char character) => CheckCharacter(character, separators);
    }
}
