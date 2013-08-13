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
                               select elements.Take(splitters.First()).Concat(elements.Skip(splitter));
                var bestBranch = branches
                    .Select(branch => ImagineBindings(branch))
                    .OrderByDescending(b => b.Count())
                    .First();
                foreach (var bindingAction in bestBranch) { bindingAction(); }
            }
        }

        private static IEnumerable<Action> ImagineBindings(IEnumerable<Word> words) {
            return from noun in words.GetNouns()
                   let pr = noun as ProperNoun
                   let np = noun.Phrase as NounPhrase
                   let gen = pr != null && np != null ? (pr.IsFemale() && !np.IsFullMale() ? 'F' : pr.IsMale() && !np.IsFullFemale() ? 'M' : 'A') : 'U'
                   let outer = new { noun, gen }
                   join inner in
                       from pro in words.GetPronouns()
                       let gen = pro.IsFemale() ? 'F' : pro.IsMale() ? 'M' : pro.IsGenderAmbiguous() ? 'A' : 'U'
                       select new { pro, gen }
                   on outer.gen equals inner.gen
                   let indexProvider = words.ToList()
                   where indexProvider.IndexOf(outer.noun) < indexProvider.IndexOf(inner.pro)
                   group new { outer.noun, inner.pro } by inner.pro into byPronoun
                   select new Action(() => byPronoun.Key.BindAsReferringTo(byPronoun.First().noun));
        }
    }
}
