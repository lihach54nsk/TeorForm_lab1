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

namespace TeorForm_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string currentFile = "";

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        } // muda

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        } // muda

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentFile = SaveAs();
        }

        string SaveAs()
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "MyTXT";
            SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                string file = SFD.FileName;
                File.WriteAllText(file, richTextBoxIn.Text.ToString());
            }
            return SFD.FileName;
        }

        void Save()
        {
            if (currentFile == "") currentFile = SaveAs();
            else File.WriteAllText(currentFile, richTextBoxIn.Text.ToString());
        }

        void Create()
        {
            SaveForm saveForm = new SaveForm();
            DialogResult dialogResult = saveForm.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                StreamWriter SW;
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.FileName = "MyTXT";
                SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    SW = new StreamWriter(SFD.FileName);
                    SW.Write(richTextBoxOut.Text.ToString());
                    SW.Close();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                File.Create(@"C:\Users\Геральт из Ривии\Desktop\NewTXT.txt");
            }
            else return;
        }

        void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            else
            {
                string file = openFileDialog.FileName;
                string txt = File.ReadAllText(file);
                richTextBoxIn.Text = txt;
                currentFile = file;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        } // muda

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxIn.SelectAll();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Эта программа создана на лабе!");
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMailForm eMailForm = new EMailForm();
            eMailForm.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }
    }
}