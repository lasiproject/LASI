using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Universal;
using System;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents an collection of usually contiguous Entities which combine to form a single subject or object. </para>
    /// <para> As such it provides both the behaviors of an Entity and an Enumerable collection of Entities. That is to say that you can use an instance of this class in </para> 
    /// <para> situtation where an IEntity is Expected, but also enumerate it via foreach(var in ...) or (from e in ...) </para>
    /// </summary>
    /// <seealso cref="IAggregateEntity"/>
    /// <seealso cref="IAggregateLexical{TLexical}"/>
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
            Constituents = (from entity in entities
                            let aggregate = entity.Match((IAggregateEntity a) => a, (IEntity e) => e.Lift())
                            from aggregated in aggregate.AsRecursivelyEnumerable()
                            select aggregated).Distinct().ToList();

            var kinds = from constituent in Constituents
                        group constituent by constituent.EntityKind into byKind
                        orderby byKind.Count() descending
                        select byKind.Key;
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
            possession.Possesser = this.ToOption<IPossesser>();
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
        public IEnumerator<IEntity> GetEnumerator() => Constituents.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        /// <summary>
        /// Returns a string representation of the aggregate entity.
        /// </summary>
        /// <returns>A string representation of the aggregate entity.</returns>
        public override string ToString()
        {
            var members = Constituents.AsRecursivelyEnumerable().ToList();
            return $@"[ {members.Count} ] {string.Join(" ",
                from member in members
                where !(member is IAggregateEntity)
                select $"{member.GetType().Name} \"{member.Text}\"")
            }";
        }

        /// <summary>
        /// Binds the <see cref="AggregateEntity"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        public void BindAsDirectObjectOf(IVerbal verbal)
        {
            DirectObjectOf = verbal.ToOption();
        }

        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Entity PronounKind; Person, Place, Thing, Organization, or Activity; of the aggregate entity instance.
        /// </summary>
        public EntityKind EntityKind { get; }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the aggregate entity is the DIRECT object of.
        /// </summary>
        public Option<IVerbal> DirectObjectOf { get; private set; } = Option.None<IVerbal>();
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the aggregate entity is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf { get; private set; }
        /// <summary>
        /// Gets the IVerbal instance, generally a Verb or VerbPhrase, which the aggregate entity is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; private set; }
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
        public Option<IPossesser> Possesser { get; set; } = Option.None<IPossesser>();
        /// <summary>
        /// A textual representation of the aggregate entity.
        /// </summary>
        public string Text => string.Join(" , ",
                    from member in Constituents.AsRecursivelyEnumerable()
                    let prepositionText = member.Match((IPrepositionLinkable i) => i.RightPrepositional?.Text ?? string.Empty)
                    select member.Text + (prepositionText.IsNullOrWhiteSpace() ? string.Empty : " " + prepositionText));

        /// <summary>
        /// Gets or sets the numeric Weight of the aggregate entity within the context of its document.
        /// </summary>
        // TODO: Revise this to compute some smart average.
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric weight of the aggregate entity over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }

        /// <summary>
        /// The sequence of entities which compose to form the aggregate entity.
        /// </summary>
        public IEnumerable<IEntity> Constituents { get; }

        #endregion

        #region Fields


        IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        IImmutableSet<IDescriptor> descriptors = ImmutableHashSet<IDescriptor>.Empty;
        IImmutableSet<IReferencer> referencers = ImmutableHashSet<IReferencer>.Empty;

        #endregion
    }
}
