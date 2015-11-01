using LASI.Content;
using System;
using System.Threading.Tasks;
using Xunit;
using TestMethod = Xunit.FactAttribute;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for IUntaggedTextSourceTest and is intended
    ///to contain all IUntaggedTextSourceTest Unit Tests
    /// </summary>
    public class IRawTextSourceTest
    {
        internal virtual IRawTextSource CreateIRawTextSource()
        {
            IRawTextSource target = new RawTextFragment(lines, "test fragment");
            return target;
        }



        /// <summary>
        ///A test for LoadText
        /// </summary>
        [TestMethod]
        public void LoadTextTest()
        {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            string expected = text;
            string actual;
            actual = target.LoadText();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [TestMethod]
        public void LoadTextAsyncTest()
        {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            string expected = text;
            string actual;
            actual = target.LoadTextAsync().Result;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");

            string actual;
            string expected = "test fragment";
            actual = target.Name;
            Assert.Equal(expected, actual);

        }
        private static readonly string[] lines = { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };

    }
}
