using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorForm_lab1
{
    class Warning
    {
        public Warning(string text, char character, int position, WarningType warningType)
        {
            Text = text;
            Position = position;
            WarningType = warningType;
            Character = character;
        }

        public string Text { get; }
        public int Position { get; }
        public WarningType WarningType { get; }
        public char Character { get; }

        public override string ToString()
        {
            return $"{WarningType}: Chartacter '{Character}' at position {Position};\nInfo: {Text};";
        }
    }

    enum WarningType : byte
    {
        Error,
        Warning,
    }
}
}
