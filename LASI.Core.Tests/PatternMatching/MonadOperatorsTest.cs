using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Test.Assertions;

namespace LASI.Core.Tests.PatternMatching
{
    [TestClass]
    public class MonadOepratorsTest
    {
        [TestMethod]
        public void SelectTest1()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new VerbPhrase(expectedWord, new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         select word;
            Assert.AreEqual(expectedWord, result.Single());
        }
        [TestMethod]
        public void SelectTest2()
        {
            var expectedString = "walk";
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         select word.Text;
            Assert.AreEqual(expectedString, result.Single());
        }
        [TestMethod]
        public void WhereTest1()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         where word.Text == "run" // false
                         select word;
            EnumerableAssert.IsEmpty(result); ;
        }
        [TestMethod]
        public void WhereTest2()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new VerbPhrase(expectedWord, new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         where word.Text == "walk" // true
                         select word;
            Assert.AreEqual(expectedWord, result.Single());
        }
        [TestMethod]
        public void SelectManyTest1()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new Clause(new VerbPhrase(expectedWord, new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         select word;
            Assert.AreEqual(expectedWord, result.First());
        }
        [TestMethod]
        public void SelectManyTest2()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new Clause(new VerbPhrase(expectedWord, new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "walk"
                         select word;
            Assert.AreEqual(expectedWord, result.Single());
        }
        [TestMethod]
        public void SelectManyTest3()
        {
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "run"
                         select word;
            EnumerableAssert.IsEmpty(result);
        }
        [TestMethod]
        public void SelectManyTest4()
        {
            var expectedString = "walk";
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "walk"
                         select word.Text;
            Assert.AreEqual(expectedString, result.Single());
        }
        [TestMethod]
        public void SelectManyTest5()
        {
            var expectedCharacter = 'w';
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "walk"
                         from c in word.Text
                         select c;
            Assert.AreEqual(expectedCharacter, result.First());
        }
        [TestMethod]
        public void SelectManyTest6()
        {
            var expected = new { Character = 'w', String = "walk" };
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         select new { Character = word.Text.First(), String = word.Text };
            Assert.AreEqual(expected, result.First());
        }
        [TestMethod]
        public void SelectManyTest7()
        {
            var word = new BaseVerb("walk");
            var verbPhrase = new VerbPhrase(word, new Adverb("briskly"));
            var expected = new { Character = 'w', word, phrase = verbPhrase };
            ILexical target = new Clause(verbPhrase);
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from w in phrase.Words.OfVerb()
                         select new { Character = w.Text.First(), word = w, phrase };
            Assert.AreEqual(expected.phrase, result.First().phrase);
        }
    }
}
