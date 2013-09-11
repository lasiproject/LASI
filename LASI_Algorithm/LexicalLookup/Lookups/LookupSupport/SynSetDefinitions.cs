using LASI.Algorithm.Lookup;
using LASI.Algorithm.Lookup.InterSetRelationshipManagement;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Lookup
{

    /// <summary>
    /// Represents a synset parsed from a line of the data.noun file of the WordNet package.
    /// </summary>
    struct NounSynSet : IEquatable<NounSynSet>
    {

        /// <summary>
        /// Initializes a new instance of the NounSynSet class based on provided arguments.
        /// </summary>
        /// <param name="id">The noun synset unique set id.</param>
        /// <param name="words">The collection of word strings contained directly within the synset.</param>
        /// <param name="pointerRelations">A collection of pairs each representing a reference to another synset in data.noun and its relationship to the initialized synset.</param>
        /// <param name="lexCategory">The lexical noun category of the synset.</param>
        public NounSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<NounSetRelationship, int>> pointerRelations, NounCategory lexCategory)
            : this() {
            ID = id;
            LexName = lexCategory;
            Words = new HashSet<string>(words);
            relatedSetsByRelationKind = pointerRelations.ToLookup(rel => rel.Key, rel => rel.Value);
            ReferencedIndexes = new HashSet<int>(pointerRelations.Select(pair => pair.Value));

        }
        /// <summary>
        /// Returns a single string representing the members of the NounSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the NounSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }

        public override int GetHashCode() {
            return ID;
        }
        public bool Equals(NounSynSet other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            return obj is NounSynSet && (NounSynSet)obj == this;
        }

        public NounCategory LexName {
            get;
            private set;
        }
        /// <summary>
        /// Returns the IDs of all other NounSynSets which are referenced from the current NounSynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationshipKind">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other NounSynSets which are referenced from the current NounSynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[NounSetRelationship relationshipKind] {
            get {
                return relatedSetsByRelationKind[relationshipKind];
            }
        }

        private ILookup<NounSetRelationship, int> relatedSetsByRelationKind;

        /// <summary>
        /// Gets all of the words belonging to the NounSynSet.
        /// </summary>
        public HashSet<string> Words {
            get;
            private set;

        }
        /// <summary>
        /// Gets the IDs of all sets referenced by the NounSynSet.
        /// </summary>
        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }
        /// <summary>
        /// Gets the ID of the NounSynSet.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        public static bool operator ==(NounSynSet lhs, NounSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(NounSynSet lhs, NounSynSet rhs) {
            return !(lhs == rhs);
        }




    }

    /// <summary>
    /// Represents a synset parsed from a line of the data.verb file of the WordNet package.
    /// </summary>
    struct VerbSynSet : IEquatable<VerbSynSet>
    {
        public VerbCategory LexName {
            get;
            private set;
        }

        public VerbSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<VerbSetRelationship, int>> pointerRelations, VerbCategory lexCategory)
            : this() {
            ID = id;
            LexName = lexCategory;
            Words = new HashSet<string>(words);
            relatedSetsByRelationKind = pointerRelations.ToLookup(rel => rel.Key, rel => rel.Value);
            ReferencedIndexes = new HashSet<int>(pointerRelations.Select(pair => pair.Value));

        }
        /// <summary>
        /// Returns a single string representing the members of the VerbThesaurusSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the VerbThesaurusSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public bool Equals(VerbSynSet other) {
            return this == other;
        }
        public override bool Equals(object obj) {
            return obj is VerbSynSet && this == (VerbSynSet)obj;
        }
        public static bool operator ==(VerbSynSet lhs, VerbSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(VerbSynSet lhs, VerbSynSet rhs) {
            return !(lhs == rhs);
        }
        /// <summary>
        /// Returns the IDs of all other VerbSynSets which are referenced from the current VerbSynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationshipKind">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other VerbSynSets which are referenced from the current VerbSynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[VerbSetRelationship relationshipKind] {
            get {
                return relatedSetsByRelationKind[relationshipKind];
            }
        }

        private ILookup<VerbSetRelationship, int> relatedSetsByRelationKind;

        internal ILookup<VerbSetRelationship, int> RelatedSetsByRelationKind {
            get { return relatedSetsByRelationKind; }
        }

        /// <summary>
        /// Gets all of the words belonging to the VerbSynSet.
        /// </summary>
        public HashSet<string> Words {
            get;
            private set;

        }
        /// <summary>
        /// Gets the IDs of all sets referenced by the VerbSynSet.
        /// </summary>
        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }
        /// <summary>
        /// Gets the ID of the VerbSynSet.
        /// </summary>
        public int ID {
            get;
            private set;
        }

    }

    /// <summary>
    /// Represents a synset parsed from the data.adj file of the WordNet package.
    /// </summary>
    sealed class AdjectiveSynSet
    {
        public AdjectiveSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<AdjectiveSetRelationship, int>> pointerRelations, AdjectiveCategory lexCategory) {
            this.ID = ID;
            LexName = lexCategory;
            Words = new HashSet<string>(words);
            relatedSetsByRelationKind = pointerRelations.ToLookup(rel => rel.Key, rel => rel.Value);
            ReferencedIndexes = new HashSet<int>(pointerRelations.Select(pair => pair.Value));

        }
        /// <summary>
        /// Returns a single string representing the members of the AdjectiveSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the AdjectiveSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as AdjectiveSynSet;
        }

        public AdjectiveCategory LexName {
            get;
            private set;
        }
        /// <summary>
        /// Returns the IDs of all other AdjectiveSynSets which are referenced from the current AdjectiveSynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationshipKind">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other AdjectiveSynSets which are referenced from the current AdjectiveSynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[AdjectiveSetRelationship relationshipKind] {
            get {
                return relatedSetsByRelationKind[relationshipKind];
            }
        }

        private ILookup<AdjectiveSetRelationship, int> relatedSetsByRelationKind;

        /// <summary>
        /// Gets all of the words belonging to the AdjectiveSynSet.
        /// </summary>
        public HashSet<string> Words {
            get;
            private set;

        }
        /// <summary>
        /// Gets the IDs of all sets referenced by the AdjectiveSynSet.
        /// </summary>
        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }
        /// <summary>
        /// Gets the ID of the AdjectiveSynSet.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        public static bool operator ==(AdjectiveSynSet lhs, AdjectiveSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(AdjectiveSynSet lhs, AdjectiveSynSet rhs) {
            return !(lhs == rhs);
        }


    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.adv file of the WordNet package.
    /// </summary>
    sealed class AdverbSynSet
    {


        public AdverbSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<AdverbSetRelationship, int>> pointerRelations, AdverbCategory lexCategory) {
            this.ID = ID;
            LexName = lexCategory;
            Words = new HashSet<string>(words);
            relatedSetsByRelationKind = pointerRelations.ToLookup(rel => rel.Key, rel => rel.Value);
            ReferencedIndexes = new HashSet<int>(pointerRelations.Select(pair => pair.Value));

        }
        /// <summary>
        /// Returns a single string representing the members of the AdverbSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the AdverbSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as AdverbSynSet;
        }

        public AdverbCategory LexName {
            get;
            private set;
        }

        /// <summary>
        /// Returns the IDs of all other AdverbSynSets which are referenced from the current AdverbSynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationshipKind">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other AdverbSynSets which are referenced from the current AdverbSynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[AdverbSetRelationship relationshipKind] {
            get {
                return relatedSetsByRelationKind[relationshipKind];
            }
        }

        private ILookup<AdverbSetRelationship, int> relatedSetsByRelationKind;

        /// <summary>
        /// Gets all of the words belonging to the AdverbSynSet.
        /// </summary>
        public HashSet<string> Words {
            get;
            private set;

        }
        /// <summary>
        /// Gets the IDs of all sets referenced by the AdverbSynSet.
        /// </summary>
        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }
        /// <summary>
        /// Gets the ID of the AdverbSynSet.
        /// </summary>
        public int ID {
            get;
            private set;
        }

        public static bool operator ==(AdverbSynSet lhs, AdverbSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(AdverbSynSet lhs, AdverbSynSet rhs) {
            return !(lhs == rhs);
        }


    }

}