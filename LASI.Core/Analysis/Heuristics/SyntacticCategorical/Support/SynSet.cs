using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics.WordNet
{
    abstract class Synset<TLinkKind> : IEquatable<Synset<TLinkKind>>
    {
        protected Synset(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<TLinkKind, int>> pointerRelationships)
        {
            Id = id;
            Words = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
            referencedSetsByLinkType = new Lazy<ILookup<TLinkKind, int>>(() => pointerRelationships.ToLookup(p => p.Key, p => p.Value));
            ReferencedSets = new HashSet<int>(pointerRelationships.Select(p => p.Value));
        }
        /// <summary>
        /// The ID of the Synset.
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Gets all of the words belonging to the Synset.
        /// </summary>
        public IEnumerable<string> Words { get; }
        /// <summary>
        /// The IDs of all sets referenced by the Synset.
        /// </summary>
        public ISet<int> ReferencedSets { get; }
        /// <summary>
        /// Returns the IDs of all other Synsets which are referenced from the current Synset in the indicated fashion. 
        /// </summary>
        /// <param name="linkKinds">The kinds of external set relationships to consider return.</param>
        /// <returns>The IDs of all other Synsets which are referenced from the current Synset in the indicated fashion.</returns>
        public IEnumerable<int> this[params TLinkKind[] linkKinds] => linkKinds.SelectMany(link => RelatedSetIdsByRelationKind[link]);

        private Lazy<ILookup<TLinkKind, int>> referencedSetsByLinkType;


        /// <summary>
        /// The <see cref="ILookup{TKey, TElement}"/> that maps the Ids of all other Synsets which are referenced from the current Synset based on the manner in which they are referenced.
        /// </summary>
        public ILookup<TLinkKind, int> RelatedSetIdsByRelationKind => referencedSetsByLinkType.Value;

        /// <summary>
        /// Returns a value indicating whether the given word is directly contained within the Synset.
        /// </summary>
        /// <param name="word">The word to find.</param>
        /// <returns> <c>true</c> if the given word is directly contained within the Synset; otherwise false.</returns>
        public bool ContainsWord(string word) => Words.Contains(word);
        /// <summary>
        /// Returns a value indicating whether the current Synset directly links to the given Synset.
        /// </summary>
        /// <param name="other">The Synset to find.</param>
        /// <returns> <c>true</c> if the current Synset directly links to given Synset; otherwise false.</returns>
        public bool DirectlyReferences(Synset<TLinkKind> other) => ReferencedSets.Contains(other.Id);

        public override int GetHashCode() => Id;

        public bool Equals(Synset<TLinkKind> other) => this == other;

        public override bool Equals(object obj) => obj as Synset<TLinkKind> == this;

        /// <summary>
        /// Returns a single string representing the Synset.
        /// </summary>
        /// <returns>A single string representing the Synset.</returns>
        public override string ToString() => "[" + Id + "] " + Words.Format(Tuple.Create(' ', ',', ' '));

        public static bool operator ==(Synset<TLinkKind> left, Synset<TLinkKind> right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            if (ReferenceEquals(right, null))
                return ReferenceEquals(left, null);
            return left.Id == right.Id;
        }
        public static bool operator !=(Synset<TLinkKind> left, Synset<TLinkKind> right) => !(left == right);

    }




    /// <summary>
    /// Represents a synset parsed from a line of the data.noun file of the WordNet package.
    /// </summary>
    sealed class NounSynset : Synset<NounLink>
    {
        public NounSynset(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<NounLink, int>> pointerRelationships, NounCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }

        public NounCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.verb file of the WordNet package.
    /// </summary>
    sealed class VerbSynset : Synset<VerbLink>
    {
        public VerbSynset(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<VerbLink, int>> referencedSets, VerbCategory category)
            : base(id, words, referencedSets)
        {
            Category = category;
        }
        public VerbCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from the data.adj file of the WordNet package.
    /// </summary>
    sealed class AdjectiveSynset : Synset<AdjectiveLink>
    {
        public AdjectiveSynset(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdjectiveLink, int>> pointerRelationships, AdjectiveCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }

        public AdjectiveCategory Category { get; }
    }
    /// <summary>
    /// Represents a synset parsed from a line of the data.adv file of the WordNet package.
    /// </summary>
    sealed class AdverbSynset : Synset<AdverbLink>
    {
        public AdverbSynset(int id, IEnumerable<string> words, IEnumerable<KeyValuePair<AdverbLink, int>> pointerRelationships, AdverbCategory category)
            : base(id, words, pointerRelationships)
        {
            Category = category;
        }
        public AdverbCategory Category { get; }
    }
}