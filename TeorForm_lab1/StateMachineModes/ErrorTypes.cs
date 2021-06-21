namespace TeorForm_lab1.StateMachineModes
{
    public class Errors
    {
        public string Text { get; }
        public char Character { get; }
        public int Position { get; }
        public ErrorType errorType { get; }

        public Errors(string text, char character, int position, ErrorType error)
        {
            Text = text;
            Character = character;
            Position = position;
            errorType = error;
        }

        public string AddError() => $"{errorType}: Character: '{Character}' at position {Position}; {Text};";
    }

    public enum ErrorType : byte // варианты ошибок
    {
        Error,
        Warning,
    }
}
