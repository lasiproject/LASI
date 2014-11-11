using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{
    class AggregateVerbal : IAggregateVerbal
    {
        public AggregateVerbal(IEnumerable<IVerbal> constituents) {
            this.constituents = constituents;
            Weight = this.constituents.Select(member => member.Weight).DefaultIfEmpty(0).Average();
        }

        public AggregateVerbal(IVerbal first, params IVerbal[] rest) : this(rest.Prepend(first)) { }

        private IEnumerable<TResult> FlattenAbout<TResult>(Func<IVerbal, IEnumerable<TResult>> flattenAbout) {
            return this.SelectMany(flattenAbout).Where(result => result != null);
        }


        public IEnumerable<IAdverbial> AdverbialModifiers {
            get { return FlattenAbout(v => v.AdverbialModifiers); }
        }

        public IAggregateEntity AggregateDirectObject {
            get { return new AggregateEntity(FlattenAbout(v => v.DirectObjects)); }
        }

        public IAggregateEntity AggregateIndirectObject {
            get { return new AggregateEntity(FlattenAbout(v => v.IndirectObjects)); }
        }
        public IAggregateEntity AggregateSubject {
            get { return new AggregateEntity(FlattenAbout(v => v.Subjects)); }
        }

        public IEnumerable<IEntity> DirectObjects {
            get { return FlattenAbout(v => v.DirectObjects).Union(directObjects); }
        }

        public IEnumerable<IEntity> IndirectObjects {
            get { return FlattenAbout(v => v.IndirectObjects).Union(indirectObjects); }
        }

        public bool IsClassifier { get { return this.All(member => member.IsClassifier); } }

        public bool IsPossessive { get { return this.All(member => member.IsPossessive); } }

        public double MetaWeight { get; set; }

        public ModalAuxilary Modality {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public ILexical ObjectOfThePreposition {
            get {
                throw new NotImplementedException();
            }
        }

        public IPrepositional PrepositionalToObject {
            get {
                throw new NotImplementedException();
            }
        }

        public IPrepositional PrepositionOnLeft { get; set; }

        public IPrepositional PrepositionOnRight { get; set; }

        public IEnumerable<IEntity> Subjects { get { return subjects; } }

        public string Text { get { return string.Join(", ", constituents.Select(member => member.Text)); } }

        public double Weight { get; set; }

        public void AttachObjectViaPreposition(IPrepositional prepositional) {
            throw new NotImplementedException();
        }

        public void BindDirectObject(IEntity directObject) {
            directObjects = directObjects.Add(directObject);
        }

        public void BindIndirectObject(IEntity indirectObject) {
            indirectObjects = indirectObjects.Add(indirectObject);
        }

        public void BindSubject(IEntity subject) {
            subjects = subjects.Add(subject);
        }

        public void ModifyWith(IAdverbial modifier) {
            adverbialModifiers = adverbialModifiers.Add(modifier);
        }

        public IEnumerator<IVerbal> GetEnumerator() { return constituents.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #region Fields

        private readonly IEnumerable<IVerbal> constituents;
        private IImmutableSet<IEntity> subjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> directObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IEntity> indirectObjects = ImmutableHashSet<IEntity>.Empty;
        private IImmutableSet<IAdverbial> adverbialModifiers = ImmutableHashSet<IAdverbial>.Empty;

        #endregion
    }
}
