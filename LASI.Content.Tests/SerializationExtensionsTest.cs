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
    public class SerializationExtensionsTest
    {
        [TestMethod]
        public void ToJArrayTest()
        {
            var target = TestHelper.GetLexicalSequence();
            var actual = target.ToJArray();
            var result = target.Zip(actual, (source, serialized) => new { Source = source, Serialized = serialized as dynamic });
            foreach (var r in result)
            {
                Assert.AreEqual(r.Source.Text, (string)r.Serialized.text);
            }
            JArray expected = new JArray(target.Select(e => e.ToJObject()));
            RemoveUniquePropertyValues(actual);
            RemoveUniquePropertyValues(expected);
            Assert.IsTrue(JToken.DeepEquals(expected, actual));

        }

        private static void RemoveUniquePropertyValues(JContainer test)
        {
            test.DescendantsAndSelf().OfType<JObject>().Properties().Where(property => property.Name == "name").ToList().ForEach(t => t.Remove());
        }

        [TestMethod]
        public void NounPhraseToJObjectTest()
        {
            var target = TestHelper.TestNounPhrase;
            JObject serialized = target.ToJObject();
            Assert.IsNotNull(serialized);
            Assert.AreEqual(target.Text, serialized["text"]);
            Assert.AreEqual(target.Words.Count(), serialized["words"].Count());
            Assert.IsTrue(JToken.DeepEquals(new JArray(target.Words.Select(w => w.ToJObject())), serialized["words"]));
        }

        [TestMethod]
        public void VerbalToJObjectTest()
        {
            var target = TestHelper.TestVerbal;
            JObject serialized = target.ToJObject();
            Assert.IsNotNull(serialized);
            Assert.AreEqual(target.Text, serialized["text"]);
            Assert.IsTrue(JToken.DeepEquals(new JArray(target.AdverbialModifiers.Select(e => e.ToJObject())), serialized["adverbialModifiers"]));
        }

        private static class TestHelper
        {
            public static IVerbal TestVerbal => new VerbPhrase(new BaseVerb("walk"), new Adverb("swiftly"));
            public static NounPhrase TestNounPhrase => new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            public static IEnumerable<ILexical> GetLexicalSequence()
            {
                foreach (var lexical in new[] { new NounPhrase(
                        new ProperPluralNoun("Americans"),
                        new Conjunction("and"),
                        new ProperPluralNoun("Canadians"))
                })
                {
                    yield return lexical;
                }
            }

        }
    }
}