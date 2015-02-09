using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Content.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.Content.Serialization.Json.Tests
{
    [TestClass]
    public class SerializationExtensionsTests
    {
        [TestMethod]
        public void ToJArrayTest()
        {
            var target = TestHelper.GetLexicalSequence();
            var result = target.Zip(target.ToJArray(), (source, serialized) => new { Source = source, Serialized = serialized as dynamic });
            foreach (var serialized in result)
            {
                Assert.AreEqual(serialized.Source.Text, (string)serialized.Serialized.text);
            }
        }

        [TestMethod]
        public void ToJObject_NounPhraseTest()
        {
            var target = TestHelper.TestNounPhrase;
            JObject serialized = target.ToJObject();
            Assert.IsNotNull(serialized);
            Assert.AreEqual(target.Text, serialized["text"]);
            Assert.AreEqual(target.Words.Count(), serialized["words"].Count());
        }

        [TestMethod]
        public void ToJObject_VerbalTest()
        {
            var target = TestHelper.TestVerbal;
            JObject serialized = target.ToJObject();
            Assert.IsNotNull(serialized);
            Assert.AreEqual(target.Text, serialized["text"]);
            Assert.AreEqual(target.AdverbialModifiers.Select(e => e.ToJObject()).ToArray(), serialized["adverbialModifiers"]);
        }

        private static class TestHelper
        {
            public static IVerbal TestVerbal => new VerbPhrase(new BaseVerb("walk"), new Adverb("swiftly"));
            public static NounPhrase TestNounPhrase => new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            public static IEnumerable<ILexical> GetLexicalSequence()
            {
                foreach (var lexical in new[] { new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians")) })
                {
                    yield return lexical;
                }
            }

        }
    }
}