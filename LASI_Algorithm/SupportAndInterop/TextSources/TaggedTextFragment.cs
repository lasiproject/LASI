using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
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
            TextSourceName = name;
        }
        /// <summary>
        /// Initializes a new instance of the TaggedTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="text">A sequence of strings containing tagged text.</param>
        /// <param name="name">The desired name of the TaggedTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public TaggedTextFragment(IEnumerable<string> text, string name) {
            taggedText = string.Join("\n", text);
            TextSourceName = name;
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
            return await Task.Run(() => taggedText).ContinueWith(t => t.Result, TaskScheduler.FromCurrentSynchronizationContext());
        }
        /// <summary>
        /// Gets the name associated with the TaggedTextFragment.
        /// </summary>
        public string TextSourceName { get; private set; }

        private string taggedText;

    }
}
