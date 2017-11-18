using System;
using System.Collections.Generic;
using System.Linq;
using LASI;
using LASI.Core;
using NFluent;
using Xunit;


namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for AdjectivePhraseTest and is intended
    ///to contain all AdjectivePhraseTest Unit Tests
    /// </summary>
    public class AdjectivePhraseTest
    {
        /// <summary>
        ///A test for AdjectivePhrase Constructor
        /// </summary>
        [Fact]
        public void AdjectivePhraseConstructorTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Adjective("soft"), new Adjective("smooth"), new Adjective("silky") };
            var target = new AdjectivePhrase(composedWords);
            Assert.Equal(target.Words, composedWords);
        }

        /// <summary>
        ///A test for Modifiers
        /// </summary>
        [Fact]
        public void AdverbialModifiersTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Adjective("soft"), new Conjunction("and"), new Adjective("silky") };
            var target = new AdjectivePhrase(composedWords);
            IEnumerable<IAdverbial> actual;
            actual = target.AdverbialModifiers;
            Assert.False(actual.Any());
            IAdverbial modifier = new Adverb("very");
            target.ModifyWith(modifier);
            Assert.Contains(modifier, target.AdverbialModifiers);
        }

        /// <summary>
        ///A test for Describes
        /// </summary>
        [Fact]
        public void DescribesTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Adverb("very"), new Adjective("tall") };
            var target = new AdjectivePhrase(composedWords);
            IEntity expected = new CommonSingularNoun("tree");
            IEntity actual;
            target.Describes = expected;
            actual = target.Describes;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for ModifyWith
        /// </summary>
        [Fact]
        public void ModifyWithTest()
        {
            var target = new AdjectivePhrase(new Word[] { new Adjective("tall") });
            IAdverbial adv = new Adverb("overly");
            target.ModifyWith(adv);
            Check.That(target.AdverbialModifiers).Contains(adv).Only();
        }
    }
}
