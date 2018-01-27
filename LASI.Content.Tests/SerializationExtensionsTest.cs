using LASI.Core;
using LASI.Testing.Assertions;
using Newtonsoft.Json.Linq;
using NFluent;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using LASI.Utilities;

namespace LASI.Content.Serialization.Json.Tests
{
    public class SerializationExtensionsTest
    {
        [Fact]
        public void NounPhraseToJObjectHasWordsInSameOrder()
        {
            var target = TestHelper.TestNounPhrase;

            var serialized = target.ToJObject();

            Check.That(serialized["words"])
                .HasSize(target.Words.Count())
                .And.Satisfies(() => JToken.DeepEquals(JArray.FromObject(target.Words.Select(w => w.ToJObject())), serialized["words"]));
        }

        [Fact]
        public void NounPhraseToJObjectIsNotNull()
        {
            var target = TestHelper.TestNounPhrase;

            var serialized = target.ToJObject();

            Check.That(serialized).IsNotNull();
        }

        [Fact]
        public void NounPhraseToJObjectYieldsJObjectHavingTextEqualToSourceText()
        {
            var target = TestHelper.TestNounPhrase;

            var serialized = target.ToJObject();

            Check.That(target.Text).IsEqualTo((string)serialized["text"]);
        }

        [Fact]
        public void ToJArrayTest()
        {
            var target = TestHelper.GetLexicalSequence();
            var actual = target.ToJArray();

            var expected = JArray.FromObject(target.Select(e => e.ToJObject()));
            TestHelper.RemoveUniquePropertyValues(actual);
            TestHelper.RemoveUniquePropertyValues(expected);

            Assert.True(JToken.DeepEquals(expected, actual));
        }

        [Fact]
        public void VerbalToJObjectTest()
        {
            var target = TestHelper.TestVerbal;
            var serialized = target.ToJObject();
            Check.That(serialized).IsNotNull();
            Check.That(target.Text).IsEqualTo((string)serialized["text"]);
            Check.That(JToken.DeepEquals(JArray.FromObject(target.AdverbialModifiers.Select(e => e.ToJObject())), serialized["adverbialModifiers"])).IsTrue();
        }

        static class TestHelper
        {
            public static IEnumerable<ILexical> GetLexicalSequence()
            {
                foreach (var lexical in new[]
                {
                    new NounPhrase(
                        new ProperPluralNoun("Americans"),
                        new Conjunction("and"),
                        new ProperPluralNoun("Canadians")
                        )
                })
                {
                    yield return lexical;
                }
            }

            public static void RemoveUniquePropertyValues(JContainer test)
            {
                foreach (var (name, _, remove) in test.DescendantsAndSelf()
                       .OfType<JObject>()
                       .Properties()
                       .ToList())
                {
                    if (name == "name")
                    {
                        remove();
                    }
                }
            }

            public static NounPhrase TestNounPhrase =>
                                     new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));

            public static IVerbal TestVerbal => new VerbPhrase(new BaseVerb("walk"), new Adverb("swiftly"));
        }
    }
}