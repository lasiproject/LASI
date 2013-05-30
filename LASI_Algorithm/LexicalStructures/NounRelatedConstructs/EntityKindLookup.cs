using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.SyntacticInterfaces;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides static management of known occurances of EntityKinds.
    /// </summary>
    /// <seealso cref="EntityKind"/>The EntityKind enumeration defining all the kinds of entities. 
    public static class EntityKindLookup
    {
        /// <summary>
        /// Inserts an entry corresponding to the text and kind of the given entity.
        /// </summary>
        /// <param name="entry">The IEntity containing values to insert.</param>
        /// <exception cref="ArgumentException">Thrown when attempting to add an entry whose text is already mapped.
        /// </exception>
        public static void AddEntry(IEntity entry) {
            try {
                _lexicalMapping.Add(entry.Text, entry.EntityKind);
            } catch (ArgumentException) {
                //Debug.WriteLine(String.Format(
                //    "EntityKindLookup: An entry for {0} -> {1} is already present in the ",
                //    entry.Text,
                //    entry.EntityKind));
            }
        }
        /// <summary>
        ///  Inserts a mapping from the given text to the given kind.
        /// </summary>
        /// <param name="textKey">the text key for the entry</param>
        /// <param name="kind">the EntityKind value for the entry</param>
        public static void AddEntry(string textKey, EntityKind kind) {
            try {
                _lexicalMapping.Add(textKey, kind);
            } catch (ArgumentException) {
                //Debug.WriteLine(String.Format(
                //    "EntityKindLookup: An entry for {0} -> {1} is already present in the ",
                //    textKey,
                //    kind));
            }
        }
        /// <summary>
        /// Returns the EntityKind indexed by the text of the entry.
        /// </summary>
        /// <param name="entry">The entry to conjugated for.</param>
        /// <returns>The EntityKind corresponding to the entities text.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when no EntityKind is indexed by the entity's text.
        /// </exception>
        public static EntityKind Lookup(IEntity entry) {
            return Lookup(entry);
        }
        /// <summary>
        /// Returns the EntityKind indexed by the given text.
        /// </summary>
        /// <param name="entry">The text key to conjugated for.</param>
        /// <returns>The EntityKind corresponding to the provided text.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when no EntityKind is indexed by the privded text.
        /// </exception>
        public static EntityKind Lookup(string entry) {
            try {
                return _lexicalMapping[entry];
            } catch (KeyNotFoundException) {
                //Debug.WriteLine(
                //    String.Format(
                //    "EntityKindLookup: no entry presently defined for {0}", entry));
                throw;
            }
        }

        private static Dictionary<string, EntityKind> _lexicalMapping = new Dictionary<string, EntityKind>();

    }
}
