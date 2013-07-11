using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;


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
            if (composedWords.GetPronouns().Any(p => p.IsBound)) {
                _boundEntity = composedWords.GetPronouns().Last().BoundEntity;
                IsBound = true;
            }
        }

        /// <summary>
        /// Returns a string representation of the PronounPhrase
        /// </summary>
        /// <returns>A string representation of the PronounPhrase</returns>
        public override string ToString() {
            return base.ToString() + (BoundEntity != null ? " referring to -> " + BoundEntity.Text : "");
        }
        private IEntityGroup _boundEntity;
        public IEntityGroup BoundEntity {
            get {
                _boundEntity = _boundEntity ?? (Words.GetPronouns().Any(p => p.IsBound) ? Words.GetPronouns().Last().BoundEntity : null);
                IsBound = _boundEntity != null;
                return _boundEntity;
            }

        }

        /// <summary>
        /// Binds the PronounPhrase to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindToEntity(IEntity target) {
            if (_boundEntity != null || !_boundEntity.Any())
                _boundEntity = new EntityGroup(new[] { target });
            else
                _boundEntity = new EntityGroup(_boundEntity.Concat(new[] { target }));
            Kind = _boundEntity.Kind;
            IsBound = true;
        }

        /// <summary>
        /// Gets a value indicating if the PronounPhrase has been bound to an Entity.
        /// </summary>
        public bool IsBound {
            get;
            private set;
        }

        /// <summary>
        /// Extremely Experimental Method!
        /// Attempts to "lift" an instance of NounPhrase, transforming it into a PronounPhrase.
        /// This is meant to be used when a NounPhrase is later identified as having the contextual role of a Referencer to some other previously introduced entity.
        /// </summary>
        /// <param name="contextualPronoun">The NounPhrase instance to Transform into a PronounPhrase. This argument must be passed via the ref keyword.</param>
        /// <returns>The NounPhrase "lifted" to a PronounPhrase.</returns>
        /// <remarks>
        /// This Method is considered extermely experimental because actually replaces the all references to the original NounPhrase
        /// with references to the PronounPhrase created from it. This includes replacing references stored as bindings to the original in other objects, 
        /// any element in a collection containing the original, and so on. The volitility comes from potential thread safety concerns.
        /// </remarks>
        internal static PronounPhrase TransformNounPhraseToPronounPhrase(ref NounPhrase contextualPronoun) {
            contextualPronoun = new PronounPhrase(contextualPronoun.Words) {
                SubjectOf = contextualPronoun.SubjectOf,
                DirectObjectOf = contextualPronoun.DirectObjectOf,
                IndirectObjectOf = contextualPronoun.IndirectObjectOf,
                ID = contextualPronoun.ID,
                Kind = contextualPronoun.Kind,
                Document = contextualPronoun.Document,
                Sentence = contextualPronoun.Sentence,
                BoundPronouns = contextualPronoun.BoundPronouns,
                InnerAttributive = contextualPronoun.InnerAttributive,
                OuterAttributive = contextualPronoun.OuterAttributive,
                Possesser = contextualPronoun.Possesser,
                Possessed = contextualPronoun.Possessed,
                PrepositionOnLeft = contextualPronoun.PrepositionOnLeft,
                PrepositionOnRight = contextualPronoun.PrepositionOnRight,
                Weight = contextualPronoun.Weight,
                MetaWeight = contextualPronoun.MetaWeight,
                Descriptors = contextualPronoun.Descriptors,
                PreviousPhrase = contextualPronoun.PreviousPhrase,
                NextPhrase = contextualPronoun.NextPhrase,
                BoundNoun = contextualPronoun.BoundNoun,
                BoundNounPhrase = contextualPronoun.BoundNounPhrase,
            };
            return contextualPronoun as PronounPhrase;
        }


    }
}
