using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an collection of usually contiguous NounPhrases which combine to form a single subject or object.
    /// As provides both the behaviors of an entity and an Enumerable collection of entities. That is to say that you can use an instance of this class in 
    /// situtation where an IEntity is Expected, but also enumerate it, via foreach(var in ...) or (from e in ...)
    /// </summary>
    /// <see cref="IEntityGroup"/>
    /// <seealso cref="IEntity"/>
    class NPAggregateSubjectObject : IEntityGroup
    {
        public NPAggregateSubjectObject(IEnumerable<IEntity> aggregates) {
            AggregatesEntities = aggregates;
        }
        public void AddPossession(IEntity possession) {
            if (!_possessions.Contains(possession)) {
                _possessions.Add(possession);
            }
        }
        public bool Equals(IEntity other) {
            return this as object == other as object;
        }
        /// <summary>
        /// Gets or sets the Entity Kind; Person, Place, Thing, Organization, or Activity; of the NPAggregateSubjectObject instance.
        /// </summary>
        public EntityKind EntityKind {
            get;
            set;
        }
        /// <summary>
        /// Gets the ITransitiveVerbial instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NPAggregateSubjectObject is the DIRECT object of.
        /// </summary>
        public ITransitiveVerbial DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ITransitiveVerbial instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NPAggregateSubjectObject is the INDIRECT object of.
        /// </summary>
        public ITransitiveVerbial IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the ITransitiveVerbial instance, generally a Verb or VerbPhrase, which the NPAggregateSubjectObject is the subject of.
        /// </summary>
        public ITransitiveVerbial SubjectOf {
            get;
            set;
        }

        public void BindPronoun(Pronoun pro) {
            _boundPronouns.Add(pro);
        }

        public IEnumerable<Pronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        public void BindDescriber(FundamentalSyntacticInterfaces.IDescriber adj) {
            if (!_describers.Contains(adj)) {
                _describers.Add(adj);
            }
        }

        public IEnumerable<FundamentalSyntacticInterfaces.IDescriber> DescribedBy {
            get {
                return _describers;
            }
        }
        /// <summary>
        /// Gets all of the constructs the NPAggregate can be determined to "own" collectively 
        /// /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessions;
            }
        }


        public override string ToString() {
            return base.ToString() + String.Format(" \"{0}\"", Text);
        }
        public IEntity Possesser {
            get;
            set;
        }

        public string Text {
            get {
                return AggregatesEntities.Aggregate("", (aggr, ent) => aggr += ent + " ").Trim();
            }
        }

        /// <summary>
        /// Gets or sets the numeric weight of the Noun-Phrase-Aggregate within the context of its document.
        /// </summary>
        public decimal Weight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the numeric weight of the Noun-Phrase-Aggregate over the context of all extant documents.
        /// </summary>
        public decimal MetaWeight {
            get;
            set;
        }


        /// <summary>
        /// Gets the EnumerableCollection of Entities compose to form the aggregate
        /// </summary>
        public IEnumerable<IEntity> AggregatesEntities {
            get;
            protected set;
        }

        public IEnumerator<IEntity> GetEnumerator() {
            return AggregatesEntities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        #region Fields

        ICollection<IEntity> _possessions = new List<IEntity>();
        ICollection<IDescriber> _describers = new List<IDescriber>();
        ICollection<Pronoun> _boundPronouns = new List<Pronoun>();

        #endregion

    }
}
