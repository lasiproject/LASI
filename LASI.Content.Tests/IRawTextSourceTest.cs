using LASI.Content;
using System;
using System.Threading.Tasks;
using Xunit;
using NFluent;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for IUntaggedTextSourceTest and is intended
    ///to contain all IUntaggedTextSourceTest Unit Tests
    /// </summary>
    public class IRawTextSourceTest
    {
        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            var text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            var expected = text;
            string actual;
            actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public void LoadTextAsyncTest()
        {
            var text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            var expected = text;
            string actual;
            actual = target.LoadTextAsync().Result;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [Fact]
        public void NameTest()
        {
            var text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");

            string actual;
            var expected = "test fragment";
            actual = target.Name;
            Check.That(actual).IsEqualTo(expected);
        }

        internal virtual IRawTextSource CreateIRawTextSource()
        {
            IRawTextSource target = new RawTextFragment(lines, "test fragment");
            return target;
        }

        private static readonly string[] lines = { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };
    }
}
