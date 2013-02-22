using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a transative verb in its base tense, a verb which can take a direct and an optional indirect object.
    /// </summary>
    public class TransitiveVerb : Verb, ITransitiveAction
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TransitiveVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public TransitiveVerb(string text)
            : base(text, VerbTense.Base) {
        }

        #endregion

        #region Properties

        public override void AttachObjectViaPreposition(IPrepositional prep) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the directobject of the TransitiveVerb.
        /// </summary>
        public virtual IEntity DirectObject {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the indirect object of the TransitiveVerb.
        /// </summary>
        public virtual IEntity IndirectObject {
            get;
            set;
        }
        #endregion

        #region Static Methods
        
        /// <summary>
        /// Promotes an instance of an intransitive Verb to an instance of TransitiveVerb.
        /// The transformation is total and irreversible, as reference is reassigned and the original Verb deleted.
        /// </summary>
        /// <param name="verb">A reference to a presumably intransitive Verb.</param>
        /// <remarks>The argument must be explicitely Passed By Reference via the ref keyword.</remarks>
        /// <returns>A reference to the newly constructed TransitiveVerb or, if the given Verb is already transitive, a reference to it as a TransitiveVerb.</returns>
        public static TransitiveVerb PromoteToTransitive(ref Verb verb) {
            //If the verb is already an instance of TransitiveVerb or one of its descendents, return it as is
            var toTransitive = verb as TransitiveVerb;
            if (toTransitive != null)
                return toTransitive;
            //Otherwise, assign verb to a new TransitiveVerb, copying over all of the common properties verbatim
            else
                verb = new TransitiveVerb(verb.Text) {
                    BoundSubject = verb.BoundSubject,
                    Modality = verb.Modality,
                    ID = verb.ID,
                    Modifiers = verb.Modifiers,
                    PrepositionOnRight = verb.PrepositionOnRight,
                    PrepositionOnLeft = verb.PrepositionOnLeft,
                    NextWord = verb.NextWord,
                    PreviousWord = verb.PreviousWord,
                    ParentPhrase = verb.ParentPhrase,
                    ParentDocument = verb.ParentDocument,
                    Tense = verb.Tense
                };
            return verb as TransitiveVerb;
        }

        #endregion

    }
}
