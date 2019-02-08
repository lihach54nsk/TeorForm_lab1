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
        private const string emailRegexString = @"(?<=(\s|^))\w[a-z0-9\._\-]*@[a-z0-9\._\-]+(?=(\s|$))";

        private static readonly Regex emailRegex =
            new Regex(emailRegexString, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static MatchCollection FindAllEmails(string input)
            => emailRegex.Matches(input);
    }
}