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
    public partial class EMailForm : Form
    {
        public EMailForm()
        {
            InitializeComponent();
        }

        private void FindEMailButton_Click(object sender, EventArgs e)
        {
            richTextBoxEMailOut.Clear();
            var result = RegExpBackend.FindAllEmails(richTextBoxEMailIn.Text.ToString());
            for (int i = 0; i < result.Count; i++) richTextBoxEMailOut.AppendText(result[i].ToString() + " Индекс: " + result[i].Index +
                " Длина адреса: " + result[i].Length + "\n");
        }
    }
}