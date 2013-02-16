using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavior of an Entity, such as a Pronoun or PronounPhrase, which acts as a reference to another entity in some lexical context.
    /// </summary>
    public interface IEntityReferencer : IEntity
    {
        /// <summary>
        /// Gets or sets the Entity which this entity refers to.
        /// </summary>
        IEntity BoundEntity {
            get;
            set;
        }
    }
}
