using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Binding.Experimental
{
    interface IAggregateLexical<TPrimary, TBase> : ILexical
        where TPrimary : TBase, ILexical
        where TBase : ILexical
    {

        IEnumerable<TBase> AllConstituents { get; }
        IEnumerable<TPrimary> PrimaryConstituents { get; }



    }
    class AggregateNounPhrase : IAggregateLexical<NounPhrase, IEntity>, IEntity
    {

        protected AggregateNounPhrase(IEnumerable<IEntity> constituents) {

            _constituents = new List<IEntity>(constituents);
        }


        public EntityKind EntityKind {
            get {
                var kinds = from EntityKind knd in Enum.GetValues(EntityKind.GetType())
                            where PrimaryConstituents.All(c => {
                                return c.EntityKind == knd ||
                                    c.EntityKind == EntityKind.Person && knd == EntityKind.PersonMultiple ||
                                    c.EntityKind == EntityKind.ThingUnknown && knd == EntityKind.ThingUnknownMultiple ||
                                    c.EntityKind == EntityKind.PersonMultiple && knd == EntityKind.Person ||
                                    c.EntityKind == EntityKind.ThingUnknownMultiple && knd == EntityKind.ThingUnknown;
                            })
                            select knd;
                var kind = kinds.FirstOrDefault();
                return kind == EntityKind.Person || kind == EntityKind.PersonMultiple ? EntityKind.PersonMultiple :
                    kind == EntityKind.ThingUnknown || kind == EntityKind.ThingUnknownMultiple ?
                    EntityKind.ThingUnknownMultiple : kind;
            }
        }

        public IVerbal SubjectOf { get; set; }

        public IVerbal DirectObjectOf { get; set; }

        public IVerbal IndirectObjectOf { get; set; }

        public void BindPronoun(IReferencer pro) {
            if (!_boundPronouns.Contains(pro)) { _boundPronouns.Add(pro); }
        }

        public IEnumerable<IReferencer> BoundPronouns {
            get { return _boundPronouns; }
        }

        public void BindDescriptor(IDescriptor descriptor) {
            foreach (var c in PrimaryConstituents) { c.BindDescriptor(descriptor); }
        }

        public IEnumerable<IDescriptor> Descriptors {
            get { return _descriptors; }
        }

        public IEnumerable<IPossessable> Possessed {
            get { return _possessions; }
        }

        public void AddPossession(IPossessable possession) {
            _possessions.Add(possession);
        }

        public IPossesser Possesser {
            get;
            set;
        }



        public IEnumerable<IEntity> AllConstituents { get { return _constituents; } }
        public IEnumerable<NounPhrase> PrimaryConstituents { get { return _constituents.OfType<NounPhrase>(); } }
        public IPrepositional PrepositionOnLeft { get; set; }
        public IPrepositional PrepositionOnRight { get; set; }
        public string Text {
            get {
                return string.Format("{0} with constituents\n{1}", this.Type, PrimaryConstituents.Format(p => p.Type + ": " + p.Text));
            }
        }
        public Type Type { get { return GetType(); } }
        public double Weight { get; set; }

        public double MetaWeight { get; set; }

        HashSet<IPossessable> _possessions = new HashSet<IPossessable>();
        HashSet<IDescriptor> _descriptors = new HashSet<IDescriptor>();
        HashSet<IReferencer> _boundPronouns = new HashSet<IReferencer>();
        protected IList<IEntity> _constituents;
    }
}
