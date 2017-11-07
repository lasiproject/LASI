using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFluent;

namespace LASI.Utilities.Tests
{
    using Fact = Xunit.FactAttribute;
    public class DictionaryExtensionsTest
    {
        private static Dictionary<string, int?> target = new Dictionary<string, int?>
        {
            ["zero"] = null,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4
        };

        [Fact]
        public void DeconstructMustProduceTheExpectedValues()
        {
            DeconstructMustProduceTheExpectedValuesTestHelper(1, "hello", (1, "hello"));
            DeconstructMustProduceTheExpectedValuesTestHelper(2, "hello1", (2, "hello1"));
            DeconstructMustProduceTheExpectedValuesTestHelper(3, "hello3", (3, "hello3"));
            DeconstructMustProduceTheExpectedValuesTestHelper(4, "hello4", (4, "hello4"));
        }

        private static void DeconstructMustProduceTheExpectedValuesTestHelper<T1, T2>(T1 key, T2 value, (T1, T2) expected)
        {
            var pair = new KeyValuePair<T1, T2>(key, value);
            var (k, v) = pair;
            Check.That((k, v)).IsEqualTo(expected);
        }
        [Fact]
        public void GetValueOrDefaultTest()
        {
            Check.That(target.GetValueOrDefault("zero")).IsNull();
            Check.That(target.GetValueOrDefault("five")).IsNull();
            Check.That(target.GetValueOrDefault("one")).IsEqualTo(1);
            Check.That(target.GetValueOrDefault("one")).IsNotNull();
        }


        [Fact]
        public void GetValueOrDefaultTest1()
        {
            Check.That(target.GetValueOrDefault("one", default(int?))).IsNotNull();
            Check.That(target.GetValueOrDefault("eight", default(int?))).IsNull();
            Check.That(target.GetValueOrDefault("eight", 8)).IsEqualTo(8);
        }

        [Fact]
        public void GetValueOrDefaultTest2()
        {
            Check.That(target.GetValueOrDefault("one", () => default)).IsNotNull();
            Check.That(target.GetValueOrDefault("eight", () => default)).IsNull();
            Check.That(target.GetValueOrDefault("eight", () => 8)).IsEqualTo(8);
            Check.That(target.GetValueOrDefault("four", () => 7)).IsEqualTo(4);
            Check.That(target.GetValueOrDefault("four", () => 7)).IsNotEqualTo(7);
        }
        [Fact]
        public void GetValueOrDefaultTest3()
        {
            foreach (var keyValuePair in target)
            {
                Check.That(target[keyValuePair.Key]).IsEqualTo(target.GetValueOrDefault(keyValuePair.Key));
            }
        }
        [Fact]
        public void ForEachTest()
        {
            target.ForEach((key, value) =>
            {
                switch (key)
                {
                    case "zero":
                        Check.That(value).IsNull();
                        break;
                    case "one":
                        Check.That(value).IsEqualTo(1);
                        break;
                    case "two":
                        Check.That(value).IsEqualTo(2);
                        break;
                    case "three":
                        Check.That(value).IsEqualTo(3);
                        break;
                    case "four":
                        Check.That(value).IsEqualTo(4);
                        break;
                    default:
                        throw new Exception("test failed");
                }
            });
        }

        [Fact]
        public void WithIndicesTest()
        {
            var indexedTarget = target.WithIndices();
            var expectedIndex = 0;
            foreach (var indexedKeyValuePair in indexedTarget)
            {
                Check.That(expectedIndex).IsEqualTo(indexedKeyValuePair.Value.index);
                expectedIndex++;
            }
        }
    }
}