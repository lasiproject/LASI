using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for VerbPhraseTest and is intended
    ///to contain all VerbPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
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


        /// <summary>
        ///A test for VerbPhrase Constructor
        ///</summary>
        [TestMethod()]
        public void VerbPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            Assert.IsTrue(composedWords == target.Words);
        }
        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            IAdverbial adv = new Adverb("daringly");
            target.ModifyWith(adv);
            Assert.IsTrue(target.Modifiers.Contains(adv));
        }

        /// <summary>
        ///A test for BoundSubject
        ///</summary>
        [TestMethod()]
        public void BoundSubjectTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("ran", VerbForm.Past), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            IEntity expected = new PersonalPronoun("he");
            IEntity actual;
            target.BindSubject(expected);
            actual = target.Subjects.First();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Modality
        ///</summary>
        [TestMethod()]
        public void ModalityTest() {
            VerbPhrase target = CreateVerbPhrase();
            ModalAuxilary expected = new ModalAuxilary("cannot");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);
        }

        private static VerbPhrase CreateVerbPhrase() {

            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            return target;
        }




        /// <summary>
        ///A test for Subjects
        ///</summary>
        [TestMethod()]
        public void SubjectsTest() { 
            VerbPhrase target = CreateVerbPhrase();
            IEnumerable<IEntity> actual;
            actual = target.Subjects;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Modifiers
        ///</summary>
        [TestMethod()]
        public void ModifiersTest() {
            VerbPhrase target = CreateVerbPhrase();

            IEnumerable<IAdverbial> actual;
            actual = target.Modifiers;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 

        /// <summary>
        ///A test for IsPossessive
        ///</summary>
        [TestMethod()]
        public void IsPossessiveTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsPossessive;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsClassifier
        ///</summary>
        [TestMethod()]
        public void IsClassifierTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsClassifier;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndirectObjects
        ///</summary>
        [TestMethod()]
        public void IndirectObjectsTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectObjects
        ///</summary>
        [TestMethod()]
        public void DirectObjectsTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target.DirectObjects;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AggregateSubject
        ///</summary>
        [TestMethod()]
        public void AggregateSubjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateSubject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        ///</summary>
        [TestMethod()]
        public void AggregateIndirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateIndirectObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AggregateDirectObject
        ///</summary>
        [TestMethod()]
        public void AggregateDirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdjectivalModifier
        ///</summary>
        [TestMethod()]
        public void AdjectivalModifierTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IDescriptor expected = null; // TODO: Initialize to an appropriate value
            IDescriptor actual;
            target.AdjectivalModifier = expected;
            actual = target.AdjectivalModifier;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 

        /// <summary>
        ///A test for HasSubjectOrObject
        ///</summary>
        [TestMethod()]
        public void HasSubjectOrObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubjectOrObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

 

        /// <summary>
        ///A test for HasSubject
        ///</summary>
        [TestMethod()]
        public void HasSubjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 
        /// <summary>
        ///A test for HasObject
        ///</summary>
        [TestMethod()]
        public void HasObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 

        /// <summary>
        ///A test for HasIndirectObject
        ///</summary>
        [TestMethod()]
        public void HasIndirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasIndirectObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 
        /// <summary>
        ///A test for HasDirectObject
        ///</summary>
        [TestMethod()]
        public void HasDirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasDirectObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

      

        /// <summary>
        ///A test for BindSubject
        ///</summary>
        [TestMethod()]
        public void BindSubjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEntity subject = null; // TODO: Initialize to an appropriate value
            target.BindSubject(subject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindIndirectObject
        ///</summary>
        [TestMethod()]
        public void BindIndirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEntity indirectObject = null; // TODO: Initialize to an appropriate value
            target.BindIndirectObject(indirectObject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindDirectObject
        ///</summary>
        [TestMethod()]
        public void BindDirectObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEntity directObject = null; // TODO: Initialize to an appropriate value
            target.BindDirectObject(directObject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AttachObjectViaPreposition
        ///</summary>
        [TestMethod()]
        public void AttachObjectViaPrepositionTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IPrepositional prepositional = null; // TODO: Initialize to an appropriate value
            target.AttachObjectViaPreposition(prepositional);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

 
    }
}
