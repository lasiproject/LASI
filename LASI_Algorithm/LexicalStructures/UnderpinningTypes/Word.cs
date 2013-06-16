using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, properties, and behaviors for all wd level gramatical constructs.
    /// </summary>
    public abstract class Word : IPrepositionLinkable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the wd class which represensts the properties
        /// and behaviors of a wd-level grammatical element.
        /// </summary>
        /// <param name="text">The key text content of the adverb.</param>
        protected Word(string text)
        {
            ID = IDProvider++;
            Text = text;
            Weight = 1;
            MetaWeight = 1;
            FrequencyCurrent = 0;
            FrequencyAcross = 0;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Establishes the linkage between the wd and its parent Phrase.
        /// </summary>
        /// <param name="parent">The Phrase to which the wd belongs.</param>
        public void EstablishParent(Phrase parent)
        {
            Phrase = parent;

        }

        /// <summary>
        /// Returns a string representation of the wd.
        /// </summary>
        /// <returns>A string containing its underlying NounPointerSymbol and its text content.</returns>
        public override string ToString()
        {
            return GetType().Name + " \"" + Text + "\"";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }




        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text content of the wd instance.
        /// </summary>
        public virtual string Text
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the globally-unique identification number associated with the wd instance.
        /// </summary>
        public int ID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the frequency of the wd in the current document.
        /// </summary>
        public int FrequencyCurrent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the normalized frequency of wd across all documents.
        /// </summary>
        public int FrequencyAcross
        {
            get;
            set;
        }


        /// <summary>
        /// Gets the document instance to which the adverb belongs.
        /// </summary>
        public LASI.Algorithm.DocumentConstructs.Document Document
        {
            get
            {
                return Phrase.Document;
            }
        }
        /// <summary>
        /// Gets, lexically speaking, the next wd in the Document to which the instance belongs.
        /// </summary>
        public Word NextWord
        {
            get;
            set;
        }
        /// <summary>
        /// Gets, lexically speaking, the previous wd in the Document to which the instance belongs.
        /// </summary>
        public Word PreviousWord
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or the Phrase the wd belongs to.
        /// </summary>
        public Phrase Phrase
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or the Sentence the wd belongs to.
        /// </summary>
        public LASI.Algorithm.DocumentConstructs.Sentence Sentence
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the first of the wd.
        /// </summary>
        public IPrepositional PrepositionOnLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the second of the wd.
        /// </summary>
        public IPrepositional PrepositionOnRight
        {
            get;
            set;
        }

        public Type Type
        {
            get
            {
                return GetType();
            }
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the wd within the context of its parent document.
        /// </summary>
        public decimal Weight
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the numeric Weight of the wd over the context of all extant documents.
        /// </summary>
        public decimal MetaWeight
        {
            get;
            set;
        }

        #endregion

        #region Static Words

        private static int IDProvider;
        public static bool VerboseOutput
        {
            get;
            set;
        }

        static Word()
        {
            VerboseOutput = false;
        }
        #endregion




    }
}
