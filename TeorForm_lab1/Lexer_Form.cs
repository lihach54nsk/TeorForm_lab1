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
            Lexer.TextData text = new Lexer.TextData(richTextBoxIn.Text.ToString());
            var result = Lexer.Lexer.GetTokens(text);

            foreach (var a in result) richTextBoxOut.AppendText(a.SourceTextPosition.ToString() + " ");
        }
    }
}