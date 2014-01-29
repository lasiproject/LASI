using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LASI.Core.Heuristics.Tests
{
    [TestClass()]
    public class LookupTests
    {
        [TestMethod()]
        public void GetGenderTest() {
            var n = new ProperSingularNoun("Patrick");

            Assert.AreEqual(n.GetGender(), n.Gender);
            Assert.AreEqual(n.GetGender(), Gender.Male);
            Assert.AreEqual((n as IEntity).GetGender(), Gender.Male);
            Assert.AreEqual((n as ProperNoun).GetGender(), Gender.Male);
            Assert.AreEqual((n as Noun).GetGender(), Gender.Male);
            var np = new NounPhrase(new Word[] { n, new ProperPluralNoun("Roberts") });
            Assert.AreEqual(np.GetGender(), n.Gender);
            Assert.AreEqual((np as IEntity).GetGender(), n.Gender);
            var np1 = new NounPhrase(new Word[] { new ProperSingularNoun("Dr."), n, new ProperPluralNoun("Rachels") });
            Assert.AreEqual(np1.GetGender(), n.Gender);
        }

        [TestMethod()]
        public void IsFemaleFullTest() {
            var np = new NounPhrase(new Word[] { new ProperSingularNoun("Julia"), new ProperPluralNoun("Roberts") });
            Assert.IsTrue(np.IsFemaleFull());
            Assert.IsFalse(np.IsMaleFull());
            var np1 = new NounPhrase(new Word[] { new ProperSingularNoun("Dr."), new ProperSingularNoun("Julia"), new ProperPluralNoun("Rachels") });
            Assert.IsTrue(np1.IsFemaleFull());
            Assert.IsFalse(np1.IsMaleFull());
        }

        [TestMethod()]
        public void IsMaleFullTest() {
            var np = new NounPhrase(new Word[] { new ProperSingularNoun("Patrick"), new ProperPluralNoun("Roberts") });
            Assert.IsTrue(np.IsMaleFull());
            Assert.IsFalse(np.IsFemaleFull());
            var np1 = new NounPhrase(new Word[] { new ProperSingularNoun("Dr."), new ProperSingularNoun("Patrick"), new ProperPluralNoun("Rachels") });
            Assert.IsTrue(np1.IsMaleFull());
            Assert.IsFalse(np1.IsFemaleFull());
        }

        [TestMethod()]
        public void IsFirstNameTest() {
            Assert.IsTrue(new ProperSingularNoun("Patrick").IsFirstName());
            Assert.IsTrue(new ProperSingularNoun("Rachel").IsFirstName());
        }

        [TestMethod()]
        public void IsLastNameTest() {
            Assert.IsTrue(new ProperSingularNoun("Patrick").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Williams").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Roberts").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Baker").IsLastName());
        }

        [TestMethod()]
        public void IsFemaleFirstTest() {
            var n = new ProperSingularNoun("Rachel");
            Assert.IsTrue(n.IsFemaleFirst());
            Assert.IsFalse(n.IsMaleFirst());
            var n2 = new ProperSingularNoun("Jamie");
            Assert.IsTrue(n.IsFemaleFirst());
        }

        [TestMethod()]
        public void IsMaleFirstTest() {
            var n = new ProperSingularNoun("Patrick");
            var n1 = new ProperSingularNoun("James");

            Assert.IsTrue(n.IsMaleFirst());
            Assert.IsTrue(n1.IsMaleFirst());
            Assert.IsFalse(n.IsFemaleFirst());
        }

        [TestMethod()]
        public void GetSynonymsTest() {
            Assert.IsTrue(new CommonSingularNoun("ball").GetSynonyms().Any());
        }

        [TestMethod()]
        public void GetSynonymsTest1() {
            Assert.IsTrue(new Verb("heal", VerbForm.Base).GetSynonyms().Any());
        }

        [TestMethod()]
        public void GetSynonymsTest2() {
            //Assert.IsTrue(new Adverb("pale").GetSynonyms().Any());
            Assert.IsTrue(new Adverb("slow").GetSynonyms().Any());
            Assert.IsTrue(new Adverb("slowly").GetSynonyms().Any());
            Assert.IsTrue(new Adverb("slowest").GetSynonyms().Any());

        }

        [TestMethod()]
        public void GetSynonymsTest3() {
            Assert.IsTrue(new Adjective("pale").GetSynonyms().Any());
        }

        [TestMethod()]
        public void IsSynonymForTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsSynonymForTest1() {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsSynonymForTest2() {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsSynonymForTest3() {
            Assert.Fail();
        }
    }
}
