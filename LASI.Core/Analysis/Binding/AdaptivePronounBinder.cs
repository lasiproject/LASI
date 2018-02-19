using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Heuristics;
using LASI.Utilities;
using static System.Linq.Enumerable;

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
        public static void Bind(IEnumerable<Word> words)
        {
            var splitPoints =
                from e in words.WithIndices()
                where e.element is Preposition || e.element is Punctuator || e.element is Conjunction
                select e.index;

            if (splitPoints.Any())
            {
                var branches = splitPoints.Count() == 1 ? new[] { words } : splitPoints
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

                var branchActions = actionsByBranch.DefaultIfEmpty(Empty<Action>()).First();

                foreach (var action in branchActions)
                {
                    action();
                }
            }
        }
        private static IEnumerable<Action> GetBranchActions(IEnumerable<Word> words)
        {
            var wordList = words.ToList();
            return from noun in wordList.OfNoun()
                   where noun.Phrase is IEntity
                   from pronoun in words.OfPronoun()
                   where noun.IsGenderEquivalentTo(pronoun)
                   where wordList.IndexOf(noun) < wordList.IndexOf(pronoun)//Only those Nouns which precede the Pronoun are considered binding candidates.
                   group noun by pronoun into byPronoun
                   let pronoun = byPronoun.Key
                   let entity = byPronoun.First()
                   select (Action)(() => entity.BindReferencer(pronoun));
        }
    }
}
