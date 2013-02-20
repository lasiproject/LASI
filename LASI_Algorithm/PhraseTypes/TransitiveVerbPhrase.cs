using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a transative verb phrase in its base tense, a VerbPhrase which can take a direct and an optional indirect object.
    /// </summary>
    public class TransitiveVerbPhrase : VerbPhrase, ITransitiveAction
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TransitiveVerbPhrase class.
        /// </summary>
        /// <param name="componentWords">The words which compose to form the TransitiveVerbPhrase.</param>
        /// <param name="tense">The Tense of the TransitiveVerbPhrase.</param>
        public TransitiveVerbPhrase(IEnumerable<Word> componentWords)
            : base(componentWords) {
        }

        #endregion

        #region Methods
        public virtual void BindToDirectObject(IActionObject verbObject) {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public virtual IEntity DirectObject {
            get;
            set;
        }


        public virtual IEntity IndirectObject {
            get;
            set;
        }

        #endregion

        #region Static Members

        /// <summary>
        /// Promotes an instance of an intransitive VerbPhrase to an instance of TransitiveVerbPhrase.
        /// The transformation is total and irreversible, as reference is reassigned and the original VerbPhrase deleted.
        /// </summary>
        /// <param name="verbPhrase">A reference to a presumably intransitive VerbPhrase.</param>
        /// <remarks>The argument must be explicitely Passed By Reference via the ref keyword.</remarks>
        /// <returns>A reference to the newly constructed TransitiveVerb or, if the given VerbPhrase is already transitive, a reference to it as a TransitiveVerbPhrase.</returns>
        public static TransitiveVerbPhrase PromoteIntransitive(ref VerbPhrase verbPhrase) {
            var toTransitive = verbPhrase as TransitiveVerbPhrase;
            if (toTransitive != null)
                return toTransitive;
            else {
                var modiffiers = verbPhrase.Modifiers;
                verbPhrase = new TransitiveVerbPhrase(verbPhrase.Words) {
                    BoundSubject = verbPhrase.BoundSubject,
                    LeftLinkedPrepositional = verbPhrase.LeftLinkedPrepositional,
                    RightLinkedPrepositional = verbPhrase.RightLinkedPrepositional,
                    Modality = verbPhrase.Modality,
                    ObjectViaPreposition = verbPhrase.ObjectViaPreposition,
                    Tense = verbPhrase.Tense
                };
                foreach (var mod in modiffiers) {
                    verbPhrase.ModifyWith(mod);
                }
                return verbPhrase as TransitiveVerbPhrase;
            }
        }

        #endregion
    }
}

