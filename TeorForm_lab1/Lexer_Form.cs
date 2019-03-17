using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeorForm_lab1
{
    public partial class Lexer_Form : Form
    {
        public Lexer_Form() => InitializeComponent();

        private void anal_Button_Click(object sender, EventArgs e)
        {
            richTextBoxOut.Clear();
            Lexer.TextData text = new Lexer.TextData(richTextBoxIn.Text.ToString());
            var result = Lexer.Lexer.GetTokens(text);

            foreach (var a in result)
            {
                switch (a)
                {
                    case Lexer.SyntaxValueToken<string> str: richTextBoxOut.AppendText(str.Value.ToString() + " - " + str.SyntaxKind.ToString() + " Позиция - " + str.SourceTextPosition.ToString() + "\n"); break;
                    case Lexer.SyntaxValueToken<int> inti: richTextBoxOut.AppendText(inti.Value.ToString() + " - " + inti.SyntaxKind.ToString() + " Позиция - " + inti.SourceTextPosition.ToString() + "\n"); break;
                    case Lexer.SyntaxValueToken<char> charType: richTextBoxOut.AppendText(charType.Value.ToString() + " - " + charType.SyntaxKind.ToString() + " Позиция - " + charType.SourceTextPosition.ToString() + "\n"); break;
                    case Lexer.SyntaxValueToken<double> doubleType: richTextBoxOut.AppendText(doubleType.Value.ToString() + " - " + doubleType.SyntaxKind.ToString() + " Позиция - " + doubleType.SourceTextPosition.ToString() + "\n"); break;
                    case Lexer.SyntaxIdentifierToken iden: richTextBoxOut.AppendText($"{ iden.IdentifierName} - {iden.SyntaxKind}  Позиция - {iden.SourceTextPosition}\n"); break;
                    case Lexer.SyntaxTriviaToken trivia: richTextBoxOut.AppendText($"{trivia.SyntaxKind} Позиция - {trivia.SourceTextPosition}"); break;
                    case Lexer.SyntaxUnknownToken unknown: richTextBoxOut.AppendText($"{unknown.Text} - {unknown.SyntaxKind} Позиция - {unknown.SourceTextPosition}\n"); break;
                }
            }
        }
    }
}