using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.Heuristics;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ISimpleGenderedTest and is intended
    ///to contain all ISimpleGenderedTest Unit Tests
    /// </summary>
    [TestClass]
    public class ISimpleGenderedTest
    {
        /// <summary>
        ///A test for Gender
        /// </summary>
        [TestMethod]
        public void GenderTest1()
        {
            ISimpleGendered target = new ProperSingularNoun("Jack");
            Gender actual;
            actual = target.Gender;
            Assert.AreEqual(Gender.Male, actual);
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [TestMethod]
        public void GenderTest2()
        {
            ISimpleGendered target = new ProperSingularNoun("Jill");
            Gender actual = target.Gender;
            Assert.AreEqual(Gender.Female, actual);
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [TestMethod]
        public void GenderTest3()
        {
            ISimpleGendered target = new ProperSingularNoun("Carnegie");
            Gender actual = target.Gender;
            Assert.AreEqual(Gender.Neutral, actual);
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [TestMethod]
        public void GenderTest4()
        {
            ISimpleGendered target = new ProperSingularNoun("LASI");
            Gender actual = target.Gender;
            Assert.AreEqual(Gender.Neutral, actual);
        }

    }
}
