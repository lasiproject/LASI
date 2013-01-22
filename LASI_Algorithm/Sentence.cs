using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class Sentence
    {
        public Sentence(IEnumerable<Phrase> phrases, Punctuator begin = null, Punctuator EOSText = null) {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunct = EOSText == null ?
            new Punctuator('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Word> words, Punctuator begin = null, Punctuator EOSText = null) {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunct = EOSText == null ?
            new Punctuator('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Clause> clauses) {
            Clauses = clauses;
            EndingPunct = clauses.Last().Phrases.Last().Words.Last(w => w is Punctuator) as Punctuator;
        }
        public Punctuator EndingPunct {
            get;
            protected set;
        }

        public IEnumerable<Clause> Clauses {
            get;
            protected set;
        }
        public IEnumerable<Phrase> Phrases {
            get {
                return from C in Clauses
                       from P in C.Phrases
                       select P;
            }
        }
        public IEnumerable<Word> Words {
            get {
                return from P in Phrases
                       from W in P.Words
                       select W;
            }
        }
        public string Text {
            get {
                return Clauses.Aggregate(" ", (txt, clause) => txt + clause.Text) + EndingPunct.Text;
            }
        }

        internal void EstablishParenthood(Paragraph paragraph) {
            OwnerParagraph = paragraph;
            ParentDoc = paragraph.ParentDoc;
            foreach (var C in Clauses)
                C.EstablishParenthood(this);
        }

        public Paragraph OwnerParagraph {
            get;
            set;
        }

        public Document ParentDoc {
            get;
            set;
        }
    }

}
