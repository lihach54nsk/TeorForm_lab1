using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorForm_lab1
{
    class EmailSearchStateMaсhine
    {
        private const string numberOrLetters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private const string separators = "^ ;,|$";
        private const string emailAllowChars = "-_";

        
        private readonly Action<char>[] states = new Action<char>[9];
        private readonly List<string> resultStrings = new List<string>();

        private StringBuilder stringBuilder;
        private Action<char> currentState;

        EmailSearchStateMaсhine()
        {
            states[0] = character =>
            {   
                if(CheckCharacter(character, separators))
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
                if (CheckCharacter(character, numberOrLetters))
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
                if ('@' == character)
                {
                    currentState = states[3];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, numberOrLetters + emailAllowChars + '.'))
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
                if ('.' == character)
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, numberOrLetters + emailAllowChars))
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
                if (CheckCharacter(character, numberOrLetters))
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
                if (CheckCharacter(character, numberOrLetters))
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
                if(character == '.')
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, separators))
                {
                    currentState = states[1];
                    resultStrings.Add(stringBuilder.ToString());
                }
                else if(CheckCharacter(character, numberOrLetters))
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
                if (character == '.')
                {
                    currentState = states[4];
                    stringBuilder.Append(character);
                }
                else if (CheckCharacter(character, numberOrLetters + emailAllowChars))
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


        public static List<string> FindEmails(string str)
        {
            var obj = new EmailSearchStateMaсhine();
            str = '^' + str + '$';

            foreach (var item in str.ToCharArray())
            {
                obj.currentState(item);
            }

            return obj.resultStrings;
        }

        private static bool CheckCharacter(char character, string allowChars)
        {
            var check = false;

            foreach (var item in allowChars)
            {
                if (item == character)
                {
                    check = true;
                    break;
                }
            }

            return check;
        }
    }
}
