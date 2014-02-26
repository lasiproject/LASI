using LASI.Core.Heuristics;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core
{
    /// <summary>
    /// <para> This class is Experimental! It represents a PronounPhrase. A PronounPhrase is a custom Phrase type introduced by LASI. </para>
    /// <para> It it corresponds to NounPhrases which either contain almost exclusively Pronoun Words or which can be contextually  </para>
    /// determined to refer to some previously mentioned entity within a constrained scope.
    /// </summary>
    public class PronounPhrase : NounPhrase, IReferencer
    {
        /// <summary>
        /// Initializes a new instance of the PronounPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PronounPhrase.</param>
        public PronounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
            if (composedWords.OfPronoun().Any(p => p.ReferredTo != null)) {
                refersTo = new AggregateEntity(composedWords.OfPronoun().Select(p => p.ReferredTo));
            }
        }

        /// <summary>
        /// Returns a string representation of the PronounPhrase
        /// </summary>
        /// <returns>A string representation of the PronounPhrase</returns>
        public override string ToString() {
            var result = base.ToString() + (ReferredTo != null && ReferredTo.Any() ? " referring to -> " + ReferredTo.Text : string.Empty);
            result += (AliasLookup.GetDefinedAliases(ReferredTo ?? this as IEntity).Any() ? "\nClassified as: " + AliasLookup.GetDefinedAliases(ReferredTo ?? this as IEntity).Format() : string.Empty);
            return result;
        }
        private IAggregateEntity refersTo;
        /// <summary>
        /// Gets the Entity which the IPronoun references.
        /// </summary>
        public IAggregateEntity ReferredTo {
            get {
                refersTo = refersTo ?? new AggregateEntity(Words.OfPronoun().Where(p => p.ReferredTo != null).Select(p => p.ReferredTo));
                return refersTo;
            }

        }

        /// <summary>
        /// Binds the PronounPhrase to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (refersTo == null) {
                refersTo = new AggregateEntity(new[] { target });
            } else {
                refersTo = new AggregateEntity(refersTo.Append(target));
            }
            EntityKind = refersTo.EntityKind;
        }




    }
}
