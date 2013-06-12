using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Describable constructs, often Nouns or NounPhrases, which can be be modified by any number of discriptive constructs.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IDescribable interface provides for generalization and abstraction over wd and Phrase types.
    /// </summary>
    /// <see cref="IDescriptor"/>
    public interface IDescribable
    {
        void BindDescriptor(IDescriptor adj);
        IEnumerable<IDescriptor> DescribedBy
        {
            get;
        }
    }



}
