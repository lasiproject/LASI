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
        public Noun(string text)
            : base(text) {

        }
        #endregion

        #region Methods


        public virtual void BindPronoun(IEntityReferencer pro) {
            pro.BoundEntity = this;
            IndirectReferences.Add(pro);
        }


        public virtual void BindDescriber(IDescriber adjective) {
            adjective.Describes = this;
            _describedBy.Add(adjective);
        }
        public bool Equals(IEntity other) {
            return this == other as Noun;
        }

        #endregion

        #region Properties

        public virtual ICollection<IEntityReferencer> IndirectReferences {
            get {
                return _indirectReferences;
            }
        }

        public virtual ICollection<IDescriber> DescribedBy {
            get {
                return _describedBy;
            }
            set {
                _describedBy = value;
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

        public virtual IIntransitiveAction SubjectOf {
            get;
            set;
        }



        public virtual ICollection<IEntity> Possessed {
            get {
                throw new NotImplementedException();
            }
        }

        public IEntity Possesser {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Fields
        private ICollection<IDescriber> _describedBy = new List<IDescriber>();
        private ICollection<IPossessable> _possessed = new List<IPossessable>();
        private ICollection<IEntityReferencer> _indirectReferences = new List<IEntityReferencer>();
        #endregion




    }
}
