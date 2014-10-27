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
        /// <param name="representative">The Entity which represents the determined lexical form for the set of entities being lifted.</param>
        /// <param name="represented">The set of entities which have been merged into this single LiftedEntity.</param>
        public LiftedEntity(IEntity representative, IEnumerable<IEntity> represented) {
            this.representative = representative;
            this.represented = represented;
            directObjectsOfVerbals = FlattenAbout(entity => entity.DirectObjectOf).ToAggregate();
            indirectObjectsOfVerbals = FlattenAbout(entity => entity.IndirectObjectOf).ToAggregate();
            subjectsOfVerbals = FlattenAbout(entity => entity.SubjectOf).ToAggregate();
            possessions = FlattenAbout(entity => entity.Possessions).ToHashSet();
            descriptors = FlattenAbout(entity => entity.Descriptors).ToHashSet();
            referencers = FlattenAbout(entity => entity.Referencers).ToHashSet();
        }
        public void BindDescriptor(IDescriptor descriptor) {
            descriptors.Add(descriptor);
            descriptor.Describes = this;
        }

        public void BindReferencer(IReferencer referencer) {
            referencers.Add(referencer);
            referencer.BindAsReferringTo(this);
        }

        public void AddPossession(IPossessable possession) {
            possessions.Add(possession);
            possession.Possesser = this;
        }

        public IEnumerable<IDescriptor> Descriptors { get { return represented.SelectMany(entity => entity.Descriptors); } }

        public EntityKind EntityKind { get { return representative.EntityKind; } }

        public IVerbal SubjectOf {
            get { return subjectsOfVerbals; }
            set { subjectsOfVerbals = new[] { value }.ToAggregate(); }
        }

        public IVerbal DirectObjectOf {
            get { return directObjectsOfVerbals; }
            set { directObjectsOfVerbals = new[] { value }.ToAggregate(); }
        }

        public IVerbal IndirectObjectOf {
            get { return indirectObjectsOfVerbals; }
            set { indirectObjectsOfVerbals = new[] { value }.ToAggregate(); }
        }

        public IEnumerable<IReferencer> Referencers { get { return referencers; } }

        public IEnumerable<IPossessable> Possessions { get { return possessions; } }

        public IPossesser Possesser { get; set; }

        public IPrepositional PrepositionOnLeft { get; set; }

        public IPrepositional PrepositionOnRight { get; set; }

        public string Text { get { return representative.Text; } }

        public double Weight { get; set; }

        public double MetaWeight { get; set; }



        #region Private Helper Methods

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IEntity, TResult> flattenAbout) {
            return represented
                .Select(flattenAbout)
                .Where(result => result != null);
        }
        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IEntity, IEnumerable<TResult>> flattenAbout) {
            return represented
                .SelectMany(flattenAbout)
                .Where(result => result != null);
        }

        #endregion

        #region Fields
        private IAggregateVerbal directObjectsOfVerbals;
        private IAggregateVerbal indirectObjectsOfVerbals;
        private IAggregateVerbal subjectsOfVerbals;

        private readonly IEntity representative;
        private readonly IEnumerable<IEntity> represented;
        private readonly ISet<IPossessable> possessions;
        private readonly ISet<IReferencer> referencers;
        private readonly ISet<IDescriptor> descriptors;

        #endregion
    }
}