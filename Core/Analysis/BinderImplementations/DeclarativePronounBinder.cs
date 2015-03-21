﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;

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

        public Paragraph Paragraph { get; }
    }
}
