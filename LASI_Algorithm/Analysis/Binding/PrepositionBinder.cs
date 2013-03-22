using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class ConjunctionBinder
    {
        public ConjunctionBinder() {
        }
        public void Bind(Sentence sentence) {
            foreach (var p in sentence.Phrases.GetPrepositionalPhrases()) {
                p.OnLeftSide = p.PreviousPhrase is ConjunctionPhrase ? p.PreviousPhrase.PreviousPhrase : p.PreviousPhrase;
                p.OnRightSide = p.NextPhrase is ConjunctionPhrase ? p.NextPhrase.NextPhrase : p.NextPhrase;
            }
        }
    }
}
