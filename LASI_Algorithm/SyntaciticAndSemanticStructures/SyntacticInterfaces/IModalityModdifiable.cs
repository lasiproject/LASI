using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavioral contract for constructs, generally Actions, which can be modiffied by modal words such as "can" or "shalt".
    /// </summary>
    public interface IModalityModifiable
    {
        /// <summary>
        /// Gets or sets the Modal w which modifies this instance.
        /// </summary>
        Modal Modality {
            get;
            set;
        }
    }
}
