using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var source = "fwioqf {wfeiofei} \" efwwefoeo\" $ 1488;";
            var tokens = Lexer.Lexer.GetTokens(new TextData(source));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
