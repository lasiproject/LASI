
using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which can be bound as entity modiffier to either entity verb construct or an adjective construct.
    /// </summary>
    public class Adverb : Word, IAdverbial, IAdverbialModifiable
    {

        /// <summary>
        /// Initializes entity new instance of the Adverb class.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public Adverb(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the verb or entity which the Adverb modiffies
        /// </summary>
        public virtual IVerbal Modified {
            get;
            set;
        }


        public void ModifyWith(IAdverbial adv) {
            if (!_modifiers.Contains(adv)) {
                _modifiers.Add(adv);
            }
        }

        public IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }
        private ICollection<IAdverbial> _modifiers = new List<IAdverbial>();
    }
}
