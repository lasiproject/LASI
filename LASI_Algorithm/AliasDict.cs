using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    public static class AliasDict
    {
        /// <summary>
        /// Checks to see if one Entity is a nown alias for another.
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="other">The second Entity</param>
        /// <returns>true if the Entities are aliases for one another, false otherwise</returns>
        public static bool IsAliasFor(this IEntity entity, IEntity other)
        {
            return Lookup(entity.Text, other.Text);
        }

        private static bool Lookup(string entityText, string aliasText)
        {
            HashSet<string> aliases;
            return aliasDictionary.TryGetValue(entityText, out aliases)
                &&
                aliases.Contains(aliasText);
        }

        /// <summary>
        /// Establishes that the given Entity has a the given textual alias. 
        /// </summary>
        /// <param name="entity">The Entity to register an alias for</param>
        /// <param name="textualAlias">The textual alias which will be registered</param>
        public static void RegisterTextualAlias(IEntity entity, string textualAlias)
        {
            DefineAliasInDictionary(entity.Text, textualAlias);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the other. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="textualAlias">The second Entity</param>
        public static void RegisterTextualAlias(IEntity entity, IEntity other)
        {
            DefineAliasInDictionary(entity.Text, other.Text);
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the other. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="textualAlias">The second Entity</param>
        public static void RegisterTextualAlias(string entityText, string aliasText)
        {
            DefineAliasInDictionary(entityText, aliasText);

        }
        /// <summary>
        /// Adds or updates the known alias relationships to reflect the relationship of the given strings.
        /// </summary>
        /// <param name="entityText">The text of the entity to define an the alias for.</param>
        /// <param name="textualAliases">One or more textual alias to define for the given entity text.</param>
        private static void DefineAliasInDictionary(string entityText, params string[] textualAliases)
        {
            aliasDictionary.AddOrUpdate(
                   entityText,
                   new HashSet<string>(textualAliases),
                   (keyString, aliases) =>
                   {
                       aliases.Concat(textualAliases);
                       return aliases;
                   });

        }

        private static ConcurrentDictionary<string, HashSet<string>> aliasDictionary = new ConcurrentDictionary<string, HashSet<string>>();
    }
}
