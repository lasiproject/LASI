using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Binding.Experimental
{
    internal interface IAggregateLexical<out TPrimary, out TBase> : ILexical
        where TPrimary : TBase, ILexical
        where TBase : ILexical
    {
        IEnumerable<TBase> Constituents { get; }

        IEnumerable<TPrimary> PrimaryConstituents { get; }
    }

    internal class AggregateNounPhrase : IAggregateLexical<NounPhrase, IEntity>, IEntity
    {
        protected AggregateNounPhrase(IEnumerable<IEntity> constituents)
        {
            this.constituents = constituents.ToList();
        }

        public void AddPossession(IPossessable possession)
        {
            possessions.Add(possession);
        }

        public void BindDescriptor(IDescriptor descriptor)
        {
            foreach (var constituent in PrimaryConstituents)
            {
                constituent.BindDescriptor(descriptor);
            }
        }

        public void BindReferencer(IReferencer referencer)
        {
            boundPronouns.Add(referencer);
        }

        /// <summary>
        /// Binds the <see cref="AggregateNounPhrase"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsSubjectOf(IVerbal verbal)
        {
            SubjectOf = verbal;
        }

        /// <summary>
        /// Binds the <see cref="AggregateNounPhrase"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsDirectObjectOf(IVerbal verbal) => DirectObjectOf = verbal;

        /// <summary>
        /// Binds the <see cref="AggregateNounPhrase"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        public void BindAsIndirectObjectOf(IVerbal verbal)
        {
            IndirectObjectOf = verbal;
        }

        public IEnumerable<IEntity> Constituents => constituents;

        public IEnumerable<IDescriptor> Descriptors => descriptors;

        /// <summary>
        /// The IVerbal instance the AggregateNounPhrase is the subject of.
        /// </summary>
        public IVerbal SubjectOf { get; private set; }

        public IVerbal DirectObjectOf { get; private set; }

        public IVerbal IndirectObjectOf { get; private set; }

        public EntityKind EntityKind
        {
            get
            {
                var kinds = from EntityKind kind in Enum.GetValues(typeof(EntityKind))
                            where PrimaryConstituents.All(np => np.EntityKind == kind ||
                                    np.EntityKind == EntityKind.Person && kind == EntityKind.PersonMultiple ||
                                    np.EntityKind == EntityKind.ThingUnknown && kind == EntityKind.ThingMultiple ||
                                    np.EntityKind == EntityKind.PersonMultiple && kind == EntityKind.Person ||
                                    np.EntityKind == EntityKind.ThingMultiple && kind == EntityKind.ThingUnknown)
                            select kind;
                var matchedKind = kinds.FirstOrDefault();
                switch (matchedKind)
                {
                    case EntityKind.Person:
                    case EntityKind.PersonMultiple:
                        return EntityKind.PersonMultiple;
                    case EntityKind.ThingUnknown:
                    case EntityKind.ThingMultiple:
                        return EntityKind.ThingMultiple;
                    default:
                        return matchedKind;
                }
            }
        }

        public double MetaWeight { get; set; }

        /// <summary>
        /// Gets or sets the possessor of the AggregateNounPhrase.
        /// </summary>
        public IPossessor Possessor { get; set; }

        public IEnumerable<IPossessable> Possessions => possessions;

        public IEnumerable<NounPhrase> PrimaryConstituents => constituents.OfType<NounPhrase>();

        public IEnumerable<IReferencer> Referencers => boundPronouns;

        public string Text => $"{GetType()} with constituents\n{PrimaryConstituents.Format(p => p.GetType() + ": " + p.Text)}";

        public double Weight { get; set; }

        private HashSet<IReferencer> boundPronouns = new HashSet<IReferencer>();
        private IList<IEntity> constituents;
        private HashSet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        private HashSet<IPossessable> possessions = new HashSet<IPossessable>();
    }
}