
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LASI.Algorithm
{
    public class Clause
    {
        public Clause(params Phrase[] phrases) {
            Phrases = phrases;
        }
        public Clause(IEnumerable<Phrase> phrases) {
            Phrases = phrases;
        }

        public Clause(IEnumerable<Word> words) {
            Phrases = new List<Phrase>(new[] { new UndeterminedPhrase(words) });
        }
        public IEnumerable<Phrase> Phrases {
            get;
            protected set;
        }

        public string Text {
            get {
                return Phrases.Aggregate(" ", (txt, phrase) => txt + phrase.Text) + "CLAUSE Punc";
            }
        }
        internal void EstablishParenthood(Sentence sentence) {
            ParentDoc = sentence.ParentDoc;
            foreach (var P in Phrases)
                P.EstablishParent(this);
        }

        public Document ParentDoc {
            get;
            set;
        }
    }
}