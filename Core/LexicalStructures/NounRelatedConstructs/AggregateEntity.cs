using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents an collection of usually contiguous Entities which combine to form a single subject or object. </para>
    /// <para> As such it provides both the behaviors of an Entity and an Enumerable collection of Entities. That is to say that you can use an instance of this class in </para> 
    /// <para> situtation where an IEntity is Expected, but also enumerate it via foreach(var in ...) or (from e in ...) </para>
    /// </summary>
    /// <seealso cref="IAggregateEntity"/>
    /// <seealso cref="IEntity"/>
    public class AggregateEntity : IAggregateEntity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of aggregate entity forming, an aggregate entity composed of the given entities
        /// </summary>
        /// <param name="entities">The entities aggregated into the group.</param>
        public AggregateEntity(IEnumerable<IEntity> entities)
        {
            constituents = ImmutableList.CreateRange((from entity in entities
                                                      select entity.Match()
                                                      .Case((IAggregateEntity a) => a.AsEnumerable())
                                                      .Result(new[] { entity }) into entitySet
                                                      from entity in entitySet
                                                      select entity).Distinct()); // entities.AsRecursivelyEnumerable().Distinct().ToImmutableList();
            var kinds = from constituent in constituents
                        group constituent by constituent.EntityKind into g
                        orderby g.Count() descending
                        select g.Key;
            EntityKind = kinds.DefaultIfEmpty(EntityKind.ThingMultiple).First();
        }

        /// <summary>
        /// Initializes a new instance of aggregate entity forming, an aggregate entity composed of the given entities
        /// </summary>
        /// <param name="first">The first entity aggregated into the group.</param>
        /// <param name="rest">The rest of the entity aggregated into the group.</param>
        public AggregateEntity(IEntity first, params IEntity[] rest) : this(rest.Prepend(first)) { }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the aggregate entity "Owns",
        /// and sets its owner to be the aggregate entity.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            possessions = possessions.Add(possession);
            possession.Possesser = this;
        }
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the aggregate entity.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the to the aggregate entity.</param>
        public void BindDescriptor(IDescriptor descriptor)
        {
            descriptors = descriptors.Add(descriptor);
            descriptor.Describes = this;
        }
        /// <summary>
        /// Binds an IPronoun, generally a Pronoun or PronounPhrase, as a reference to the aggregate entity.
        /// </summary>
        /// <param name="referencer">The referencer which refers to the aggregate entity Instance.</param>
        public void BindReferencer(IReferencer referencer)
        {
            referencers = referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the members of the aggregate entity.
        /// </summary>
        /// <returns>An enumerator that iterates through the members of the aggregate entity.</returns>
        public IEnumerator<IEntity> GetEnumerator() => constituents.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Returns a string representation of the aggregate entity.
        /// </summary>
        /// <returns>A string representation of the aggregate entity.</returns>
        public override string ToString()
        {
            var members = constituents.AsRecursivelyEnumerable().ToList();
            return $@"[ {members.Count} ] {string.Join(" ",
                from member in members
                let quote = '\"'
                where !(member is IAggregateEntity)
                select $"{member.GetType().Name} {quote}{member.Text}{quote}")
            }";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the aggregate entity instance.
        /// </summary>
        public EntityKind EntityKind { get; }
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
        public IEnumerable<IReferencer> Referencers => referencers;
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the aggregate entity.
        /// </summary>
        public IEnumerable<IDescriptor> Descriptors => descriptors;
        /// <summary>
        /// Gets all of the constructs the aggregate entity can be determined to "own" collectively.
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;
        /// <summary>
        /// Gets or sets the Entity which is inferred to "own" all members the aggregate entity.
        /// </summary>
        public IPossesser Possesser { get; set; }
        /// <summary>
        /// Gets a textual representation of the aggregate entity.
        /// </summary>
        public string Text
        {
            get
            {
                return string.Join(" , ",
                    from member in constituents.AsRecursivelyEnumerable()
                    let prepositionText = member.Match().Case((IPrepositionLinkable i) => i.PrepositionOnRight?.Text ?? string.Empty).Result()
                    select member.Text + (prepositionText.IsNullOrWhiteSpace() ? string.Empty : " " + prepositionText));
            }
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the aggregate entity within the context of its document.
        /// </summary>
        // TODO: Revise this to compute some smart average.
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric weight of the aggregate entity over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }

        #endregion

        #region Fields

        /// <summary>
        /// The sequence of entities which compose to form the aggregate entity.
        /// </summary>
        private readonly IImmutableList<IEntity> constituents;
        IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;

        #endregion
    }
}
