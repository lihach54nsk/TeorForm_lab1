namespace TeorForm_lab1
{
    class Warning
    {
        public Warning(string text, string character, int position, WarningType warningType)
        {
            Text = text;
            Position = position;
            WarningType = warningType;
            Character = character;
        }

        public string Text { get; }
        public int Position { get; }
        public WarningType WarningType { get; }
        public string Character { get; }

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
