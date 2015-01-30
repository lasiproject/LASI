using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for VerbPhraseTest and is intended
    ///to contain all VerbPhraseTest Unit Tests
    ///</summary>
    [TestClass]
    public class VerbPhraseTest
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


        private static VerbPhrase CreateVerbPhrase1() {
            return new VerbPhrase(new SimpleVerb("help"));
        }

        /// <summary>
        ///A test for VerbPhrase Constructor
        ///</summary>
        [TestMethod]
        public void VerbPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new Word[] { new SimpleVerb("run"), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            Assert.IsTrue(composedWords == target.Words);
        }
        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod]
        public void ModifyWithTest() {
            VerbPhrase target = CreateVerbPhrase();
            IAdverbial adv = new Adverb("daringly");
            target.ModifyWith(adv);
            Assert.IsTrue(target.AdverbialModifiers.Contains(adv));
        }

        /// <summary>
        ///A test for BoundSubject
        ///</summary>
        [TestMethod]
        public void BoundSubjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEntity expected = new PersonalPronoun("he");
            IEntity actual;
            target.BindSubject(expected);
            actual = target.Subjects.First();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Modality
        ///</summary>
        [TestMethod]
        public void ModalityTest() {
            VerbPhrase target = CreateVerbPhrase();
            ModalAuxilary expected = new ModalAuxilary("cannot");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);
        }

        private static VerbPhrase CreateVerbPhrase() {
            IEnumerable<Word> composedWords = new Word[] { new SimpleVerb("run"), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            return target;
        }



        /// <summary>
        ///A test for IsPossessive
        ///</summary>
        [TestMethod]
        public void IsPossessiveTest() {
            VerbPhrase target = new VerbPhrase(new Word[] {
                new Adverb("certainly"),
                new PastTenseVerb("had"),
                new Quantifier("many")});
            bool actual;
            actual = target.IsPossessive;
            Assert.IsTrue(actual);
        }
        /// <summary>
        ///A test for IsPossessive
        ///</summary>
        [TestMethod]
        public void IsPossessiveTest1() {
            VerbPhrase target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("was")
            );
            bool actual;
            actual = target.IsPossessive;
            Assert.IsFalse(actual);
        }
        /// <summary>
        ///A test for IsClassifier
        ///</summary>
        [TestMethod]
        public void IsClassifierTest() {
            VerbPhrase target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("is")
            );
            bool actual;
            actual = target.IsClassifier;
            Assert.IsTrue(actual);
        }
        /// <summary>
        ///A test for IsClassifier
        ///</summary>
        [TestMethod]
        public void IsClassifierTest1() {
            VerbPhrase target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("had"),
                new Quantifier("many")
            );
            bool actual;
            actual = target.IsClassifier;
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for IndirectObjects
        ///</summary>
        [TestMethod]
        public void IndirectObjectsTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Assert.IsTrue(target.IndirectObjects.None());
            IEntity indirectObject = new PersonalPronoun("them");
            target.BindIndirectObject(indirectObject);
            Assert.IsTrue(target.IndirectObjects.Contains(indirectObject));
        }

        /// <summary>
        ///A test for DirectObjects
        ///</summary>
        [TestMethod]
        public void DirectObjectsTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEnumerable<IEntity> actual;
            actual = target.DirectObjects;
            Assert.IsTrue(target.DirectObjects.None());
            IEntity directObject = new NounPhrase(
                new Determiner("the"),
                new CommonSingularNoun("book")
            );
            target.BindDirectObject(directObject);
            Assert.IsTrue(target.DirectObjects.Contains(directObject));
        }

        /// <summary>
        ///A test for AggregateSubject
        ///</summary>
        [TestMethod]
        public void AggregateSubjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            var subject = new AggregateEntity(
                new NounPhrase(
                    new ProperSingularNoun("John"),
                    new ProperSingularNoun("Smith")
                ), new NounPhrase(
                    new PossessivePronoun("his"),
                    new CommonPluralNoun("cats")
                )
            );
            target.BindSubject(subject);
            IAggregateEntity actual;
            actual = target.AggregateSubject;
            Assert.IsFalse(actual.Except(subject).Any());
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        ///</summary>
        [TestMethod]
        public void AggregateIndirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            var indirectObject = new AggregateEntity(
                new NounPhrase(
                    new ProperSingularNoun("John"),
                    new ProperSingularNoun("Smith")
                ), new NounPhrase(
                    new PossessivePronoun("his"),
                    new CommonPluralNoun("cats")
                )
            );
            target.BindIndirectObject(indirectObject);
            IAggregateEntity actual;
            actual = target.AggregateIndirectObject;
            Assert.IsFalse(actual.Except(indirectObject).Any());
        }

        /// <summary>
        ///A test for AggregateDirectObject
        ///</summary>
        [TestMethod]
        public void AggregateDirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            IAggregateEntity directObject = new AggregateEntity(
                new NounPhrase(new ProperSingularNoun("John"), new ProperSingularNoun("Smith")),
                new NounPhrase(new PossessivePronoun("his"), new CommonPluralNoun("cats"))
            );
            target.BindDirectObject(directObject);
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Assert.IsFalse(directObject.Except(actual).Any());
        }

        /// <summary>
        ///A test for AdjectivalModifier
        ///</summary>
        [TestMethod]
        public void AdjectivalModifierTest() {
            VerbPhrase target = new VerbPhrase(new PastTenseVerb("grew"));
            IEntity subject = new PersonalPronoun("he");
            target.BindSubject(subject);
            IDescriptor expected = new Adjective("tall");
            IDescriptor actual;
            target.PostpositiveDescriptor = expected;
            actual = target.PostpositiveDescriptor;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(subject.Descriptors.Contains(expected));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            Phrase.VerboseOutput = false;
            VerbPhrase target = new VerbPhrase(
                new SimpleVerb("run"),
                new Adverb("swiftly"),
                new Preposition("through")
            );
            string expected = "VerbPhrase \"run swiftly through\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for HasSubjectOrObject
        ///</summary>
        [TestMethod]
        public void HasSubjectOrObjectTest() {
            VerbPhrase target = CreateVerbPhrase1();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindSubject(entity);
            Func<IEntity, bool> predicate = e => e.Text == "cats";
            bool expected = true;
            bool actual;
            actual = target.HasSubjectOrObject(predicate);
            Assert.AreEqual(expected, actual);
            target.BindDirectObject(entity);
            actual = target.HasSubjectOrObject(predicate);
            Assert.AreEqual(expected, actual);
            target.BindIndirectObject(entity);
            actual = target.HasSubjectOrObject(predicate);
            Assert.AreEqual(expected, actual);

        }



        /// <summary>
        ///A test for HasSubject
        ///</summary>
        [TestMethod]
        public void HasSubjectTest() {
            VerbPhrase target = CreateVerbPhrase1();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindSubject(entity);
            Func<IEntity, bool> predicate = e => e.Text == "cats";
            bool expected = true;
            bool actual;
            actual = target.HasSubject(predicate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HasObject
        ///</summary>
        [TestMethod]
        public void HasObjectTest() {
            VerbPhrase target = CreateVerbPhrase1();
            IEntity entity = new CommonPluralNoun("cats");

            Func<IEntity, bool> predicate = e => e.Text == "cats";
            bool expected = true;
            bool actual;
            target.BindDirectObject(entity);
            actual = target.HasObject(predicate);
            Assert.AreEqual(expected, actual);
            target.BindIndirectObject(entity);
            actual = target.HasObject(predicate);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for HasIndirectObject
        ///</summary>
        [TestMethod]
        public void HasIndirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase1();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindIndirectObject(entity);
            Func<IEntity, bool> predicate = e => e.Text == "cats";
            bool expected = true;
            bool actual;
            actual = target.HasIndirectObject(predicate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HasDirectObject
        ///</summary>
        [TestMethod]
        public void HasDirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindDirectObject(entity);
            Func<IEntity, bool> predicate = e => e.Text == "cats";
            bool expected = true;
            bool actual;
            actual = target.HasDirectObject(predicate);
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for BindSubject
        ///</summary>
        [TestMethod]
        public void BindSubjectTest() {
            VerbPhrase target = CreateVerbPhrase1();
            IEntity subject = new CommonPluralNoun("cats");
            target.BindSubject(subject);
            Assert.IsTrue(target.HasSubject(e => e == subject));

        }

        /// <summary>
        ///A test for BindIndirectObject
        ///</summary>
        [TestMethod]
        public void BindIndirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEntity indirectObject = new ProperSingularNoun("John");
            target.BindIndirectObject(indirectObject);
            Assert.IsTrue(target.HasIndirectObject(e => e == indirectObject));
        }

        /// <summary>
        ///A test for BindDirectObject
        ///</summary>
        [TestMethod]
        public void BindDirectObjectTest() {
            VerbPhrase target = CreateVerbPhrase();
            IEntity directObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("forest"));
            target.BindDirectObject(directObject);
            Assert.IsTrue(target.HasDirectObject(e => e == directObject));
        }

        /// <summary>
        ///A test for AttachObjectViaPreposition
        ///</summary>
        [TestMethod]
        public void AttachObjectViaPrepositionTest() {
            VerbPhrase target = new VerbPhrase(new SimpleVerb("consume"));
            IPrepositional prepositional = new Preposition("with");
            ILexical prepositionalObject = new NounPhrase(new Adjective("great"), new CommonSingularNoun("haste"));
            prepositional.BindObject(prepositionalObject);
            target.AttachObjectViaPreposition(prepositional);
            Assert.AreEqual(target.ObjectOfThePreposition, prepositionalObject);
        }


    }
}
