using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for ITaggedTextSourceTest and is intended
    ///to contain all ITaggedTextSourceTest Unit Tests
    /// </summary>
    [TestClass]
    public class ITaggedTextSourceTest
    {
        /// <summary>
        ///A test for GetTextAsync
        /// </summary>
        [TestMethod]
        public void GetTextAsyncTest()
        {
            ITaggedTextSource target = CreateITaggedTextSource();
            string expected = ExpectedText;
            string actual;
            actual = target.GetTextAsync().Result;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetText
        /// </summary>
        [TestMethod]
        public void GetTextTest()
        {
            ITaggedTextSource target = CreateITaggedTextSource();
            string expected = ExpectedText;
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            ITaggedTextSource target = CreateITaggedTextSource();
            string actual;
            actual = target.Name;
            Assert.AreEqual("test fragment", actual);
        }

        internal virtual ITaggedTextSource CreateITaggedTextSource()
        {
            // TODO: Instantiate an appropriate concrete class.
            ITaggedTextSource target = new TaggedTextFragment(Tagger.TaggedFromRaw(new[] {
                "John enjoyed, with his usual lack of humility, consuming the object in question.",
                "Some may call him a heathen, but they are mistaken.",
                "Heathens are far less dangerous than he." }),
                "test fragment");
            return target;
        }


        private static Tagging.Tagger Tagger => new Tagging.Tagger();

        private static readonly string ExpectedText = Tagger.TaggedFromRaw(new[] {
                "John enjoyed, with his usual lack of humility, consuming the object in question.",
                "Some may call him a heathen, but they are mistaken.",
                "Heathens are far less dangerous than he." });

    }
}