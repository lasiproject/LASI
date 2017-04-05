using LASI.Content;
using LASI.Content.Tagging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NFluent;
using Fact = Xunit.FactAttribute;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for TaggedTextFragmentTest and is intended
    ///to contain all TaggedTextFragmentTest Unit Tests
    /// </summary>
    public class TaggedTextFragmentTest
    {

        private Tagger Tagger => new Tagger();

        /// <summary>
        ///A test for TaggedTextFragment Constructor
        /// </summary>
        [Fact]
        public void TaggedTextFragmentConstructorTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            var name = "Test Fragment";
            var target = new TaggedTextFragment(lines, name);
            Check.That(target.Name).IsEqualTo(name);
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            var name = "Test Fragment";
            var target = new TaggedTextFragment(lines, name);
            var expected = string.Join("\n", lines);
            string actual;
            actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public async Task LoadTextAsyncTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            var name = "Test Fragment";
            var target = new TaggedTextFragment(lines, name);
            var expected = string.Join("\n", lines);
            var actual = await target.LoadTextAsync();
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
