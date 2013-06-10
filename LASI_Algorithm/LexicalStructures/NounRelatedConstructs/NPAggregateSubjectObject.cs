
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an collection of usually contiguous NounPhrases which combine to form a single subject or object.
    /// As such it provides both the behaviors of an entity and an Enumerable collection of entities. That is to say that you can use an instance of this class in 
    /// situtation where an IEntity is Expected, but also enumerate it, via foreach(var in ...) or (from e in ...)
    /// </summary>
    /// <see cref="IEntityGroup"/>
    /// <seealso cref="IEntity"/>
    public class NPAggregateSubjectObject : IEntityGroup
    {
        public NPAggregateSubjectObject(IEnumerable<IEntity> aggregates)
        {
            AggregatesEntities = aggregates;
        }
        public void AddPossession(IEntity possession)
        {
            if (!_possessions.Contains(possession)) {
                _possessions.Add(possession);
            }
        }

        /// <summary>
        /// Gets or sets the Entity Kind; Person, Place, Thing, Organization, or Activity; of the NPAggregateSubjectObject instance.
        /// </summary>
        public EntityKind EntityKind
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NPAggregateSubjectObject is the DIRECT object of.
        /// </summary>
        public IVerbal DirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NPAggregateSubjectObject is the INDIRECT object of.
        /// </summary>
        public IVerbal IndirectObjectOf
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the IVerbal instance, generally a Verb or VerbPhrase, which the NPAggregateSubjectObject is the subject of.
        /// </summary>
        public IVerbal SubjectOf
        {
            get;
            set;
        }

        public void BindPronoun(IPronoun pro)
        {
            if (!BoundPronouns.Contains(pro)) {
                _boundPronouns.Add(pro);
            }
        }

        public IEnumerable<IPronoun> BoundPronouns
        {
            get
            {
                return _boundPronouns;
            }
        }

        public void BindDescriber(IDescriber adj)
        {
            if (!_describers.Contains(adj)) {
                _describers.Add(adj);
            }
        }

        public IEnumerable<IDescriber> DescribedBy
        {
            get
            {
                return _describers;
            }
        }
        /// <summary>
        /// Gets all of the constructs the NPAggregate can be determined to "own" collectively 
        /// /// </summary>
        public IEnumerable<IEntity> Possessed
        {
            get
            {
                return _possessions;
            }
        }


        public override string ToString()
        {
            return base.ToString() + String.Format(" \"{0}\"", Text);
        }
        public IEntity Possesser
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return AggregatesEntities.Aggregate("", (aggr, ent) => aggr += ent + " ").Trim();
            }
        }
        public Type Type
        {
            get
            {
                return GetType();
            }
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the Noun-Phrase-Aggregate within the context of its document.
        /// </summary>
        public decimal Weight
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the Noun-Phrase-Aggregate over the context of all extant documents.
        /// </summary>
        public decimal MetaWeight
        {
            get;
            set;
        }



        /// <summary>
        /// Gets the EnumerableCollection of Entities compose to form the aggregate
        /// </summary>
        public IEnumerable<IEntity> AggregatesEntities
        {
            get;
            protected set;
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return AggregatesEntities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #region Fields

        ICollection<IEntity> _possessions = new List<IEntity>();
        ICollection<IDescriber> _describers = new List<IDescriber>();
        ICollection<IPronoun> _boundPronouns = new List<IPronoun>();

        #endregion








    }
}
