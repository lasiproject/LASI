using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Universal;

namespace LASI.Core.Analysis.Melding
{
    /// <summary>
    /// Represents and Entity which abstracts over a group of related entities, transparently standing in as any one of them.
    /// </summary>
    /// <remarks>
    /// While this class bears strong internal structural similarities to the AggregateEntity class, its purpose is different.
    /// While the former provides an implementation of the IEntity interface which can be treated as a single instance or a collection of instances,
    /// The LiftedEntity class exists to abstract over the set of entities it contains and does not allow them to be iterated. 
    /// Its purpose is to be an entity which can transparently stand in for any one of the entities it was composed from.
    /// It is simply an instance of this class can only be treated as an instance of the IEntity interface, 
    /// providing no externally visible additional behavior or capabilities.
    /// </remarks>
    internal class LiftedEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the LiftedEntity class.
        /// </summary>
        /// <param name="avatar">The Entity which represents the determined lexical form for the set of entities being lifted.</param>
        /// <param name="represented">The set of entities which have been merged into this single LiftedEntity.</param>
        public LiftedEntity(IEntity avatar, IEnumerable<IEntity> represented)
        {
            Avatar = avatar;

            Represented = represented.ToImmutableList();

            directObjectsOfVerbals = FlattenAbout(e => e.DirectObjectOf.Match()
                    .Case((IAggregateVerbal v) => v.AsEnumerable())
                    .Case((IVerbal v) => v.Lift())
                    .Result())
                .ToAggregate();

            indirectObjectsOfVerbals = FlattenAbout(e => e.IndirectObjectOf).ToAggregate();

            subjectsOfVerbals = FlattenAbout(e => e.SubjectOf).ToAggregate();

            possessions = FlattenAbout(e => e.Possessions).ToImmutableHashSet();

            descriptors = FlattenAbout(e => e.Descriptors).ToImmutableHashSet();

            referencers = FlattenAbout(e => e.Referencers).ToImmutableHashSet();
        }
        public void BindDescriptor(IDescriptor descriptor)
        {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }

        public void BindReferencer(IReferencer referencer)
        {
            referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }

        public void AddPossession(IPossessable possession)
        {
            possessions.Add(possession);
            possession.Possesser = this.ToOption<IPossesser>();
        }

        /// <summary>
        /// Binds the <see cref="LiftedEntity"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="LiftedEntity"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal)
        {
            directObjectsOfVerbals = directObjectsOfVerbals.Append(verbal).ToAggregate();
        }

        /// <summary>
        /// Binds the <see cref="LiftedEntity"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }

        public IEnumerable<IDescriptor> Descriptors => descriptors;
        public IEnumerable<IReferencer> Referencers => referencers;
        public IEnumerable<IPossessable> Possessions => possessions;

        public EntityKind EntityKind => Avatar.EntityKind;

        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the subject of.
        /// </summary>
        public IVerbal SubjectOf
        {
            get => subjectsOfVerbals;
            private set => subjectsOfVerbals = new[] { value }.ToAggregate();
        }
        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf => directObjectsOfVerbals;

        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf
        {
            get => indirectObjectsOfVerbals;
            private set => indirectObjectsOfVerbals = new[] { value }.ToAggregate();
        }


        public Option<IPossesser> Possesser { get; set; } = Option.None<IPossesser>();

        public string Text => Avatar.Text;

        public double Weight
        {
            get => Represented.Average(w => w.Weight);
            set => Represented.ForEach(entity => entity.Weight = value);
        }

        public double MetaWeight
        {
            get => Represented.Average(w => w.MetaWeight);
            set => Represented.ForEach(entity => entity.MetaWeight = value);
        }

        public IEntity Avatar { get; }

        public ImmutableList<IEntity> Represented { get; }


        #region Helper Methods

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IEntity, TResult> selector) where TResult : ILexical =>
            from r in Represented
            let result = selector(r)
            where result != null
            select result;

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IEntity, IEnumerable<TResult>> collectionSelector) =>
            Represented.SelectMany(e => collectionSelector(e).NonNull());
        #endregion Private Helper Methods

        #region Fields

        private readonly IImmutableSet<IPossessable> possessions;
        private readonly IImmutableSet<IReferencer> referencers;
        private readonly IImmutableSet<IDescriptor> descriptors;

        private IAggregateVerbal directObjectsOfVerbals;
        private IAggregateVerbal indirectObjectsOfVerbals;
        private IAggregateVerbal subjectsOfVerbals;

        #endregion Fields
    }
}