using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics.WordNet
{
    abstract class SynSet<TLinkKind> : IEquatable<SynSet<TLinkKind>>
    {
        protected SynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<TLinkKind, int>> pointerRelationships)
        {
            Id = id;
            Words = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
            referencedSetsByLinkType = new Lazy<ILookup<TLinkKind, int>>(() => pointerRelationships.ToLookup(p => p.Key, p => p.Value));
            ReferencedSets = new HashSet<int>(pointerRelationships.Select(p => p.Value));
        }
        /// <summary>
        /// Gets the ID of the SynSet.
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Gets all of the words belonging to the SynSet.
        /// </summary>
        public IEnumerable<string> Words { get; }
        /// <summary>
        /// Gets the IDs of all sets referenced by the SynSet.
        /// </summary>
        public ISet<int> ReferencedSets { get; }
        /// <summary>
        /// Returns the IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion. 
        /// </summary>
        /// <param name="linkKinds">The kinds of external set relationships to consider return.</param>
        /// <returns>The IDs of all other SynSets which are referenced from the current SynSet in the indicated fashion.</returns>
        public IEnumerable<int> this[params TLinkKind[] linkKinds] => linkKinds.SelectMany(link => RelatedSetIdsByRelationKind[link]);

        private Lazy<ILookup<TLinkKind, int>> referencedSetsByLinkType;


        /// <summary>
        /// Gets the <see cref="ILookup{TKey, TElement}"/> that maps the Ids of all other SynSets which are referenced from the current SynSet based on the manner in which they are referenced.
        /// </summary>
        public ILookup<TLinkKind, int> RelatedSetIdsByRelationKind => referencedSetsByLinkType.Value;

        /// <summary>
        /// Returns a value indicating whether the given word is directly contained within the SynSet.
        /// </summary>
        /// <param name="word">The word to find.</param>
        /// <returns> <c>true</c> if the given word is directly contained within the Synset; otherwise false.</returns>
        public bool ContainsWord(string word) => Words.Contains(word);
        /// <summary>
        /// Returns a value indicating whether the current SynSet directly links to the given SynSet.
        /// </summary>
        /// <param name="other">The SynSet to find.</param>
        /// <returns> <c>true</c> if the current SynSet directly links to given SynSet; otherwise false.</returns>
        public bool DirectlyReferences(SynSet<TLinkKind> other) => ReferencedSets.Contains(other.Id);

        public override int GetHashCode() => Id;

        public bool Equals(SynSet<TLinkKind> other) => this == other;

        public override bool Equals(object obj) => obj as SynSet<TLinkKind> == this;

        /// <summary>
        /// Returns a single string representing the SynSet.
        /// </summary>
        /// <returns>A single string representing the SynSet.</returns>
        public override string ToString() => "[" + Id + "] " + Words.Format(Tuple.Create(' ', ',', ' '));

        public static bool operator ==(SynSet<TLinkKind> left, SynSet<TLinkKind> right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            if (ReferenceEquals(right, null))
                return ReferenceEquals(left, null);
            return left.Id == right.Id;
        }
        public static bool operator !=(SynSet<TLinkKind> left, SynSet<TLinkKind> right) => !(left == right);


    }




    /// <summary>
    /// Represents a synset parsed from a line of the data.noun file of the WordNet package.
    /// </summary>
    sealed class NounSynSet : SynSet<NounLink>
    {
        public NounSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<NounLink, int>> pointerRelationships, NounCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }

        public NounCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.verb file of the WordNet package.
    /// </summary>
    sealed class VerbSynSet : SynSet<VerbLink>
    {
        public VerbSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<VerbLink, int>> referencedSets, VerbCategory category)
            : base(id, words, referencedSets)
        {
            Category = category;
        }
        public VerbCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from the data.adj file of the WordNet package.
    /// </summary>
    sealed class AdjectiveSynSet : SynSet<AdjectiveLink>
    {
        public AdjectiveSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdjectiveLink, int>> pointerRelationships, AdjectiveCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }

        public AdjectiveCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.adv file of the WordNet package.
    /// </summary>
    sealed class AdverbSynSet : SynSet<AdverbLink>
    {
        public AdverbSynSet(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdverbLink, int>> pointerRelationships, AdverbCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }
        public AdverbCategory Category { get; }
    }

}