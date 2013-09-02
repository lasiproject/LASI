using LASI.Algorithm.Lookup;
using LASI.Algorithm.Patternization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    /// <summary>
    /// Provides for registration of Entities as aliases for other Entities.
    /// </summary>
    public static class AliasDictionary
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
        public static void DefineAlias(IEntity entity, IEntity other) {
            DefineAliasInDictionary(entity.Text, other.Text);
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
                   (key, current) => current.Concat(textualAliases).ToSet()
                   );
        }
        private static void DefineAliasInDictionary(IEntity entity, params IEntity[] aliases) {
            aliasedEntityReferenceMap.AddOrUpdate(
                   entity, new HashSet<IEntity>(aliases),
                   (key, current) => current.Union(aliases).ToSet()
                   );

        }

        internal static IEnumerable<string> GetLikelyAliases(IEntity entity) {
            return entity.Match().To<IEnumerable<string>>()
                .With<NounPhrase>(n => DefineAliases(n))
                .When(e => e.SubjectOf.IsClassifier)
                .With<IEntity>(e => e.SubjectOf
                    .DirectObjects
                    .SelectMany(direct => direct.Match().To<IEnumerable<string>>()
                        .When<IPronoun>(p => p.RefersTo.Any())
                        .With<IPronoun>(p => p.RefersTo.SelectMany(r => GetLikelyAliases(r)))
                        .With<Noun>(n => LexicalLookup.GetSynonyms(n))
                    .Result()))
                .Result() ?? Enumerable.Empty<string>();
        }

        private static IEnumerable<string> DefineAliases(NounPhrase nounPhrase) {
            throw new NotImplementedException();
        }

        private static ConcurrentDictionary<IEntity, ISet<IEntity>> aliasedEntityReferenceMap = new ConcurrentDictionary<IEntity, ISet<IEntity>>();
        private static ConcurrentDictionary<string, ISet<string>> aliasDictionary = new ConcurrentDictionary<string, ISet<string>>();

    }
}
