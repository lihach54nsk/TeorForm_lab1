using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TeorForm_lab1
{
    public static class RegExpBackend
    {
        public static MatchCollection FindAllEmails(string input)
        {
            var except = @"(?<=(\s|^))\w[a-z0-9\._\-]*@[a-z0-9\._\-]+(?=(\s|$))";
            var reg = new Regex(except, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = reg.Matches(input);

            return result;
        }
    }
}
