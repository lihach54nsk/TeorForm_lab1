using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorForm_lab1.Lexer
{
    class Lexer
    {
        internal struct TokenInfo
        {
            internal SyntaxKind kind;
            internal int IntValue;

            internal string StringValue;
            internal char CharValue;
            internal double DoubleValue;
        }

        //private readonly string _source;
        //private int counter;

        internal List<ISyntaxToken> GetTokens(TextData data)
        {
            TokenInfo tokenInfo;

            switch (data.PeekChar())
            {
                default:
                    break;
            }

            throw new NotImplementedException();
        }

        internal ISyntaxToken GetToken(ref TokenInfo tokenInfo)
        {
            throw new NotImplementedException();
        }
    }

    class TextData
    {
        private char[] sourceCode;
        private int position;

        public TextData(string source)
        {
            sourceCode = source.ToCharArray();
        }

        public char PeekChar()
        {
            if(position == sourceCode.Length)
            {
                return '\0';
            }
            else
            {
                return sourceCode[position];
            }
        }

        public char PeekChar(int offset)
        {
            if (position + offset >= sourceCode.Length)
            {
                return '\0';
            }
            else
            {
                return sourceCode[position + offset];
            }
        }

        public void AdvanceChar() 
            => position += 1;

        public void AdvanceChar(int offset)
            => position += offset;
    }

    class SyntaxValueToken<T> : ISyntaxToken
    {
        public SyntaxValueToken(T value, SyntaxKind syntaxKind)
        {
            Value = value;
            SyntaxKind = syntaxKind;
        }

        public SyntaxKind SyntaxKind { get; }
        public T Value { get; }
    }

    class SyntaxIdentifierToken : ISyntaxToken
    {
        public SyntaxIdentifierToken(SyntaxKind syntaxKind, string identifierName)
        {
            IdentifierName = identifierName;
        }

        public SyntaxKind SyntaxKind => SyntaxKind.IdentifierToken;
        public string IdentifierName { get; }
    }

    class SyntaxTriviaToken : ISyntaxToken
    {
        public SyntaxTriviaToken(SyntaxKind syntaxKind)
        {
            SyntaxKind = syntaxKind;
        }

        public SyntaxKind SyntaxKind { get; }
    }

    enum SyntaxKind
    {
        StringKeyword,
        IntKeyword,
        IdentifierToken,
    };

    enum SpecialType
    {
        System_Int32,
        System_String,
    };
}
