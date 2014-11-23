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
    /// <para> It corresponds to NounPhrases which either contain almost exclusively Pronoun Words or which can be contextually  </para>
    /// determined to refer to some previously mentioned entity within a constrained scope.
    /// </summary>
    public class PronounPhrase : NounPhrase, IReferencer
    {
        /// <summary>
        /// Initializes a new instance of the PronounPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the PronounPhrase.</param>
        public PronounPhrase(IEnumerable<Word> words)
            : base(words) {
            referredTo = new AggregateEntity(words.OfType<IReferencer>());
        }
        /// <summary>
        /// Initializes a new instance of the PronounPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the PronounPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the PronounPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of PronounPhrases. 
        /// Thus, its purpose is to simplify test code.</remarks>
        public PronounPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
        /// <summary>
        /// Returns a string representation of the PronounPhrase
        /// </summary>
        /// <returns>A string representation of the PronounPhrase</returns>
        public override string ToString() {
            var result = base.ToString() + (RefersTo != null && RefersTo.Any() ? " referring to -> " + RefersTo.Text : string.Empty);
            result += AliasLookup.GetDefinedAliases(RefersTo ?? this as IEntity).Any() ? "\nClassified as: " + AliasLookup.GetDefinedAliases(RefersTo ?? this as IEntity).Format() : string.Empty;
            return result;
        }
        private IAggregateEntity referredTo;
        /// <summary>
        /// Gets the Entity which the IPronoun references.
        /// </summary>
        public IAggregateEntity RefersTo => referredTo ?? new AggregateEntity(Words.OfPronoun().Where(p => p.RefersTo != null).Select(p => p.RefersTo));


        /// <summary>
        /// Binds the PronounPhrase to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        public void BindAsReferringTo(IEntity target) {
            if (referredTo == null) {
                referredTo = new AggregateEntity(new[] { target });
            } else {
                referredTo = new AggregateEntity(referredTo.Append(target));
            }
            EntityKind = referredTo.EntityKind;
        }
    }
}
