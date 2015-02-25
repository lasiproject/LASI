using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.Heuristics.WordMorphing;
using LASI.Core.Heuristics;
using LASI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Test.Assertions;

namespace LASI.Core.Heuristics.Tests
{

    [TestClass]
    public class LexiconTests
    {
        [TestMethod]
        public void GetGenderTest()
        {
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

        [TestMethod]
        public void IsFemaleFullTest()
        {
            var np = new NounPhrase(new Word[] { new ProperSingularNoun("Julia"), new ProperPluralNoun("Roberts") });
            Assert.IsTrue(np.IsFemaleFull());
            Assert.IsFalse(np.IsMaleFull());
            var np1 = new NounPhrase(new Word[] { new ProperSingularNoun("Dr."), new ProperSingularNoun("Julia"), new ProperPluralNoun("Rachels") });
            Assert.IsTrue(np1.IsFemaleFull());
            Assert.IsFalse(np1.IsMaleFull());
        }

        [TestMethod]
        public void IsMaleFullTest()
        {
            var np = new NounPhrase(new Word[] { new ProperSingularNoun("Patrick"), new ProperPluralNoun("Roberts") });
            Assert.IsTrue(np.IsMaleFull());
            Assert.IsFalse(np.IsFemaleFull());
            var np1 = new NounPhrase(new Word[] { new ProperSingularNoun("Dr."), new ProperSingularNoun("Patrick"), new ProperPluralNoun("Rachels") });
            Assert.IsTrue(np1.IsMaleFull());
            Assert.IsFalse(np1.IsFemaleFull());
        }

        [TestMethod]
        public void IsFirstNameTest()
        {
            Assert.IsTrue(new ProperSingularNoun("Patrick").IsFirstName());
            Assert.IsTrue(new ProperSingularNoun("Rachel").IsFirstName());
        }

        [TestMethod]
        public void IsLastNameTest()
        {
            Assert.IsTrue(new ProperSingularNoun("Patrick").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Williams").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Roberts").IsLastName());
            Assert.IsTrue(new ProperSingularNoun("Baker").IsLastName());
        }

        [TestMethod]
        public void IsFemaleFirstTest()
        {
            var n = new ProperSingularNoun("Rachel");
            Assert.IsTrue(n.IsFemaleFirst());
            Assert.IsFalse(n.IsMaleFirst());
            var n2 = new ProperSingularNoun("Jamie");
            Assert.IsTrue(n.IsFemaleFirst());
        }

        [TestMethod]
        public void IsMaleFirstTest()
        {
            var n = new ProperSingularNoun("Patrick");
            var n1 = new ProperSingularNoun("James");

            Assert.IsTrue(n.IsMaleFirst());
            Assert.IsTrue(n1.IsMaleFirst());
            Assert.IsFalse(n.IsFemaleFirst());
        }

        [TestMethod]
        public void GetSynonymsTest()
        {
            Noun noun = new CommonSingularNoun("ball");
            Assert.IsTrue(noun.GetSynonyms().Any(n => !n.EqualsIgnoreCase(noun.Text)));
        }

        [TestMethod]
        public void GetSynonymsTest1()
        {
            Verb verb = new BaseVerb("heal");
            Assert.IsTrue(verb.GetSynonyms().Any(v => !v.EqualsIgnoreCase(verb.Text)));
        }


        [TestMethod]
        public void GetSynonymsTest3()
        {
            Adjective adjective = new Adjective("pale");
            Assert.IsTrue(adjective.GetSynonyms().Any(a => a.EqualsIgnoreCase(adjective.Text)));
        }

        [TestMethod]
        public void IsSynonymForTest()
        {
            Noun noun1 = new CommonSingularNoun("hobby");
            Noun noun2 = new CommonSingularNoun("passtime");

            Assert.IsTrue(noun1.IsSynonymFor(noun2));
            Assert.IsTrue(noun2.IsSynonymFor(noun1));
        }

        [TestMethod]
        public void IsSynonymForTest1()
        {
            Verb verb1 = new BaseVerb("walk");
            Verb verb2 = new BaseVerb("perambulate");
            Assert.IsTrue(verb1.IsSynonymFor(verb2));
            Assert.IsTrue(verb2.IsSynonymFor(verb1));
        }

        [TestMethod]
        public void IsSynonymForTest2()
        {
            Adverb adverb1 = new Adverb("fertively");
            Adverb adverb2 = new Adverb("stealthily");
            Assert.IsTrue(adverb1.IsSynonymFor(adverb2));
            Assert.IsTrue(adverb2.IsSynonymFor(adverb1));
        }

        [TestMethod]
        public void IsSynonymForTest3()
        {
            Adjective adjective1 = new Adjective("pale");
            Adjective adjective2 = new Adjective("palid");
            Assert.IsTrue(adjective1.IsSynonymFor(adjective2));
            Assert.IsTrue(adjective2.IsSynonymFor(adjective1));
        }
    }
}
