using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.ContentSystem.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LASI.ContentSystem.Serialization.Json.Tests
{
    [TestClass]
    public class SerializationExtensionsTests
    {
        [TestMethod]
        public void ToJArrayTest() {
            var target = InternalHelpers.GetLexicalSequence();
            var result = target.ToJArray();
            Assert.Fail();
        }

        [TestMethod]
        public void ToJObjectTest() {
            Assert.Fail();
        }

        [TestMethod]
        public void ToJObjectTest1() {
            Assert.Fail();
        }

        [TestMethod]
        public void ToJObjectTest2() {
            Assert.Fail();
        }
        private static class InternalHelpers
        {
            public static JsonSerializerSettings SerializerSettings {
                get {
                    return new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                }
            }
            public static IEnumerable<ILexical> GetLexicalSequence() {
                foreach (var lexical in new[] {
                    new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians")) }) {
                    yield return lexical;
                }
            }

        }
    }
}