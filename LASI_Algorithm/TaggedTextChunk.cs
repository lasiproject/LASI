using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public sealed class TaggedTextChunk : ITaggedTextSource
    {

        public TaggedTextChunk(string text, string name) { DataName = name; taggedText = text; }
        public TaggedTextChunk(IEnumerable<string> lines, string name) { DataName = name; taggedText = string.Join("\n", taggedText); }
        public string DataName { get; private set; }
        private string taggedText;

        public string GetText() {
            return taggedText;
        }

        public async Task<string> GetTextAsync() {
            return await Task.Run(() => taggedText).ContinueWith(t => t.Result, TaskScheduler.FromCurrentSynchronizationContext());
        }



    }
}
