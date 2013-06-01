using LASI.Algorithm.SyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Algorithm.LexicalStructures.NounRelatedConstructs;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a noun phrase such as "The Pinko-Commy Conspiracy".
    /// Note that noun componentPhrases are the constructs which wrap both nouns and pronouns at the phrase level.
    /// </summary>
    public class NounPhrase : Phrase, IEntity, ISimilarityComparablePhrase<NounPhrase>, IAliasableEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the NounPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the NounPhrase.</param>
        public NounPhrase(IEnumerable<Word> composedWords)
            : base(composedWords)
        {
            determineEntityType();
        }
        #endregion


        #region Methods

        /// <summary>
        /// Current,  somewhat sloppy determination of the type, person, place, thing etc, of nounphrase by 
        /// selecting the most common type between its nouns and from its bound pronouns 
        /// </summary>
        protected virtual void determineEntityType()
        {

            var kindsOfNouns = from N in Words.GetNouns()
                               select N.EntityKind;
            var internalKinds = from K in kindsOfNouns
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
        public virtual void BindPronoun(LASI.Algorithm.SyntacticInterfaces.IPronoun pro)
        {
            _boundPronouns.Add(pro);
            pro.BindToIEntity(this);
        }
        /// <summary>
        /// Binds an IDescriber, generally an Adjective or AdjectivePhrase, as a descriptor of the NounPhrase.
        /// </summary>
        /// <param name="adjective">The IDescriber instance which will be added to the NounPhrase's descriptors.</param>
        public void BindDescriber(IDescriber adjective)
        {
            if (!_describedBy.Contains(adjective))
                _describedBy.Add(adjective);
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of the NounPhrase "Owns",
        /// and sets its owner to be the NounPhrase.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IEntity possession)
        {
            if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
                possession.Possesser = this;
            }
        }
        public bool Equals(IEntity other)
        {
            return this == other as NounPhrase;
        }
        public override string ToString()
        {
            var result = base.ToString();
            if (Phrase.VerboseOutput && Possessed.Count() > 0) {
                result += "\n\tpossessions:\n";
                foreach (var s in Possessed) {
                    result += s + "\n";
                }
            }
            if (Phrase.VerboseOutput) {
                if (Possesser != null)
                    result += "\n\towned By:\n\t\t" + Possesser.ToString();

                if (InnerAttributed != null) {
                    result += "\n\tDefines:\n\t\t" + InnerAttributed;
                }
                if (OuterAttributive != null) {
                    result += "\n\tDefines:\n\t\t" + OuterAttributive;
                }
            }
            return result;

        }

        #endregion

        /// <summary>
        /// Gets the ITransitiveVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the DIRECT object of.
        /// </summary>
        public virtual ITransitiveVerbal DirectObjectOf
        {
            get
            {
                return _direcObjectOf;
            }
            set
            {
                _direcObjectOf = value;
                foreach (var N in Words.OfType<IVerbalObject>()) {
                    N.DirectObjectOf = _direcObjectOf;
                }
            }
        }

        /// <summary>
        /// Gets the ITransitiveVerbal instance, generally a TransitiveVerb or TransitiveVerbPhrase, which the NounPhrase is the INDIRECT object of.
        /// </summary>
        public virtual ITransitiveVerbal IndirectObjectOf
        {
            get
            {
                return _indirecObjectOf;
            }
            set
            {
                _indirecObjectOf = value;
                foreach (var N in Words.OfType<IVerbalObject>()) {
                    N.IndirectObjectOf = IndirectObjectOf;
                }
            }
        }

        /// <summary>
        /// Gets the ITransitiveVerbal instance, generally a Verb or VerbPhrase, which the NounPhrase is the subject of.
        /// </summary>
        public virtual ITransitiveVerbal SubjectOf
        {
            get
            {
                return _subjectOf;
            }
            set
            {
                _subjectOf = value;
                foreach (var N in Words.OfType<IVerbalSubject>()) {
                    N.SubjectOf = _subjectOf;
                }
            }
        }
        private ITransitiveVerbal _subjectOf;
        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<IPronoun> BoundPronouns
        {
            get
            {
                return _boundPronouns;
            }
            protected set
            {
                _boundPronouns = value.ToList();
            }
        }

        /// <summary>
        /// Gets all of the IDescriber constructs,generally Adjectives or AdjectivePhrases, which describe the NounPhrase Instance.
        /// </summary>
        public virtual IEnumerable<IDescriber> DescribedBy
        {
            get
            {
                return _describedBy;
            }
            protected set
            {
                _describedBy = value.ToList();
            }
        }


        /// <summary>
        /// Gets all of the constructs which the NounPhrase "owns".
        /// </summary>
        public IEnumerable<IEntity> Possessed
        {
            get
            {
                return _possessed;
            }
            protected set
            {
                _possessed = value.ToList();
            }
        }
        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the NounPhrase.
        /// </summary>
        public IEntity Possesser
        {
            get
            {
                return _possessor;
            }
            set
            {
                _possessor = value;
                foreach (var item in this.Words.GetNouns().Concat<IEntity>(this.Words.GetPronouns())) {
                    value.AddPossession(item);
                }
            }
        }

        public NounPhrase OuterAttributive
        {
            get;
            set;
        }
        public NounPhrase InnerAttributed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Entity Kind; Person, Place, Thing, Organization, or Activity; of the NounPhrase.
        /// </summary>
        public EntityKind EntityKind
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Noun to Nounphrase
        /// </summary>
        public Noun BoundNoun
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets NounPhrase to NounPhrase
        /// </summary>
        public NounPhrase BoundNounPhrase
        {
            get;
            set;
        }


        private bool wasBound = false;

        public bool WasBound
        {
            get
            {
                return wasBound;
            }
            set
            {
                wasBound = value;
            }
        }

        private IList<IDescriber> _describedBy = new List<IDescriber>();
        private IList<IEntity> _possessed = new List<IEntity>();
        private IList<IPronoun> _boundPronouns = new List<IPronoun>();
        private IEntity _possessor;
        private ITransitiveVerbal _direcObjectOf;
        private ITransitiveVerbal _indirecObjectOf;







        public bool IsAliasFor(IAliasableEntity other)
        {
            throw new NotImplementedException();
        }

        public bool IsSimilarTo(NounPhrase first, NounPhrase second)
        {
            throw new NotImplementedException();
        }
    }
}


