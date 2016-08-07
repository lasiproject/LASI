using LASI;
using LASI.Core;

using System;
using System.Xml;
using System.Xml.Schema;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for WordTest and is intended
    ///to contain all WordTest Unit Tests
    /// </summary>
    public class WordTest
    {

        [Fact]
        public void CreateWordWithOnlyWhiteSpaceTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun(" \r\n\t")).Throws<ArgumentException>();
        }

        [Fact]
        public void CreateWordWithSpaceInTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun("cat hat")).Throws<ArgumentException>();
        }
        [Fact]
        public void CreateWordWithTabInTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun("\t")).Throws<ArgumentException>();
        }
        [Fact]
        public void CreateWordWithNewLineInTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun("\n")).Throws<ArgumentException>();
        }
        [Fact]
        public void CreateWordWithCarriageReturnInTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun("\r")).Throws<ArgumentException>();
        }

        [Fact]
        public void CreateWordWithEmptyTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun("")).Throws<ArgumentException>();
        }

        [Fact]
        public void CreateWordWithNullTextThrowsArgumentException()
        {
            Check.ThatCode(() => new CommonSingularNoun(null)).Throws<ArgumentException>();
        }

        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            var target = CreateWord();
            object obj = CreateWord();
            var expected = false;
            bool actual;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for GetHashCode
        /// </summary>
        [Fact]
        public void GetHashCodeTest()
        {
            var target = CreateWord();
            var expected = (target).GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Check.That(actual).IsEqualTo(expected);

        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var target = CreateWord();
            var expected = target.GetType().Name + " \"" + target.Text + "\"";
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }



        /// <summary>
        ///A test for Document
        /// </summary>
        [Fact]
        public void ParentDocTest()
        {
            var target = CreateWord();
            var parent = new Document(new Paragraph(ParagraphKind.Default, new Sentence(new[] { new Clause(new NounPhrase(target)) }, ending: null)));

            var expected = parent;
            var actual = target.Document;

            Check.That(actual).IsEqualTo(expected);
        }



        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            Word target = new BaseVerb("run");
            var expected = "run";
            string actual;
            actual = target.Text;
            Check.That(actual).IsEqualTo(expected);

        }


        /// <summary>
        ///A test for Weight
        /// </summary>
        [Fact]
        public void WeightTest()
        {
            var target = CreateWord();
            var expected = new Random().NextDouble() * double.MaxValue;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for VerboseOutput
        /// </summary>
        [Fact]
        public void VerboseOutputTest()
        {
            var expected = false;
            bool actual;
            Word.VerboseOutput = expected;
            actual = Word.VerboseOutput;
            Check.That(actual).IsEqualTo(expected);

        }

        static Word CreateWord() => new CommonSingularNoun("dog");

    }
}
