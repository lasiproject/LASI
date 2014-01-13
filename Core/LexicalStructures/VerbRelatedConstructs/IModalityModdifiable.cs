using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for constructs, generally Actions, which can be modiffied by modal words such as "can" or "shalt". </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IModalityModifiable interface provides for generalization and abstraction over word and Phrase types.</para>
    /// </summary>
    public interface IModalityModifiable
    {
        /// <summary>
        /// Gets or sets the ModalAuxilary word which modifies this instance.
        /// </summary>
        ModalAuxilary Modality {
            get;
            set;
        }
    }
}
