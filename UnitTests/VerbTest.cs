using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for VerbTest and is intended
    ///to contain all VerbTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VerbTest
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
        ///A test for Verb Constructor
        ///</summary>
        [TestMethod()]
        public void VerbConstructorTest() {
            string text = "insulate";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);

            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.Tense == tense);
            Assert.IsTrue(target.Subjects.Count() == 0);
            Assert.IsTrue(target.DirectObjects.Count() == 0);
            Assert.IsTrue(target.IndirectObjects.Count() == 0);
            Assert.IsTrue(target.Modality == null);
            Assert.IsTrue(target.IsPossessive == false);
        }

        /// <summary>
        ///A test for AttachObjectViaPreposition
        ///</summary>
        [TestMethod()]
        public void AttachObjectViaPrepositionTest() {
            string text = "insulate";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            NounPhrase prepositionObject = new NounPhrase(new[] { new PersonalPronoun("them") });
            IPrepositional prep = new Preposition("for");
            prep.BindObject(prepositionObject);
            target.AttachObjectViaPreposition(prep);
            Assert.IsTrue(target.ObjectOfThePreoposition == prepositionObject);
        }

        /// <summary>
        ///A test for BindDirectObject
        ///</summary>
        [TestMethod()]
        public void BindDirectObjectTest() {
            string text = "gave";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            IEntity directObject = new NounPhrase(new Word[] { new Determiner("a"), new CommonSingularNoun("cake") });
            target.BindDirectObject(directObject);
            Assert.IsTrue(target.DirectObjects.Count() == 1);
            Assert.IsTrue(target.DirectObjects.Contains(directObject));
        }

        /// <summary>
        ///A test for BindIndirectObject
        ///</summary>
        [TestMethod()]
        public void BindIndirectObjectTest() {
            string text = "gave";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            IEntity indirectObject = new PersonalPronoun("him");
            target.BindIndirectObject(indirectObject);
            Assert.IsTrue(target.IndirectObjects.Count() == 1);
            Assert.IsTrue(target.IndirectObjects.Contains(indirectObject));
        }

        /// <summary>
        ///A test for BindSubject
        ///</summary>
        [TestMethod()]
        public void BindSubjectTest() {
            string text = "gave";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            IEntity subject = new PersonalPronoun("he");
            target.BindSubject(subject);
            Assert.IsTrue(target.Subjects.Count() == 1);
            Assert.IsTrue(target.Subjects.Contains(subject));
        }



        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            string text = "insulate";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            IAdverbial adv = new Adverb("sufficiently");
            target.ModifyWith(adv);
            Assert.IsTrue(target.AdverbialModifiers.Contains(adv) && target.AdverbialModifiers.Count() == 1);
        }



        /// <summary>
        ///A test for Modality
        ///</summary>
        [TestMethod()]
        public void ModalityTest() {
            string text = "insulate";
            VerbForm tense = VerbForm.Base;
            Verb target = new Verb(text, tense);
            ModalAuxilary expected = new ModalAuxilary("can");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);

        }



        /// <summary>
        ///A test for Subjects
        ///</summary>
        [TestMethod()]
        public void SubjectsTest() {
            string text = "attack";
            //            VerbForm form = (VerbForm)(new Random(DateTime.Now.Millisecond).Next(0, Enum.GetValues(typeof(VerbForm)).Length)); // TODO: Initialize to an appropriate value
            VerbForm form = VerbForm.Base;
            Verb target = new Verb(text, form);
            IEnumerable<IEntity> actual;
            actual = target.Subjects;
            Assert.IsTrue(!actual.Any());
            IEntity subject = new CommonPluralNoun("chimpanzees");
            target.BindSubject(subject);
            actual = target.Subjects;
            Assert.IsTrue(actual.Contains(subject));
            Assert.IsTrue(target.AggregateSubject.Contains(subject));
        }

        /// <summary>
        ///A test for Modifiers
        ///</summary>
        [TestMethod()]
        public void ModifiersTest() {
            string text = "attacked";
            VerbForm form = VerbForm.Past;
            Verb target = new Verb(text, form);
            IEnumerable<IAdverbial> actual;
            actual = target.AdverbialModifiers;
            Assert.IsTrue(!actual.Any());
            IAdverbial modifier = new Adverb("swiftly");
            target.ModifyWith(modifier);
            actual = target.AdverbialModifiers;
            Assert.IsTrue(actual.Contains(modifier));
            Assert.IsTrue(modifier.Modifies == target);
        }



        /// <summary>
        ///A test for IsPossessive
        ///</summary>
        [TestMethod()]
        public void IsPossessiveTest() {
            string text = "has";
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsPossessive;
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for IsClassifier
        ///</summary>
        [TestMethod()]
        public void IsClassifierTest() {
            string text = "is";
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsClassifier;
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for IndirectObjects
        ///</summary>
        [TestMethod()]
        public void IndirectObjectsTest() {
            string text = "attack";
            VerbForm form = VerbForm.Base;
            Verb target = new Verb(text, form);
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Assert.IsTrue(!actual.Any());
            IEntity indirectObject = new CommonPluralNoun("allies");
            target.BindIndirectObject(indirectObject);
            actual = target.IndirectObjects;
            Assert.IsTrue(actual.Contains(indirectObject));
            Assert.IsTrue(target.AggregateIndirectObject.Contains(indirectObject));

        }

        /// <summary>
        ///A test for DirectObjects
        ///</summary>
        [TestMethod()]
        public void DirectObjectsTest() {
            string text = "attack";
            VerbForm form = VerbForm.Base;
            Verb target = new Verb(text, form);
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Assert.IsTrue(!actual.Any());
            IEntity directObject = new CommonPluralNoun("monkeys");
            target.BindDirectObject(directObject);
            actual = target.DirectObjects;
            Assert.IsTrue(actual.Contains(directObject));
            Assert.IsTrue(target.AggregateDirectObject.Contains(directObject));
        }

        /// <summary>
        ///A test for AggregateSubject
        ///</summary>
        [TestMethod()]
        public void AggregateSubjectTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateSubject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        ///</summary>
        [TestMethod()]
        public void AggregateIndirectObjectTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateIndirectObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AggregateDirectObject
        ///</summary>
        [TestMethod()]
        public void AggregateDirectObjectTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }




        /// <summary>
        ///A test for HasSubjectOrObject
        ///</summary>
        [TestMethod()]
        public void HasSubjectOrObjectTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            VerbForm form = new VerbForm(); // TODO: Initialize to an appropriate value
            Verb target = new Verb(text, form); // TODO: Initialize to an appropriate value
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
            string text = "hired";
            VerbForm form = VerbForm.Past;
            Verb target = new Verb(text, form);
            Assert.IsFalse(target.HasSubject());
            target.BindSubject(new PersonalPronoun("him"));
            Func<IEntity, bool> predicate = s => s.Text == "her";
            bool expected = false;
            bool actual;
            actual = target.HasSubject(predicate);
            Assert.AreEqual(expected, actual);
            target.BindSubject(new PersonalPronoun("her"));
            expected = true;
            actual = target.HasSubject(predicate);
            Assert.AreEqual(expected, actual);
        }


    }
}
