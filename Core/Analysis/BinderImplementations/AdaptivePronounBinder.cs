using LASI.Core.Heuristics;
using System;
using LASI.Core.PatternMatching;
using System.Collections.Generic;
using System.Linq;
namespace LASI.Core.Binding.Experimental
{
    /// <summary>
    /// An experimental class which uses a variety of binding techniques to infer the likely clause structure of a set of contiguous lexical elements.
    /// </summary>
    public static class AdaptivePronounBinder
    {
        /// <summary>
        /// Binds and identifies Clauses over the provided set of Words. Assumes that the Words supplied begin a sentence (or possibly follow a semicolon).
        /// </summary>
        /// <param name="words">The set of Words to bind over.</param>
        public static void Bind(IEnumerable<Word> words) {
            var splitPoints =
                from e in words.Select((w, i) => new { Word = w, Index = i })
                where e.Word is Preposition || e.Word is Punctuator || e.Word is Conjunction
                select e.Index;
            if (splitPoints.Any()) {
                var branches = splitPoints.Count() == 1 ? Enumerable.Repeat(words, 1) : splitPoints
                    .Skip(1)
                    .Select(splitter => words
                        .Take(splitPoints.First())
                        .Concat(words.Skip(splitter)));
                // for now, we will take the most fruitful branch. That is, the branch which produces the most actions 
                var actionsByBranch = from branch in branches
                                          //.AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                      let actions = GetBranchActions(branch)
                                      where actions.Any()
                                      orderby actions.Count() descending
                                      select actions;
                foreach (var action in actionsByBranch.DefaultIfEmpty(Enumerable.Empty<Action>()).First()) {
                    action();
                }
            }
        }
        private static IEnumerable<Action> GetBranchActions(IEnumerable<Word> words) {
            var wordList = words.ToList();
            var actions =
                from noun in
                    from noun in wordList.OfNoun()
                    where noun.Phrase is IEntity
                    select new {
                Noun = noun,
                Key = noun.Match().Yield<char>()
                              .With((ProperSingularNoun proper) => proper.IsGenderEquivalentTo(proper.Phrase as IEntity) ?
                                  proper.Gender.IsFemale() ? 'F' : proper.Gender.IsMale() ? 'M' : 'S' : 'A')
                              .With((CommonSingularNoun s) => 'S')
                              .With((IQuantifiable q) => 'P')
                            .Result('U')
                    }
                join pronoun in
                    from pronoun in words.OfPronoun()
                    select new {
                Pronoun = pronoun,
                Key = pronoun.IsFemale() ? 'F' : pronoun.IsMale() ? 'M' : pronoun.IsPlural() ? 'P' : pronoun.IsGenderAmbiguous() ? 'A' : !pronoun.IsPlural() ? 'S' : 'U'
                    }
                on noun.Key equals pronoun.Key
                where wordList.IndexOf(noun.Noun) < wordList.IndexOf(pronoun.Pronoun)//Only those Nouns which precede the Pronoun are considered binding candidates.
                group noun.Noun by pronoun.Pronoun into byPronoun
                select new Action(() => byPronoun.Key.BindAsReferringTo(byPronoun.First()));
            return actions;
        }
    }
}
