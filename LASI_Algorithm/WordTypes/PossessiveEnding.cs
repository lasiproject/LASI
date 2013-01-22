using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PossessiveEnding : Word, IPossesser
    {
        public PossessiveEnding(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the possessing Entity.
        /// </summary>
        public IEntity Possesser {
            get;
            set;
        }

        public ICollection<IEntity> Possessed {
            get {
                throw new NotImplementedException();
            }
        }
        private ICollection<IPossessable> _possessed = new List<IPossessable>();

        
    }
}
