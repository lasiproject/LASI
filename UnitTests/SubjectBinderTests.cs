using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LASI.Core.Binding.Tests
{
    using Test = TestMethodAttribute;

    [TestClass]
    public class SubjectBinderTests
    {
        [TestMethod]
        public void BindTest() {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
        [TestMethod]
        public void HasSubjectPronounTest() {
            PronounPhrase phrase = new PronounPhrase(new[] { new PersonalPronoun("they"), new PersonalPronoun("themselves") });
            Assert.IsTrue(phrase.HasSubjectPronoun());
        }
        [TestMethod]
        public void HasSubjectPronounTest1() {
            PronounPhrase phrase = new PronounPhrase(new Word[] { new PersonalPronoun("he"), new Conjunction("and"), new PersonalPronoun("it") });
            Assert.IsTrue(phrase.HasSubjectPronoun());
        }
        [TestMethod]
        public void HasSubjectPronounTest2() {
            PronounPhrase phrase = new PronounPhrase(new Word[] { new PersonalPronoun("she"), new Conjunction("and"), new PersonalPronoun("it") });
            Assert.IsTrue(phrase.HasSubjectPronoun());
        }
        [TestMethod]
        public void HasSubjectPronounTest3() {
            PronounPhrase phrase = new PronounPhrase(new Word[] { new PersonalPronoun("him"), new Conjunction("and"), new PersonalPronoun("her") });
            Assert.IsFalse(phrase.HasSubjectPronoun());
        }
    }
}
