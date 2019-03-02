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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { } // muda

        private void файлToolStripMenuItem_Click(object sender, EventArgs e) { } // muda

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e) => currentFile = SaveAs();

        string SaveAs()
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "";
            SFD.Filter = "TXT (*.txt)|*.txt|RTF (*.rtf)|*.rtf";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                string file = SFD.FileName;
                File.WriteAllText(file, richTextBoxIn.Text.ToString());
                return SFD.FileName;
            }
            else return currentFile;
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

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMailForm eMailForm = new EMailForm();
            eMailForm.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) => Open();

        private void OpenButton_Click(object sender, EventArgs e) => открытьToolStripMenuItem_Click(sender, e);

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBoxIn.Clear();

        private void emailStateMachineToolStripMenuItem_Click(object sender, EventArgs e) { } // muda
    }
}