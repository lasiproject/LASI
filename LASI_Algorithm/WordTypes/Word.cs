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

    // made Word class a normal class instead of an abstract class
    public abstract class Word : IPrepositionLinkable, IEquatable<Word>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Word class which represensts a the properties
        /// and behaviors of a word-level grammatical element.
        /// </summary>
        /// <param name="text">The literal text content of the word.</param>
        protected Word(string text) {
            GUID = GUIDProvider++;
            Text = text;
            Weights = new Dictionary<Weighting.WeightKind, Weighting.Weight> {
                { Weighting.WeightKind.Individual, null },
                { Weighting.WeightKind.Meta, null }
            };
        }


        #endregion

        #region Methods

        public void EstablishParent(Phrase phrase) {
            ParentPhrase = phrase;
            ParentDocument = phrase.ParentDocument;
            ID = ParentDocument.Words.ToList().IndexOf(this);
        }

        /// <summary>
        /// Returns a string representation of the Word.
        /// </summary>
        /// <returns>A string containing its underlying type and its text content.</returns>
        public override string ToString() {
            return GetType().Name + " \"" + Text + "\"";
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public abstract System.Xml.Linq.XElement Serialize();


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
        /// Gets the document-unique identification number associated with the Word instance.
        /// </summary>
        public int ID {
            get;
            private set;
        }
        /// <summary>
        /// Gets the globally-unique identification number associated with the Word instance.
        /// </summary>
        public int GUID {
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
        /// Gets or sets the Phrase the Word belongs to.
        /// </summary>
        public Phrase ParentPhrase {
            get;
            set;
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

        #endregion

        #region Static Members

        private static int GUIDProvider;

        #endregion

        #region Operators

        ///// <summary>
        ///// Overlaods the equality comparison operator such that two words compare equal if and only if they have the same text content and 
        ///// are instances of the same Word subtype.
        ///// </summary>
        ///// <param name="A">The word on the left-hand-side of the operator</param>
        ///// <param name="B">The word on the right-hand-side of the operator</param>
        ///// <returns>A boolean value indicating the result of the comparison</returns>
        //public static bool operator ==(Word A, Word B) {

        //    if (A as object == null || B as object == null) {
        //        var bothNull = A as Object == null && B as Object == null;
        //        return bothNull;
        //    }
        //    return A.Text == B.Text && A.GetType() == B.GetType();
        //}
        ///// <summary>
        ///// Overlaods the inequality comparison operator such that two words compare not equal unless they have the same text content and 
        ///// are instances of the same Word subtype.
        ///// </summary>
        ///// <param name="A">The word on the left-hand-side of the operator</param>
        ///// <param name="B">The word on the right-hand-side of the operator</param>
        ///// <returns>A boolean value indicating the result of the comparison</returns>
        //public static bool operator !=(Word A, Word B) {
        //    return !(A == B);
        //}

        #endregion



        public Dictionary<Weighting.WeightKind, Weighting.Weight> Weights {
            get;
            private set;
        }
    }
}
