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

            this.constituents = constituents.ToList();
        }


        public EntityKind EntityKind {
            get {
                var kinds = from EntityKind knd in Enum.GetValues(EntityKind.GetType())
                            where PrimaryConstituents.All(c => {
                                return c.EntityKind == knd ||
                                    c.EntityKind == EntityKind.Person && knd == EntityKind.PersonMultiple ||
                                    c.EntityKind == EntityKind.ThingUnknown && knd == EntityKind.ThingMultiple ||
                                    c.EntityKind == EntityKind.PersonMultiple && knd == EntityKind.Person ||
                                    c.EntityKind == EntityKind.ThingMultiple && knd == EntityKind.ThingUnknown;
                            })
                            select knd;
                var kind = kinds.FirstOrDefault();
                return kind == EntityKind.Person || kind == EntityKind.PersonMultiple ? EntityKind.PersonMultiple :
                    kind == EntityKind.ThingUnknown || kind == EntityKind.ThingMultiple ?
                    EntityKind.ThingMultiple : kind;
            }
        }

        public IVerbal SubjectOf { get; set; }

        public IVerbal DirectObjectOf { get; set; }

        public IVerbal IndirectObjectOf { get; set; }

        public void BindReferencer(IReferencer pro) {
            if (!boundPronouns.Contains(pro)) { boundPronouns.Add(pro); }
        }

        public IEnumerable<IReferencer> Referencers {
            get { return boundPronouns; }
        }

        public void BindDescriptor(IDescriptor descriptor) {
            foreach (var c in PrimaryConstituents) { c.BindDescriptor(descriptor); }
        }

        public IEnumerable<IDescriptor> Descriptors {
            get { return descriptors; }
        }

        public IEnumerable<IPossessable> Possessions {
            get { return possessions; }
        }

        public void AddPossession(IPossessable possession) {
            possessions.Add(possession);
        }

        public IPossesser Possesser {
            get;
            set;
        }



        public IEnumerable<IEntity> AllConstituents { get { return this.constituents; } }
        public IEnumerable<NounPhrase> PrimaryConstituents { get { return this.constituents.OfType<NounPhrase>(); } }
        public IPrepositional PrepositionOnLeft { get; set; }
        public IPrepositional PrepositionOnRight { get; set; }
        public string Text {
            get {
                return string.Format("{0} with constituents\n{1}", this.GetType(), PrimaryConstituents.Format(p => p.GetType() + ": " + p.Text));
            }
        }
        public double Weight { get; set; }

        public double MetaWeight { get; set; }

        HashSet<IPossessable> possessions = new HashSet<IPossessable>();
        HashSet<IDescriptor> descriptors = new HashSet<IDescriptor>();
        HashSet<IReferencer> boundPronouns = new HashSet<IReferencer>();
        private IList<IEntity> constituents;



    }
}
