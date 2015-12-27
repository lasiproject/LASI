using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Tests.PatternMatching
{
    public class MonadOperatorsTest
    {
        [Fact]
        public void SelectTest1()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new VerbPhrase(expectedWord, new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         select word;
            Assert.Equal(expectedWord, result.Single());
        }
        [Fact]
        public void SelectTest2()
        {
            var expectedString = "walk";
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         select word.Text;
            Assert.Equal(expectedString, result.Single());
        }
        [Fact]
        public void WhereTest1()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         where word.Text == "run" // false
                         select word;
            Check.That(result).IsEmpty();
        }
        [Fact]
        public void WhereTest2()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new VerbPhrase(expectedWord, new Adverb("briskly"));
            var result = from word in target.Match()
                         .Case((VerbPhrase v) => v.Words.OfVerb().First())
                         where word.Text == "walk" // true
                         select word;
            Assert.Equal(expectedWord, result.Single());
        }
        [Fact]
        public void SelectManyTest1()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new Clause(new VerbPhrase(expectedWord, new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         select word;
            Assert.Equal(expectedWord, result.First());
        }
        [Fact]
        public void SelectManyTest2()
        {
            var expectedWord = new BaseVerb("walk");
            ILexical target = new Clause(new VerbPhrase(expectedWord, new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "walk"
                         select word;
            Assert.Equal(expectedWord, result.Single());
        }
        [Fact]
        public void SelectManyTest3()
        {
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "run"
                         select word;
            Check.That(result).IsEmpty();
        }
        [Fact]
        public void SelectManyTest4()
        {
            var expectedString = "walk";
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match().Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         where word.Text == "walk"
                         select word.Text;
            Assert.Equal(expectedString, result.Single());
        }
        [Fact]
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
            Assert.Equal(expectedCharacter, result.First());
        }
        [Fact]
        public void SelectManyTest6()
        {
            var expected = new { Character = 'w', String = "walk" };
            ILexical target = new Clause(new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly")));
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from word in phrase.Words
                         select new { Character = word.Text.First(), String = word.Text };
            Assert.Equal(expected, result.First());
        }
        [Fact]
        public void SelectManyTest7()
        {
            var word = new BaseVerb("walk");
            var verbPhrase = new VerbPhrase(word, new Adverb("briskly"));
            var expected = new { Character = 'w', Word = word, Phrase = verbPhrase };
            ILexical target = new Clause(verbPhrase);
            var result = from phrase in target.Match()
                         .Case((Clause c) => c.Phrases.OfVerbPhrase().First())
                         from w in phrase.Words.OfVerb()
                         select new { Character = w.Text.First(), word = w, Phrase = phrase };
            Assert.Equal(expected.Phrase, result.First().Phrase);
        }
    }
}
