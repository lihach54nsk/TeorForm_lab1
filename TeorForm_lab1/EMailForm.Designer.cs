namespace TeorForm_lab1
{
    partial class EMailForm
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
            this.richTextBoxEMailIn = new System.Windows.Forms.RichTextBox();
            this.FindEMailButton = new System.Windows.Forms.Button();
            this.richTextBoxEMailOut = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxEMailIn
            // 
            this.richTextBoxEMailIn.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxEMailIn.Name = "richTextBoxEMailIn";
            this.richTextBoxEMailIn.Size = new System.Drawing.Size(384, 97);
            this.richTextBoxEMailIn.TabIndex = 0;
            this.richTextBoxEMailIn.Text = "maks@gmail.com lexa@mail.ru tomilov@nstu.ru";
            // 
            // FindEMailButton
            // 
            this.FindEMailButton.Location = new System.Drawing.Point(12, 115);
            this.FindEMailButton.Name = "FindEMailButton";
            this.FindEMailButton.Size = new System.Drawing.Size(120, 23);
            this.FindEMailButton.TabIndex = 1;
            this.FindEMailButton.Text = "Найти e-mail";
            this.FindEMailButton.UseVisualStyleBackColor = true;
            this.FindEMailButton.Click += new System.EventHandler(this.FindEMailButton_Click);
            // 
            // richTextBoxEMailOut
            // 
            this.richTextBoxEMailOut.Location = new System.Drawing.Point(402, 12);
            this.richTextBoxEMailOut.Name = "richTextBoxEMailOut";
            this.richTextBoxEMailOut.Size = new System.Drawing.Size(384, 97);
            this.richTextBoxEMailOut.TabIndex = 2;
            this.richTextBoxEMailOut.Text = "";
            // 
            // EMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 144);
            this.Controls.Add(this.richTextBoxEMailOut);
            this.Controls.Add(this.FindEMailButton);
            this.Controls.Add(this.richTextBoxEMailIn);
            this.Name = "EMailForm";
            this.Text = "EMailForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxEMailIn;
        private System.Windows.Forms.Button FindEMailButton;
        private System.Windows.Forms.RichTextBox richTextBoxEMailOut;
    }
}