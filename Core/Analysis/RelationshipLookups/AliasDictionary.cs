using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

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
        /// <returns> <c>true</c> if the Entities are aliases for one another, false otherwise</returns>
        public static bool IsAliasFor(this IEntity possibleAlias, IEntity other) => possibleAlias != null && other != null && LookupAlias(other, possibleAlias);

        private static bool LookupAlias(IEntity possibleAlias, IEntity possiblyAliasedBy)
        {
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
        public static void DefineAlias(IEntity entity, string textualAlias)
        {
            DefineAliasesImplementation(entity.Text, textualAlias);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the end. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="other">The second Entity</param>
        public static void DefineAlias<TEntity>(TEntity entity, TEntity other) where TEntity : IEntity
        {
            DefineAliasesImplementation(entity.Text, other.Text);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the end. 
        /// </summary>
        /// <param name="entityText">The first Entity</param>
        /// <param name="aliasText">The second Entity</param>
        public static void DefineAlias(string entityText, string aliasText)
        {
            DefineAliasesImplementation(entityText, aliasText);
        }

        /// <summary>
        /// Adds or updates the known alias relationships to reflect the relationship of the given strings.
        /// </summary>
        /// <param name="entityText">The text of the entity to define an the alias for.</param>
        /// <param name="textualAliases">One or more textual alias to define for the given entity text.</param>
        private static void DefineAliasesImplementation(string entityText, params string[] textualAliases)
        {
            aliasDictionary.AddOrUpdate(
                   entityText, new HashSet<string>(textualAliases),
                   (key, value) => new HashSet<string>(value.Concat(textualAliases)));
        }
        private static void DefineAliasesImplementation(IEntity entity, IEntity alias, params IEntity[] aliases)
        {
            aliasDictionary.AddOrUpdate(
                entity.Text,
                key => new HashSet<string>(aliases.Select(a => a.Text)),
                (key, value) => new HashSet<string>(value.Union(aliases.Select(a => a.Text))));
            aliasedEntityReferenceMap.AddOrUpdate(
                entity, new HashSet<IEntity>(aliases),
                (key, value) => new HashSet<IEntity>(value.Union(aliases)));

        }
        /// <summary>
        /// Gets the textual representations of all known aliases defined for the given entity.
        /// </summary>
        /// <param name="aliased">The entity who's aliases will be returned.</param>
        /// <returns>The textual representations of all known aliases defined for the given entity.</returns>
        public static IEnumerable<string> GetDefinedAliases(this IEntity aliased)
        {
            ISet<string> outval1;
            aliasDictionary.TryGetValue(aliased.Text, out outval1);
            outval1 = outval1 ?? new HashSet<string>();
            ISet<IEntity> outval2;
            aliasedEntityReferenceMap.TryGetValue(aliased, out outval2);
            outval2 = outval2 ?? new HashSet<IEntity>();
            return outval1.Concat(outval2.Select(e => e.Text));
        }


        internal static IEnumerable<string> GetLikelyAliases(IEntity entity) => entity.Match()
            .Case((NounPhrase n) => DefineAliases(n))
            .When(e => e.SubjectOf.IsClassifier)
            .Then(e => e.SubjectOf.DirectObjects.SelectMany(o => o.Match()
                    .When((IReferencer p) => p.RefersTo.Any())
                    .Then((IReferencer p) => p.RefersTo.SelectMany(r => GetLikelyAliases(r)))
                    .Case((Noun n) => n.GetSynonyms())
                .Result()))
            .Result(Enumerable.Empty<string>());

        private static IEnumerable<string> DefineAliases(NounPhrase nounPhrase) =>
            nounPhrase.Words.OfNoun().Count() == 1 && !nounPhrase.Words.Any(w => w is ProperNoun) ?
            Lexicon.GetSynonyms(nounPhrase.Words.OfNoun().First()) :
            Enumerable.Empty<string>();

        private static ConcurrentDictionary<IEntity, ISet<IEntity>> aliasedEntityReferenceMap = new ConcurrentDictionary<IEntity, ISet<IEntity>>();
        private static ConcurrentDictionary<string, ISet<string>> aliasDictionary = new ConcurrentDictionary<string, ISet<string>>();

    }
}
