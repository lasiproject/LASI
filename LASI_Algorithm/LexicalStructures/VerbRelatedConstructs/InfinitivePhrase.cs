using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Analysis.Binding
{
    class InfinitivePhrase : Phrase, LASI.Algorithm.FundamentalSyntacticInterfaces.IVerbialObject, LASI.Algorithm.FundamentalSyntacticInterfaces.IVerbalSubject
    {
        private IEnumerable<Phrase> componentPhrases;

        public InfinitivePhrase(IEnumerable<Phrase> phrases)
            : base(from r in phrases
                   from w in r.Words
                   select w) {
            componentPhrases = phrases;
            this.IntroducingVerbial = phrases.First() as VerbPhrase;
            if (IntroducingVerbial == null)
                throw new InvalidInfinitePhraseConstructedException("An infinitive phrase must start with a Verbial type");
        }
        public VerbPhrase IntroducingVerbial {
            get;
            private set;
        }

        public ITransitiveVerbal DirectObjectOf {
            get;
            set;
        }

        public ITransitiveVerbal IndirectObjectOf {
            get;
            set;
        }

        public ITransitiveVerbal SubjectOf {
            get;
            set;
        }
    }
}
