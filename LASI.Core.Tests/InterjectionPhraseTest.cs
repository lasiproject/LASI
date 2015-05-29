using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Shared.Test.Assertions;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for InterjectionPhraseTest and is intended
    ///to contain all InterjectionPhraseTest Unit Tests
    /// </summary>
    [TestClass]
    public class InterjectionPhraseTest
    {
        /// <summary>
        ///A test for InterjectionPhrase Constructor
        /// </summary>
        [TestMethod]
        public void InterjectionPhraseConstructorTest()
        {
            IEnumerable<Word> composed = new Word[] { new Preposition("by"), new Interjection("jove") };
            InterjectionPhrase target = new InterjectionPhrase(composed);
            Assert.AreEqual("by jove", target.Text);
            EnumerableAssert.AreSequenceEqual(composed, target.Words);
        }
    }
}
