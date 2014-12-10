using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content
{
    /// <summary>
    /// Encapsulates a string containing Tagged Text and an associated name. Provides synchronous and asynchronous acess to it contents.
    /// </summary>
    public class TaggedTextFragment : ITaggedTextSource
    {

        /// <summary>
        /// Initializes a new instance of the TaggedTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="text">A string containing tagged text.</param>
        /// <param name="name">The desired name of the TaggedTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public TaggedTextFragment(string text, string name) {
            taggedText = text;
            SourceName = name;
        }
        /// <summary>
        /// Initializes a new instance of the TaggedTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="lines">A sequence of strings containing tagged text.</param>
        /// <param name="name">The desired name of the TaggedTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public TaggedTextFragment(IEnumerable<string> lines, string name) {
            taggedText = string.Join("\n", lines);
            SourceName = name;
        }

        /// <summary>
        /// Returns a single string containing all of the Tagged Text in the TaggedTextFragment.
        /// </summary>
        /// <returns>A single string containing all of the Tagged Text in the TaggedTextFragment.</returns>
        public string GetText() {
            return taggedText;
        }
        /// <summary>
        /// Returns a system.Threading.Task.Task which, when awaited, yields a single string containing all of the Tagged Text in the TaggedTextFragment.
        /// </summary>
        /// <returns>A System.Threading.Task.Task which, when awaited, yields a single string containing all of the Tagged Text in the TaggedTextFragment.</returns>
        public async Task<string> GetTextAsync() {
            await Task.Yield();
            return taggedText;
        }
        /// <summary>
        /// Gets the name associated with the TaggedTextFragment.
        /// </summary>
        public string SourceName { get; private set; }

        private string taggedText;

    }
}
