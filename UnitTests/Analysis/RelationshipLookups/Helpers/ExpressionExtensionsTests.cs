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
    public class ExpressionExtensionsTests
    {
        [TestMethod]
        public void IsRelatedToTest() {

            IEntity e1 = new CommonPluralNoun("dogs");
            IEntity e2 = new CommonPluralNoun("cats");
            IVerbal v = new Verb("chase", VerbForm.Base);

            v.BindSubject(e1);
            v.BindDirectObject(e2);

            Assert.IsTrue(e1.IsRelatedTo(e2).On(v));
        }

        [TestMethod]
        public void OnTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void SetRelationshipLookupTest() {
            Assert.Fail();
        }
    }
}