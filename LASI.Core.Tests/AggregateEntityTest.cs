using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using LASI.Utilities;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for AggregateEntityTest and is intended
    ///to contain all AggregateEntityTest Unit Tests
    /// </summary>
    [TestClass]
    public class AggregateEntityTest
    {
        private TestContext testContextInstance;

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
        //Use ClassCleanup to run code after all tests in a class have run
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

        /// <summary>
        ///A test for AggregateEntity Constructor
        /// </summary>
        [TestMethod]
        public void AggregateEntityConstructorTest()
        {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members);
            foreach (var m in members) { Assert.IsTrue(target.Contains(m)); }
        }

        /// <summary>
        ///A test for AggregateEntity Constructor
        /// </summary>
        [TestMethod]
        public void AggregateEntityConstructorTest1()
        {
            IEntity e1 = new NounPhrase(new ProperPluralNoun("Americans"));
            IEntity e2 = new NounPhrase(new ProperPluralNoun("Canadians"));
            AggregateEntity target = new AggregateEntity(e1, e2);
            Assert.IsTrue(target.Contains(e1));
            Assert.IsTrue(target.Contains(e2));

        }
        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [TestMethod]
        public void AddPossessionTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IPossessable possession = new NounPhrase(new CommonSingularNoun("fur"));
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessions.Contains(possession));
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [TestMethod]
        public void BindDescriptorTest()
        {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members
            );
            IDescriptor descriptor = new Adjective("rambunctious");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [TestMethod]
        public void BindPronounTest()
        {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members);
            IReferencer pro = new PersonalPronoun("them");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro));
            Assert.IsTrue(pro.RefersTo.Contains(target) || pro.RefersTo == target || pro.RefersTo.SetEqual(target));
            foreach (IEntity e in members)
            {
                Assert.IsTrue(pro.RefersTo.Contains(e) || pro.RefersTo == e);

            }
        }

        /// <summary>
        ///A test for GetEnumerator
        /// </summary>
        [TestMethod]
        public void GetEnumeratorTest()
        {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members
            );
            IEnumerator<IEntity> expected = members.GetEnumerator();
            IEnumerator<IEntity> actual;
            actual = target.GetEnumerator();
            while (expected.MoveNext() | actual.MoveNext())
            {
                Assert.AreEqual(expected.Current, actual.Current);
            }
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            string expected = "[ 2 ] NounPhrase \"Americans\" NounPhrase \"Canadians\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for BoundPronouns
        /// </summary>
        [TestMethod]
        public void BoundPronounsTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            IEnumerable<IReferencer> actual;
            IEnumerable<IReferencer> expected = new[] { new PersonalPronoun("them") };

            foreach (var r in expected) { target.BindReferencer(r); }
            actual = target.Referencers;
            foreach (var r in expected) { Assert.IsTrue(actual.Contains(r)); }
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [TestMethod]
        public void DescriptorsTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            IEnumerable<IDescriptor> actual;
            IDescriptor adj1 = new Adjective("western");
            IDescriptor adj2 = new Adjective("proud");
            target.BindDescriptor(adj1);
            target.BindDescriptor(adj2);
            actual = target.Descriptors;
            Assert.IsTrue(target.Descriptors.Contains(adj1) && adj1.Describes == target);
            Assert.IsTrue(target.Descriptors.Contains(adj2) && adj2.Describes == target);
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [TestMethod]
        public void DirectObjectOfTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IVerbal expected = new VerbPhrase(new BaseVerb("eat"));
            target.DirectObjectOf = expected;

            IVerbal actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        /// </summary>
        [TestMethod]
        public void IndirectObjectOfTest()
        {
            AggregateEntity target = new AggregateEntity(
                new CommonSingularNoun("spoon"),
                new CommonSingularNoun("fork")
            );
            IVerbal expected = new VerbPhrase(new PastTenseVerb("were"), new PastParticiple("eaten"));
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MetaWeight
        /// </summary>
        [TestMethod]
        public void MetaWeightTest()
        {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members);
            double expected = new Random().NextDouble();
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Possessed
        /// </summary>
        [TestMethod]
        public void PossessedTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            var possessions = new[]  {
                new NounPhrase(new CommonPluralNoun("claws")),
                new NounPhrase(new CommonPluralNoun("teeth"))
            };
            foreach (var possession in possessions) { target.AddPossession(possession); }
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            foreach (var possession in possessions) { Assert.IsTrue(actual.Contains(possession)); }

        }

        /// <summary>
        ///A test for Possesser
        /// </summary>
        [TestMethod]
        public void PossesserTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IPossesser expected = new NounPhrase(new ProperPluralNoun("Americans"));
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [TestMethod]
        public void SubjectOfTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IVerbal expected = new BaseVerb("eat");
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [TestMethod]
        public void TextTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            string expected = "Americans ," + " Canadians";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Weight
        /// </summary>
        [TestMethod]
        public void WeightTest()
        {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members);
            double expected = new Random().NextDouble();
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
        }
    }
}
