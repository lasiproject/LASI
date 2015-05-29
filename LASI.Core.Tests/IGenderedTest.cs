using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.Heuristics;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IGenderedTest and is intended
    ///to contain all IGenderedTest Unit Tests
    /// </summary>
    [TestClass]
    public class IGenderedTest
    {
        internal virtual ISimpleGendered CreateGendered()
        {
            ISimpleGendered target = new PersonalPronoun("he");
            return target;
        }

        /// <summary>
        ///A test for Gender
        /// </summary>
        [TestMethod]
        public void GenderTest()
        {
            ISimpleGendered target = CreateGendered();
            Gender actual;
            actual = target.Gender;
            Assert.AreEqual(actual, target.Gender);
        }
    }
}
