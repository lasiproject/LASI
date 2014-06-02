using LASI.Core.Heuristics;
using LASI.Core.Heuristics.Morphemization;
using LASI.Core.PatternMatching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities;
using System.Threading.Tasks;


namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Provides for registration of Entities as aliases for other Entities.
    /// </summary>
    public static class AliasLookup
    {
        /// <summary>
        /// Checks to see if one Entity is a known alias for another.
        /// </summary>
        /// <param name="possibleAlias">The first Entity</param>
        /// <param name="other">The second Entity</param>
        /// <returns>true if the Entities are aliases for one another, false otherwise</returns>
        public static bool IsAliasFor(this IEntity possibleAlias, IEntity other) {
            return possibleAlias != null && other != null && LookupAlias(other, possibleAlias);
        }

        private static bool LookupAlias(IEntity possibleAlias, IEntity possiblyAliasedBy) {
            ISet<IEntity> aliasedBy;
            ISet<string> aliases;
            return aliasedEntityReferenceMap.TryGetValue(possiblyAliasedBy, out aliasedBy) &&
                aliasedBy.Contains(possibleAlias) ||
                aliasDictionary.TryGetValue(possiblyAliasedBy.Text, out aliases) &&
                aliases.Contains(possibleAlias.Text);
        }
        /// <summary>
        /// Establishes that the given Entity has a the given textual alias. 
        /// </summary>
        /// <param name="entity">The Entity to register an alias for</param>
        /// <param name="textualAlias">The textual alias which will be registered</param>
        public static void DefineAlias(IEntity entity, string textualAlias) {
            DefineAliasInDictionary(entity.Text, textualAlias);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the end. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="other">The second Entity</param>
        public static void DefineAlias<TE>(TE entity, TE other) where TE : IEntity {
            DefineAliasInDictionary(entity.Text, other.Text);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the end. 
        /// </summary>
        /// <param name="entityText">The first Entity</param>
        /// <param name="aliasText">The second Entity</param>
        public static void DefineAlias(string entityText, string aliasText) {
            DefineAliasInDictionary(entityText, aliasText);
        }

        /// <summary>
        /// Adds or updates the known alias relationships to reflect the relationship of the given strings.
        /// </summary>
        /// <param name="entityText">The text of the entity to define an the alias for.</param>
        /// <param name="textualAliases">One or more textual alias to define for the given entity text.</param>
        private static void DefineAliasInDictionary(string entityText, params string[] textualAliases) {
            aliasDictionary.AddOrUpdate(
                   entityText, new HashSet<string>(textualAliases),
                   (key, current) => current.Concat(textualAliases).ToHashSet()
                   );
        }
        private static void DefineAliasInDictionary(IEntity entity, params IEntity[] aliases) {
            aliasDictionary.AddOrUpdate(entity.Text, key => aliases
                .Select(a => a.Text).ToHashSet(),
                (key, current) => current.Concat(aliases.Select(a => a.Text)).ToHashSet());
            aliasedEntityReferenceMap.AddOrUpdate(
                entity, new HashSet<IEntity>(aliases),
                (key, current) => current.Union(aliases).ToHashSet()
                );

        }
        /// <summary>
        /// Gets the textual representations of all known aliases defined for the given entity.
        /// </summary>
        /// <param name="aliased">The entity who's aliases will be returned.</param>
        /// <returns>The textual representations of all known aliases defined for the given entity.</returns>
        public static IEnumerable<string> GetDefinedAliases(this IEntity aliased) {
            ISet<string> outval1;
            aliasDictionary.TryGetValue(aliased.Text, out outval1);
            outval1 = outval1 ?? new HashSet<string>();
            //
            ISet<IEntity> outval2;
            aliasedEntityReferenceMap.TryGetValue(aliased, out outval2);
            outval2 = outval2 ?? new HashSet<IEntity>();
            return outval1.Concat(outval2.Select(e => e.Text));
        }


        internal static IEnumerable<string> GetLikelyAliases(IEntity entity) {
            return entity.Match().Yield<IEnumerable<string>>()
                .With<NounPhrase>(n => DefineAliases(n))
                .When(e => e.SubjectOf.IsClassifier)
                .Then<IEntity>(e => e.SubjectOf
                    .DirectObjects
                    .SelectMany(direct => direct.Match().Yield<IEnumerable<string>>()
                        .When<IReferencer>(p => p.ReferesTo.Any())
                        .Then<IReferencer>(p => p.ReferesTo.SelectMany(r => GetLikelyAliases(r)))
                        .With<Noun>(n => n.GetSynonyms())
                    .Result()))
                .Result(defaultValue: Enumerable.Empty<string>());
        }

        private static IEnumerable<string> DefineAliases(NounPhrase nounPhrase) {
            return nounPhrase.Words.OfNoun().Count() == 1 && nounPhrase.Words.None(w => w is ProperNoun) ? Lookup.GetSynonyms(nounPhrase.Words.OfNoun().First()) : Enumerable.Empty<string>();
        }

        private static ConcurrentDictionary<IEntity, ISet<IEntity>> aliasedEntityReferenceMap = new ConcurrentDictionary<IEntity, ISet<IEntity>>();
        private static ConcurrentDictionary<string, ISet<string>> aliasDictionary = new ConcurrentDictionary<string, ISet<string>>();

    }
}
