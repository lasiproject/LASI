using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Describable constructs, often Nouns or NounPhrases, which can be be modified by any number of discriptive constructs.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IDescribable interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    /// <see cref="IDescriber"/>
    public interface IDescribable
    {
        void BindDescriber(IDescriber adj);
        IEnumerable<IDescriber> DescribedBy {
            get;
        }
    }



}
