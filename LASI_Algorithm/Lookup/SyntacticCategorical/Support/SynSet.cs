using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.ComparativeHeuristics
{
    abstract class SynSet<TSetRelationship> : IEquatable<SynSet<TSetRelationship>>
    {
        protected SynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<TSetRelationship, int>> pointerRelationships) {
            Id = id;
            Words = new HashSet<string>(words);
            relatedSetsByRelationKind = pointerRelationships.ToLookup(p => p.Key, p => p.Value);
            ReferencedIndeces = new HashSet<int>(pointerRelationships.Select(p => p.Value));
        }
        /// <summary>
        /// Gets the ID of the SynSet.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Gets all of the words belonging to the SynSet.
        /// </summary>
        public HashSet<string> Words { get; private set; }
        /// <summary>
        /// Gets the IDs of all sets referenced by the SynSet.
        /// </summary>
        public HashSet<int> ReferencedIndeces { get; private set; }
        /// <summary>
        /// Returns the IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationship">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[TSetRelationship relationship] {
            get { return relatedSetsByRelationKind[relationship]; }
        }
        private ILookup<TSetRelationship, int> relatedSetsByRelationKind;
        /// <summary>
        /// Returns the IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion. 
        /// </summary>
        /// <param name="relationshipKind">The kind of external set references to relationships to return.</param>
        /// <returns>The IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion.</returns>
        public ILookup<TSetRelationship, int> RelatedSetsByRelationKind {
            get { return relatedSetsByRelationKind; }
        }

        public override int GetHashCode() {
            return Id;
        }
        public bool Equals(SynSet<TSetRelationship> other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            return obj as SynSet<TSetRelationship> == this;
        }
        /// <summary>
        /// Returns a single string representing the SynSet.
        /// </summary>
        /// <returns>A single string representing the SynSet.</returns>
        public override string ToString() {
            return "[" + Id + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public static bool operator ==(SynSet<TSetRelationship> lhs, SynSet<TSetRelationship> rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.Id == rhs.Id;
        }
        public static bool operator !=(SynSet<TSetRelationship> lhs, SynSet<TSetRelationship> rhs) {
            return !(lhs == rhs);
        }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.noun file of the WordNet package.
    /// </summary>
    internal sealed class NounSynSet : SynSet<NounSetRelationship>
    {
        public NounSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<NounSetRelationship, int>> pointerRelationships, NounLookup.Category lexicalCategory)
            : base(id, words, pointerRelationships) {
            LexicalCategory = lexicalCategory;
        }

        public NounLookup.Category LexicalCategory { get; private set; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.verb file of the WordNet package.
    /// </summary>
    internal sealed class VerbSynSet : SynSet<VerbSetRelationship>
    {
        public VerbSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<VerbSetRelationship, int>> pointerRelationships, VerbLookup.Category lexicalCategory)
            : base(id, words, pointerRelationships) {
            LexicalCategory = lexicalCategory;
        }
        public VerbLookup.Category LexicalCategory { get; private set; }
    }
    /// <summary>
    /// Represents a synset parsed from the data.adj file of the WordNet package.
    /// </summary>
    internal sealed class AdjectiveSynSet : SynSet<AdjectiveSetRelationship>
    {
        public AdjectiveSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdjectiveSetRelationship, int>> pointerRelationships, AdjectiveLookup.Category lexicalCategory)
            : base(id, words, pointerRelationships) {
            LexicalCategory = lexicalCategory;
        }

        public AdjectiveLookup.Category LexicalCategory { get; private set; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.adv file of the WordNet package.
    /// </summary>
    internal sealed class AdverbSynSet : SynSet<AdverbSetRelationship>
    {
        public AdverbSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdverbSetRelationship, int>> pointerRelationships, AdverbLookup.Category lexicalCategory)
            : base(id, words, pointerRelationships) {
            LexicalCategory = lexicalCategory;
        }
        public AdverbLookup.Category LexicalCategory { get; private set; }
    }

}