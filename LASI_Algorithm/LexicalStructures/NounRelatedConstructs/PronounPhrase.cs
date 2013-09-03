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
                _refersTo = new AggregateEntity(composedWords.GetPronouns().Select(p => p.RefersTo));
            }
        }

        /// <summary>
        /// Returns a string representation of the PronounPhrase
        /// </summary>
        /// <returns>A string representation of the PronounPhrase</returns>
        public override string ToString() {
            return base.ToString() + (RefersTo != null && RefersTo.Any() ? " referring to -> " + RefersTo.Text : string.Empty);
        }
        private IAggregateEntity _refersTo;
        /// <summary>
        /// Gets the Entity which the IPronoun references.
        /// </summary>
        public IAggregateEntity RefersTo {
            get {
                _refersTo = _refersTo ?? new AggregateEntity(Words.GetPronouns().Where(p => p.RefersTo != null).Select(p => p.RefersTo));
                return _refersTo;
            }

        }

        /// <summary>
        /// Binds the PronounPhrase to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (_refersTo == null) {
                _refersTo = new AggregateEntity(new[] { target });
            } else {
                _refersTo = new AggregateEntity(_refersTo.EnumerateRecursively().Append(target));
            }
            EntityKind = _refersTo.EntityKind;
        }




    }
}
