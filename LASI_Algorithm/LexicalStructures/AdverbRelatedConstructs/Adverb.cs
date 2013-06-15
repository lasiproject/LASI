

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which can be bound as a modiffier to either a adverb construct or an adjective construct.
    /// </summary>
    public class Adverb : Word, IAdverbial, IAdverbialModifiable
    {

        /// <summary>
        /// Initializes a new instance of the Adverb class.
        /// </summary>
        /// <param name="text">The key text content of the adverb.</param>
        public Adverb(string text)
            : base(text)
        {
        }
        /// <summary>
        /// Gets or sets the verbal construct which the Adverb modiffies
        /// </summary>
        public virtual IAdverbialModifiable Modifies
        {
            get;
            set;
        }


        public void ModifyWith(IAdverbial adv)
        {
            if (!_modifiers.Contains(adv)) {
                _modifiers.Add(adv);
            }
        }

        public IEnumerable<IAdverbial> Modifiers
        {
            get
            {
                return _modifiers;
            }
        }
        private ICollection<IAdverbial> _modifiers = new List<IAdverbial>();
    }
}
