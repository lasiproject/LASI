using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
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
            Assert.IsTrue(target.Modifiers.Contains(adv) && target.Modifiers.Count() == 1);
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


    }
}
