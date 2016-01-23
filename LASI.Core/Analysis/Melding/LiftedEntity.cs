using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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
            this.avatar = avatar;
            this.represented = represented.ToImmutableList();

            directObjectsOfVerbals = FlattenAbout(e => e.DirectObjectOf).ToAggregate();
            indirectObjectsOfVerbals = FlattenAbout(e => e.IndirectObjectOf).ToAggregate();
            subjectsOfVerbals = FlattenAbout(e => e.SubjectOf).ToAggregate();

            possessions = FlattenAbout(e => e.Possessions);
            descriptors = FlattenAbout(e => e.Descriptors);
            referencers = FlattenAbout(e => e.Referencers);
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
            possession.Possesser = this;
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
            DirectObjectOf = verbal;
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

        public EntityKind EntityKind => avatar.EntityKind;
        
        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the subject of.
        /// </summary>
        public IVerbal SubjectOf
        {
            get { return subjectsOfVerbals; }
            private set { subjectsOfVerbals = new[] { value }.ToAggregate(); }
        }
        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the direct object of.
        /// </summary>
        public IVerbal DirectObjectOf
        {
            get { return directObjectsOfVerbals; }
            private set { directObjectsOfVerbals = new[] { value }.ToAggregate(); }
        }
        /// <summary>
        /// Gets the <see cref="IVerbal"/> of which the entity is the indirect object of.
        /// </summary>
        public IVerbal IndirectObjectOf
        {
            get { return indirectObjectsOfVerbals; }
            private set { indirectObjectsOfVerbals = new[] { value }.ToAggregate(); }
        }


        public IPossesser Possesser { get; set; }

        public string Text => avatar.Text;

        public double Weight
        {
            get { return represented.Average(w => w.Weight); }
            set { represented.ForEach(entity => entity.Weight = value); }
        }

        public double MetaWeight
        {
            get { return represented.Average(w => w.MetaWeight); }
            set { represented.ForEach(entity => entity.MetaWeight = value); }
        }


        #region Helper Methods

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IEntity, TResult> selector) =>
            from r in represented
            let result = selector(r)
            where result != null
            select result;

        private IImmutableSet<TResult> FlattenAbout<TResult>(Func<IEntity, IEnumerable<TResult>> collectionSelector) =>
            represented.SelectMany(e => collectionSelector(e).Where(r => r != null)).ToImmutableHashSet();
        #endregion Private Helper Methods

        #region Fields


        private readonly IEntity avatar;
        private readonly ImmutableList<IEntity> represented;

        private readonly IImmutableSet<IPossessable> possessions;
        private readonly IImmutableSet<IReferencer> referencers;
        private readonly IImmutableSet<IDescriptor> descriptors;

        private IAggregateVerbal directObjectsOfVerbals;
        private IAggregateVerbal indirectObjectsOfVerbals;
        private IAggregateVerbal subjectsOfVerbals;

        #endregion Fields
    }
}