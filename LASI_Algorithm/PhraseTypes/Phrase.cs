using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{

    /// <summary>
    /// Provides the base class, properties, and behaviors for all phrase level gramatical constructs.
    /// </summary>
    public abstract class Phrase : IEquatable<Phrase>, IPrepositionLinkable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Phrase class.
        /// </summary>
        /// <param name="composedWords">The one or more instances of the Word class of which the Phrase is composed.</param>
        public Phrase(IEnumerable<Word> composedWords) {
            Words = composedWords;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the ToString method to augment the string representation of Phrase to include the text of the words it is composed of.
        /// </summary>
        /// <returns>A string containing the type information of the instance as well as the textual representations of the words it is composed of.</returns>
        public override string ToString() {
            return base.ToString() + String.Format("\n{0}", Text);
        }

        public void EstablishParent(Clause clause) {
            foreach (var W in Words)
                W.EstablishParent(this);
        }

        public virtual bool Equals(Phrase other) {
            return this == other;
        }

        public abstract void DetermineHeadWord();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the concatenated text content of all of the words which compose the phrase.
        /// </summary>
        public virtual string Text {
            get {
                return Words.Aggregate("", (str, word) => str + " " + word.Text);
            }
        }
        /// <summary>
        /// Gets the collection of words which from which the phrase is composed.
        /// </summary>
        public virtual IEnumerable<Word> Words {
            get;
            protected set;
        }
        public IPrepositional LeftLinkedPrepositional {
            get;
            set;
        }
        public IPrepositional RightLinkedPrepositional {
            get;
            set;
        }

        public abstract Word HeadWord {
            get;
            protected set;
        }

        public Type Type {
            get {
                return this.GetType();
            }
        }


        #endregion

    }
}
