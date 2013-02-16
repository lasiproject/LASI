using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, properties, and behaviors for all word level gramatical constructs.
    /// </summary>
    public abstract class Word : IEquatable<Word>, IPrepositionLinkable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Word class which represensts a the properties
        /// and behaviors of a word-level grammatical element.
        /// </summary>
        /// <param name="text">The literal text content of the word.</param>
        protected Word(string text) {
            this.Text = text;
            ID = IDNumProvider;
            ++IDNumProvider;
            // PreviousWord = ParentDocument.DocBuilder.LastBuilt;
            //ParentDocument.DocBuilder.Update(this);
        }
        #endregion

        #region Methods

        public void EstablishParent(Phrase phrase) {
            ParentPhrase = phrase;
        }

        /// <summary>
        /// Returns a string representation of the Word instance.
        /// </summary>
        /// <returns>The string representation of the Word instance.</returns>
        public override string ToString() {
            return base.ToString() + " " + Text;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }


        public bool Equals(Word other) {
            return this == other;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text content of the Word instance.
        /// </summary>
        public virtual string Text {
            get;
            set;
        }

        /// <summary>
        /// Gets the unique identification number associated with the Word instance.
        /// </summary>
        public int ID {
            get;
            private set;
        }
        /// <summary>
        /// Gets the document instance to which the word belongs.
        /// </summary>
        public Document ParentDocument {
            get;
            set;
        }
        /// <summary>
        /// Gets, lexically speaking, the next Word in the Document to which the instance belongs.
        /// </summary>
        public Word NextWord {
            get;
            set;
        }
        /// <summary>
        /// Gets, lexically speaking, the previous Word in the Document to which the instance belongs.
        /// </summary>
        public Word PreviousWord {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Phrase the Word belongs to.
        /// </summary>
        public Phrase ParentPhrase {
            get;
            set;
        }
        public IPrepositional LeftLinkedPrepositional {
            get;
            set;
        }

        public IPrepositional RightLinkedPrepositional {
            get;
            set;
        }

        public Type Type {
            get {
                return this.GetType();
            }
        }

        #endregion

        #region Static Members
        private static int IDNumProvider = 0;
        #endregion

        #region Operators

        public static bool operator ==(Word A, Word B) {

            if (A as object == null || B as object == null) {
                var bothNull = A as Object == null && B as Object == null;
                return bothNull;
            }
            return A.Text == B.Text;
        }
        public static bool operator !=(Word A, Word B) {
            return !(A == B);
        }

        #endregion

    }
}
