using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Weighting;
using System.Xml.Linq;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, properties, and behaviors for all word level gramatical constructs.
    /// </summary>
    public abstract class Word : IPrepositionLinkable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Word class which represensts a the properties
        /// and behaviors of a w-level grammatical element.
        /// </summary>
        /// <param name="text">The literal text content of the w.</param>
        protected Word(string text) {
            ID = IDProvider++;
            Text = text;
            Weights = new Dictionary<Weighting.WeightKind, Weighting.Weight>();
        }


        #endregion

        #region Methods

        public void EstablishParent(Phrase phrase) {
            ParentPhrase = phrase;
        }

        /// <summary>
        /// Returns a string representation of the Word.
        /// </summary>
        /// <returns>a string containing its underlying type and its text content.</returns>
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
        /// Gets the document instance to which the w belongs.
        /// </summary>
        public Document ParentDocument {
            get {
                return ParentSentence.ParentDocument;
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
        public Sentence ParentSentence {
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


        public Dictionary<WeightKind, Weight> Weights {
            get;
            private set;
        }

        #endregion

        #region Static Members

        private static int IDProvider;

        #endregion



    }
}
