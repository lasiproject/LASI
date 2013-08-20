using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// This class is Experimental! It represents a PronounPhrase. A PronounPhrase is a custom Phrase type introduced by LASI.
    /// It it corresponds to NounPhrases which either contain almost exclusively Pronoun Words or which can be contextually 
    /// determined to refer to some previously mentioned entity within a constraned scope.
    /// </summary>
    public class PronounPhrase : NounPhrase, IPronoun
    {
        /// <summary>
        /// Initializes a new instance of the PronounPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PronounPhrase.</param>
        public PronounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
            if (composedWords.GetPronouns().Any(p => p.RefersTo != null)) {
                _boundEntity = composedWords.GetPronouns().Last().RefersTo;
            }
        }

        /// <summary>
        /// Returns a string representation of the PronounPhrase
        /// </summary>
        /// <returns>A string representation of the PronounPhrase</returns>
        public override string ToString() {
            return base.ToString() + (RefersTo != null ? " referring to -> " + RefersTo.Text : "");
        }
        private IAggregatedEntityCollection _boundEntity;
        /// <summary>
        /// Gets the Entity which the IPronoun references.
        /// </summary>
        public IAggregatedEntityCollection RefersTo {
            get {
                _boundEntity = _boundEntity ?? (Words.GetPronouns().Any(p => p.RefersTo != null) ? Words.GetPronouns().Last().RefersTo : null);
                return _boundEntity;
            }

        }

        /// <summary>
        /// Binds the PronounPhrase to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (_boundEntity != null || !_boundEntity.Any())//This condition seems wrong and must be investigated.
                _boundEntity = new AggregateEntity(new[] { target });
            else
                _boundEntity = new AggregateEntity(_boundEntity.Concat(new[] { target }));
            EntityKind = _boundEntity.EntityKind;
        }

        ///// <summary>
        ///// Extremely Experimental Method!
        ///// Attempts to "lift" an instance of NounPhrase, transforming it into a PronounPhrase.
        ///// This is meant to be used when a NounPhrase is later identified as having the contextual role of a Referencer to some other previously introduced entity.
        ///// </summary>
        ///// <param name="np">The NounPhrase instance to Transform into a PronounPhrase. This argument must be passed via the ref keyword.</param>
        ///// <returns>The NounPhrase "lifted" to a PronounPhrase.</returns>
        ///// <remarks>
        ///// This Method is considered extermely experimental because actually replaces the all references to the original NounPhrase
        ///// with references to the PronounPhrase created from it. This includes replacing references stored as bindings to the original in other objects, 
        ///// any element in a collection containing the original, and so on. The volitility comes from potential thread safety concerns.
        ///// </remarks>
        //public static PronounPhrase TransformNounPhraseToPronounPhrase(ref NounPhrase np) {
        //    np = np as PronounPhrase ?? //If it the argument is already a PronounPhrase instance, we do not want to do anything.
        //        new PronounPhrase(np.Words) { //Assign a new PronounPhrase to the NounPhrase ref reference, transferring all of the applicable state from the original NounPhrase
        //            SubjectOf = np.SubjectOf,
        //            DirectObjectOf = np.DirectObjectOf,
        //            IndirectObjectOf = np.IndirectObjectOf,
        //            ID = np.ID, //The ID provider will still be incremeneted, but the created PronounPhrase will have the same id as the original NounPhrase before it is assigned.
        //            EntityKind = np.EntityKind,
        //            Document = np.Document,
        //            Sentence = np.Sentence,
        //            BoundPronouns = np.BoundPronouns,
        //            InnerAttributive = np.InnerAttributive,
        //            OuterAttributive = np.OuterAttributive,
        //            Possesser = np.Possesser,
        //            Possessed = np.Possessed,
        //            PrepositionOnLeft = np.PrepositionOnLeft,
        //            PrepositionOnRight = np.PrepositionOnRight,
        //            Weight = np.Weight,
        //            MetaWeight = np.MetaWeight,
        //            Descriptors = np.Descriptors,
        //            PreviousPhrase = np.PreviousPhrase,
        //            NextPhrase = np.NextPhrase,
        //        };
        //    return np as PronounPhrase;//This is mainly convenience, as even if the return value is discarded, the transformation has irrevocably changed the state of the reference passed in.

        //}


    }
}
