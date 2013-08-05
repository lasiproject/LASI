using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.LexicalLookup;
namespace LASI.Algorithm.Analysis.Binders.Experimental
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

        private IEnumerable<Action> ImagineBindings(IEnumerable<Word> branch) {
            var indexProvider = branch.ToList();
            var joined = from n in branch.GetNouns()
                         select n is ProperNoun ?
                         new
                         {
                             n, g = (n as ProperNoun).IsFemaleName() &&
                             !(n.Phrase as NounPhrase).IsFullMaleName() ? 'F' :
                             (n as ProperNoun).IsMaleName() &&
                             !(n.Phrase as NounPhrase).IsFullFemaleName() ? 'M' : 'A'
                         } :
                         new { n, g = 'U' }
                             into outer
                             join inner in
                                 from p in branch.GetPronouns() select new { p, g = p.PronounKind.IsFemale() ? 'F' : p.PronounKind.IsMale() ? 'M' : p.PronounKind.IsGenderAmbiguous() ? 'A' : 'U' }
                             on outer.g equals inner.g
                             where indexProvider.IndexOf(outer.n) < indexProvider.IndexOf(inner.p)
                             select new { outer.n, inner.p } into pair
                             group pair by pair.p into g
                             select g.First() into distinguished
                             select new Action(() => distinguished.p.BindAsReferringTo(distinguished.n));
            return joined;
        }
    }
}
