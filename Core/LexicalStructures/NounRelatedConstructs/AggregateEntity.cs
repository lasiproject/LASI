using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.PatternMatching;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents an collection of usually contiguous entities which combine to form a single subject or object. </para>
    /// <para> As such it provides both the behaviors of an entity and an Enumerable collection of describables. That is to say that you can use an instance of this class in </para> 
    /// <para> situtation where an IEntity is Expected, but also enumerate it, via foreach(var in ...) or (from e in ...) </para>
    /// </summary>
    /// <see cref="IAggregateEntity"/>
    /// <seealso cref="IEntity"/>
    public class AggregateEntity : IAggregateEntity
    {
        /// <summary>
        /// Initializes a new instance of aggregate entity forming, an aggregate entity composed of the given entities
        /// </summary>
        /// <param name="entities">The Entities aggregated into the group.</param>
        public AggregateEntity(IEnumerable<IEntity> entities) {
            members = from e in entities
                      let aggregate = e as
                      IAggregateEntity
                      select aggregate == null ? new[] { e } : aggregate.AsEnumerable() into ens
                      from e in ens
                      select e;
            EntityKind = EntityKind.ThingMultiple;
        }

        #region Methods
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the aggregate entity "Owns",
        /// and sets its owner to be the aggregate entity.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession) {
            possessions.Add(possession);
            possession.Possesser = this;
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the aggregate entity.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the to the aggregate entity.</param>
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);

            descriptor.Describes = this;
        }
        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the aggregate entity.
        /// </summary>
        /// <param name="pro">The referencer which refers to the aggregate entity Instance.</param>
        public void BindReferencer(IReferencer pro) {
            boundPronouns.Add(pro);
            pro.BindAsReferringTo(this);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the members of the aggregate entity.
        /// </summary>
        /// <returns>An enumerator that iterates through the members of the aggregate entity.</returns>
        public IEnumerator<IEntity> GetEnumerator() {
            return members.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        /// <summary>
        /// Returns a string representation of the aggregate entity.
        /// </summary>
        /// <returns>A string representation of the aggregate entity.</returns>
        public override string ToString() {
            return string.Format("{0}{1}", members.Count() > 1 ? "[ " + members.Count() + " ] " : string.Empty, string.Join(" ", members.AsRecursiveEnumerable()
                .Where(m => !(m is IAggregateEntity))
                .Select(p => p.GetType().Name + " \"" + p.Text + "\"")));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the aggregate entity instance.
        /// </summary>
        public EntityKind EntityKind { get; private set; }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the aggregate entity is the DIRECT object of.
        /// </summary>
        public IVerbal DirectObjectOf { get; set; }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the aggregate entity is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; set; }
        /// <summary>
        /// Gets the IVerbal instance, generally a Verb or VerbPhrase, which the aggregate entity is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; set; }
        /// <summary>
        /// Gets all of the IPronoun instances, generally Pronouns or PronounPhrases, which refer to the aggregate entity.
        /// </summary>
        public IEnumerable<IReferencer> Referencers { get { return boundPronouns; } }
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the aggregate entity.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors { get { return descriptors; } }
        /// <summary>
        /// Gets all of the constructs the aggregate entity can be determined to "own" collectively.
        /// </summary>
        public IEnumerable<IPossessable> Possessed { get { return possessions; } }
        /// <summary>
        /// Gets or sets the Entity which is inferred to "own" all members the aggregate entity.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Gets a textual representation of the aggregate entity.
        /// </summary>
        public string Text {
            get {
                return string.Join(" , ", members.AsRecursiveEnumerable().Select(p => p.Text + (p.PrepositionOnRight != null ? " " + p.PrepositionOnRight.Text : string.Empty)));
            }
        } 
        /// <summary>
        /// Gets the Type of the aggregate entity.
        /// </summary>
        public Type Type { get { return GetType(); } }
        /// <summary>
        /// Gets or sets the numeric Weight of the aggregate entity within the context of its document.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric weight of the aggregate entity over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the aggregate entity.
        /// </summary>
        public IPrepositional PrepositionOnLeft { get; set; }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the aggregate entity.
        /// </summary>
        public IPrepositional PrepositionOnRight { get; set; }

        #endregion

        #region Fields

        /// <summary>
        /// The sequence of entities which compose to form the aggregate entity.
        /// </summary>
        private IEnumerable<IEntity> members;
        ISet<IPossessable> possessions = new HashSet<IPossessable>();
        ISet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        ISet<IReferencer> boundPronouns = new HashSet<IReferencer>();

        #endregion
    }
}
