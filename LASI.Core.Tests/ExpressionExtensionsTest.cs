using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics.Tests
{
    [TestClass]
    public class ExpressionExtensionsTest
    {
        [TestMethod]
        public void IsRelatedToTest1() {

            IEntity e1 = new CommonPluralNoun("dogs");
            IEntity e2 = new CommonPluralNoun("cats");
            IVerbal v = new BaseVerb("chase");
            e1.SetRelationshipLookup(new SampleRelationshipLookup(new[] { v }));
            v.BindSubject(e1);
            v.BindDirectObject(e2);
            Assert.IsTrue(e1.IsRelatedTo(e2).On(v));
        } /// <summary>
        ///A test for SetRelationshipLookup
        /// </summary>
        [TestMethod()]
        public void SetRelationshipLookupTest() {
            IEntity entity = null; // TODO: Initialize to an appropriate value
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = null; // TODO: Initialize to an appropriate value
            ExpressionExtensions.SetRelationshipLookup(entity, relationshipLookup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for On
        /// </summary>
        [TestMethod()]
        public void OnTest() {
            Nullable<ActionsRelatedOn> relatorSet = new Nullable<ActionsRelatedOn>(); // TODO: Initialize to an appropriate value
            IVerbal relator = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = ExpressionExtensions.On(relatorSet, relator);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsRelatedTo
        /// </summary>
        [TestMethod()]
        public void IsRelatedToTest2() {
            IEntity performer = null; // TODO: Initialize to an appropriate value
            IEntity receiver = null; // TODO: Initialize to an appropriate value
            Nullable<ActionsRelatedOn> expected = new Nullable<ActionsRelatedOn>(); // TODO: Initialize to an appropriate value
            Nullable<ActionsRelatedOn> actual;
            actual = ExpressionExtensions.IsRelatedTo(performer, receiver);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}