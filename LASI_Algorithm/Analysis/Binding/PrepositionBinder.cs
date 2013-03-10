using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binding
{
    public class PrepositionBinder
    {
        public PrepositionBinder() {
        }
        public void Bind(Sentence sentence) {
            foreach (var p in sentence.Phrases.GetPrepositionalPhrases()) {
                p.OnLeftSide = p.PreviousPhrase;
                p.OnRightSide = p.NextPhrase;
            }
        }
    }
}
