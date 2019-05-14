using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TeorForm_lab1.RecursiveDescent;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        string currentFile = "";

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e) => currentFile = SaveAs();

        string SaveAs()
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "";
            SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                string file = SFD.FileName;
                File.WriteAllText(file, richTextBoxOut.Text.ToString());
                return SFD.FileName;
            }
            else return currentFile;
        }

        void Save()
        {
            if (currentFile == "") currentFile = SaveAs();
            else File.WriteAllText(currentFile, richTextBoxOut.Text.ToString());
        }

        void Create()
        {
            SaveForm saveForm = new SaveForm();
            DialogResult dialogResult = saveForm.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                if (currentFile == "")
                {
                    StreamWriter SW;
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.FileName = "";
                    SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

                    if (SFD.ShowDialog() == DialogResult.OK)
                    {
                        SW = new StreamWriter(SFD.FileName);
                        SW.Write(richTextBoxIn.Text.ToString());
                        SW.Close();
                        richTextBoxIn.Clear();
                    }
                    currentFile = "";
                }
                else
                {
                    Save();
                    currentFile = "";
                    richTextBoxIn.Clear();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                richTextBoxIn.Clear();
                currentFile = "";
            }
            else return;
        }

        void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            else
            {
                SaveForm saveForm = new SaveForm();
                DialogResult dialogResult = saveForm.ShowDialog();
                if (dialogResult == DialogResult.Yes)
                {
                    if (currentFile == "")
                    {
                        StreamWriter SW;
                        SaveFileDialog SFD = new SaveFileDialog();
                        SFD.FileName = "";
                        SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

                        if (SFD.ShowDialog() == DialogResult.OK)
                        {
                            SW = new StreamWriter(SFD.FileName);
                            SW.Write(richTextBoxIn.Text.ToString());
                            SW.Close();
                        }
                        else return;
                    }
                    else Save();
                }
                string file = openFileDialog.FileName;
                string txt = File.ReadAllText(file);
                richTextBoxIn.Text = txt;
                currentFile = file;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e) => Create();

        private void CreateButton_Click(object sender, EventArgs e) => создатьToolStripMenuItem_Click(sender, e);

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) => Save();

        private void SaveButton_Click(object sender, EventArgs e) => сохранитьToolStripMenuItem_Click(sender, e);

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Undo();

        private void UndoButton_Click(object sender, EventArgs e) => отменитьToolStripMenuItem_Click(sender, e);

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Redo();

        private void RedoButton_Click(object sender, EventArgs e) => повторитьToolStripMenuItem_Click(sender, e);

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Cut();

        private void CutButton_Click(object sender, EventArgs e) => вырезатьToolStripMenuItem_Click(sender, e);

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Copy();

        private void CopyButton_Click(object sender, EventArgs e) => копироватьToolStripMenuItem_Click(sender, e);

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Paste();

        private void PasteButton_Click(object sender, EventArgs e) => вставитьToolStripMenuItem_Click(sender, e);

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.SelectAll();

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Эта программа создана на лабе!");

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e) => Help.ShowHelp(this, "help.chm");

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) => Open();

        private void OpenButton_Click(object sender, EventArgs e) => открытьToolStripMenuItem_Click(sender, e);

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Clear();

        private void emailStateMachineToolStripMenuItem_Click(object sender, EventArgs e) { } // muda

        private void лексическийАнализаторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = LexerAnalisys(richTextBoxIn.Text);
        }

        void Parse(string input)
        {
            var tokenaizer = Lexer.Lexer.GetTokens(new TextData(richTextBoxIn.Text));
            var result = ArithmeticExpressionParser.Parse(tokenaizer);
            var sb = new StringBuilder();

            foreach (var a in result.ResultString)
            {
                sb.AppendLine(a.ToString());
                richTextBoxOut.Text = sb.ToString();
            }
        }

        private string LexerAnalisys(string input)
        {
            var result = Lexer.Lexer.GetTokens(new Lexer.TextData(input));
            var sb = new StringBuilder();

            foreach (var a in result)
            {
                switch (a)
                {
                    case Lexer.SyntaxValueToken<string> str:
                        sb.AppendLine($"Значение: {str.Value}; Тип: {str.SyntaxKind}; Позиция: {str.SourceTextPosition};");
                        break;
                    case Lexer.SyntaxValueToken<int> inti:
                        sb.AppendLine($"Значение: {inti.Value}; Тип: {inti.SyntaxKind}; Позиция: {inti.SourceTextPosition};");
                        break; 
                    case Lexer.SyntaxValueToken<char> charType:
                        sb.AppendLine($"Значение: {charType.Value}; Тип: {charType.SyntaxKind}; Позиция: {charType.SourceTextPosition};");
                        break;
                    case Lexer.SyntaxValueToken<double> doubleType:
                        sb.AppendLine($"Значение: {doubleType.Value}; Тип: {doubleType.SyntaxKind}; Позиция: {doubleType.SourceTextPosition};");
                        break;
                    case Lexer.SyntaxIdentifierToken iden:
                        sb.AppendLine($"Имя идентификатора: {iden.IdentifierName}; Тип: {iden.SyntaxKind}; Позиция: {iden.SourceTextPosition};");
                        break;
                    case Lexer.SyntaxTriviaToken trivia:
                        sb.AppendLine($"Тип: {trivia.SyntaxKind}; Позиция: {trivia.SourceTextPosition};");
                        break;
                    case Lexer.SyntaxUnknownToken unknown:
                        sb.AppendLine($"Неизвестный токен; Значение: {unknown.Text}; Тип: {unknown.SyntaxKind}; Позиция: {unknown.SourceTextPosition}");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return sb.ToString();
        }

        private void рекурсивныйСпускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Text = RecoursiveDescent(richTextBoxIn.Text);
        }

        private string RecoursiveDescent(string input)
        {
            var result = ArithmeticExpressionParser.Parse(Lexer.Lexer.GetTokens(new TextData(input)));
            var builder = new StringBuilder();

            builder.AppendLine($"Прочитанная строка: {result.ResultString}")
                .Append("Перечень состояний:\n");

            for (int i = result.States.Count - 1; i >= 0; i--) 
            {
                builder.Append(" ➜ ").Append(result.States.ElementAt(i));
            }
            return builder.ToString();
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e) => Help.ShowHelp(this, "TT.chm");
        private void ГрамматикаToolStripMenuItem_Click(object sender, EventArgs e) => Help.ShowHelp(this, "Gram.chm");
        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e) => Help.ShowHelp(this, "Klass.chm");
        private void методАнализаToolStripMenuItem_Click_1(object sender, EventArgs e) => Help.ShowHelp(this, "Metod.chm");
        private void тестовыйПримерToolStripMenuItem_Click_1(object sender, EventArgs e) => Help.ShowHelp(this, "Test.chm");
        private void списокЛитературыToolStripMenuItem_Click_1(object sender, EventArgs e) => Help.ShowHelp(this, "Litra.chm");
    }
}