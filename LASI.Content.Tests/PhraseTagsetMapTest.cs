using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LASI.Content;
using LASI.Content.Exceptions;
using LASI.Content.Tagging;
using LASI.Core;
using NFluent;
using Fact = Xunit.FactAttribute;

namespace LASI.Content.Tests
{

    /// <summary>
    ///This is a test class for PhraseTagsetMapTest and is intended
    ///to contain all PhraseTagsetMapTest Unit Tests
    /// </summary>
    public class PhraseTagsetMapTest
    {
        class TestPhraseTagsetMap : PhraseTagsetMap
        {
            public override Func<IEnumerable<Word>, Phrase> this[string tag]
            {
                get
                {
                    try
                    {
                        return mappings[tag];
                    }
                    catch (KeyNotFoundException)
                    {
                        throw new UnknownPhraseTagException(tag);
                    }
                }
            }

            public override string this[Phrase phrase] =>
                (from mapping in mappings
                 where mapping.Value.Invoke(new Word[] { }).GetType() == phrase.GetType()
                 select mapping.Key).Single();

            private readonly IDictionary<string, Func<IEnumerable<Word>, Phrase>> mappings = new Dictionary<string, Func<IEnumerable<Word>, Phrase>>
            {
                ["NP"] = ws => new NounPhrase(ws),
                ["VP"] = ws => new VerbPhrase(ws),
            };
        }

        private static PhraseTagsetMap CreatePhraseTagsetMap() => new TestPhraseTagsetMap();

        /// <summary>
        ///A test for Item
        /// </summary>
        [Fact]
        public void ItemTest()
        {
            var target = CreatePhraseTagsetMap();
            var tag = "NP";
            Func<IEnumerable<Word>, Phrase> actual;
            actual = target[tag];
            var phrase = actual(new Word[] { new PersonalPronoun("he") });
            Check.That(tag).IsEqualTo(target[phrase]);
        }

        /// <summary>
        ///A test for Item
        /// </summary>
        [Fact]
        public void ItemTest2()
        {
            var target = CreatePhraseTagsetMap();
            Phrase phrase = new NounPhrase(new PersonalPronoun("he"));
            string actual;
            actual = target[phrase];
            Check.That("NP").IsEqualTo(actual);
        }
        [Fact]
        public void ItemTest3_FailureExpected()
        {
            var target = CreatePhraseTagsetMap();
            Check.ThatCode(() => target["NOTMAPPED"]).Throws<UnknownPhraseTagException>();
        }
    }
}
