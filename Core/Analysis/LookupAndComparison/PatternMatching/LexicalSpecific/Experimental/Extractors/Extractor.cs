using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI;
using LASI.Core;
using LASI.Utilities;

namespace LASI.Core.Analysis.LookupAndComparison.PatternMatching.LexicalSpecific.Experimental.Extractors
{
    class Extractor<T> where T : class, ILexical
    {
        internal Extractor(T value) { this.Value = value; }

        public T Value { get; private set; }
    }
    class WordExtractor<TR> : Extractor<Word>
    {
        internal WordExtractor(Word value)
            : base(value) { }
        public WordExtractor<TR> Match<TP>(Func<TP, String, TR> exec) where TP : class, ILexical {


            throw new NotImplementedException();
        }

    }
    class PhraseExtractor<TR> : Extractor<Phrase>
    {
        internal PhraseExtractor(Phrase value)
            : base(value) { }

        public PhraseExtractor<TR> Match<TP>(Func<TP, String, IEnumerable<Word>, TR> exec) where TP : class, ILexical {
            throw new NotImplementedException();
        }

    }
    class ClauseExtractor<TR> : Extractor<Clause>
    {
        internal ClauseExtractor(Clause value)
            : base(value) { }

        public ClauseExtractor<TR> Match<TP>(Func<TP, String, IEnumerable<Phrase>, TR> exec) where TP : class, ILexical {
            throw new NotImplementedException();
        }

    }
}
