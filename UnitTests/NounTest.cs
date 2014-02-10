using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for NounTest and is intended
    ///to contain all NounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NounTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in A class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual Noun CreateNoun() {
            Noun target = new CommonSingularNoun("dog");
            return target;
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            Noun target = CreateNoun();
            IEntity possession = new NounPhrase(new[] { new CommonSingularNoun("chew"), new CommonSingularNoun("toy") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessed.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriberTest() {
            Noun target = CreateNoun();
            IDescriptor adjective = new Adjective("rambunctious");
            target.BindDescriptor(adjective);
            Assert.IsTrue(target.Descriptors.Contains(adjective) && adjective.Describes == target);
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            Noun target = CreateNoun();
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro) && pro.ReferredTo.Any(e => e == target));
        }

        ///// <summary>
        /////A test for Equals
        /////</summary>
        //[TestMethod()]
        //public void EqualsTest() {
        //    Noun from = CreateNoun(); // TODO: Initialize to an appropriate value
        //    IEntity second = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = from.Equals(second);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescribedByTest() {
            Noun target = CreateNoun();

            Assert.IsTrue(target.Descriptors != null && target.Descriptors.Count() == 0);
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("walked");
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("gave");
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectReferences
        ///</summary>
        [TestMethod()]
        public void IndirectReferencesTest() {
            Noun target = CreateNoun();
            IEnumerable<IReferencer> actual;
            actual = target.Referees;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            Noun target = CreateNoun();
            IEnumerable<IPossessable> actual;
            actual = target.Possessed;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for PossessesFor
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity expected = new NounPhrase(new Word[] { new Adjective("Red"), new CommonSingularNoun("Team") });
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new Verb("runs", VerbForm.SingularPresent);
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SuperTaxonomicNoun
        ///</summary>
        [TestMethod()]
        public void SuperTaxonomicNounTest() {
            Noun target = CreateNoun();              Noun expected = new ProperSingularNoun("Highland"); 
            Noun actual;
            target.SuperTaxonomicNoun = expected;
            actual = target.SuperTaxonomicNoun;
            Assert.AreEqual(expected, actual);
         }
 

        /// <summary>
        ///A test for SubTaxonomicNoun
        ///</summary>
        [TestMethod()]
        public void SubTaxonomicNounTest() {
            Noun target = CreateNoun();              Noun expected = new CommonSingularNoun("food");
            Noun actual;
            target.SubTaxonomicNoun = expected;
            actual = target.SubTaxonomicNoun;
            Assert.AreEqual(expected, actual);
         }

        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod()]
        public void RefereesTest() {
            Noun target = CreateNoun(); 
            IEnumerable<IReferencer> actual;
            actual = target.Referees;
            Assert.IsTrue(!actual.Any());
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro));
            Assert.IsTrue(target.Referees.All(r => r.ReferredTo == target || r.ReferredTo.Contains(target)));
         }

        /// <summary>
        ///A test for QuantifiedBy
        ///</summary>
        [TestMethod()]
        public void QuantifiedByTest() {
            Noun target = CreateNoun();  
            IQuantifier expected = new Quantifier("3"); 
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.QuantifiedBy.Quantifies == target);
        }
  

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescriptorsTest() {
            Noun target = CreateNoun();  
            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindReferencer
        ///</summary>
        [TestMethod()]
        public void BindReferencerTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IReferencer pro  = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro));
            Assert.IsTrue(target.Referees.All(r => r.ReferredTo == target || r.ReferredTo.Contains(target)));

          }

        /// <summary>
        ///A test for BindDeterminer
        ///</summary>
        [TestMethod()]
        public void BindDeterminerTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            Determiner determiner = null; // TODO: Initialize to an appropriate value
            target.BindDeterminer(determiner);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriptorTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IDescriptor descriptor = null; // TODO: Initialize to an appropriate value
            target.BindDescriptor(descriptor);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

 
    }
}
