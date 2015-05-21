using System;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.BinderImplementations
{
    public class DeclarativePronounBinder
    {
        public DeclarativePronounBinder(Paragraph paragraph)
        {
            this.Paragraph = paragraph;
            var results = Bind(Paragraph);
        }

        private object Bind(Paragraph paragraph)
        {
            var match = paragraph.Sentences
                .WithIndex()
                .SelectMany((s, i) => s.Element
                    .Phrases
                    .Select((p, pi) => new { Phrase = p, Rank = s.Index + i + pi }))
                    .OrderBy(e => e.Rank)
                    .Select(e => e.Phrase);
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the paragraph the binder operates over.
        /// </summary>
        public Paragraph Paragraph { get; }
    }
}
