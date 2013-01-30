using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a transative verb in its base tense, a verb which can take a direct and an optional indirect object.
    /// </summary>
    public class TransitiveVerb : Verb, ITransitiveAction
    {
        /// <summary>
        /// Initializes a new instance of the TransitiveVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public TransitiveVerb(string text)
            : base(text, VerbTense.Base) {
        }
        ///// <summary>
        ///// Binds the TransitiveVerb to its direct object.
        ///// </summary>
        ///// <param name="verbObject">The IActionObject instance that represents the object of the TransitiveVerb.</param>
        //public virtual void BindToDirectObject(IActionObject verbObject) {
        //    DirectObject = verbObject;
        //    verbObject.DirectObjectOf = this;
        //    verbObject.DirectObjectOf = null;
        //}
        /// <summary>
        /// Gets or sets the directobject of the TransitiveVerb.
        /// </summary>
        public virtual IActionObject DirectObject {
            get;
            set;
        }
        ///// <summary>
        ///// Brinds the TransitiveVerb to an optional indirect object.
        ///// </summary>
        ///// <param name="verbObject">The IActionObject instance that represents the indirect object of the TransitiveVerb.</param>
        //public virtual void BindToIndirectObject(IActionObject verbObject) {
        //    IndirectObject = verbObject;
        //    verbObject.IndirectObjectOf = this;
        //    verbObject.IndirectObjectOf = null;
        //}
        /// <summary>
        /// Gets or sets the indirect object of the TransitiveVerb.
        /// </summary>
        public virtual IActionObject IndirectObject {
            get;
            set;
        }
    }
}
