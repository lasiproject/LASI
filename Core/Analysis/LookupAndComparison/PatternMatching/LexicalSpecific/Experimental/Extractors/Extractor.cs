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
    abstract class Extractor<TLexical, TResult> where TLexical : class, ILexical
    {

        internal Extractor(TLexical value) { this.Value = value; ResultValue = default(TResult); }

        public TLexical Value { get; private set; }
        public virtual TResult Result() { return ResultValue; }
        public virtual TResult Result(TResult defaultValue) {
            return defaultValue;
        }
        public virtual TResult Result(Func<TResult> defaultValueFactory) {
            return defaultValueFactory();
        }
        public virtual TResult Result(Func<TLexical, TResult> defaultValueFactory) {
            return (Value != null) ? defaultValueFactory(Value) : ResultValue;
        }
        public Extractor<TLexical, TResult> With<TPattern>(TResult result) where TPattern : class, ILexical {
            if (!Accepted) { ResultValue = result; }
            return this;
        }

        public virtual Extractor<TLexical, TResult> With<TPattern>(Func<TPattern, TResult> func) where TPattern : class, ILexical {
            return this.With<TPattern>((v, s) => func(v));
        }
        public virtual Extractor<TLexical, TResult> With<TPattern>(Func<TPattern, string, TResult> func) where TPattern : class, ILexical {
            if (!Accepted) {
                var matched = Value as TPattern;
                if (matched != null) {
                    Accepted = true;
                    ResultValue = func(matched, matched.Text);
                }
            }
            return this;
        }
        protected bool Accepted { get; set; }

        protected TResult ResultValue { get; set; }
    }
    class WordExtractor<TWord, TResult> : Extractor<TWord, TResult> where TWord : Word
    {
        internal WordExtractor(TWord value)
            : base(value) { }



    }
    class PhraseExtractor<TPhrase, TResult> : Extractor<TPhrase, TResult> where TPhrase : Phrase
    {
        internal PhraseExtractor(TPhrase value)
            : base(value) { }
        public virtual PhraseExtractor<TPhrase, TResult> With<TPattern>(Func<TPattern, string, IEnumerable<Word>, TResult> func) where TPattern : class,ILexical {
            if (!Accepted) {
                var matched = Value as TPattern;
                if (matched != null) {
                    Accepted = true;
                    ResultValue = func(matched, Value.Text, Value.Words);
                }
            }
            return this;
        }

    }
    class ClauseExtractor<TClause, TResult> : Extractor<TClause, TResult> where TClause : Clause
    {
        internal ClauseExtractor(TClause value)
            : base(value) { }
        public virtual ClauseExtractor<TClause, TResult> With<TPattern>(Func<TPattern, string, IEnumerable<Phrase>, TResult> func) where TPattern : class,ILexical {
            throw new NotImplementedException();
        }

    }
}
