using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PossessivePronoun : Pronoun
    {
        public PossessivePronoun(string text)
            : base(text) {
        }
        public virtual IEntity PossessedEntity {
            get;
            set;
        }
    }
}
