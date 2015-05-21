using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para>
    /// Represents an collection of Verbals which are aggregated through abstraction over a linked entity.
    /// </para>
    /// <para>
    /// As such it provides both the behaviors of a Verbal and an Enumerable collection of Verbals.
    /// That is to say that you can use an instance of this class in
    /// </para>
    /// <para>
    /// situation where an IEntity is Expected, but also enumerate it via foreach(var in ...) or
    /// (from e in ...)
    /// </para>
    /// </summary>
    /// <see cref="IAggregateVerbal"/>
    /// <seealso cref="IVerbal"/>
    public class AggregateVerbal : IAggregateVerbal
    {
        /// <summary>
        /// Initializes a new instance of the AggregateVerbal class.
        /// </summary>
        /// <param name="constituents">The collection of verbals which form the aggregate.</param>
        public AggregateVerbal(IEnumerable<IVerbal> constituents)
        {
            this.constituents = constituents.ToImmutableList();
            Weight = this.constituents.Select(member => member.Weight)
                         .DefaultIfEmpty(0)
                         .Average();
        }

        /// <summary>
        /// Initializes a new instance of the AggregateVerbal class.
        /// </summary>
        /// <param name="first">The first verbal of the aggregate.</param>
        /// <param name="rest">The remaining verbals which form the aggregate.</param>
        public AggregateVerbal(IVerbal first, params IVerbal[] rest) : this(rest.Prepend(first))
        {
        }

        public void AttachObjectViaPreposition(IPrepositional prepositional)
        {
            throw new NotImplementedException();
        }

        public void BindDirectObject(IEntity directObject) => directObjects = directObjects.Add(directObject);

        public void BindIndirectObject(IEntity indirectObject) => indirectObjects = indirectObjects.Add(indirectObject);

        public void BindSubject(IEntity subject) => subjects = subjects.Add(subject);
        /// <summary>
        /// Gets an enumerator which enumerates the <see cref="AggregateVerbal"/>.
        /// </summary>
        /// <returns>An enumerator which enumerates the <see cref="AggregateVerbal"/>.</returns>
        public IEnumerator<IVerbal> GetEnumerator() => constituents.GetEnumerator();

        public void ModifyWith(IAdverbial modifier) => adverbialModifiers = adverbialModifiers.Add(modifier);

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IVerbal, IEnumerable<TResult>> flattenAbout) => this.SelectMany(flattenAbout).Where(result => result != null);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Gets all of the adverbial modifiers of the AggregateVerbal.
        /// </summary>
        public IEnumerable<IAdverbial> AdverbialModifiers => FlattenAbout(v => v.AdverbialModifiers);

        /// <summary>
        /// Gets the aggregate of all Direct objects of the AggregateVerbal.
        /// </summary>
        public IAggregateEntity AggregateDirectObject => FlattenAbout(v => v.DirectObjects).ToAggregate();
        /// <summary>
        /// Gets the aggregate of all Indirect objects of the AggregateVerbal.
        /// </summary>
        public IAggregateEntity AggregateIndirectObject => FlattenAbout(v => v.IndirectObjects).ToAggregate();

        /// <summary>
        /// Gets the aggregate of all Subjects of the AggregateVerbal.
        /// </summary>
        public IAggregateEntity AggregateSubject => FlattenAbout(v => v.Subjects).ToAggregate();

        /// <summary>
        /// Gets all of the adverbial modifiers of the AggregateVerbal.
        /// </summary>
        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;

        public IVerbal AttributedTo => AdverbialModifiers as IVerbal;

        /// <summary>
        /// Gets all of the Direct and Indirect objects of the AggregateVerbal.
        /// </summary>
        public IEnumerable<IEntity> DirectAndIndirectObjects => FlattenAbout(v => v.DirectAndIndirectObjects);

        /// <summary>
        /// Gets all of the Direct objects of the AggregateVerbal.
        /// </summary>
        public IEnumerable<IEntity> DirectObjects => FlattenAbout(v => v.DirectObjects).Union(directObjects);

        /// <summary>
        /// Gets all of the Indirect objects of the AggregateVerbal.
        /// </summary>
        public IEnumerable<IEntity> IndirectObjects => FlattenAbout(v => v.IndirectObjects).Union(indirectObjects);
        /// <summary>
        /// Gets a value indicating if the <see cref="AggregateVerbal"/> is a classifier.
        /// </summary>
        public bool IsClassifier => this.All(e => e.IsClassifier);
        /// <summary>
        /// Gets a value indicating if the <see cref="AggregateVerbal"/> is possessive.
        /// </summary>
        public bool IsPossessive => this.All(e => e.IsPossessive);
        /// <summary>
        /// Gets the meta-weight of the <see cref="AggregateVerbal"/>
        /// </summary>
        public double MetaWeight { get; set; }

        /// <summary>
        /// Gets the weight of the <see cref="AggregateVerbal"/>
        /// </summary>
        public double Weight { get; set; }

        public ModalAuxilary Modality
        {
            get
            {
                return this.Select(member => member.Modality).DefaultIfEmpty().GroupBy(modality => modality?.Text).MaxBy(group => group.Count()).First();
            }
            set
            {
                throw new NotSupportedException($"Cannot Modify The Modality of an Aggregate Verbal.{this.ToString()}");
            }
        }

        public ILexical ObjectOfThePreposition { get { throw new NotImplementedException(); } }

        public IPrepositional PrepositionalToObject { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gets or sets the subject complement of the <see cref="AggregateVerbal"/>
        /// </summary>
        public ILexical SubjectComplement { get; set; }
        /// <summary>
        /// Gets the subjects of the <see cref="AggregateVerbal"/>
        /// </summary>
        public IEnumerable<IEntity> Subjects => FlattenAbout(member => member.Subjects).Union(subjects);
        /// <summary>
        /// Gets the text of the <see cref="AggregateVerbal"/>
        /// </summary>
        public string Text => string.Join(", ", this.Select(c => c.Text));


        #region Fields

        private readonly IImmutableList<IVerbal> constituents;
        private IImmutableSet<IAdverbial> adverbialModifiers = ImmutableHashSet<IAdverbial>.Empty;
        private IImmutableSet<IEntity> directObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> indirectObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> subjects = ImmutableHashSet<IEntity>.Empty;

        #endregion Fields
    }
}