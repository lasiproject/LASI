using LASI.Core.Heuristics;
using System;
using LASI.Core.Patternization;
using System.Collections.Generic;
using System.Linq;
namespace LASI.Core.Binding.Experimental
{
    /// <summary>
    /// An experimental class which uses a variety of binding techniques to infer the likely clause structure of a set of contiguous lexical elements.
    /// </summary>
    static class ClauseSeperatingBranchingBinder
    {
        /// <summary>
        /// Binds and identifies Clauses over the provided set of Words. Assumes that the Words supplied begin a sentence (or possibly follow a semicolon).
        /// </summary>
        /// <param name="elements">The set of Words to bind over.</param>
        public static void Bind(IEnumerable<Word> elements) {
            var splitters = elements
                .Select((e, index) => new { Word = e as Preposition ?? e as Punctuator ?? e as Conjunction as Word, Location = index })
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


        private static IEnumerable<Action> ImagineBindings(IEnumerable<Word> words) {
            return from noun in words.OfNoun()
                   let np = noun.Phrase as NounPhrase
                   let gen = np != null ?
                   (noun as Word)
                   .Match().Yield<char>()
                       .With<ProperSingularNoun>(n => n.IsGenderEquivalentTo(np) ? n.Gender.IsFemale() ? 'F' : n.Gender.IsMale() ? 'M' : 'S' : 'A')
                       .With<CommonSingularNoun>('S')
                       .With<IQuantifiable>('P')
                   .Result('U') : 'U'
                   let outer = new { noun, gen }
                   join inner in
                       from pro in words.OfPronoun()
                       let gen = pro.IsFemale() ? 'F' : pro.IsMale() ? 'M' : pro.IsPlural() ? 'P' : pro.IsGenderAmbiguous() ? 'A' : !pro.IsPlural() ? 'S' : 'U'
                       select new { pro, gen }
                   on outer.gen equals inner.gen
                   let indexProvider = words.ToList()
                   where indexProvider.IndexOf(outer.noun) < indexProvider.IndexOf(inner.pro)
                   group new { outer.noun, inner.pro } by inner.pro into byPronoun
                   select new Action(() => byPronoun.Key.BindAsReference(byPronoun.First().noun));
        }
    }
}
