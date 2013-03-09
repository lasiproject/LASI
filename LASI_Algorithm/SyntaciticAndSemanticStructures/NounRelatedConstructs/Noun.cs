using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Algorithm;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class, shared properties, and shared behaviors for all noun words.
    /// </summary>
    public abstract class Noun : Word, IEntity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Noun class.
        /// </summary>
        /// <param name="text">The literal text content of the Noun.</param>
        protected Noun(string text)
            : base(text) {
            EntityKind = EntityKind.Unknown;
            DetermineKind();
        }

        private void DetermineKind() {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<([^>]+)>");
            var found = regex.Match(Text).Value ?? "";
            Text = found.Length > 0 ? new string(Text.Skip(found.Length).TakeWhile(c => c != '<').ToArray()) : Text;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Binds an EntityReferency, generall a Pronoun or PronounPhrase to refer to the Noun.
        /// </summary>
        /// <param name="pro">The EntityReferency to Bind.</param>
        public virtual void BindPronoun(Pronoun pro) {
            pro.BoundEntity = this;
            _boundPronouns.Add(pro);
        }

        /// <summary>
        /// Binds an IDescriber, generally an Adjective or AdjectivePhrase, as a descriptor of the Noun.
        /// </summary>
        /// <param name="adjective">The IDescriber instance which will be added to the Noun'd descriptors.</param>
        public virtual void BindDescriber(IDescriber adjective) {
            adjective.Described = this;
            _describedBy.Add(adjective);
        }

        public void AddPossession(IEntity possession) {
            if (!_possessed.Contains(possession))
                _possessed.Add(possession);
            possession.Possesser = this;
        }

        public bool Equals(IEntity other) {
            return this == other as Noun;
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the Noun Instance.
        /// </summary>
        public virtual IEnumerable<Pronoun> BoundPronouns {
            get {
                return _boundPronouns;
            }
        }

        /// <summary>
        /// Gets all of the IDescriber constructs,generally Adjectives or AdjectivePhrases, which describe the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IDescriber> DescribedBy {
            get {
                return _describedBy;
            }
        }


        /// <summary>
        ///Gets or sets the ITRansitiveAction instance, usually a Verb or VerbPhrase, which the Noun is the direct object of.
        /// </summary>
        public virtual ITransitiveVerbial DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbial instance the Noun is the indirect object of.
        /// </summary>
        public virtual ITransitiveVerbial IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        ///Gets or sets the IVerbial instance the Noun is the subject of.
        /// </summary>
        public virtual ITransitiveVerbial SubjectOf {
            get;
            set;
        }



        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IEntity> Possessed {
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

        /// <summary>
        /// Gets or sets the Entity Kind; Person, Place, Thing, Organization, or Activity;  of the Noun.
        /// </summary>
        public EntityKind EntityKind {
            get;
            set;
        }
        public EntityThemeMemberKind ThemeMemberKind {
            get;
            set;
        }

        #endregion

        #region Fields
        private ICollection<IDescriber> _describedBy = new List<IDescriber>();
        private ICollection<IEntity> _possessed = new List<IEntity>();
        private ICollection<Pronoun> _boundPronouns = new List<Pronoun>();
        #endregion












    }
}
