namespace TeorForm_lab1
{
    class Warning:IWarning
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
            return $"Info: {Text}; {WarningType}: {Character} at position {Position}.";
        }
    }
    enum WarningType : byte
    {
        Error,
        Warning,
    }

    interface IWarning { }
    class UnkWarning : IWarning
    {
        public UnkWarning(string text, string character, WarningType warningType)
        {
            Text = text;
            WarningType = warningType;
            Character = character;
        }

        public string Text { get; }

        public WarningType WarningType { get; }
        public string Character { get; }

        public override string ToString()
        {
            return $"{WarningType}: {Character}; Info: {Text}.";
        }
    }
}
