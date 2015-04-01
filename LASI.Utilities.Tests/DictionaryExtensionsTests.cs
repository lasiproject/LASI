using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities.Tests
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private static Dictionary<string, int?> target = new Dictionary<string, int?>
        {
            ["zero"] = null,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4
        };


        [TestMethod]
        public void GetValueOrDefaultTest()
        {
            Assert.IsNull(target.GetValueOrDefault("zero"));
            Assert.IsNull(target.GetValueOrDefault("five"));
            Assert.AreEqual(1, target.GetValueOrDefault("one"));
            Assert.IsNotNull(target.GetValueOrDefault("one"));
        }


        [TestMethod]
        public void GetValueOrDefaultTest1()
        {
            Assert.IsNotNull(target.GetValueOrDefault("one", default(int?)));
            Assert.IsNull(target.GetValueOrDefault("eight", default(int?)));
            Assert.AreEqual(target.GetValueOrDefault("eight", 8), 8);
        }

        [TestMethod]
        public void GetValueOrDefaultTest2()
        {
            Assert.IsNotNull(target.GetValueOrDefault("one", () => default(int?)));
            Assert.IsNull(target.GetValueOrDefault("eight", () => default(int?)));
            Assert.AreEqual(target.GetValueOrDefault("eight", () => 8), 8);
            Assert.AreEqual(target.GetValueOrDefault("four", () => 7), 4);
            Assert.AreNotEqual(target.GetValueOrDefault("four", () => 7), 7);
        }
        [TestMethod]
        public void GetValueOrDefaultTest3()
        {
            foreach (var keyValuePair in target)
            {
                Assert.AreEqual(target[keyValuePair.Key], target.GetValueOrDefault(keyValuePair.Key));
            }
        }
        [TestMethod]
        public void ForEachTest()
        {
            target.ForEach((key, value) =>
            {
                switch (key)
                {
                    case "zero": Assert.AreEqual(value, null); break;
                    case "one": Assert.AreEqual(value, 1); break;
                    case "two": Assert.AreEqual(value, 2); break;
                    case "three": Assert.AreEqual(value, 3); break;
                    case "four": Assert.AreEqual(value, 4); break;
                    default: throw new Exception("test failed");
                }
            });
        }

        [TestMethod]
        public void WithIndexTest()
        {
            var target = DictionaryExtensionsTests.target.WithIndex();
            var expectedIndex = 0;
            foreach (var indexedKeyValuePair in target)
            {
                Assert.AreEqual(expectedIndex, indexedKeyValuePair.Value.Index);
                expectedIndex++;
            }
        }
    }
}