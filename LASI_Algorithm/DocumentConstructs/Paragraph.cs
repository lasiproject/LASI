using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm
{
    public sealed class Paragraph
    {
        private ParagraphKind paragraphKind;

        public ParagraphKind ParagraphKind {
            get {
                return paragraphKind;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class containing the given sentences and belonging to the given ParentDocument.
        /// </summary>
        /// <param name="sentences">The collection of sentences which comprise the Paragraph.</param>
        ///<param name="paraKind">Argument indicating the kind of paragraph.</param>
        public Paragraph(IEnumerable<Sentence> sentences, ParagraphKind paraKind = ParagraphKind.Default) {
            paragraphKind = paraKind;
            Sentences = sentences;
            ID = IDNumProvider;
            ++IDNumProvider;
        }



        /// <summary>
        /// Establish the nested links between the g, its parent document, and the sentencies comprising it.
        /// </summary>
        /// <param name="parentDoc">The document instance to identified as the g'd parent.</param>
        public void EstablishParent(Document parentDoc) {
            ParentDocument = parentDoc;
            foreach (var S in Sentences)
                S.EstablishParenthood(this);
        }
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase phrase) {
            return Phrases.SkipWhile(r => r != phrase).Skip(1);
        }
        /// <summary>
        /// Gets or sets the collection of sentences which comprise the g.
        /// </summary>
        public IEnumerable<Sentence> Sentences {
            get;
            set;
        }

        public IEnumerable<Word> Words {
            get {
                return from S in Sentences
                       from W in S.Words
                       select W;
            }
        }
        public IEnumerable<Phrase> Phrases {
            get {
                return from S in Sentences
                       from R in S.Phrases
                       select R;
            }
        }

        /// <summary>
        /// Gets or sets the document the g belongs to.
        /// </summary>
        public Document ParentDocument {
            get;
            private set;
        }
        public override string ToString() {
            return base.ToString() + " " + Sentences.Count() + " sentences\n\"" + Text + "\"";
        }

        private static int IDNumProvider;

        /// <summary>
        /// Returns the document-unique identification number of the Paragraph.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        /// <summary>
        /// Gets the textual content of the g
        /// </summary>
        public string Text {
            get {
                return Sentences.Aggregate("", (str, sent) => str + " " + sent.Text).Trim();
            }
        }
    }
}

/*
    Paragraph p1(list of sentences[]);
    int id = p1.ID;
*/
namespace LASI.Algorithm.DocumentConstructs
{
    public enum ParagraphKind
    {
        Default,
        EnumerationContent,
        Heading
    }
}
