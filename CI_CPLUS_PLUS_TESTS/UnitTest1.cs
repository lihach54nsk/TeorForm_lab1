using NUnit.Framework;
using System.Collections.Generic;
using TeorForm_lab1;
using TeorForm_lab1.Lexer;
using TeorForm_lab1.StateMachineModes;

namespace CI_CPLUS_PLUS_TESTS
{
    public class Tests
    {
        StateMachine stateMachine;
        [SetUp]
        public void Setup()
        {
            stateMachine = new StateMachine();
        }

        [Test]
        public void When_abc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_cc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("cc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abcc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abcc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abccabc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abccabc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abccabcc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abccabcc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abcabcc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abcabcc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abcccabc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abcccabc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_c_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("c"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abcacc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abccacc"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenEmptyThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData(""), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_abcacbc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abcacbc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 1);
        }

        [Test]
        public void When_aaac_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("aaac"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 2);
        }

        [Test]
        public void When_abbbac_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abbbac"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 3);
        }

        [Test]
        public void When_abccabbbcc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abccabbbcc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 2);
        }

        [Test]
        public void When_abc_AndSpaces_And_ccabcc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abc         ccabcc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 9);
        }

        [Test]
        public void When_abc_AndSpaces_And_ccacbbcc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("abc         ccacbbcc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 11);
        }

        [Test]
        public void When_ab_And_Space_And_c_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("ab\nc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 1);
        }

        [Test]
        public void When_cba_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("cba"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 2 && resultString == "a");
        }

        [Test]
        public void When_cab_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("cab"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 1 && resultString == "ab");
        }

        [Test]
        public void When_c_AndSpacesAnd_a_AndSpaceAnd_b_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("c    a\nb"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 6 && resultString == "ab");
        }

        [Test]
        public void When_cabccabc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("cabccabc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 1 && resultString == "abccabc");
        }

        [Test]
        public void When_c_AndEmptiness_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("c"), out errors, out resultString);

            Assert.IsTrue(result && resultString == "c");
        }

        [Test]
        public void When_cccabababc_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("cccabababc"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 4);
        }

        [Test]
        public void WhenSomeTextThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("dfjhgklsdjdjkhgwskdlmdfnanklfgna"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 31);
        }

        [Test]
        public void WhenSpacesAnd_abc_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("    abc"), out errors, out resultString);

            Assert.IsTrue(result);
        }
    }
}