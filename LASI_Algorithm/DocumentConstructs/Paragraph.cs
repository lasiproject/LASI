using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public sealed class Paragraph
    {

        public Paragraph(params  Sentence[] sentences) {
            Sentences = sentences;
            foreach (var S in Sentences)
                S.EstablishParenthood(this);

            ID = IDNumProvider;
            ++IDNumProvider;
        }
        /// <summary>
        /// Initializes a new instance of the Paragraph class containing the given sentences and belonging to the given Document.
        /// </summary>
        /// <param name="parentDoc">The Document instance the paragraph belongs to.</param>
        /// <param name="sentences">The collection of sentences which comprise the paragraph.</param>
        public Paragraph(Document parentDoc, IEnumerable<Sentence> sentences) {
            Sentences = sentences;
            ParentDoc = parentDoc;
            ID = IDNumProvider;
            ++IDNumProvider;
        }
        /// <summary>
        /// Gets or sets the collection of sentences which comprise the paragraph.
        /// </summary>
        public IEnumerable<Sentence> Sentences {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the document the paragraph belongs to.
        /// </summary>
        public Document ParentDoc {
            get;
            set;
        }
        public override string ToString() {
            return base.ToString() + Text;
        }

        private static int IDNumProvider = 0;

        /// <summary>
        /// Returns the paragraphs unique identification number.
        /// </summary>
        public int ID {
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
