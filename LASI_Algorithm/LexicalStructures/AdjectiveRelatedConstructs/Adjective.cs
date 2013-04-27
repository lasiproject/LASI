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
    /// Represents an adjective which can describe a Noun, NounPhrase, or rhs IDescribable
    /// </summary>
    public class Adjective : Word, IAdverbialModifiable, IDescriber
    {
        /// <summary>
        /// Initializes a new instance of the Adjective class.
        /// </summary>
        /// <param name="text">The literal text content of the w.</param>
        public Adjective(string text)
            : base(text) {
        }

        /// <summary>
        /// Gets or sets the Descriabable construct the Adjective describes
        /// </summary>
        public virtual IEntity Described {
            get;
            set;
        }
        /// <summary>
        /// Binds a modifier to the Adjective, modifying it.
        /// </summary>
        /// <param name="adv">The IModifier instance (probably an Adverb or AdverbPhrase) to Bind to the Adjective.</param>
        public virtual void ModifyWith(IAdverbial adv) {
            _modifiers.Add(adv);
        }

        private IList<IAdverbial> _modifiers = new List<IAdverbial>();

        /// <summary>
        /// Gets or sets the collection of Adverbial constructs which modify the AdjectivePhrase
        /// </summary>
        public virtual IEnumerable<IAdverbial> Modifiers {
            get {
                return _modifiers;
            }

        }


    }


}
