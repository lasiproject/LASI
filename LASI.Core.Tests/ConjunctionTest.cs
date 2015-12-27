﻿using LASI;
using LASI.Core;
using System;
using NFluent;
using TestMethod = Xunit.FactAttribute;
namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ConjunctionTest and is intended
    ///to contain all ConjunctionTest Unit Tests
    /// </summary>
    public class ConjunctionTest
    {
        /// <summary>
        ///A test for Conjunction Constructor
        /// </summary>
        [TestMethod]
        public void ConjunctionConstructorTest()
        {
            string text = "and";
            Conjunction target = new Conjunction(text);
            Check.That(target.Text).IsEqualTo(text);
        }

        /// <summary>
        ///A test for OnLeft
        /// </summary>
        [TestMethod]
        public void OnLeftTest()
        {
            string text = "and";
            Conjunction target = new Conjunction(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("program") });
            ILexical actual;
            target.JoinedLeft = expected;
            actual = target.JoinedLeft;
            Check.That(expected).IsEqualTo(actual);

        }

        /// <summary>
        ///A test for OnRight
        /// </summary>
        [TestMethod]
        public void OnRightTest()
        {
            string text = "and";
            Conjunction target = new Conjunction(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("program") });
            ILexical actual;
            target.JoinedRight = expected;
            actual = target.JoinedRight;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for JoinedLeft
        /// </summary>
        [TestMethod]
        public void JoinedLeftTest()
        {
            string text = "or";
            Conjunction target = new Conjunction(text);
            ILexical expected = new ProperSingularNoun("Jacob");
            ILexical actual;
            target.JoinedLeft = expected;
            actual = target.JoinedLeft;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for JoinedRight
        /// </summary>
        [TestMethod]
        public void JoinedRightTest()
        {
            Conjunction target = new Conjunction("and");
            ILexical expected = new AggregateEntity(new ProperSingularNoun("Jacob"), new ProperSingularNoun("Jessica"));
            ILexical actual;
            target.JoinedRight = expected;
            actual = target.JoinedRight;
            Check.That(expected).IsEqualTo(actual);
        }






    }
}
