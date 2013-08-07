
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an collection of usually contiguous NounPhrases which combine to form a single subject or object.
    /// As such it provides both the behaviors of an entity and an Enumerable collection of describables. That is to say that you can use an instance of this class in 
    /// situtation where an IEntity is Expected, but also enumerate it, via foreach(var in ...) or (from e in ...)
    /// </summary>
    /// <see cref="IEntityGroup"/>
    /// <seealso cref="IEntity"/>
    public class EntityGroup : IEntityGroup
    {
        /// <summary>
        /// Initializes a new instance of EntityGroup forming, an aggregate entity composed of the given entities
        /// </summary>
        /// <param name="members">The Entities aggregated into the group.</param>
        public EntityGroup(IEnumerable<IEntity> members) {
            Members = members;
        }

        #region Methods
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the EntityGroup "Owns",
        /// and sets its owner to be the EntityGroup.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IEntity possession) {
            _possessions.Add(possession);
            possession.Possesser = this;
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the EntityGroup.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the EntityGroup's descriptors.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            _descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the EntityGroup.
        /// </summary>
        /// <param name="pro">The referencer which refers to the EntityGroup Instance.</param>
        public void BindPronoun(IPronoun pro) {
            _boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the members of the EntityGroup.
        /// </summary>
        /// <returns>An enumerator that iterates through the members of the EntityGroup.</returns>
        public IEnumerator<IEntity> GetEnumerator() {
            foreach (var entity in Members.EnumerateRecursively()) {
                yield return entity;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            foreach (var entity in Members.EnumerateRecursively()) {
                yield return entity;
            }
        }
        /// <summary>
        /// Returns a string representation of the EntityGroup.
        /// </summary>
        /// <returns>A string representation of the EntityGroup.</returns>
        public override string ToString() {
            return base.ToString() + String.Format(" \"{0}\"", Text);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the EntityGroup instance.
        /// </summary>
        public EntityKind EntityKind {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the EntityGroup is the DIRECT object of.
        /// </summary>
        public IVerbal DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the EntityGroup is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a Verb or VerbPhrase, which the EntityGroup is the subject of.
        /// </summary>
        public IVerbal SubjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the EntityGroup.
        /// </summary>
        public IEnumerable<IPronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the EntityGroup.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors {
            get {
                return _descriptors;
            }
        }
        /// <summary>
        /// Gets all of the constructs the EntityGroup can be determined to "own" collectively.
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessions;
            }
        }
        /// <summary>
        /// Gets or sets the Entity which is inferred to "own" all members the EntityGroup.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }
        /// <summary>
        /// Gets a textual representation of the EntityGroup.
        /// </summary>
        public string Text {
            get {
                return Members.Aggregate("", (aggr, ent) => aggr += ent.Text + " ").Trim();
            }
        }
        /// <summary>
        /// Gets the Type of the EntityGroup.
        /// </summary>
        public Type Type {
            get {
                return GetType();
            }
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the EntityGroup within the context of its document.
        /// </summary>
        public double Weight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the EntityGroup over the context of all extant documents.
        /// </summary>
        public double MetaWeight {
            get;
            set;
        }
        /// <summary>
        /// Gets the EnumerableCollection of Entities which compose to form the EntityGroup
        /// </summary>
        public IEnumerable<IEntity> Members {
            get;
            protected set;
        }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the EntityGroup.
        /// </summary>
        public IPrepositional PrepositionOnLeft {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the EntityGroup.
        /// </summary>
        public IPrepositional PrepositionOnRight {
            get;
            set;
        }

        #endregion

        #region Fields

        HashSet<IEntity> _possessions = new HashSet<IEntity>();
        HashSet<IDescriptor> _descriptors = new HashSet<IDescriptor>();
        HashSet<IPronoun> _boundPronouns = new HashSet<IPronoun>();

        #endregion
    }
}
