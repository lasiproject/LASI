using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{
    /// <summary>
    /// This is a test class for IEntityTest and is intended to contain all IEntityTest Unit Tests
    /// </summary>
    [TestClass]
    public class IEntityTest
    {
        /// <summary>
        ///A test for EntityKind
        /// </summary>
        [TestMethod]
        public void EntityKindTest()
        {
            IEntity target = new CommonSingularNoun("cat");
            EntityKind actual;
            actual = target.EntityKind;
            Assert.AreEqual(actual, EntityKind.Thing);
        }
    }
}