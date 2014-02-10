using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for AggregateEntityTest and is intended
    ///to contain all AggregateEntityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AggregateEntityTest
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
        ///</summary>
        [TestMethod()]
        public void AggregateEntityConstructorTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members);
            foreach (var membr in members) {
                Assert.IsTrue(members.Contains(membr));
            }
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new CommonPluralNoun("cats") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("dogs") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IPossessable possession = new NounPhrase(new Word[] { new CommonSingularNoun("fur") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessed.Contains(possession));
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriptorTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IDescriptor descriptor = new Adjective("rambunctious");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IReferencer pro = new PersonalPronoun("them");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro));
            Assert.IsTrue(pro.ReferredTo.Contains(target));
            foreach (IEntity e in members) {
                Assert.IsTrue(pro.ReferredTo.Contains(e));
                e.Referees.Contains(pro);
            }
        }

        /// <summary>
        ///A test for GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest() {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members);
            IEnumerator<IEntity> expected = members.GetEnumerator();
            IEnumerator<IEntity> actual;
            actual = target.GetEnumerator();
            while (expected.MoveNext() | actual.MoveNext()) {
                Assert.AreEqual(expected.Current, actual.Current);
            }
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual("[ 2 ] NounPhrase \"Americans\" NounPhrase \"Canadians\"", actual);
        }

        /// <summary>
        ///A test for BoundPronouns
        ///</summary>
        [TestMethod()]
        public void BoundPronounsTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members); // TODO: Initialize to an appropriate value
            IEnumerable<IReferencer> actual;
            IEnumerable<IReferencer> expected = new[] { new PersonalPronoun("them") };
            actual = target.Referees;
            foreach (var pro in expected) { target.BindReferencer(pro); }
            foreach (var pro in expected) { Assert.IsTrue(actual.Contains(pro)); }
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescriptorsTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members);
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
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new CommonPluralNoun("cats") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("dogs") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IVerbal expected = new VerbPhrase(new Verb[] { new Verb("eat", VerbForm.Base) }); // TODO: Initialize to an appropriate value
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
            IEnumerable<IEntity> members = new[] { new CommonSingularNoun("spoon"), new CommonSingularNoun("fork") };
            AggregateEntity target = new AggregateEntity(members);
            IVerbal expected = new VerbPhrase(new Verb[] { new PastTenseVerb("were"), new PastParticipleVerb("eaten") });
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MetaWeight
        ///</summary>
        [TestMethod()]
        public void MetaWeightTest() {
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
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new CommonPluralNoun("cats") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("dogs") })
            };
            var possessions = new[]  {
                new NounPhrase(new Word[] { new CommonPluralNoun("claws") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("teeth") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IEnumerable<IPossessable> actual;
            actual = target.Possessed;
            foreach (var possession in possessions) { target.AddPossession(possession); }
            foreach (var possession in possessions) { Assert.IsTrue(actual.Contains(possession)); }

        }

        /// <summary>
        ///A test for Possesser
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new CommonPluralNoun("cats") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("dogs") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IPossesser expected = new NounPhrase(new Word[] { new ProperPluralNoun("Americans") });
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
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new CommonPluralNoun("cats") }),
                new NounPhrase(new Word[] { new CommonPluralNoun("dogs") })
            };
            AggregateEntity target = new AggregateEntity(members);
            IVerbal expected = new Verb("eat", VerbForm.Base);
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new Word[] { new ProperPluralNoun("Americans") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("Canadians") })
            };
            AggregateEntity target = new AggregateEntity(members);
            string actual;
            actual = target.Text;
            Assert.AreEqual("Americans ," + " Canadians", actual);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest() {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members);
            Type expected = target.GetType();
            Type actual;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod()]
        public void WeightTest() {
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
