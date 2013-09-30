using LASI.Algorithm.LexicalLookup;
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
            if (splitters.Count() > 1) {
                var branches = splitters
                    .Skip(1)
                    .Select(splitter => elements.Take(splitters.First()).Concat(elements.Skip(splitter)));
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
            return from noun in words.GetNouns()
                   let np = noun.Phrase as NounPhrase
                   let gender = np != null ?
                   noun.Match().Yield<char>()
                       .Case<ProperSingularNoun>(n => n.IsFemaleFirstName() && !np.IsFullMale() ? 'F' :
                           n.IsMaleFirstName() && !np.IsFullFemale() ? 'M' : !n.IsFirstName() ? 's' : 'A')
                       .Case<CommonSingularNoun>('s')
                       .Case<ProperPluralNoun>('p')
                       .Case<CommonPluralNoun>('p')
                   .Result('U') : 'U'
                   let outer = new { noun, gender }
                   join inner in
                       from pro in words.GetPronouns()
                       let gender = pro.IsFemale() ? 'F' : pro.IsMale() ? 'M' : pro.IsGenderAmbiguous() ? 'A' : pro.IsPlural() ? 'p' : !pro.IsPlural() ? 's' : 'U'
                       select new { pro, gender }
                   on outer.gender equals inner.gender
                   let indexProvider = words.ToList()
                   where indexProvider.IndexOf(outer.noun) < indexProvider.IndexOf(inner.pro)
                   group new { outer.noun, inner.pro } by inner.pro into byPronoun
                   from p in byPronoun
                   select new Action(() => byPronoun.Key.BindAsReferringTo(byPronoun.First().noun));
        }
    }
}
