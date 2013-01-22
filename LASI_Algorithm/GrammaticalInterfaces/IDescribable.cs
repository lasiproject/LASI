using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavior of a Describable construct which can be be modified by any number of discriptive constructs.
    /// </summary>
    /// <see cref="IDescriber"/>
    public interface IDescribable
    {
        void BindDescriber(IDescriber adj);
        ICollection<IDescriber> DescribedBy {
            get;
        }
    }



}
