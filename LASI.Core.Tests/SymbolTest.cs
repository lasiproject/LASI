using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for SymbolTest and is intended
    ///to contain all SymbolTest Unit Tests
    /// </summary>
    [TestClass]
    public class SymbolTest
    {
        /// <summary>
        ///A test for Symbol Constructor
        /// </summary>
        [TestMethod]
        public void SymbolConstructorTest()
        {
            char character = ',';
            Symbol target = new Symbol(character.ToString());
            Assert.AreEqual(character, target.LiteralCharacter);
            Assert.AreEqual(character.ToString(), target.Text);
        }

    }
}
