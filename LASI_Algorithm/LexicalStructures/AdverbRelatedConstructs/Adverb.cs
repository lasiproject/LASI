

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
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the IAdverbialModifiable construct; such as an Adjective, AdjectivePhrase, Verb, or VerbPhrase; which the Adverb Modifies. 
        /// </summary>
        public virtual IAdverbialModifiable Modifies {
            get;
            set;
        }

        /// <summary>
        /// Attaches an IAdverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the current Adverb
        /// <param name="adv">The IAdverbial construct by which to modify the current Adverb.</param>
        /// </summary>
        public void ModifyWith(IAdverbial adv) {
            if (!_modifiers.Contains(adv)) {
                _modifiers.Add(adv);
            }
        }
        /// <summary>
        /// Gets the List of IAdverbial modifiers which modify the Adverb.
        /// </summary>
        public IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }
        }
        private ICollection<IAdverbial> _modifiers = new List<IAdverbial>();
    }
}
