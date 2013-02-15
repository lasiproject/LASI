using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class Sentence
    {
        public Sentence(params Phrase[] phrases) {
            Clauses = new[] { new Clause(from P in phrases select P) };

        }
        public Sentence(IEnumerable<Phrase> phrases, SentenceDelimiter begin = null, SentenceDelimiter EOSText = null) {
            Clauses = new[] { new Clause(from P in phrases select P) };
            EndingPunct = EOSText == null ?
            new SentenceDelimiter('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Word> words, SentenceDelimiter begin = null, SentenceDelimiter EOSText = null) {
            Clauses = new[] { new Clause(from W in words select W) };
            EndingPunct = EOSText == null ?
            new SentenceDelimiter('.') :
            EOSText;
        }
        public Sentence(IEnumerable<Clause> clauses) {
            Clauses = clauses;
            //EndingPunct = clauses.Last().Phrases.Last().Words.Last(w => w is SentenceDelimiter) as SentenceDelimiter;
        }
        public SentenceDelimiter EndingPunct {
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
                return Phrases.Aggregate(" ", (txt, clause) => txt + clause.Text);
            }
        }

        internal void EstablishParenthood(Paragraph paragraph) {
            //  throw new NotImplementedException();
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

        public override string ToString()
        {
            return base.ToString() + Text + '\n';
        }


    }

}
