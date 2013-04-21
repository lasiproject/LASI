using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role requirements for constructs, generally Actions, which can be modiffied by modal words such as "can" or "shalt".
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IModalityModifiable interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IModalityModifiable
    {
        /// <summary>
        /// Gets or sets the ModalAuxilary w which modifies this instance.
        /// </summary>
        ModalAuxilary Modality {
            get;
            set;
        }
    }
}
