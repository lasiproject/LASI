using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{

    public class AggregateVerbal : IAggregateVerbal
    {
        /// <summary>
        /// Intializes a new instance of the AggregateVerbal class.
        /// </summary>
        /// <param name="constituents">The collection of verbals which form the aggregate.</param>
        public AggregateVerbal(IEnumerable<IVerbal> constituents) {
            this.constituents = constituents.ToImmutableList();
            Weight = this.constituents.Select(member => member.Weight)
                         .DefaultIfEmpty(0)
                         .Average();
        }
        public AggregateVerbal(IVerbal first, params IVerbal[] rest) : this(rest.Prepend(first)) { }

        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;
        public IVerbal AttributedTo => AdverbialModifiers as IVerbal;


        public IEnumerable<IAdverbial> AdverbialModifiers => FlattenAbout(v => v.AdverbialModifiers);

        public IAggregateEntity AggregateDirectObject => FlattenAbout(v => v.DirectObjects).ToAggregate();

        public IAggregateEntity AggregateIndirectObject => FlattenAbout(v => v.IndirectObjects).ToAggregate();

        public IAggregateEntity AggregateSubject => FlattenAbout(v => v.Subjects).ToAggregate();

        public IEnumerable<IEntity> DirectObjects => FlattenAbout(v => v.DirectObjects).Union(directObjects);

        public IEnumerable<IEntity> IndirectObjects => FlattenAbout(v => v.IndirectObjects).Union(indirectObjects);

        public IEnumerable<IEntity> Subjects => FlattenAbout(member => member.Subjects).Union(subjects);

        public bool IsClassifier => this.All(e => e.IsClassifier);
        public bool IsPossessive => this.All(e => e.IsPossessive);

        public double MetaWeight { get; set; }

        public ModalAuxilary Modality {
            get {
                return this.Select(member => member.Modality).DefaultIfEmpty().GroupBy(modality => modality?.Text).MaxBy(group => group.Count()).First();
            }
            set {
                throw new NotSupportedException($"Cannot Modify The Modality of an Aggregate Verbal.{this.ToString()}");
            }
        }

        public ILexical ObjectOfThePreposition { get { throw new NotImplementedException(); } }

        public IPrepositional PrepositionalToObject { get { throw new NotImplementedException(); } }

        public string Text => string.Join(", ", this.Select(c => c.Text));

        public double Weight { get; set; }

        public void AttachObjectViaPreposition(IPrepositional prepositional) { throw new NotImplementedException(); }

        public void BindDirectObject(IEntity directObject) => directObjects = directObjects.Add(directObject);


        public void BindIndirectObject(IEntity indirectObject) => indirectObjects = indirectObjects.Add(indirectObject);

        public void BindSubject(IEntity subject) => subjects = subjects.Add(subject);


        public void ModifyWith(IAdverbial modifier) => adverbialModifiers = adverbialModifiers.Add(modifier);


        public IEnumerator<IVerbal> GetEnumerator() => constituents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IVerbal, IEnumerable<TResult>> flattenAbout) {
            return this.SelectMany(flattenAbout).Where(result => result != null);
        }

        #region Fields

        private readonly IImmutableList<IVerbal> constituents;
        private IImmutableSet<IEntity> subjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> directObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> indirectObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IAdverbial> adverbialModifiers = ImmutableHashSet<IAdverbial>.Empty;

        #endregion
    }
}