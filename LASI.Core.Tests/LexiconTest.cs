using System;
using System.Linq;
using LASI.Utilities;
using NFluent;
using Fact = Xunit.FactAttribute;

namespace LASI.Core.Heuristics.Tests
{
    public class LexiconTests
    {
        [Fact]
        public void GetGenderTest()
        {
            var patrick = new ProperSingularNoun("Patrick");

            Check.That(patrick.GetGender()).IsEqualTo(patrick.GetGender());
            Check.That(patrick.GetGender()).IsEqualTo(Gender.Male);
            Check.That((patrick as IEntity).GetGender()).IsEqualTo(Gender.Male);
            Check.That((patrick as ProperNoun).GetGender()).IsEqualTo(Gender.Male);
            Check.That((patrick as Noun).GetGender()).IsEqualTo(Gender.Male);
        }

        [Fact]
        public void FullNameWithPrefixGenderIsSameAsFirstNameGender()
        {
            var patrick = new ProperSingularNoun("Patrick");

            var drPatrickRachels = new NounPhrase(new ProperSingularNoun("Dr."), new ProperSingularNoun("Patrick"), new ProperPluralNoun("Rachels"));
            Check.That(drPatrickRachels.GetGender()).IsEqualTo(patrick.Gender);
        }

        [Fact]
        public void FullNameGenderIsSameAsFirstNameGender()
        {
            var patrickRoberts = new NounPhrase(new ProperSingularNoun("Patrick"), new ProperPluralNoun("Roberts"));
            var patrick = new ProperSingularNoun("Patrick");

            Check.That(patrickRoberts.GetGender()).IsEqualTo(patrick.GetGender());
            Check.That((patrickRoberts as IEntity).GetGender()).IsEqualTo(patrick.GetGender());
        }

        [Fact]
        public void IsFemaleFullTest()
        {
            var np = new NounPhrase(new ProperSingularNoun("Julia"), new ProperPluralNoun("Roberts"));
            Check.That(np.IsFullFemaleName()).IsTrue();
            Check.That(np.IsFullMaleName()).IsFalse();
            var np1 = new NounPhrase(new ProperSingularNoun("Dr."), new ProperSingularNoun("Julia"), new ProperPluralNoun("Rachels"));
            Check.That(np1.IsFullFemaleName()).IsTrue();
            Check.That(np1.IsFullMaleName()).IsFalse();
        }

        [Fact]
        public void IsMaleFullTest()
        {
            var np = new NounPhrase(new ProperSingularNoun("Patrick"), new ProperPluralNoun("Roberts"));
            Check.That(np.IsFullMaleName()).IsTrue();
            Check.That(np.IsFullFemaleName()).IsFalse();
            var np1 = new NounPhrase(new ProperSingularNoun("Dr."), new ProperSingularNoun("Patrick"), new ProperPluralNoun("Rachels"));
            Check.That(np1.IsFullMaleName()).IsTrue();
            Check.That(np1.IsFullFemaleName()).IsFalse();
        }

        [Fact]
        public void IsFirstNameTest()
        {
            Check.That(new ProperSingularNoun("Patrick").IsFirstName()).IsTrue();
            Check.That(new ProperSingularNoun("Rachel").IsFirstName()).IsTrue();
        }

        [Fact]
        public void IsLastNameTest()
        {
            Check.That(new ProperSingularNoun("Patrick").IsLastName()).IsTrue();
            Check.That(new ProperSingularNoun("Williams").IsLastName()).IsTrue();
            Check.That(new ProperSingularNoun("Roberts").IsLastName()).IsTrue();
            Check.That(new ProperSingularNoun("Baker").IsLastName()).IsTrue();
        }

        [Fact]
        public void IsFemaleFirstTest()
        {
            var n = new ProperSingularNoun("Rachel");
            Check.That(n.IsFemaleFirstName()).IsTrue();
            Check.That(n.IsMaleFirstName()).IsFalse();
            Check.That(n.IsFemaleFirstName()).IsTrue();
            var n2 = new ProperSingularNoun("Jaimie");
            Check.That(n2.IsFemaleFirstName()).IsTrue();
        }

        [Fact]
        public void IsMaleFirstTest()
        {
            var n = new ProperSingularNoun("Patrick");
            var n1 = new ProperSingularNoun("James");

            Check.That(n.IsMaleFirstName()).IsTrue();
            Check.That(n1.IsMaleFirstName()).IsTrue();
            Check.That(n.IsFemaleFirstName()).IsFalse();
        }

        [Fact]
        public void GetSynonymsOfNounIncludesOwnText()
        {
            Noun ball = new CommonSingularNoun("ball");
            Check.That(ball.GetSynonyms().Contains(ball.Text, StringComparer.OrdinalIgnoreCase)).IsTrue();
        }

        [Fact]
        public void GetSynonymsOfVerbIncludesOwnText()
        {
            Verb heal = new BaseVerb("heal");
            Check.That(heal.GetSynonyms().Contains(heal.Text, StringComparer.OrdinalIgnoreCase)).IsTrue();
        }


        [Fact]
        public void GetSynonymsOfAdjectiveIncludesOwnText()
        {
            Adjective pale = new Adjective("Pale");
            Check.That(pale.GetSynonyms().Contains(pale.Text, StringComparer.OrdinalIgnoreCase)).IsTrue();
        }

        [Fact]
        public void GetSynonymsOfAdverbIncludesOwnText()
        {
            Adverb slyly = new Adverb("slyly");
            Check.That(slyly.GetSynonyms().Contains(slyly.Text, StringComparer.OrdinalIgnoreCase)).IsTrue();
        }

        [Fact]
        public void GetSynonymsOfNounIsReflexive()
        {
            var hobby = new CommonSingularNoun("hobby");
            var pastime = new CommonSingularNoun("pastime");

            Check.That(hobby.GetSynonyms()).Contains("pastime");
            Check.That(pastime.GetSynonyms()).Contains("hobby");
        }

        [Fact]
        public void GetSynonymsOfVerbIsReflexive()
        {
            Verb walk = new BaseVerb("walk");
            Verb perambulate = new BaseVerb("perambulate");

            Check.That(walk.GetSynonyms()).Contains("perambulate");
            Check.That(perambulate.GetSynonyms()).Contains("walk");
        }

        [Fact]
        public void GetSynonymsOfAdverbIsReflexive()
        {
            Adverb furtively = new Adverb("furtively");
            Adverb stealthily = new Adverb("stealthily");
            Check.That(furtively.GetSynonyms()).Contains("stealthily");
            Check.That(stealthily.GetSynonyms()).Contains("furtively");
        }

        [Fact]
        public void GetSynonymsOfAdjectiveIsReflexive()
        {
            Adjective pale = new Adjective("pale");
            Adjective pallid = new Adjective("pallid");
            Check.That(pale.GetSynonyms()).Contains("pallid");
            Check.That(pallid.GetSynonyms()).Contains("pale");
        }
    }
}
