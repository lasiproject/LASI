using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class NounPhrase : Phrase, IEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the NounPhrase.</param>
        public NounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
            determineEntityType();
        }
        #endregion


        /// <summary>
        /// Current,  somewhat sloppy determination of the type, person, place, thing etc, of nounphrase by 
        /// selecting the most common type between its nouns and from its bound pronouns 
        /// </summary>
        protected virtual void determineEntityType() {
            var kindsOfNouns = from N in Words.GetNouns()
                               select N.EntityKind;
            var kindsOfPronouns = from P in Words.GetPronouns()
                                  select P.EntityKind;
            var internalKinds = from K in kindsOfNouns.Concat(kindsOfPronouns)
                                group K by K into KindGroup
                                orderby KindGroup.Count()
                                select KindGroup;
            /**
             * I'm not sure why this is causing my program to crash.
             * But when I comment it out my program works.
             * - Scott
             */
            if (internalKinds.Count() > 0)
                EntityKind = internalKinds.First().Key;
        }


        /// <summary>
        /// Binds a Pronoun or PronounPhrase as a reference to the NounPhrase Instance.
        /// </summary>
        /// <param name="pro">The referencer which refers to the NounPhrase Instance.</param>
        public virtual void BindPronoun(Pronoun pro) {
            _boundPronouns.Add(pro);
            pro.BoundEntity = this;
        }
        /// <summary>
        /// Binds an IDescriber, generally an Adjective or AdjectivePhrase, as a descriptor of the NounPhrase.
        /// </summary>
        /// <param name="adjective">The IDescriber instance which will be added to the NounPhrase's descriptors.</param>
        public void BindDescriber(IDescriber adjective) {
            if (!_describedBy.Contains(adjective))
                _describedBy.Add(adjective);
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the NounPhrase "Owns",
        /// and sets its owner to be the NounPhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession"></param>
        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
                possession.Possesser = this;
            }
        }
        public bool Equals(IEntity other) {
            return this == other as NounPhrase;
        }

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the object of.
        /// </summary>
        public virtual ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets the ITransitiveAction instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveAction IndirectObjectOf {
            get;
            set;
        }

        /// <summary>
        /// Gets the IAction instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual IAction SubjectOf {
            get {
                return _subjectOf;
            }
            set {
                _subjectOf = value;
                foreach (var N in Words.GetNouns()) {
                    N.SubjectOf = _subjectOf;
                }
            }
        }
        protected IAction _subjectOf;
        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<Pronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        /// <summary>
        /// Gets all of the IDescriber constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<IDescriber> DescribedBy {
            get {
                return _describedBy;
            }
        }


        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the Noun.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }

        public EntityKind EntityKind {
            get;
            set;
        }


        private IList<IDescriber> _describedBy = new List<IDescriber>();
        private IList<IEntity> _possessed = new List<IEntity>();
        private IList<Pronoun> _boundPronouns = new List<Pronoun>();





        











        public override XElement Serialize() {
            throw new NotImplementedException();
        }


        public EntityThemeMemberKind ThemeMemberKind {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}


