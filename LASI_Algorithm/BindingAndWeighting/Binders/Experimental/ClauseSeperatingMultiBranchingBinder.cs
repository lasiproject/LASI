using LASI.Algorithm.LexicalLookup;
using LASI.Utilities;
using System;
using LASI.Algorithm.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm.Binding.Experimental
{

    /// <summary>
    /// An experimental class which uses a variety of binding techniques to infer the likely clause structure of a set of contiguous lexical elements.
    /// </summary>
    public static class ClauseSeperatingMultiBranchingBinder
    {
        /// <summary>
        /// Binds and identifies Clauses over the provided set of Words. Assumes that the Words supplied begin a sentence (or possibly follow a semicolon).
        /// </summary>
        /// <param name="elements">The set of Words to bind over.</param>
        public static void Bind(IEnumerable<Word> elements)
        {
            var splitters = elements
                .Select((e, index) => new { Word = e as Preposition ?? e as Punctuation ?? e as Conjunction as Word, Location = index })
                .Where(r => r.Word != null)
                .Select(r => r.Location);
            if (splitters.Any()) {
                var branches = splitters.Count() > 1 ?
                    splitters.Skip(1)
                    .Select(splitter => elements.Take(splitters.First()).Concat(elements.Skip(splitter))) : Enumerable.Repeat(elements, 1);
                var bestBranch = branches // for now, we will consider the most fruitful branch to be the best. 
                    .Select(branch => ImagineBindings(branch))
                    .OrderByDescending(b => b.Count())
                    .First();
                // this should not be parallelized because the binding actions computed above cause, intentionally, stateful changes.
                foreach (var bindingAction in bestBranch) { bindingAction(); }
            }
        }

        private static IEnumerable<Action> ImagineBindings(IEnumerable<Word> words)
        {
            return from N in words.GetNouns()
                   let IndexOf = GetIndexComputer(words)
                   let np = N.Phrase as NounPhrase
                   let G = np == null ? 'U' : N.Match()
                   .Yield<char>()
                        .Case<ProperSingularNoun>(n => n.IsFemaleFirstName() && !np.IsFullMale() ? 'F' : n.IsMaleFirstName() && !np.IsFullFemale() ? 'M' : !n.IsFirstName() ? 's' : 'A')
                        .Case<CommonSingularNoun>('s')
                        .Case<ProperPluralNoun>('p')
                        .Case<CommonPluralNoun>('p')
                   .Result('U')
                   let outer = new { N, G }
                   join inner in
                       from P in words.GetPronouns()
                       select new { P, G = P.IsFemale() ? 'F' : P.IsMale() ? 'M' : P.IsGenderAmbiguous() ? 'A' : P.IsPlural() ? 'p' : !P.IsPlural() ? 's' : 'U' }
                       on outer.G equals inner.G
                   where IndexOf(outer.N) < IndexOf(inner.P)
                   group new { P = inner.P, N = outer.N } by inner.P into byPro
                   where byPro.Any()
                   let pair = byPro.First()
                   select new Action(() => pair.P.BindAsReference(pair.N));
        }

        private static Func<Word, int> GetIndexComputer(IEnumerable<Word> indexProvider)
        {
            var l = indexProvider.ToList();
            return w => l.IndexOf(w);
        }
    }
}
