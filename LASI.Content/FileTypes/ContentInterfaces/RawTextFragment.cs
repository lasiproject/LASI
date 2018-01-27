﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content
{
    /// <summary>
    /// Encapsulates a string containing Raw, untagged text and an associated name. Provides synchronous and asynchronous acess to it contents.
    /// </summary>
    public class RawTextFragment : IRawTextSource
    {
        /// <summary>
        /// Initializes a new instance of the RawTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="name">The desired name of the RawTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        /// <param name="text">A string containing untagged text.</param>
        public RawTextFragment(string name, string text)
        {
            content = text;
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the RawTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="text">A sequence of strings containing untagged text.</param>
        /// <param name="name">The desired name of the RawTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public RawTextFragment(IEnumerable<string> text, string name)
        {
            content = string.Join("\n", text);
            Name = name;
        }
        /// <summary>
        /// Returns a single string containing all of the Raw Text in the RawTextFragment.
        /// </summary>
        /// <returns>A single string containing all of the Raw Text in the RawTextFragment.</returns>
        public string LoadText() => content;
        /// <summary>
        /// Returns a system.Threading.Task.Task which, when awaited, yields a single string containing all of the Raw Text in the RawTextFragment.
        /// </summary>
        /// <returns>A System.Threading.Task.Task which, when awaited, yields a single string containing all of the Raw Text in the RawTextFragment.</returns>
        public Task<string> LoadTextAsync() => Task.FromResult(content);

        /// <summary>
        /// The name associated with the RawTextFragment.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// implicitly converts the RawText into a string containing its content.
        /// </summary>
        /// <param name="fragment">The RawTextFragment to convert.</param>
        /// <returns>A string containing the content of the RawTextFragment.</returns>
        public static implicit operator string (RawTextFragment fragment) => fragment.LoadText();

        /// <summary>
        /// Loads the content and returns it as the string representation of <see cref="RawTextFragment"/>.
        /// </summary>
        /// <returns>A string representation of <see cref="RawTextFragment"/>.</returns>
        public override string ToString() => LoadText();
        private string content;
    }
}
