using NFluent;
using Xunit;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for ITaggedTextSourceTest and is intended
    ///to contain all ITaggedTextSourceTest Unit Tests
    /// </summary>
    public class ITaggedTextSourceTest
    {
        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public void GetTextAsyncTest()
        {
            var target = CreateITaggedTextSource();
            var expected = ExpectedText;
            string actual;
            actual = target.LoadTextAsync().Result;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void GetTextTest()
        {
            var target = CreateITaggedTextSource();
            var expected = ExpectedText;
            string actual;
            actual = target.LoadText();
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [Fact]
        public void NameTest()
        {
            var target = CreateITaggedTextSource();
            Check.That(target.Name).IsEqualTo("test fragment");
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