using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class EntityAliasMap
    {
        /// <summary>
        /// Checks to see if one Entity is a nown alias for another.
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="other">The second Entity</param>
        /// <returns>true if the Entities are aliases for one another, false otherwise</returns>
        public static bool IsAliasFor(this IEntity entity, IEntity other) {
            HashSet<string> aliases;
            if (AliasDictionary.TryGetValue(entity.Text, out aliases)) {
                return aliases.Contains(other.Text);
            }
            return false;
        }

        /// <summary>
        /// Establishes that the given Entity has a the given textual alias. 
        /// </summary>
        /// <param name="entity">The Entity to register an alias for</param>
        /// <param name="textualAlias">The textual alias which will be registered</param>
        public static void RegisterTextualAlias(IEntity entity, string textualAlias) {
            if (AliasDictionary.ContainsKey(entity.Text)) {
                AliasDictionary[entity.Text].Add(textualAlias);
            } else {
                AliasDictionary[entity.Text] = new HashSet<string>(new[] { textualAlias });
            }
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the other. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="textualAlias">The second Entity</param>
        public static void RegisterTextualAlias(IEntity entity, IEntity other) {
            if (AliasDictionary.ContainsKey(entity.Text)) {
                AliasDictionary[entity.Text].Add(other.Text);
            } else {
                AliasDictionary[entity.Text] = new HashSet<string>(new[] { other.Text });
            }
        }
        /// <summary>
        /// Establishes that one Entity is an alias for the other. 
        /// </summary>
        /// <param name="entity">The first Entity</param>
        /// <param name="textualAlias">The second Entity</param>
        public static void RegisterTextualAlias(string entityText, string aliasText) {
            if (AliasDictionary.ContainsKey(entityText)) {
                AliasDictionary[entityText].Add(aliasText);
            } else {
                AliasDictionary[entityText] = new HashSet<string>(new[] { aliasText });
            }

        }

        private static ConcurrentDictionary<string, HashSet<string>> AliasDictionary = new ConcurrentDictionary<string, HashSet<string>>();
    }
}
