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
            var source = "04949.1232";
            var parser = new RecursiveDescent.DecimalParser();
            var result = parser.ParseDecimalConst(new TextData(source), out var warnings);
            //var tokens = Lexer.Lexer.GetTokens(new TextData(source));

            var resultStr = new System.Text.StringBuilder();
            foreach (var item in warnings)
            {
                resultStr.AppendLine(item.ToString());
            }

            Console.WriteLine(resultStr.ToString());
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
