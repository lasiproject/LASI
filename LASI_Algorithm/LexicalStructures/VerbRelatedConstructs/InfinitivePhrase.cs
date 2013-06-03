﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    public class InfinitivePhrase : Phrase, LASI.Algorithm.IVerbalObject, LASI.Algorithm.IVerbalSubject
    {


        public InfinitivePhrase(IEnumerable<Word> composed)
            : base(composed)
        {


            //if (IntroducingVerbal == null)
            //    throw new InvalidInfinitePhraseConstructedException("An infinitive parent must start with a Verbial type");
        }
        public VerbPhrase IntroducingVerbal
        {
            get;
            private set;
        }

        public ITransitiveVerbal DirectObjectOf
        {
            get;
            set;
        }

        public ITransitiveVerbal IndirectObjectOf
        {
            get;
            set;
        }

        public ITransitiveVerbal SubjectOf
        {
            get;
            set;
        }
    }
}
