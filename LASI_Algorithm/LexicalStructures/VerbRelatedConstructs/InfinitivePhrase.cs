using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    public class InfinitivePhrase : VerbPhrase, LASI.Algorithm.IVerbalObject, LASI.Algorithm.IVerbalSubject
    {


        public InfinitivePhrase(IEnumerable<Word> composed)
            : base(composed)
        {
        }
        public VerbPhrase IntroducingVerbal
        {
            get;
            private set;
        }

        public IVerbal DirectObjectOf
        {
            get;
            set;
        }

        public IVerbal IndirectObjectOf
        {
            get;
            set;
        }

        public IVerbal SubjectOf
        {
            get;
            set;
        }
    }
}
