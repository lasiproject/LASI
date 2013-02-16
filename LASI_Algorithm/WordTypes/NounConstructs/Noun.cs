using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public abstract class Noun : Word, IEntity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Noun class.
        /// </summary>
        /// <param name="text">The literal text content of the Noun.</param>
        protected Noun(string text)
            : base(text) {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Binds an EntityReferency, generall a Pronoun or PronounPhrase to refer to the Noun.
        /// </summary>
        /// <param name="pro">The EntityReferency to bind.</param>
        public virtual void BindPronoun(IEntityReferencer pro) {
            pro.BoundEntity = this;
            _indirectReferences.Add(pro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adjective"></param>
        public virtual void BindDescriber(IDescriber adjective) {
            adjective.Describes = this;
            _describedBy.Add(adjective);
        }
        public bool Equals(IEntity other) {
            return this == other as Noun;
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets all of the IEntityReferences instances, generally Pronouns or PronounPhrases, which refer to the Noun Instance.
        /// </summary>
        public virtual IEnumerable<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
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



        public virtual ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        public virtual ITransitiveAction IndirectObjectOf {
            get;
            set;
        }

        public virtual IAction SubjectOf {
            get;
            set;
        }



        /// <summary>
        /// Gets all of the IPossessable constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IEntity> Possessed {
            get {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the Entity which "owns" the instance of the Noun.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }
        #endregion

        #region Fields
        private ICollection<IDescriber> _describedBy = new List<IDescriber>();
        private ICollection<IPossessable> _possessed = new List<IPossessable>();
        private ICollection<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();
        #endregion






        public void AddPossession(IEntity possession) {
            throw new NotImplementedException();
        }
    }
}
