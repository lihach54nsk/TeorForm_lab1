using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeorForm_lab1
{
    class EmailSearchStateMaсhine
    {
        private const string separators = "^ ;,|$";
        private const string emailAllowChars = "-_";


        private readonly Action<char>[] states = new Action<char>[9];
        private readonly List<string> resultStrings = new List<string>();

        private StringBuilder stringBuilder;
        private List<int> traceData;
        private Action<char> currentState;

        EmailSearchStateMaсhine()
        {
            traceData = new List<int>();

            states[0] = character =>
            {
                traceData.Add(0);

                if (CheckCharacter(character, separators))
                {
                    currentState = states[1];
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[1] = character =>
            {
                traceData.Add(1);

                if (char.IsLetterOrDigit(character))
                {
                    currentState = states[2];
                    stringBuilder = new StringBuilder();
                    stringBuilder.Append(character);
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[2] = character =>
            {
                traceData.Add(2);

                if ('@' == character)
                {
                    currentState = states[3];
                    stringBuilder.Append(character);
                }
                else if (char.IsLetterOrDigit(character) || IsEmailAllowSymbol(character) || character == '.')
                {
                    currentState = states[2];
                    stringBuilder.Append(character);
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[3] = character =>
            {
                traceData.Add(3);

                if ('.' == character)
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (char.IsLetterOrDigit(character) || IsEmailAllowSymbol(character))
                {
                    currentState = states[3];
                    stringBuilder.Append(character);
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[4] = character =>
            {
                traceData.Add(4);

                if (char.IsLetterOrDigit(character))
                {
                    currentState = states[5];
                    stringBuilder.Append(character);
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[5] = character =>
            {
                traceData.Add(5);

                if (char.IsLetterOrDigit(character))
                {
                    currentState = states[6];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, separators))
                {
                    currentState = states[1];
                    resultStrings.Add(stringBuilder.ToString());
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[6] = character =>
            {
                traceData.Add(6);

                if (character == '.')
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, separators))
                {
                    currentState = states[1];
                    resultStrings.Add(stringBuilder.ToString());
                }
                else if (char.IsLetterOrDigit(character))
                {
                    currentState = states[6];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, emailAllowChars))
                {
                    currentState = states[7];
                    stringBuilder.Append(character);
                }
                else
                {
                    currentState = states[0];
                }
            };

            states[7] = character =>
            {
                traceData.Add(7);

                if (character == '.')
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (char.IsLetterOrDigit(character) || IsEmailAllowSymbol(character))
                {
                    currentState = states[7];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, separators))
                {
                    currentState = states[1];
                }
                else
                {
                    currentState = states[0];
                }
            };

            currentState = states[0];
        }

        /// <summary>
        /// Производит поиск всех Email в исходной строке
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <param name="stateTarce">Данные о состояниях конечного автомата</param>
        /// <returns></returns>
        public static List<string> FindEmails(string str, out List<int> stateTarce)
        {
            var obj = new EmailSearchStateMaсhine();
            var result = FindEmails(str, obj);

            stateTarce = obj.traceData;

            return obj.resultStrings;
        }

        /// <summary>
        /// Производит поиск всех Email в исходной строке
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns></returns>
        public static List<string> FindEmails(string str)
        {
            var obj = new EmailSearchStateMaсhine();
            var result = FindEmails(str, obj);

            return obj.resultStrings;
        }

        static List<string> FindEmails(string str, EmailSearchStateMaсhine stateMaсhine)
        {
            str = '^' + str + '$';

            foreach (var item in str.ToCharArray())
            {
                stateMaсhine.currentState(item);
            }

            return stateMaсhine.resultStrings;
        }

        private static bool CheckCharacter(char character, string allowChars)
        {
            foreach (var item in allowChars)
            {
                if (item == character)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsEmailAllowSymbol(char character) =>
            CheckCharacter(character, emailAllowChars);
    }
}