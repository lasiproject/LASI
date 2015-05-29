﻿using System;
using System.Linq;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ILexicalTest and is intended
    ///to contain all ILexicalTest Unit Tests
    /// </summary>
    [TestClass]
    public class ILexicalTest
    {
        /// <summary>
        ///A test for MetaWeight
        /// </summary>
        [TestMethod]
        public void MetaWeightTest()
        {
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [TestMethod]
        public void TextTest()
        {
            ILexical target = CreateILexical();
            string actual;
            actual = target.Text;
            Assert.AreEqual(target.Text, actual);
        }

        /// <summary>
        ///A test for Weight
        /// </summary>
        [TestMethod]
        public void WeightTest()
        {
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        internal virtual ILexical CreateILexical()
        {
            ILexical target = new AggregateEntity(
                new PersonalPronoun("him"),
                new ProperSingularNoun("Patrick"),
                new NounPhrase(new ProperSingularNoun("Brittany"))
            );
            return target;
        }

        private TestContext testContextInstance;

        #region Additional test attributes

        #endregion Additional test attributes
    }
}