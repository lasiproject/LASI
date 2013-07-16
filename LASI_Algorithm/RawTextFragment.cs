using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class RawTextFragment : IRawTextSource
    {
        private string taggedText;
        /// <summary>
        /// Initializes a new instance of the RawTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="text">A string containing untagged text.</param>
        /// <param name="name">The desired name of the RawTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public RawTextFragment(string text, string name) {
            taggedText = text;
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the RawTextFragment class containing the provided text and having the provided name.
        /// </summary>
        /// <param name="text">A sequence of strings containing untagged text.</param>
        /// <param name="name">The desired name of the RawTextFragment. This name does not have to be unique and serves no special purpose. It is simply provided for convenience.</param>
        public RawTextFragment(IEnumerable<string> text, string name) {
            taggedText = string.Join("\n", text);
            Name = name;
        }
        public string GetText() {
            throw new NotImplementedException();
        }

        public Task<string> GetTextAsync() {
            throw new NotImplementedException();
        }

        public string Name {
            get;
            private set;
        }
        public static implicit operator string(RawTextFragment fragment) {
            return fragment.GetText();
        }
    }
}
