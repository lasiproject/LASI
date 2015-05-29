﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Core;

namespace LASI.Core.Tests
{
    [TestClass]
    public class IPrepositionLinkableTest
    {
        /// <summary>
        ///A test for PrepositionOnLeft
        /// </summary>
        [TestMethod]
        public void PrepositionOnLeftTest()
        {
            IPrepositionLinkable target = CreatePrepositionLinkable();
            IPrepositional expected = new Preposition("with");
            IPrepositional actual;
            target.PrepositionOnLeft = expected;
            actual = target.PrepositionOnLeft;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrepositionOnRight
        /// </summary>
        [TestMethod]
        public void PrepositionOnRightTest()
        {
            IPrepositionLinkable target = CreatePrepositionLinkable();
            IPrepositional expected = new Preposition("with");
            IPrepositional actual;
            target.PrepositionOnRight = expected;
            actual = target.PrepositionOnRight;
            Assert.AreEqual(expected, actual);
        }

        private static IPrepositionLinkable CreatePrepositionLinkable() => new CommonSingularNoun("bacon");
    }
}
