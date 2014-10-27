using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    class AggregateVerbal : IAggregateVerbal
    {
        public AggregateVerbal(IEnumerable<IVerbal> aggregate) {
            members = aggregate;
            Weight = members.Select(member => member.Weight).DefaultIfEmpty(0).Average();
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

        public string Text { get { return string.Join(", ", members.Select(member => member.Text)); } }

        public double Weight { get; set; }

        public void AttachObjectViaPreposition(IPrepositional prepositional) {
            throw new NotImplementedException();
        }

        public void BindDirectObject(IEntity directObject) {
            directObjects.Add(directObject);
        }

        public void BindIndirectObject(IEntity indirectObject) {
            indirectObjects.Add(indirectObject);
        }

        public void BindSubject(IEntity subject) {
            subjects.Add(subject);
        }

        public void ModifyWith(IAdverbial modifier) {
            adverbialModifiers.Add(modifier);
        }

        public IEnumerator<IVerbal> GetEnumerator() { return members.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

        #region Fields

        private readonly IEnumerable<IVerbal> members;
        private ISet<IEntity> subjects = new HashSet<IEntity>();
        private ISet<IEntity> directObjects = new HashSet<IEntity>();
        private ISet<IEntity> indirectObjects = new HashSet<IEntity>();
        private ISet<IAdverbial> adverbialModifiers = new HashSet<IAdverbial>();

        #endregion
    }
}
