using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeorForm_lab1.Lexer;

namespace TeorForm_lab1
{
   public class Parser
    {
        private LinkedList<Warning> warnings;
        private TextData textData;
        private StringBuilder resultString;

        public ParserStatus ParseConst(TextData data)
        {
            warnings = new LinkedList<Warning>();
            textData = data;
            resultString = new StringBuilder();


        }

    }
}
