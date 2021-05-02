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
        public void WhenTwoSlashesAndSomeText_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("//YA EST GRUT!"), out errors, out resultString);

            Assert.IsTrue(result);
        }
        
        [Test]
        public void WhenSlashAndStarAndSomeTextAndStarAndSlash_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("/*YA EST GRUT!*/"), out errors, out resultString);

            Assert.IsTrue(result);
        }   
        
        [Test]
        public void WhenSlashAndStarAndSomeTextAndStar_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("/*YA EST GRUT!*"), out errors, out resultString);

            Assert.IsTrue(result);
        }  
        
        [Test]
        public void WhenSlashAndStarAndSomeTextAndStarAndSlashAndTreeSpacesAndSomeText_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("/*YA EST GRUT!*/   \n//Ya est Steve Rojers"), out errors, out resultString);
            

            Assert.IsTrue(result);
        }   
        
        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeText_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("         AfterSecondSlash, // 7"), out errors, out resultString);

            Assert.IsTrue(!result && errors.Count == 17);
        }
        
        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndTwoSlashes_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("//YA EST // GRUT"), out errors, out resultString);

            Assert.IsTrue(result);
        }    
        
        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndTwoSlashesAndSomeText_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n// GRUT"), out errors, out resultString);

            Assert.IsTrue(result);
        }        

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeText_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeTextAndStar_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT*"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeTextAndEnterAndStar_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT\n*"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeTextAndAndStarAndSlash_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT*/"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeTextAndEnterAndStarAndSlash_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT\n*/"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenSpacesAndTwoSlashesAndSomeTextAndEnterAndSlashAndStarAndSomeTextAndEnterAndStarAndSpaceAndSlash_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("   //YA EST \n/* GRUT\n* /"), out errors, out resultString);

            Assert.IsTrue(result);
        }
        
        [Test]
        public void WhenSlashAndStar_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("/*"), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenEmpty_ThenReturnTrue()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData(""), out errors, out resultString);

            Assert.IsTrue(result);
        }

        [Test]
        public void WhenOneSlashAndSomeText_ThenReturnFalseAndListOfErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("/YA EST GRUT!"), out errors, out resultString);

            Assert.IsTrue(!result && (errors.Count == 13));
        }  
        
        [Test]
        public void WhenTwoSlashesAndSomeTextAndEnterAndTwoSymbols_ThenReturnFalseAndListWithTwoErrors()
        {
            var resultString = "";
            var errors = new List<Errors>();
            var result = stateMachine.Parser(new TextData("// This is the comment\n42"), out errors, out resultString);

            Assert.IsTrue(!result && (errors.Count == 2));
        }
    }
}