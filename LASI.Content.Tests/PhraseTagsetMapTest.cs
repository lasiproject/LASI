using LASI.Content.Tagging;
using System;
using System.Linq;
using LASI.Core;
using System.Collections.Generic;
using System.Reflection;
using LASI.Content;
using NFluent;
using TestMethod = Xunit.FactAttribute;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PhraseTagsetMapTest and is intended
    ///to contain all PhraseTagsetMapTest Unit Tests
    /// </summary>
    public class PhraseTagsetMapTest
    {

        #region Additional test attributes

        #endregion
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
        [TestMethod]
        public void ItemTest()
        {
            PhraseTagsetMap target = CreatePhraseTagsetMap();
            string tag = "NP";
            Func<IEnumerable<Word>, Phrase> actual;
            actual = target[tag];
            var phrase = actual(new Word[] { new PersonalPronoun("he") });
            Check.That(tag).IsEqualTo(target[phrase]);
        }

        /// <summary>
        ///A test for Item
        /// </summary>
        [TestMethod]
        public void ItemTest2()
        {
            PhraseTagsetMap target = CreatePhraseTagsetMap();
            Phrase phrase = new NounPhrase(new Word[] { new PersonalPronoun("he") });
            string actual;
            actual = target[phrase];
            Check.That("NP").IsEqualTo(actual);
        }
        [TestMethod]
        public void ItemTest3_FailureExpected()
        {
            PhraseTagsetMap target = CreatePhraseTagsetMap();
            Check.ThatCode(() => target["NOTMAPPED"]).Throws<UnknownPhraseTagException>();
        }
    }
}
