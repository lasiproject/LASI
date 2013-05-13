using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, properties, and behaviors for all word level gramatical constructs.
    /// </summary>
    public abstract class Word : IPrepositionLinkable, ILexical
    {
        #region Constructors
        /// <summary>
        /// Initializes entity new instance of the Word class which represensts entity the properties
        /// and behaviors of entity verb-level grammatical element.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        protected Word(string text) {
            ID = IDProvider++;
            Text = text;
            Weight = 1;
            MetaWeight = 1;
            FrequencyCurrent = 0;
            FrequencyAcross = 0;
            Synonyms = -1;
        }


        #endregion

        #region Methods

        public void EstablishParent(Phrase phrase) {
            ParentPhrase = phrase;

        }

        /// <summary>
        /// Returns entity string representation of the Word.
        /// </summary>
        /// <returns>entity string containing its underlying type and its text content.</returns>
        public override string ToString() {
            return GetType().Name + " \"" + Text + "\"";
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }




        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text content of the Word instance.
        /// </summary>
        public virtual string Text {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the globally-unique identification number associated with the Word instance.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        /// <summary>
        /// Gets the frequency of the word in the current document.
        /// </summary>
        public int FrequencyCurrent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the normalized frequency of word across all documents.
        /// </summary>
        public int FrequencyAcross
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets the normalized frequency of word across all documents.
        /// </summary>
        public int Synonyms
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the document instance to which the verb belongs.
        /// </summary>
        public LASI.Algorithm.DocumentConstructs.Document ParentDocument {
            get {
                return ParentPhrase.ParentDocument;
            }
        }
        /// <summary>
        /// Gets, lexically speaking, the next Word in the ParentDocument to which the instance belongs.
        /// </summary>
        public Word NextWord {
            get;
            set;
        }
        /// <summary>
        /// Gets, lexically speaking, the previous Word in the ParentDocument to which the instance belongs.
        /// </summary>
        public Word PreviousWord {
            get;
            set;
        }
        /// <summary>
        /// Gets or the Phrase the Word belongs to.
        /// </summary>
        public Phrase ParentPhrase {
            get;
            private set;
        }
        /// <summary>
        /// Gets or the Sentence the Word belongs to.
        /// </summary>
        public LASI.Algorithm.DocumentConstructs.Sentence ParentSentence {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the left of the Word.
        /// </summary>
        public IPrepositional PrepositionOnLeft {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the right of the Word.
        /// </summary>
        public IPrepositional PrepositionOnRight {
            get;
            set;
        }

        public Type Type {
            get {
                return GetType();
            }
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the Word within the context of its parent document.
        /// </summary>
        public decimal Weight {
            get;
            set;
        }
    

        /// <summary>
        /// Gets or sets the numeric Weight of the Word over the context of all extant documents.
        /// </summary>
        public decimal MetaWeight {
            get;
            set;
        }

        #endregion

        #region Static Members

        private static int IDProvider;

        #endregion




    }
}
