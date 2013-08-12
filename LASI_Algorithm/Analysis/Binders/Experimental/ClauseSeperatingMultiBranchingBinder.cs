using LASI.Algorithm.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm.Binding.Experimental
{
    /// <summary>
    /// An experimental class which uses a variety of binding techniques to infer the likely clause structure of a set of contiguous lexical elements.
    /// </summary>
    public class ClauseSeperatingMultiBranchingBinder
    {
        ///// <summary>
        ///// Binds and identifies Clauses over the provided set of elements. Assumes that the elements supplied begin a sentence (or possibly follow a semicolon).
        ///// </summary>
        ///// <param name="elements">The set of ILexical elements to bind over.</param>
        //public void Bind(IEnumerable<ILexical> elements) { Bind(elements.GetWords()); }

        /// <summary>
        /// Binds and identifies Clauses over the provided set of Words. Assumes that the Words supplied begin a sentence (or possibly follow a semicolon).
        /// </summary>
        /// <param name="elements">The set of Words to bind over.</param>
        public void Bind(IEnumerable<Word> elements) {
            var splitters = elements
                .Select((e, index) => new { Word = e as Preposition ?? e as Punctuation ?? e as Conjunction as Word, Location = index })
                .Where(r => r.Word != null)
                .Select(r => r.Location);
            if (splitters.Count() > 1) {
                var branches = from splitter in splitters.Skip(1)
                               let start = splitters.First()
                               select elements.Take(start).Concat(elements.Skip(splitter));
                var bestBranch = branches.Select(branch => ImagineBindings(branch)).OrderByDescending(b => b.Count()).First();
                foreach (var bindingAction in bestBranch) { bindingAction(); }
            }
        }

        private IEnumerable<Action> ImagineBindings(IEnumerable<Word> words) {
            return from noun in words.GetNouns()
                   let proper = noun as ProperNoun
                   let np = noun.Phrase as NounPhrase
                   let gen = proper != null && np != null ?
                   (proper.IsFemaleName() && !np.IsFullMaleName() ?
                   'F' : proper.IsMaleName() && !np.IsFullFemaleName() ?
                   'M' :
                   'A') : 'U'
                   let outer = new { noun, gen }
                   join inner in
                       from pronoun in words.GetPronouns()
                       let gen = pronoun.IsFemale() ?
                       'F' : pronoun.IsMale() ?
                       'M' : pronoun.IsGenderAmbiguous() ?
                       'A' : 'U'
                       select new { pronoun, gen }
                       on outer.gen equals inner.gen
                   let indexProvider = words.ToList()
                   where indexProvider.IndexOf(outer.noun) < indexProvider.IndexOf(inner.pronoun)
                   group new { outer.noun, inner.pronoun } by inner.pronoun into byPronoun
                   select new Action(() => byPronoun.Key.BindAsReferringTo(byPronoun.First().noun));
        }
    }
}
