using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public sealed class Paragraph
    {

        public Paragraph(IEnumerable<Sentence> sentences) {
            Sentences = sentences;
            ID = IDNumProvider;
            ++IDNumProvider;
        }
        /// <summary>
        /// Initializes a new instance of the Paragraph class containing the given sentences and belonging to the given ParentDocument.
        /// </summary>
        /// <param name="parentDoc">The ParentDocument instance the paragraph belongs to.</param>
        /// <param name="sentences">The collection of sentences which comprise the paragraph.</param>
        public Paragraph(Document parentDoc, IEnumerable<Sentence> sentences) {
            Sentences = sentences;
            ParentDocument = parentDoc;
            ID = IDNumProvider++;
        }

        /// <summary>
        /// Establish the nested links between the paragraph, its parent document, and the sentencies comprising it.
        /// </summary>
        /// <param name="parentDoc">The document instance to identified as the paragraph's parent.</param>
        public void EstablishParent(Document parentDoc) {
            ParentDocument = parentDoc;
            foreach (var S in Sentences)
                S.EstablishParenthood(this);
        }

        /// <summary>
        /// Gets or sets the collection of sentences which comprise the paragraph.
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

        /// <summary>
        /// Gets or sets the document the paragraph belongs to.
        /// </summary>
        public Document ParentDocument {
            get;
            set;
        }
        public override string ToString() {
            return base.ToString() + " " + Sentences.Count() + " sentences\n" + Text;
        }

        private static int IDNumProvider;

        /// <summary>
        /// Returns the paragraphs unique identification number.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        public int GUI {
            get;
            private set;
        }

        public string Text {
            get {
                return Sentences.Aggregate("", (str, sent) => str + " " + sent.Text);
            }
        }
    }
}

/*
    Paragraph p1(list of sentences[]);
    int id = p1.ID;
*/