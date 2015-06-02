using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.Heuristics;
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
            var firstName = new ProperSingularNoun("Patrick");

            Assert.AreEqual(firstName.GetGender(), firstName.Gender);
            Assert.AreEqual(firstName.GetGender(), Gender.Male);
            Assert.AreEqual((firstName as IEntity).GetGender(), Gender.Male);
            Assert.AreEqual((firstName as ProperNoun).GetGender(), Gender.Male);
            Assert.AreEqual((firstName as Noun).GetGender(), Gender.Male);
            var fullName = new NounPhrase(firstName, new ProperPluralNoun("Roberts"));
            Assert.AreEqual(fullName.GetGender(), firstName.Gender);
            Assert.AreEqual((fullName as IEntity).GetGender(), firstName.Gender);
            var np1 = new NounPhrase(new ProperSingularNoun("Dr."), firstName, new ProperPluralNoun("Rachels"));
            Assert.AreEqual(np1.GetGender(), firstName.Gender);
        }

        [TestMethod]
        public void IsFemaleFullTest()
        {
            var np = new NounPhrase(new ProperSingularNoun("Julia"), new ProperPluralNoun("Roberts"));
            Assert.IsTrue(np.IsFullFemaleName());
            Assert.IsFalse(np.IsFullMaleName());
            var np1 = new NounPhrase(new ProperSingularNoun("Dr."), new ProperSingularNoun("Julia"), new ProperPluralNoun("Rachels"));
            Assert.IsTrue(np1.IsFullFemaleName());
            Assert.IsFalse(np1.IsFullMaleName());
        }

        [TestMethod]
        public void IsMaleFullTest()
        {
            var np = new NounPhrase(new ProperSingularNoun("Patrick"), new ProperPluralNoun("Roberts"));
            Assert.IsTrue(np.IsFullMaleName());
            Assert.IsFalse(np.IsFullFemaleName());
            var np1 = new NounPhrase(new ProperSingularNoun("Dr."), new ProperSingularNoun("Patrick"), new ProperPluralNoun("Rachels"));
            Assert.IsTrue(np1.IsFullMaleName());
            Assert.IsFalse(np1.IsFullFemaleName());
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
            Assert.IsTrue(n.IsFemaleFirstName());
            Assert.IsFalse(n.IsMaleFirstName());
            var n2 = new ProperSingularNoun("Jamie");
            Assert.IsTrue(n.IsFemaleFirstName());
        }

        [TestMethod]
        public void IsMaleFirstTest()
        {
            var n = new ProperSingularNoun("Patrick");
            var n1 = new ProperSingularNoun("James");

            Assert.IsTrue(n.IsMaleFirstName());
            Assert.IsTrue(n1.IsMaleFirstName());
            Assert.IsFalse(n.IsFemaleFirstName());
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
            Adjective adjective = new Adjective("Pale");
            Assert.IsTrue(adjective.GetSynonyms().Any(a => a.EqualsIgnoreCase(adjective.Text)));
        }

        [TestMethod]
        public void IsSynonymForTest()
        {
            Noun noun1 = new CommonSingularNoun("hobby");
            Noun noun2 = new CommonSingularNoun("pastime");

            Assert.IsTrue(noun1.IsSynonymFor(noun2));
            Assert.IsTrue(noun2.IsSynonymFor(noun1));
        }

        [TestMethod]
        public void IsSynonymForTest1()
        {
            Verb verb1 = new BaseVerb("walk");
            Verb verb2 = new BaseVerb("perambulate");
            Assert.IsTrue(verb2.IsSynonymFor(verb1));
            Assert.IsTrue(verb1.IsSynonymFor(verb2));
        }

        [TestMethod]
        public void IsSynonymForTest2()
        {
            Adverb adverb1 = new Adverb("furtively");
            Adverb adverb2 = new Adverb("stealthily");
            Assert.IsTrue(adverb1.IsSynonymFor(adverb2));
            Assert.IsTrue(adverb2.IsSynonymFor(adverb1));
        }

        [TestMethod]
        public void IsSynonymForTest3()
        {
            Adjective adjective1 = new Adjective("pale");
            Adjective adjective2 = new Adjective("pallid");
            Assert.IsTrue(adjective1.IsSynonymFor(adjective2));
            Assert.IsTrue(adjective2.IsSynonymFor(adjective1));
        }
    }
}
