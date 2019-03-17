namespace TeorForm_lab1
{
    partial class Lexer_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.anal_Button = new System.Windows.Forms.Button();
            this.richTextBoxIn = new System.Windows.Forms.RichTextBox();
            this.richTextBoxOut = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // anal_Button
            // 
            this.anal_Button.Location = new System.Drawing.Point(12, 226);
            this.anal_Button.Name = "anal_Button";
            this.anal_Button.Size = new System.Drawing.Size(175, 23);
            this.anal_Button.TabIndex = 0;
            this.anal_Button.Text = "Ковальски, анализ!";
            this.anal_Button.UseVisualStyleBackColor = true;
            this.anal_Button.Click += new System.EventHandler(this.anal_Button_Click);
            // 
            // richTextBoxIn
            // 
            this.richTextBoxIn.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxIn.Name = "richTextBoxIn";
            this.richTextBoxIn.Size = new System.Drawing.Size(776, 101);
            this.richTextBoxIn.TabIndex = 1;
            this.richTextBoxIn.Text = "int \"double\" fuck float 5 \"+\" \"++\" - \'*\' \'/\'";
            // 
            // richTextBoxOut
            // 
            this.richTextBoxOut.Location = new System.Drawing.Point(12, 119);
            this.richTextBoxOut.Name = "richTextBoxOut";
            this.richTextBoxOut.ReadOnly = true;
            this.richTextBoxOut.Size = new System.Drawing.Size(776, 101);
            this.richTextBoxOut.TabIndex = 2;
            this.richTextBoxOut.Text = "";
            // 
            // Lexer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 256);
            this.Controls.Add(this.richTextBoxOut);
            this.Controls.Add(this.richTextBoxIn);
            this.Controls.Add(this.anal_Button);
            this.Name = "Lexer_Form";
            this.Text = "Lexer_Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button anal_Button;
        private System.Windows.Forms.RichTextBox richTextBoxIn;
        private System.Windows.Forms.RichTextBox richTextBoxOut;
    }
}